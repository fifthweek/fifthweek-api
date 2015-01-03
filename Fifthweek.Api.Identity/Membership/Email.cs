using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Fifthweek.Api.Identity.Membership
{
    public class Email
    {
        protected Email()
        {
        }

        public string Value { get; protected set; }

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
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out email, out errorMessages);
        }

        public static bool TryParse(string value, out Email email, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            try
            {
                if (new MailAddress(value).Address != value.Trim())
                {
                    errorMessageList.Add("Invalid email format");
                    email = null;
                    return false;
                }
            }
            catch
            {
                email = null;
                return false;
            }

            if (value.Contains("\""))
            {
                // Quoted names are valid, but to keep things sane we're not accepting them.
                errorMessageList.Add("Email must not contain quotes");
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