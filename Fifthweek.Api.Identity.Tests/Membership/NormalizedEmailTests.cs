using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NormalizedEmailTests : EmailTestsBase<NormalizedEmail>
    {
        [TestMethod]
        public void ItShouldNotAllowLeadingOrTrailingWhitespace()
        {
            this.BadValue(" joe@bloggs.com");
            this.BadValue("joe@bloggs.com ");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadValue("Joe@Bloggs.com");
        }

        protected override NormalizedEmail Parse(string value)
        {
            return NormalizedEmail.Parse(value);
        }

        protected override bool TryParse(string value, out NormalizedEmail parsedObject)
        {
            return NormalizedEmail.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out NormalizedEmail parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return NormalizedEmail.TryParse(value, out parsedObject, out errorMessages);
        }
    }
}