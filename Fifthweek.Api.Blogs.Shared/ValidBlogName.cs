namespace Fifthweek.Api.Blogs.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Identity.Shared.Membership;

    public partial class ValidBlogName : BlogName
    {
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

        public static bool TryParse(string value, out ValidBlogName blogName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out blogName, out errorMessages);
        }

        public static bool TryParse(string value, out ValidBlogName blogName, out IReadOnlyCollection<string> errorMessages)
        {
            // Blog names and creator names must have same validity rules, as creator names are used
            // as the default blog names.
            ValidCreatorName creatorName;
            if (ValidCreatorName.TryParse(value, out creatorName, out errorMessages))
            {
                blogName = new ValidBlogName(value);
                return true;
            }

            blogName = null;
            return false;
        }
    }
}