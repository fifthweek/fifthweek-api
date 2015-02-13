namespace Fifthweek.Api.EndToEndTestMailboxes.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MailboxNameTests : ValidatedStringTests<MailboxName>
    {
        protected override string ValueA
        {
            get { return "wd_4495245740241"; }
        }

        protected override string ValueB
        {
            get { return "wd_6367342625267"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = MailboxName.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = MailboxName.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = MailboxName.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = MailboxName.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowValidMailboxNames()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldBeExactly16Characters()
        {
            this.BadValue("wd_636734262526");
            this.BadValue("wd_63673426252678");
        }

        [TestMethod]
        public void ItShouldNotAllowAnyDeviationOnPrefix()
        {
            this.BadValue("Wd_6367342625267");
            this.BadValue("WD_6367342625267");
            this.BadValue("wd-6367342625267");
        }

        protected override MailboxName Parse(string value)
        {
            return MailboxName.Parse(value);
        }

        protected override bool TryParse(string value, out MailboxName parsedObject)
        {
            return MailboxName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out MailboxName parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return MailboxName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(MailboxName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}