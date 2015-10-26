namespace Fifthweek.Api.Posts.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidPreviewText : PreviewText
    {
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 1000;

        private ValidPreviewText(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidPreviewText Parse(string value)
        {
            ValidPreviewText retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid preview text", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidPreviewText comment)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out comment, out errorMessages);
        }

        public static bool TryParse(string value, out ValidPreviewText comment, out IReadOnlyCollection<string> errorMessages)
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

            comment = new ValidPreviewText(value);

            return true;
        }
    }
}