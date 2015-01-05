using System;
using System.Collections.Generic;

namespace Fifthweek.Api.Subscriptions
{
    public class WeeklySubscriptionPriceInUSCents
    {
        public static readonly int MinValue = 25;

        protected WeeklySubscriptionPriceInUSCents()
        {
        }

        public int Value { get; protected set; }

        protected bool Equals(WeeklySubscriptionPriceInUSCents other)
        {
            return string.Equals(this.Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((WeeklySubscriptionPriceInUSCents)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static WeeklySubscriptionPriceInUSCents Parse(int value)
        {
            WeeklySubscriptionPriceInUSCents retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid weekly subscription price", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out WeeklySubscriptionPriceInUSCents weeklySubscriptionPrice)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out weeklySubscriptionPrice, out errorMessages);
        }

        public static bool TryParse(int value, out WeeklySubscriptionPriceInUSCents weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
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

            weeklySubscriptionPrice = new WeeklySubscriptionPriceInUSCents
            {
                Value = value
            };

            return true;
        }
    }
}