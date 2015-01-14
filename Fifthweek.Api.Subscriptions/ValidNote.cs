namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class ValidNote
    {
        public static readonly string ForbiddenCharacters = "\t"; // Tweets allow new lines, so these have been removed from the exclusion list.
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 280; // Twice a tweet :)

        private const string ForbiddenCharacterMessage = "Must not contain tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidNote()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value)
        {
            // Whitespace is considered a value, since values are not trimmed/normalized.
            return string.IsNullOrEmpty(value); // Trimmed types use IsNullOrWhiteSpace
        }

        public static ValidNote Parse(string value)
        {
            ValidNote retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid note", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidNote note)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out note, out errorMessages);
        }

        public static bool TryParse(string value, out ValidNote note, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
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
                note = null;
                return false;
            }

            note = new ValidNote
            {
                Value = value
            };

            return true;
        }
    }
}