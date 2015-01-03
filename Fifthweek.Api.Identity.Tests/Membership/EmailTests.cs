using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmailTests : EmailTestsBase
    {
        [TestMethod]
        public void ItShouldAllowLeadingOrTrailingWhitespace()
        {
            this.GoodEmail(" joe@bloggs.com");
            this.GoodEmail("joe@bloggs.com ");
        }

        [TestMethod]
        public void ItShouldAllowUppercase()
        {
            this.GoodEmail("Joe@Bloggs.com");
        }

        protected override Email Parse(string emailValue)
        {
            return Email.Parse(emailValue);
        }

        protected override bool TryParse(string emailValue, out Email email)
        {
            return Email.TryParse(emailValue, out email);
        }
    }
}