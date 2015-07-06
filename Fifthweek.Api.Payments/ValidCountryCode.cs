namespace Fifthweek.Api.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidCountryCode
    {
        public static readonly int MinLength = 2;
        public static readonly int MaxLength = 3;

        private ValidCountryCode()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidCountryCode Parse(string value)
        {
            ValidCountryCode retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid country code", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidCountryCode parsedValue)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out parsedValue, out errorMessages);
        }

        public static bool TryParse(string value, out ValidCountryCode parsedValue, out IReadOnlyCollection<string> errorMessages)
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
                value = value.Trim().ToUpperInvariant();

                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                if (value.Any(v => !char.IsLetter(v)))
                {
                    errorMessageList.Add("Invalid format");
                }
            }

            if (errorMessageList.Count > 0)
            {
                parsedValue = null;
                return false;
            }

            parsedValue = new ValidCountryCode
                              {
                                  Value = value
                              };

            return true;
        }
    }
}