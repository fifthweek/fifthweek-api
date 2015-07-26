namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;

    public abstract class ValidChannelPriceBase<T>
        where T : ValidChannelPriceBase<T>
    {
        public abstract int Value { get; protected set; }

        protected static T Parse(int value, int minValue, Func<T> construct)
        {
            T retval;
            if (!TryParse(value, minValue, construct, out retval))
            {
                throw new ArgumentException("Invalid weekly price", "value");
            }

            return retval;
        }

        protected static bool TryParse(int value, int minValue, Func<T> construct, out T weeklySubscriptionPrice)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, minValue, construct, out weeklySubscriptionPrice, out errorMessages);
        }

        protected static bool TryParse(int value, int minValue, Func<T> construct, out T weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < minValue)
            {
                errorMessageList.Add(string.Format("Weekly price must be at least {0}", minValue));
            }

            if (errorMessageList.Count > 0)
            {
                weeklySubscriptionPrice = null;
                return false;
            }

            weeklySubscriptionPrice = construct();
            weeklySubscriptionPrice.Value = value;

            return true;
        }
    }
}