namespace Fifthweek.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class PositiveInt
    {
        public static readonly int MinValue = 1;

        private PositiveInt()
        {
        }

        public int Value { get; protected set; }

        public static PositiveInt Parse(int value)
        {
            PositiveInt retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid positive integer", "value");
            }

            return retval;
        }

        public static bool TryParse(int value, out PositiveInt positiveInt)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out positiveInt, out errorMessages);
        }

        public static bool TryParse(int value, out PositiveInt positiveInt, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value < MinValue)
            {
                errorMessageList.Add(string.Format("Must be at least {0}", MinValue));
            }

            if (errorMessageList.Count > 0)
            {
                positiveInt = null;
                return false;
            }

            positiveInt = new PositiveInt
            {
                Value = value
            };

            return true;
        }
    }
}