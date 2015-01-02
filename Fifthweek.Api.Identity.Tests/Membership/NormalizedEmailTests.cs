using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NormalizedEmailTests : EmailTests
    {
        [TestMethod]
        public void ItShouldNotAllowWhitespace()
        {
            this.BadEmail(" joe@bloggs.com");
            this.BadEmail("joe@bloggs.com ");
            this.BadEmail("joe @bloggs.com");
            this.BadEmail("joe@ bloggs.com");
            this.BadEmail("jo e@bloggs.com");
            this.BadEmail("joe@blo ggs.com");
            this.BadEmail("joe@bloggs .com");
            this.BadEmail("joe@bloggs. com");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadEmail("Joe@Bloggs.com");
        }

        override protected Email Parse(string emailValue)
        {
            return NormalizedEmail.Parse(emailValue);
        }

        override protected bool TryParse(string emailValue, out Email email)
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