using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fifthweek.Api.Identity.Membership
{
    public class Username
    {
        public static readonly Regex Pattern = new Regex(@"^[a-z0-9_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly int MinLength = 6;
        public static readonly int MaxLength = 20;

        protected Username()
        {
        }

        public string Value { get; protected set; }

        protected bool Equals(Username other)
        {
            return string.Equals(this.Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Username)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public static Username Parse(string value)
        {
            Username retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid username", value);
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
            var trimmedUsername = value.Trim();
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (trimmedUsername.Length < MinLength || trimmedUsername.Length > MaxLength)
            {
                errorMessageList.Add(string.Format("Length must be at least {0} and at most {1}", MinLength, MaxLength));
            }

            if (!Pattern.IsMatch(trimmedUsername))
            {
                errorMessageList.Add("Only alphanumeric characters and underscores are allowed in the username.");
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