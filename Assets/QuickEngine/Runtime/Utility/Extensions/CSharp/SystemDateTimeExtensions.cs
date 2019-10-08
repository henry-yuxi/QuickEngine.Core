namespace QuickEngine.Extensions
{
    using System;

    public static class DateTimeConstants
    {
        #region Constants

        internal const int DAYS_PER_WEEK = 7;

        internal const int DAYS_PER_FORTNIGHT = DAYS_PER_WEEK * 2;

        internal const int WEEKS_PER_FORTNIGHT = 2;

        internal const int YEARS_PER_DECADE = 10;

        internal const int YEARS_PER_CENTURY = 100;

        internal const int JANUARY = 1;

        internal const int FEBRUARY = 2;

        internal const int MARCH = 3;

        internal const int APRIL = 4;

        internal const int MAY = 5;

        internal const int JUNE = 6;

        internal const int JULY = 7;

        internal const int AUGUST = 8;

        internal const int SEPTEMBER = 9;

        internal const int OCTOBER = 10;

        internal const int NOVEMBER = 11;

        internal const int DECEMBER = 12;

        #endregion Constants

        #region DataTime Formats

        public const string DEFAULT_DATE_TIME_FORMAT_PATTERN = "yyyy-MM-dd HH:mm:ss";

        public const string DEFAULT_DATE_TIME_HHmm_FORMAT_PATTERN = "yyyy-MM-dd HH:mm";

        public const string DEFAULT_DATE_TIME_HH_FORMAT_PATTERN = "yyyy-MM-dd HH";

        public const string DEFAULT_DATE_FORMAT_PATTERN = "yyyy-MM-dd";

        public const string DEFAULT_TIME_FORMAT_PATTERN = "HH:mm:ss";

        public const string DEFAULT_TIME_HHmm_FORMAT_PATTERN = "HH:mm";

        #endregion DataTime Formats

        public static readonly string[] WeekDescribes = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
    }

    public static partial class CSharpExtensions
    {
        private static readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        private static readonly DateTime LocalTime = TimeZone.CurrentTimeZone.ToLocalTime(StartTime);

        public static long ToTimeStamp(this DateTime date, bool mIsUnix = true)
        {
            return mIsUnix ? (long)(date - StartTime).TotalSeconds : (long)date.Subtract(StartTime).TotalMilliseconds;
        }

        public static DateTime ToDateTime(this long timeStamp, bool mIsUnix = true)
        {
            return mIsUnix ? LocalTime.AddSeconds(timeStamp) : LocalTime.AddMilliseconds(timeStamp);
        }

        public static DateTime ToDateTime(this string timeStamp, bool mIsUnix = true)
        {
            return LocalTime.Add(new TimeSpan(long.Parse(timeStamp + (mIsUnix ? "0000000" : "0000"))));
        }

        public static string ToFormat(this DateTime date, string format)
        {
            return date.ToString(format);
        }

        public static string ToFormat(this DateTime date, string format, string separator)
        {
            return string.IsNullOrEmpty(separator) ? date.ToFormat(format) : date.ToString(string.Format(format, separator));
        }

        public static string ToWeekName(this DateTime date, string[] days = null)
        {
            if (days.IsNullOrEmpty())
            {
                days = DateTimeConstants.WeekDescribes;
            }
            return days[Convert.ToInt16(date.DayOfWeek)];
        }

        public static bool IsBetween(this DateTime value, DateTime from, DateTime to)
        {
            return ((from <= value) && (to >= value));
        }

        public static DateTime Midnight(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }

        public static DateTime DateFromDay(this int day, int month)
        {
            return new DateTime(DateTime.Now.Year, month, day);
        }

        public static DateTime DateFromDay(this int day, int month, int year)
        {
            return new DateTime(year, month, day);
        }

        public static DateTime DateFromMonth(this int month, int day)
        {
            return new DateTime(DateTime.Now.Year, month, day);
        }

        public static DateTime DateFromMonth(this int month, int day, int year)
        {
            return new DateTime(year, month, day);
        }

        public static DateTime Yesterday(this DateTime value)
        {
            return value.AddDays(-1);
        }

        public static DateTime YesterdayMidnight(this DateTime value)
        {
            return value.Yesterday().Midnight();
        }

        public static DateTime Tomorrow(this DateTime value)
        {
            return value.AddDays(1);
        }

        public static DateTime TomorrowMidnight(this DateTime value)
        {
            return value.Tomorrow().Midnight();
        }

        public static bool IsBefore(this DateTime date, DateTime other)
        {
            return date.CompareTo(other) < 0;
        }

        public static bool IsAfter(this DateTime date, DateTime other)
        {
            return date.CompareTo(other) > 0;
        }

        public static bool IsSameDay(this DateTime value, DateTime compareDate)
        {
            return value.Midnight().Equals(compareDate.Midnight());
        }

        public static bool IsToday(this DateTime date)
        {
            return date.Date == DateTime.Now.Date;
        }

        public static bool IsTomorrow(this DateTime date)
        {
            return date.Date == DateTime.Now.Date.AddDays(1);
        }

        public static bool IsYesterday(this DateTime date)
        {
            return date.Date == DateTime.Now.Date.AddDays(-1);
        }

        public static bool IsLeapYear(this int year)
        {
            if (year <= 0001 || year >= 9999) return false;
            return DateTime.IsLeapYear(year);
        }

        #region IsDayOfWeek

        public static bool IsMonday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Monday;
        }

        public static bool IsTuesday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Tuesday;
        }

        public static bool IsWednesday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Wednesday;
        }

        public static bool IsThursday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Thursday;
        }

        public static bool IsFriday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        public static bool IsSaturday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsSunday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday;
        }

        #endregion IsDayOfWeek

        #region IsInMonth

        public static bool IsInJanuary(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.JANUARY);
        }

        public static bool IsInFebruary(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.FEBRUARY);
        }

        public static bool IsInMarch(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.MARCH);
        }

        public static bool IsInApril(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.APRIL);
        }

        public static bool IsInMay(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.MAY);
        }

        public static bool IsInJune(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.JUNE);
        }

        public static bool IsInJuly(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.JULY);
        }

        public static bool IsInAugust(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.AUGUST);
        }

        public static bool IsInSeptember(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.SEPTEMBER);
        }

        public static bool IsInOctober(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.OCTOBER);
        }

        public static bool IsInNovember(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.NOVEMBER);
        }

        public static bool IsInDecember(this DateTime date)
        {
            return date.Month.Equals(DateTimeConstants.DECEMBER);
        }

        #endregion IsInMonth
    }
}