using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;

namespace Fifthweek.Api.Identity.Membership
{
    public class Email
    {
        private Email()
        {
        }

        public string Value { get; private set; }

        protected bool Equals(Email other)
        {
            return string.Equals(this.Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Email) obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public static Email Parse(string value)
        {
            Email retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid email address", value);
            }

            return retval;
        }

        public static bool TryParse(string value, out Email email)
        {
            try
            {
                new MailAddress(value);
            }
            catch
            {
                email = null;
                return false;
            }

            if (value.Contains("\""))
            {
                // Quoted names are valid, but to keep things sane we're not accepting them.
                email = null;
                return false;
            }

            if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
            {
                // Email addresses must be normalised to trimmed lowercase.
                email = null;
                return false;
            }

            email = new Email
            {
                Value = value
            };

            return true;
        }
    }
}