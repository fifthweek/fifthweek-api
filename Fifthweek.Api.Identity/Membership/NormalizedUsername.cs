using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fifthweek.Api.Identity.Membership
{
    public class NormalizedUsername : Username
    {
        protected NormalizedUsername()
        {
        }

        public static NormalizedUsername Normalize(Username username)
        {
            return new NormalizedUsername
            {
                Value = username.Value.Trim().ToLower()
            };
        }

        new public static NormalizedUsername Parse(string value)
        {
            NormalizedUsername retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid username", value);
            }

            return retval;
        }

        public static bool TryParse(string value, out NormalizedUsername normalizedUsername)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out normalizedUsername, out errorMessages);
        }

        public static bool TryParse(string value, out NormalizedUsername normalizedUsername, out IReadOnlyCollection<string> errorMessages)
        {
            Username username;
            Username.TryParse(value, out username, out errorMessages);

            if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
            {
                // Usernames must be normalised to trimmed lowercase.
                var errorMessageList = new List<String>();
                errorMessageList.AddRange(errorMessages);
                errorMessageList.Add("Must be normalized");
                errorMessages = errorMessageList;
            }

            if (errorMessages.Count > 0)
            {
                normalizedUsername = null;
                return false;
            }

            normalizedUsername = new NormalizedUsername
            {
                Value = value
            };

            return true;
        }
    }
}