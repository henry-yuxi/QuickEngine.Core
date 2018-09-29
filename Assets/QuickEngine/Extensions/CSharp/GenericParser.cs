namespace QuickEngine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public static class GenericParser
    {
        private static readonly Dictionary<Type, Func<string, object>> Parsers = new Dictionary<Type, Func<string, object>>
        {
            { typeof(object), s => s },
            { typeof(string), s => s },
            { typeof(bool), s => bool.Parse(s) },
            { typeof(int), s => int.Parse(s) },
            { typeof(long), s => long.Parse(s) },
            { typeof(double), s => double.Parse(s) },
            { typeof(float), s => float.Parse(s) },
            { typeof(decimal), s => decimal.Parse(s) },
            { typeof(short), s => short.Parse(s) },
            { typeof(byte), s => byte.Parse(s) },
            { typeof(sbyte), s => sbyte.Parse(s) },
            { typeof(uint), s => uint.Parse(s) },
            { typeof(ulong), s => ulong.Parse(s) },
            { typeof(ushort), s => ushort.Parse(s) },
            { typeof(char), s => char.Parse(s) },
            { typeof(bool?), s => string.IsNullOrEmpty(s) ? null : (bool?) bool.Parse(s) },
            { typeof(int?), s => string.IsNullOrEmpty(s) ? null : (int?)  int.Parse(s) },
            { typeof(long?), s => string.IsNullOrEmpty(s) ? null : (long?)  long.Parse(s) },
            { typeof(double?), s => string.IsNullOrEmpty(s) ? null : (double?)  double.Parse(s) },
            { typeof(float?), s => string.IsNullOrEmpty(s) ? null : (float?)  float.Parse(s) },
            { typeof(decimal?), s => string.IsNullOrEmpty(s) ? null : (decimal?)  decimal.Parse(s) },
            { typeof(short?), s => string.IsNullOrEmpty(s) ? null : (short?)  short.Parse(s) },
            { typeof(byte?), s => string.IsNullOrEmpty(s) ? null : (byte?)  byte.Parse(s) },
            { typeof(sbyte?), s => string.IsNullOrEmpty(s) ? null : (sbyte?)  sbyte.Parse(s) },
            { typeof(uint?), s => string.IsNullOrEmpty(s) ? null : (uint?)  uint.Parse(s) },
            { typeof(ulong?), s => string.IsNullOrEmpty(s) ? null : (ulong?)  ulong.Parse(s) },
            { typeof(ushort?), s => string.IsNullOrEmpty(s) ? null : (ushort?)  ushort.Parse(s) },
            { typeof(char?), s => s == " " ? ' ' : (string.IsNullOrEmpty(s) ? null : (char?)  char.Parse(s)) },
            { typeof(DateTime), s => DateTime.Parse(s, CultureInfo.InvariantCulture)},
            { typeof(DateTimeOffset), s => DateTimeOffset.Parse(s, CultureInfo.InvariantCulture) },
            //{ typeof(TimeSpan), s => TimeSpan.Parse(s, CultureInfo.InvariantCulture) },
            //{ typeof(Guid), s => Guid.Parse(s) },
            { typeof(DateTime?), s => string.IsNullOrEmpty(s) ? null : (DateTime?) DateTime.Parse(s, CultureInfo.InvariantCulture) },
            { typeof(DateTimeOffset?), s => string.IsNullOrEmpty(s) ? null : (DateTimeOffset?) DateTimeOffset.Parse(s, CultureInfo.InvariantCulture) },
            //{ typeof(TimeSpan?), s => string.IsNullOrEmpty(s) ? null : (TimeSpan?) TimeSpan.Parse(s, CultureInfo.InvariantCulture) },
            //{ typeof(Guid?), s => string.IsNullOrEmpty(s) ? null : (Guid?) Guid.Parse(s) },
        };

        public static T Parse<T>(string stringValue)
        {
            var type = typeof(T);
            if (!Parsers.ContainsKey(type))
            {
                throw new ArgumentException("No parser have been registered for type: {type}");
            }
            return (T)Parsers[type].Invoke(stringValue);
        }

        public static bool TryParse<T>(string stringValue, out T value)
        {
            try
            {
                value = Parse<T>(stringValue);
            }
            catch
            {
                value = default(T);
                return false;
            }
            return true;
        }

        public static void RegisterParser(Type type, Func<string, object> parseFunc)
        {
            Parsers[type] = parseFunc;
        }
    }
}