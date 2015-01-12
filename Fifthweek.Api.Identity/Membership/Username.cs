namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Fifthweek.Api.Core;

    /// <remarks>
    /// Important: refer to `UpdatingValidationBehaviour.md` when changing validation behaviour.
    /// </remarks>
    [AutoEqualityMembers]
    public partial class Username
    {
        public static readonly Regex Pattern = new Regex(@"^[a-z0-9_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly int MinLength = 6;
        public static readonly int MaxLength = 20;

        private Username()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value, bool exact = false)
        {
            return exact ? string.IsNullOrEmpty(value) : string.IsNullOrWhiteSpace(value);
        }

        public static Username Parse(string value, bool exact = false)
        {
            Username retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages, exact))
            {
                throw new ArgumentException("Invalid username", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out Username username, bool exact = false)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out username, out errorMessages, exact);
        }

        public static bool TryParse(string value, out Username username, out IReadOnlyCollection<string> errorMessages, bool exact = false)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value, exact))
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (!exact)
                {
                    value = Normalize(value);
                }

                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                if (!Pattern.IsMatch(value))
                {
                    errorMessageList.Add("Only alphanumeric characters and underscores are allowed");
                }

                if (value.Any(c => char.IsUpper(c) || char.IsWhiteSpace(c)))
                {
                    errorMessageList.Add("Only lowercase with no surrounding whitespace is allowed");
                }
            }

            if (errorMessageList.Count > 0)
            {
                username = null;
                return false;
            }

            username = new Username
            {
                Value = value
            };

            return true;
        }

        private static string Normalize(string value)
        {
            return value.Trim().ToLower();
        }
    }
}