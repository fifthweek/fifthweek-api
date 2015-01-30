namespace Fifthweek.CodeGeneration.Tests.Parsing
{
    using System;
    using System.Collections.Generic;

    [AutoEqualityMembers]
    public partial class ParsedMappedCollection
    {
        private ParsedMappedCollection()
        {
        }

        public IReadOnlyList<ParsedNormalizedString> Value { get; private set; }

        public static bool IsEmpty(IReadOnlyList<ParsedNormalizedString> value)
        {
            return value == null || value.Count == 0;
        }

        public static ParsedMappedCollection Parse(IReadOnlyList<ParsedNormalizedString> value)
        {
            ParsedMappedCollection retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid value", "value");
            }

            return retval;
        }

        public static bool TryParse(IReadOnlyList<ParsedNormalizedString> value, out ParsedMappedCollection @object, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value == null)
            {
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (value.Count < 2)
                {
                    errorMessageList.Add("Need at least 2 elements");
                }
            }

            if (errorMessageList.Count > 0)
            {
                @object = null;
                return false;
            }

            @object = new ParsedMappedCollection
            {
                Value = value
            };

            return true;
        }
    }
}