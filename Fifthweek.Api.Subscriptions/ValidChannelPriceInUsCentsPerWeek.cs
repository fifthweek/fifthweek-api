namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidChannelPriceInUsCentsPerWeek
    {
        public static readonly int MinValue = 1;

        private ValidChannelPriceInUsCentsPerWeek()
        {
        }

        public int Value { get; protected set; }

        public static bool IsEmpty(int value)
        {
            return false;
        }

        public static ValidChannelPriceInUsCentsPerWeek Parse(int value)
        {
            ValidChannelPriceInUsCentsPerWeek retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid weekly price", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek weeklySubscriptionPrice)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out weeklySubscriptionPrice, out errorMessages);
        }

        public static bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < MinValue)
            {
                errorMessageList.Add(string.Format("Weekly price must be at least {0}", MinValue));
            }

            if (errorMessageList.Count > 0)
            {
                weeklySubscriptionPrice = null;
                return false;
            }

            weeklySubscriptionPrice = new ValidChannelPriceInUsCentsPerWeek
            {
                Value = value
            };

            return true;
        }
    }
}