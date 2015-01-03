using System;
using System.Collections.Generic;

namespace Fifthweek.Api.Identity.Membership
{
    public class Password
    {
        public static readonly int MinLength = 6;
        public static readonly int MaxLength = 100;

        protected Password()
        {
        }

        public string Value { get; protected set; }

        protected bool Equals(Password other)
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
            return Equals((Password)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public static Password Parse(string value)
        {
            Password retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid password", value);
            }

            return retval;
        }

        public static bool TryParse(string value, out Password password)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out password, out errorMessages);
        }

        public static bool TryParse(string value, out Password password, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                errorMessageList.Add(string.Format("Password length must be from {0} to {1} characters", MinLength, MaxLength));
            }

            if (errorMessageList.Count > 0)
            {
                password = null;
                return false;
            }

            password = new Password
            {
                Value = value
            };

            return true;
        }
    }
}