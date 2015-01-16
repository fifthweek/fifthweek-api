namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    [AutoEqualityMembers]
    public partial class ParsedInt
    {
        private ParsedInt()
        {
        }

        public int Value { get; private set; }

        public static bool IsEmpty(int value)
        {
            return false;
        }

        public static ParsedInt Parse(int value)
        {
            ParsedInt retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid value", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out ParsedInt @object, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < 0)
            {
                errorMessageList.Add("Must be positive");
            }

            if (errorMessageList.Count > 0)
            {
                @object = null;
                return false;
            }

            @object = new ParsedInt
            {
                Value = value
            };

            return true;
        }
    }
}