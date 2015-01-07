using System;

namespace Fifthweek.Api.Core.Tests
{
    using System.Collections.Generic;
    [AutoEqualityMembers]
    public partial class ClassAugmentationParsingDummy
    {
        public int NotStrongTyped { get; set; }

        #region Constructed Strings

        [Constructed(typeof(ConstructedString))]
        public string SomeConstructedString { get; set; }

        [Optional]
        [Constructed(typeof(ConstructedString))]
        public string OptionalConstructedString { get; set; }

        #endregion


        #region Parsed Strings

        [Parsed(typeof(ParsedString))]
        public string SomeParsedString { get; set; }

        [Optional]
        [Parsed(typeof(ParsedString))]
        public string OptionalParsedString { get; set; }

        #endregion


        #region Constructed Integers

        [Constructed(typeof(ConstructedInt))]
        public int SomeConstructedInt { get; set; }

        #endregion


        #region Parsed Integers

        [Parsed(typeof(ParsedInt))]
        public int SomeParsedInt { get; set; }

        [Optional]
        [Parsed(typeof(ParsedInt))]
        public int OptionalParsedInt { get; set; }

        #endregion


        [AutoEqualityMembers, AutoConstructor]
        public partial class ConstructedString
        {
            [Optional]
            public string Value { get; private set; }
        }

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

        [AutoEqualityMembers, AutoConstructor]
        public partial class ConstructedInt
        {
            public int Value { get; private set; }
        }

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
}
