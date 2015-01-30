namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class WeeklyReleaseSchedule
    {
        // Natural maximum, since number of elements cannot exceed this range whilst remaining unique.
        public const int MaxElements = 24 * 7;

        private WeeklyReleaseSchedule()
        {
        }

        public IReadOnlyList<HourOfWeek> Value { get; private set; }

        public static bool IsEmpty(IReadOnlyList<HourOfWeek> value)
        {
            return value == null 
                || value.Count == 0; // Very important. There must always be at least one weekly release time per collection.
        }

        public static WeeklyReleaseSchedule Parse(IReadOnlyList<HourOfWeek> value)
        {
            WeeklyReleaseSchedule retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid weekly price", "value");
            }

            return retval;
        }

        public static bool TryParse(IReadOnlyList<HourOfWeek> value, out WeeklyReleaseSchedule weeklyReleaseSchedule)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out weeklyReleaseSchedule, out errorMessages);
        }

        public static bool TryParse(IReadOnlyList<HourOfWeek> value, out WeeklyReleaseSchedule weeklyReleaseSchedule, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (value.Count > MaxElements)
                {
                    errorMessageList.Add(string.Format("Must have no more than {0} release times", MaxElements));
                }

                if (value.Distinct().Count() != value.Count)
                {
                    errorMessageList.Add("Release times must be unique");
                }
            }

            if (errorMessageList.Count > 0)
            {
                weeklyReleaseSchedule = null;
                return false;
            }

            weeklyReleaseSchedule = new WeeklyReleaseSchedule
            {
                Value = value
            };

            return true;
        }
    }
}