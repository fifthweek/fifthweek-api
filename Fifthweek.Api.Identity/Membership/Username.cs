using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership
{
    [AutoEqualityMembers]
    public partial class Username
    {
        public static readonly Regex Pattern = new Regex(@"^[a-z0-9_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly int MinLength = 6;
        public static readonly int MaxLength = 20;

        protected Username()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value)
        {
            // Whitespace is considered an empty value. It is treated the same as null.
            return string.IsNullOrWhiteSpace(value);
        }

        public static Username Parse(string value)
        {
            Username retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid username", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out Username username)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out username, out errorMessages);
        }

        public static bool TryParse(string value, out Username username, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                var trimmedUsername = value.Trim();

                if (trimmedUsername.Length < MinLength || trimmedUsername.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                if (!Pattern.IsMatch(trimmedUsername))
                {
                    errorMessageList.Add("Only alphanumeric characters and underscores are allowed");
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
    }
}