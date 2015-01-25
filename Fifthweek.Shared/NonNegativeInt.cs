namespace Fifthweek.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NonNegativeInt
    {
        public static readonly int MinValue = 0;

        private NonNegativeInt()
        {
        }

        public int Value { get; protected set; }

        public static bool IsEmpty(int value)
        {
            return false;
        }

        public static NonNegativeInt Parse(int value)
        {
            NonNegativeInt retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid non-negative integer", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out NonNegativeInt nonNegativeInt)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out nonNegativeInt, out errorMessages);
        }

        public static bool TryParse(int value, out NonNegativeInt nonNegativeInt, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < MinValue)
            {
                errorMessageList.Add(string.Format("Must be at least {0}", MinValue));
            }

            if (errorMessageList.Count > 0)
            {
                nonNegativeInt = null;
                return false;
            }

            nonNegativeInt = new NonNegativeInt
            {
                Value = value
            };

            return true;
        } 
    }
}