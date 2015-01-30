namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public class ValidEmail : Email
    {
        public static readonly int MinLength = 3;
        public static readonly int MaxLength = 256; // Taken from ASP.NET Identity.

        private ValidEmail(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static ValidEmail Parse(string value)
        {
            ValidEmail retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid email address", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidEmail email)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out email, out errorMessages);
        }

        public static bool TryParse(string value, out ValidEmail email, out IReadOnlyCollection<string> errorMessages)
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
                value = Email.Normalize(value);

                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
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
            }

            if (errorMessageList.Count > 0)
            {
                email = null;
                return false;
            }

            email = new ValidEmail(value);

            return true;
        }
    }
}