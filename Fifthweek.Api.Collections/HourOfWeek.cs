namespace Fifthweek.Api.Collections
{
    using System;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// Hour of the week, represented as UTC, starting with Sunday 00:00AM as 0, to be consistent with .NET's DayOfWeek enum.
    /// </summary>
    [AutoEqualityMembers]
    public partial class HourOfWeek
    {
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

        public HourOfWeek(byte value)
        {
            if (value >= HoursInWeek)
            {
                throw new ArgumentOutOfRangeException(string.Format("Must be less than {0}", HoursInWeek));
            }

            this.Value = value;
        }

        public byte Value { get; private set; }
    }
}