namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ValidTagline : Tagline
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 5;
        public static readonly int MaxLength = 55; // Need to support XKCD ;) "A webcomic of romance, sarcasm, math, and language."

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidTagline(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidTagline Parse(string value)
        {
            ValidTagline retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid tagline", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidTagline tagline)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out tagline, out errorMessages);
        }

        public static bool TryParse(string value, out ValidTagline tagline, out IReadOnlyCollection<string> errorMessages)
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
                tagline = null;
                return false;
            }

            tagline = new ValidTagline(value);

            return true;
        }
    }
}