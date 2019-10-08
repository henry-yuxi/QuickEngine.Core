namespace QuickEngine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static partial class CSharpExtensions
    {

        #region Bool Extensions

        public static bool IsTrue(this bool item)
        {
            return item;
        }

        public static bool IsFalse(this bool item)
        {
            return !item;
        }

        public static bool Toggle(this bool item)
        {
            return !item;
        }

        public static int ToInt(this bool item)
        {
            return item ? 1 : 0;
        }

        public static string ToLowerString(this bool item)
        {
            return item.ToString().ToLower();
        }

        public static string ToYesNo(this bool item)
        {
            return item.ToString("Yes", "No");
        }

        public static string ToString(this bool item, string trueString, string falseString)
        {
            return item.ToType<string>(trueString, falseString);
        }

        public static T ToType<T>(this bool item, T trueValue, T falseValue)
        {
            return item ? trueValue : falseValue;
        }

        #endregion Bool Extensions

        #region Enum Extensions

        public static int ToInt(this Enum enumName)
        {
            return Convert.ToInt32(enumName);
        }

        public static T ToEnum<T>(this int index) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                return default(T);
            }
            try
            {
                return (T)Enum.ToObject(typeof(T), index);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T ToEnum<T>(this string name) where T : struct, IConvertible
        {
            return name.ToEnum<T>(false);
        }

        public static T ToEnum<T>(this string name, bool ignoreCase) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(name))
            {
                return default(T);
            }
            if (!typeof(T).IsEnum)
            {
                return default(T);
            }
            try
            {
                return (T)Enum.Parse(typeof(T), name, ignoreCase);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string GetDescription(this Enum enumName)
        {
            FieldInfo fi = enumName.GetType().GetField(enumName.ToString());
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return arrDesc[0].Description;
        }

        #endregion Enum Extensions

        #region Int Extensions

        private static string[] UnitStrs = new string[] { "K", "M", "B", "T" };

        //计数单位：K、M、B、T、aa、bb、cc.....AA、BB、CC.....
        public static string ToUnitFormat(this int gold)
        {
            if (Math.Abs(gold) < 1000)
            {
                return gold.ToString();
            }
            string result = "";
            int index = 1;
            float quotient = gold;
            while (Math.Abs(quotient) >= 1000)
            {
                quotient = quotient / 1000f;
                index++;
            }
            result = ((int)quotient).ToString();
            if (index > 1)
            {
                result = Math.Round((decimal)quotient, (3 - result.Length)).ToString();
                result += UnitStrs[index - 2];
            }
            return result;
        }

        public static string ToDivision(this int src)
        {
            return string.Format("{0:N0}", src);
        }

        #endregion Int Extensions

        #region Random Extensions

        public static bool NextBool(this System.Random random)
        {
            return random.NextDouble() > 0.5;
        }

        public static T NextEnum<T>(this System.Random random) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        #endregion Random Extensions

        #region String Extensions

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value != null)
            {
                int length = value.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static string Format(this string value, params object[] args)
        {
            return args.Length > 0 ? string.Format(value, args) : value;
        }

        public static string Format(this string value, object arg0)
        {
            return arg0 != null ? string.Format(value, arg0) : value;
        }

        public static string Join(this string[] s, string separator)
        {
            return string.Join(separator, s);
        }

        public static string Join(this IEnumerable<string> s, string separator)
        {
            return string.Join(separator, s.ToArray());
        }

        public static string ToTitleCase(this string value)
        {
            if (value.IsNullOrEmpty())
                return value;
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength">字符串的最大长度</param>
        /// <param name="endWith">超过长度的后缀</param>
        /// <returns>如果超过长度，返回截断后的新字符串加上后缀，否则，返回原字符串</returns>
        public static string ToTruncat(this string value, int maxLength, string endWith)
        {
            if (value.IsNullOrEmpty()) { return value; }
            if (maxLength < 1) { return string.Empty; }
            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength) + (endWith.IsNullOrEmpty() ? string.Empty : endWith);
            }
            return value;
        }

        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }

        public static string Capitalize(this string word)
        {
            if (word.Length <= 1)
                return word;
            return word[0].ToString().ToUpper() + word.Substring(1);
        }

        public static T Parse<T>(this string stringValue)
        {
            return GenericParser.Parse<T>(stringValue);
        }

        public static T TryParse<T>(this string stringValue)
        {
            T result = default(T);
            return GenericParser.TryParse<T>(stringValue, out result) ? result : default(T);
        }

        public static int ToInt(this string value)
        {
            return Int32.Parse(value);
        }

        public static int TryParseInt32(this string str, int defaultValue)
        {
            int result = defaultValue;
            return int.TryParse(str, out result) ? result : defaultValue;
        }

        public static string Take(this string value, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, value.Length);

            return (ellipsis && lengthToTake < value.Length) ?
                string.Format("{0}...", value.Substring(0, lengthToTake)) :
                value.Substring(0, lengthToTake);
        }

        public static string Skip(this string value, int count)
        {
            return value.Substring(Math.Min(count, value.Length) - 1);
        }

        #endregion String Extensions

        #region StringBuilder Extensions

        public static void Clear(this StringBuilder builder)
        {
            if (!builder.HasValue()) { return; } // TODO: raise exception or log error
            builder.Length = 0;
        }

        public static void AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendLine(string.Format(format, args));
        }

        #endregion StringBuilder Extensions



        #region Object Extensions

        public static T As<T>(this object obj) where T : class
        {
            return obj as T;
        }

        public static bool HasValue(this object obj)
        {
            return !(obj == null);
        }

        #endregion Object Extensions
    }
}
