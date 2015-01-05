using System;
using System.Collections.Generic;
using System.Linq;

namespace Fifthweek.Api.Subscriptions
{
    public class SubscriptionName
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 25;

        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        protected SubscriptionName()
        {
        }

        public string Value { get; protected set; }

        protected bool Equals(SubscriptionName other)
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
            return Equals((SubscriptionName)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public static SubscriptionName Parse(string value)
        {
            SubscriptionName retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid subscription name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out SubscriptionName subscriptionName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out subscriptionName, out errorMessages);
        }

        public static bool TryParse(string value, out SubscriptionName subscriptionName, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                errorMessageList.Add(string.Format("Subscription name length must be from {0} to {1} characters", MinLength, MaxLength));
            }

            if (value.Any(ForbiddenCharactersHashSet.Contains))
            {
                errorMessageList.Add("Subscription name must not contain new lines or tabs");
            }

            if (errorMessageList.Count > 0)
            {
                subscriptionName = null;
                return false;
            }

            subscriptionName = new SubscriptionName
            {
                Value = value
            };

            return true;
        }
    }
}