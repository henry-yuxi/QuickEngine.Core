namespace QuickEngine.Extensions
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static partial class CSharpExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if ((string.IsNullOrEmpty(email)) || (email.Length > 100))
                return false;
            return new EmailValidator().IsValidEmail(email);
        }

        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        public static bool IsValidURL(this string source)
        {
            if (source.IsNullOrEmpty()) { return false; }// TODO: raise exception or log error
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsMatch(this string input, string pattern)
        {
            if (input.IsNullOrEmpty()) return false;
            return Regex.IsMatch(input, pattern);
        }

        public static string Match(this string input, string pattern)
        {
            if (input.IsNullOrEmpty()) return string.Empty;
            return Regex.Match(input, pattern).Value;
        }
    }

    internal class EmailValidator
    {
        private bool invalid;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);

            if (invalid)
                return false;

            return Regex.IsMatch(strIn,
                                 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                 RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}