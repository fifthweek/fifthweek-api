namespace Fifthweek.Api.EndToEndTestMailboxes.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class MailboxName
    {
        public static readonly Regex Pattern = new Regex(@"^wd_[0-9]{13}$", RegexOptions.Compiled);

        private MailboxName()
        {
        }

        public string Value { get; private set; }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static MailboxName Parse(string value)
        {
            MailboxName retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid mailbox name", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out MailboxName mailboxName)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out mailboxName, out errorMessages);
        }

        public static bool TryParse(string value, out MailboxName mailboxName, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (!Pattern.IsMatch(value))
                {
                    errorMessageList.Add("Must match the pattern 'wd_1234567890123'");
                }
            }

            if (errorMessageList.Count > 0)
            {
                mailboxName = null;
                return false;
            }

            mailboxName = new MailboxName
            {
                Value = value
            };

            return true;
        }
    }
}