namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidIntroduction
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 15; // Must be at least a few words.
        public static readonly int MaxLength = 250; // Approximately 3 lines of content at 750px in Lato Regular 18px.

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Parse method requires other private static members to be initialized.")]
        public static readonly ValidIntroduction Default = Parse(
            "Hello! Thinking of subscribing? Awesome! Subscriptions allow me to produce more of my awesome creations for all you lovely people to see here!");

        private ValidIntroduction()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidIntroduction Parse(string value)
        {
            ValidIntroduction retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid introduction", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidIntroduction introduction)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out introduction, out errorMessages);
        }

        public static bool TryParse(string value, out ValidIntroduction introduction, out IReadOnlyCollection<string> errorMessages)
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
                introduction = null;
                return false;
            }

            introduction = new ValidIntroduction
            {
                Value = value
            };

            return true;
        }
 
    }
}