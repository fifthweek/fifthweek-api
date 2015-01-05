using System;
using System.Collections.Generic;
using System.Linq;

namespace Fifthweek.Api.Identity.Membership
{
    public class NormalizedEmail : Email
    {
        protected NormalizedEmail()
        {
        }

        public static NormalizedEmail Normalize(Email email)
        {
            return new NormalizedEmail
            {
                Value = email.Value.Trim().ToLower()
            };
        }

        new public static NormalizedEmail Parse(string value)
        {
            NormalizedEmail retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid normalized email address", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out NormalizedEmail normalizedEmail)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out normalizedEmail, out errorMessages);
        }

        public static bool TryParse(string value, out NormalizedEmail normalizedEmail, out IReadOnlyCollection<string> errorMessages)
        {
            Email email;
            Email.TryParse(value, out email, out errorMessages);

            if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
            {
                // Email addresses must be normalised to trimmed lowercase.
                var errorMessageList = new List<String>();
                errorMessageList.AddRange(errorMessages);
                errorMessageList.Add("Must be normalized");
                errorMessages = errorMessageList;
            }

            if (errorMessages.Count > 0)
            {
                normalizedEmail = null;
                return false;
            }

            normalizedEmail = new NormalizedEmail
            {
                Value = value
            };

            return true;
        }
    }
}