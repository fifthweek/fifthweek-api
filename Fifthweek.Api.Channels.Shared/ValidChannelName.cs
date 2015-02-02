namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidChannelName
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 50;

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidChannelName()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidChannelName Parse(string value)
        {
            ValidChannelName retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid channel name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidChannelName channelName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out channelName, out errorMessages);
        }

        public static bool TryParse(string value, out ValidChannelName channelName, out IReadOnlyCollection<string> errorMessages)
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
                channelName = null;
                return false;
            }

            channelName = new ValidChannelName
            {
                Value = value
            };

            return true;
        }
    }
}