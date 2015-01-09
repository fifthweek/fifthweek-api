using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership
{
    [AutoEqualityMembers]
    public partial class Email
    {
        private Email()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value, bool exact = false)
        {
            return exact ? string.IsNullOrEmpty(value) : string.IsNullOrWhiteSpace(value);
        }

        public static Email Parse(string value, bool exact = false)
        {
            Email retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages, exact))
            {
                throw new ArgumentException("Invalid email address", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out Email email, bool exact = false)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out email, out errorMessages, exact);
        }

        public static bool TryParse(string value, out Email email, out IReadOnlyCollection<string> errorMessages, bool exact = false)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value, exact))
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (!exact)
                {
                    value = Normalize(value);
                }

                try
                {
                    if (new MailAddress(value).Address != value.Trim())
                    {
                        errorMessageList.Add("Invalid format");
                    }
                }
                catch
                {
                    errorMessageList.Add("Invalid format");
                }

                if (value.Contains("\""))
                {
                    // Quoted names are valid, but to keep things sane we're not accepting them.
                    errorMessageList.Add("Must not contain quotes");
                }

                if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
                {
                    errorMessageList.Add("Only lowercase with no surrounding whitespace is allowed");
                }
            }

            if (errorMessageList.Count > 0)
            {
                email = null;
                return false;
            }

            email = new Email
            {
                Value = value
            };

            return true;
        }

        private static string Normalize(string value)
        {
            return value.Trim().ToLower();
        }
    }
}