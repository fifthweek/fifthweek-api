using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NormalizedUsernameTests : UsernameTestsBase<NormalizedUsername>
    {
        [TestMethod]
        public void ItShouldNotAllowLeadingOrTrailingWhitespace()
        {
            this.BadValue(" joebloggs");
            this.BadValue("joebloggs ");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadValue("JoeBloggs");
        }

        protected override NormalizedUsername Parse(string value)
        {
            return NormalizedUsername.Parse(value);
        }

        protected override bool TryParse(string value, out NormalizedUsername parsedObject)
        {
            return NormalizedUsername.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out NormalizedUsername parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return NormalizedUsername.TryParse(value, out parsedObject, out errorMessages);
        }
    }
}