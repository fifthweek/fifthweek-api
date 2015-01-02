using System;
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
                throw new ArgumentException("Invalid normalized email address", value);
            }

            return retval;
        }

        public static bool TryParse(string value, out NormalizedEmail normalizedEmail)
        {
            Email email;
            if (Email.TryParse(value, out email))
            {
                if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
                {
                    // Email addresses must be normalised to trimmed lowercase.
                    normalizedEmail = null;
                    return false;
                }

                normalizedEmail = new NormalizedEmail
                {
                    Value = value
                };

                return true;
            }

            normalizedEmail = null;
            return false;
        }
    }
}