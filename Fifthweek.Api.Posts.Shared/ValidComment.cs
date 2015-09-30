namespace Fifthweek.Api.Posts.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidComment : Comment
    {
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 50000; // Some of the longer comments on Tumblr are over 1000 characters.

        private ValidComment(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
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
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
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

            comment = new ValidComment(value);

            return true;
        }
    }
}