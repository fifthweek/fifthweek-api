namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IntroductionTests : ValidatedStringTests<Introduction>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "Hi, I'm Lawrence. Thinking of subscribing? Awesome!"; }
        }

        protected override string ValueB
        {
            get { return "Hi, I'm James. Thinking of subscribing? Awesome!"; }
        }

        [TestMethod]
        public void ItShouldAllowBasicIntroductions()
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
        public void ItShouldNotAllowIntroductionsUnder15Characters()
        {
            this.AssertMinLength(15);
        }

        [TestMethod]
        public void ItShouldNotAllowIntroductionsOver250Characters()
        {
            this.AssertMaxLength(250);
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

        protected override Introduction Parse(string value, bool exact)
        {
            return Introduction.Parse(value);
        }

        protected override bool TryParse(string value, out Introduction parsedObject, bool exact)
        {
            return Introduction.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out Introduction parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Introduction.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(Introduction parsedObject)
        {
            return parsedObject.Value;
        }
    }
}