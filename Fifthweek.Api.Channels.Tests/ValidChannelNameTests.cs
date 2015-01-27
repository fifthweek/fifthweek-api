namespace Fifthweek.Api.Channels.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidChannelNameTests : ValidatedStringTests<ValidChannelName>
    {
        public static readonly string InvalidValue = string.Empty;

        protected override string ValueA
        {
            get { return "Lawrence"; }
        }

        protected override string ValueB
        {
            get { return "James"; }
        }

        [TestMethod]
        public void ItShouldAllowBasicChannelNames()
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
        public void ItShouldAllowPunctuation()
        {
            this.AssertPunctuationAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowEmptyChannelNames()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowChannelNamesOver25Characters()
        {
            this.AssertMaxLength(25);
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.AssertTabsNotAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.AssertNewLinesNotAllowed();
        }

        protected override ValidChannelName Parse(string value, bool exact)
        {
            return ValidChannelName.Parse(value);
        }

        protected override bool TryParse(string value, out ValidChannelName parsedObject, bool exact)
        {
            return ValidChannelName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidChannelName parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidChannelName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidChannelName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}