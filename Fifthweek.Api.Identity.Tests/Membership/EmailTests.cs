using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmailTests : EmailTestsBase<Email>
    {
        [TestMethod]
        public void ItShouldAllowLeadingOrTrailingWhitespace()
        {
            this.GoodValue(" joe@bloggs.com");
            this.GoodValue("joe@bloggs.com ");
        }

        [TestMethod]
        public void ItShouldAllowUppercase()
        {
            this.GoodValue("Joe@Bloggs.com");
        }

        protected override Email Parse(string value, bool exact)
        {
            return Email.Parse(value);
        }

        protected override bool TryParse(string value, out Email parsedObject, bool exact)
        {
            return Email.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out Email parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Email.TryParse(value, out parsedObject, out errorMessages);
        }

        public static readonly string InvalidValue = "!";
    }
}