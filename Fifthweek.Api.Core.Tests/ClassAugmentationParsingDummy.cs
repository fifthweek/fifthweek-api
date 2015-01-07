using System.Linq;

namespace Fifthweek.Api.Core.Tests
{
    using System.Collections.Generic;
    [AutoEqualityMembers]
    public partial class ClassAugmentationParsingDummy
    {
        public int NotStrongTyped { get; set; }

        #region Unconditional Strings

        [StronglyTyped(typeof(UnconditionalString))]
        public string SomeUnconditionalString { get; set; }

        [Optional]
        [StronglyTyped(typeof(UnconditionalString))]
        public string OptionalUnconditionalString { get; set; }

        #endregion


        #region Conditional Strings

        [StronglyTyped(typeof(ConditionalString))]
        public string SomeConditionalString { get; set; }

        [Optional]
        [StronglyTyped(typeof(ConditionalString))]
        public string OptionalConditionalString { get; set; }

        #endregion


        #region Unconditional Integers

        [StronglyTyped(typeof(UnconditionalInt))]
        public int SomeUnconditionalInt { get; set; }

        #endregion


        #region Conditional Integers

        [StronglyTyped(typeof(ConditionalInt))]
        public int SomeConditionalInt { get; set; }

        [Optional]
        [StronglyTyped(typeof(ConditionalInt))]
        public int OptionalConditionalInt { get; set; }

        #endregion


        [AutoEqualityMembers, AutoConstructor]
        public partial class UnconditionalString
        {
            [Optional]
            public string Value { get; private set; }
        }

        [AutoEqualityMembers]
        public partial class ConditionalString
        {
            private ConditionalString()
            {
            }

            public string Value { get; private set; }

            public static bool IsEmpty(string value)
            {
                // Dummy logic. Sometimes this is just 'IsNullOrEmpty' when the type is whitespace sensitive, like on passwords.
                return string.IsNullOrWhiteSpace(value);
            }

            public static bool TryParse(string value, out ConditionalString @object, out IReadOnlyCollection<string> errorMessages)
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
                    if (value.Trim().Length == 1)
                    {
                        errorMessageList.Add("Length must be at least 1 character");
                    }
                }

                if (errorMessageList.Count > 0)
                {
                    @object = null;
                    return false;
                }

                @object = new ConditionalString
                {
                    Value = value
                };

                return true;
            }
        }

        [AutoEqualityMembers, AutoConstructor]
        public partial class UnconditionalInt
        {
            public int Value { get; private set; }
        }

        [AutoEqualityMembers]
        public partial class ConditionalInt
        {
            private ConditionalInt()
            {
            }

            public int Value { get; private set; }

            public static bool IsEmpty(int value)
            {
                return false;
            }

            public static bool TryParse(int value, out ConditionalInt @object, out IReadOnlyCollection<string> errorMessages)
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

                @object = new ConditionalInt
                {
                    Value = value
                };

                return true;
            }
        }
    }
}
