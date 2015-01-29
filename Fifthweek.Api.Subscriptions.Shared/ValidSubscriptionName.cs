namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidSubscriptionName
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 25;

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidSubscriptionName()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidSubscriptionName Parse(string value)
        {
            ValidSubscriptionName retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid subscription name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidSubscriptionName subscriptionName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out subscriptionName, out errorMessages);
        }

        public static bool TryParse(string value, out ValidSubscriptionName subscriptionName, out IReadOnlyCollection<string> errorMessages)
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
                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                if (value.Any(ForbiddenCharactersHashSet.Contains))
                {
                    errorMessageList.Add(ForbiddenCharacterMessage);
                }
            }

            if (errorMessageList.Count > 0)
            {
                subscriptionName = null;
                return false;
            }

            subscriptionName = new ValidSubscriptionName
            {
                Value = value
            };

            return true;
        }
    }
}