namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ValidCreatorName : CreatorName
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 25;

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidCreatorName(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidCreatorName Parse(string value)
        {
            ValidCreatorName retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid creator name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidCreatorName creatorName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out creatorName, out errorMessages);
        }

        public static bool TryParse(string value, out ValidCreatorName creatorName, out IReadOnlyCollection<string> errorMessages)
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
                creatorName = null;
                return false;
            }

            creatorName = new ValidCreatorName(value);

            return true;
        }
    }
}