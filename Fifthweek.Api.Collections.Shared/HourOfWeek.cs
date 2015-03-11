namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// Hour of the week, represented as UTC, starting with Sunday 00:00AM as 0, to be consistent with .NET's DayOfWeek enum.
    /// </summary>
    [AutoEqualityMembers, AutoJson]
    public partial class HourOfWeek
    {
        public const int MinValue = 0;
        public const int MaxValue = HoursInWeek - 1;

        private const int DaysInWeek = 7;
        private const int HoursInDay = 24;
        private const int HoursInWeek = DaysInWeek * HoursInDay;

        /// <remarks>
        /// Public constructor provided to allow JSON serialisation. As a note going forward: perhaps all types should have a constructor,
        /// either instead of Parse, or as synonymous functionality to Parse.
        /// </remarks>
        public HourOfWeek(int value)
        {
            this.Value = Parse(value).Value;
        }

        private HourOfWeek()
        {
        }

        public int Value { get; private set; }

        public static HourOfWeek Parse(int value)
        {
            HourOfWeek retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid hour of week", "value");
            }

            return retval;
        }

        public static HourOfWeek Parse(DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Must be UTC", "value");
            }

            return new HourOfWeek
            {
                Value = ((int)value.DayOfWeek * 24) + value.Hour
            };
        }

        public static bool TryParse(int value, out HourOfWeek hourOfWeek)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out hourOfWeek, out errorMessages);
        }

        public static bool TryParse(int value, out HourOfWeek hourOfWeek, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value > MaxValue)
            {
                errorMessageList.Add(string.Format("Must be no greater than {0}", MaxValue));
            }

            if (errorMessageList.Count > 0)
            {
                hourOfWeek = null;
                return false;
            }

            hourOfWeek = new HourOfWeek
            {
                Value = value
            };

            return true;
        }
    }
}