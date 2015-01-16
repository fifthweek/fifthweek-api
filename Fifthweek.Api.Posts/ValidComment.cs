namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidComment
    {
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 2000; // Some of the longer comments on Tumblr are over 1000 characters.

        private ValidComment()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value)
        {
            // Whitespace is considered a value, since values are not trimmed/normalized.
            return string.IsNullOrEmpty(value); // Trimmed types use IsNullOrWhiteSpace
        }

        public static ValidComment Parse(string value)
        {
            ValidComment retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid comment", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidComment comment)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out comment, out errorMessages);
        }

        public static bool TryParse(string value, out ValidComment comment, out IReadOnlyCollection<string> errorMessages)
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
            }

            if (errorMessageList.Count > 0)
            {
                comment = null;
                return false;
            }

            comment = new ValidComment
            {
                Value = value
            };

            return true;
        }
    }
}