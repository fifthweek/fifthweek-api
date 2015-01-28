namespace Fifthweek.Api.Collections.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// Hour of the week, represented as UTC, starting with Sunday 00:00AM as 0, to be consistent with .NET's DayOfWeek enum.
    /// </summary>
    [AutoEqualityMembers]
    public partial class HourOfWeek
    {
        public const int MinValue = 0;
        public const int MaxValue = HoursInWeek - 1;

        private const int DaysInWeek = 7;
        private const int HoursInDay = 24;
        private const int HoursInWeek = DaysInWeek * HoursInDay;

        public HourOfWeek(DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Must be UTC", "value");
            }

            this.Value = (byte)(((int)value.DayOfWeek * 24) + value.Hour);
        }

        public HourOfWeek(byte zeroBasedHourOfWeek)
        {
            if (zeroBasedHourOfWeek > MaxValue)
            {
                throw new ArgumentOutOfRangeException(string.Format("Must be no greater than {0}", MaxValue));
            }

            this.Value = zeroBasedHourOfWeek;
        }

        public byte Value { get; private set; }
    }
}