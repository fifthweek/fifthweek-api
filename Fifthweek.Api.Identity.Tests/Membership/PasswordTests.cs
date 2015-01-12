namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordTests : ValidatedStringTests<Password>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "password"; }
        }

        protected override string ValueB
        {
            get { return "password2"; }
        }

        [TestMethod]
        public void ItShouldAllowBasicPasswords()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldNowAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsUnder6Characters()
        {
            this.AssertMinLength(6);
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsOver100Characters()
        {
            this.AssertMaxLength(100);
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
    }
}