namespace Fifthweek.Api.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidCreditCardPrefix
    {
        public static readonly int RequiredLength = 6;

        private ValidCreditCardPrefix()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static ValidCreditCardPrefix Parse(string value)
        {
            ValidCreditCardPrefix retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid credit card prefix", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidCreditCardPrefix parsedValue)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out parsedValue, out errorMessages);
        }

        public static bool TryParse(string value, out ValidCreditCardPrefix parsedValue, out IReadOnlyCollection<string> errorMessages)
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
                value = value.Trim();

                if (value.Length != RequiredLength)
                {
                    errorMessageList.Add(string.Format("Length must be {0} characters", RequiredLength));
                }

                if (value.Any(v => !char.IsNumber(v)))
                {
                    errorMessageList.Add(string.Format("Must only contain numeric characters"));
                }
            }

            if (errorMessageList.Count > 0)
            {
                parsedValue = null;
                return false;
            }

            parsedValue = new ValidCreditCardPrefix
                              {
                                  Value = value
                              };

            return true;
        }
    }
}