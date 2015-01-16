using System;
using System.Collections.Generic;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions
{
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class ChannelPriceInUsCentsPerWeek
    {
        public static readonly int MinValue = 1;

        private ChannelPriceInUsCentsPerWeek()
        {
        }

        public int Value { get; protected set; }

        public static bool IsEmpty(int value)
        {
            return false;
        }

        public static ChannelPriceInUsCentsPerWeek Parse(int value)
        {
            ChannelPriceInUsCentsPerWeek retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid weekly subscription price", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out ChannelPriceInUsCentsPerWeek weeklySubscriptionPrice)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out weeklySubscriptionPrice, out errorMessages);
        }

        public static bool TryParse(int value, out ChannelPriceInUsCentsPerWeek weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < MinValue)
            {
                errorMessageList.Add(string.Format("Weekly subscription price must be at least {0}", MinValue));
            }

            if (errorMessageList.Count > 0)
            {
                weeklySubscriptionPrice = null;
                return false;
            }

            weeklySubscriptionPrice = new ChannelPriceInUsCentsPerWeek
            {
                Value = value
            };

            return true;
        }
    }
}