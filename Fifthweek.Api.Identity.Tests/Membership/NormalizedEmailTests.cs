using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NormalizedEmailTests : EmailTestsBase
    {
        [TestMethod]
        public void ItShouldNotAllowLeadingOrTrailingWhitespace()
        {
            this.BadEmail(" joe@bloggs.com");
            this.BadEmail("joe@bloggs.com ");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadEmail("Joe@Bloggs.com");
        }

        protected override Email Parse(string emailValue)
        {
            return NormalizedEmail.Parse(emailValue);
        }

        protected override bool TryParse(string emailValue, out Email email)
        {
            NormalizedEmail normalizedEmail;
            if (NormalizedEmail.TryParse(emailValue, out normalizedEmail))
            {
                email = normalizedEmail;
                return true;
            }

            email = null;
            return false;
        }
    }
}