using System;
using System.Collections.Generic;
using System.Linq;

namespace Fifthweek.Api.Subscriptions
{
    public class Tagline
    {
        public static readonly string ForbiddenCharacters = "\r\n\t";
        public static readonly int MinLength = 5;
        public static readonly int MaxLength = 55; // Need to support XKCD ;) "A webcomic of romance, sarcasm, math, and language."

        private static readonly HashSet<char> ForbiddenCharactersHashSet = new HashSet<char>(ForbiddenCharacters);

        protected Tagline()
        {
        }

        public string Value { get; protected set; }

        protected bool Equals(Tagline other)
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
            return Equals((Tagline)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public static Tagline Parse(string value)
        {
            Tagline retval;
            if (!TryParse(value, out retval))
            {
                throw new ArgumentException("Invalid tagline", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out Tagline tagline)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out tagline, out errorMessages);
        }

        public static bool TryParse(string value, out Tagline tagline, out IReadOnlyCollection<string> errorMessages)
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
                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                if (value.Any(ForbiddenCharactersHashSet.Contains))
                {
                    errorMessageList.Add("Must not contain new lines or tabs");
                }
            }

            if (errorMessageList.Count > 0)
            {
                tagline = null;
                return false;
            }

            tagline = new Tagline
            {
                Value = value
            };

            return true;
        }
    }
}