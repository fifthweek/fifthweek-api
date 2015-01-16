using System;
using System.Collections.Generic;

namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class ParsedString
    {
        private ParsedString()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            // Dummy logic. Sometimes this is just 'IsNullOrEmpty' when the type is whitespace sensitive, like on passwords.
            return string.IsNullOrWhiteSpace(value);
        }

        public static ParsedString Parse(string value)
        {
            ParsedString retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid value", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ParsedString @object, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value == null)
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (value.Trim().Length < 2)
                {
                    errorMessageList.Add("Length must be at least 2 characters");
                }
            }

            if (errorMessageList.Count > 0)
            {
                @object = null;
                return false;
            }

            @object = new ParsedString
            {
                Value = value
            };

            return true;
        }
    }
}