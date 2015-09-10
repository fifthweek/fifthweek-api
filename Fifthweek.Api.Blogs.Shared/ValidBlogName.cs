namespace Fifthweek.Api.Blogs.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;


    /// <summary>
    /// This should match ValidChannelName, as the blog name is used as the default channel name.
    /// </summary>
    public partial class ValidBlogName : BlogName
    {
        public static readonly string ForbiddenCharacters = ValidChannelName.ForbiddenCharacters;
        public static readonly int MinLength = ValidChannelName.MinLength;
        public static readonly int MaxLength = ValidChannelName.MaxLength;

        private const string ForbiddenCharacterMessage = "Must not contain new lines or tabs";
        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        private ValidBlogName(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidBlogName Parse(string value)
        {
            ValidBlogName retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid blog name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidBlogName creatorName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out creatorName, out errorMessages);
        }

        public static bool TryParse(string value, out ValidBlogName creatorName, out IReadOnlyCollection<string> errorMessages)
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

            creatorName = new ValidBlogName(value);

            return true;
        }
    }
}