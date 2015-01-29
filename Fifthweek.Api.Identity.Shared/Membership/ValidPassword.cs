namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Collections.Generic;

    public class ValidPassword : Password
    {
        public static readonly int MinLength = 6;

        public static readonly int MaxLength = 100;

        protected ValidPassword(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidPassword Parse(string value)
        {
            ValidPassword retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid password", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidPassword password)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out password, out errorMessages);
        }

        public static bool TryParse(string value, out ValidPassword password, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else if (value.Length < MinLength || value.Length > MaxLength)
            {
                errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
            }

            if (errorMessageList.Count > 0)
            {
                password = null;
                return false;
            }

            password = new ValidPassword(value);

            return true;
        }
    }
}