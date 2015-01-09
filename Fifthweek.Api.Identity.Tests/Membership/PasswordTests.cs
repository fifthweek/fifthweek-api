using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordTests : ValidatedPrimitiveEqualityTests<Password, string>
    {
        [TestMethod]
        public void ItShouldAllowBasicPasswords()
        {
            this.GoodValue("Secr3T!");
        }

        [TestMethod]
        public void ItShouldNowAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsUnder6Characters()
        {
            this.GoodValue("passwo");
            this.BadValue("passw");
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsOver100Characters()
        {
            this.GoodValue(new string(' ', 100));
            this.BadValue(new string(' ', 101));
        }

        protected override string ValueA
        {
            get { return "password"; }
        }

        protected override string ValueB
        {
            get { return "password2"; }
        }

        protected override Password Parse(string value, bool exact)
        {
            return Password.Parse(value);
        }

        protected override bool TryParse(string value, out Password parsedObject, bool exact)
        {
            return Password.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out Password parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Password.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(Password parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly string InvalidValue = "!";
    }
}