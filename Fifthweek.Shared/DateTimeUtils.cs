namespace Fifthweek.Shared
{
    using System;
    using System.Globalization;

    public static class DateTimeUtils
    {
        public static string ToIso8601String(this DateTime input)
        {
            return input.ToString("o", CultureInfo.InvariantCulture);
        }

        public static DateTime FromIso8601String(this string input)
        {
            return DateTime.Parse(input, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
        }

        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            if (dt.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Time must be in UTC");
            }

            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks, DateTimeKind.Utc);
        }
    }
}