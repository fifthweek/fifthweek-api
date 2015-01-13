namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaglineTests : ValidatedStringTests<ValidTagline>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "Web Comics and More"; }
        }

        protected override string ValueB
        {
            get { return "Web Comics and Much, Much More"; }
        }

        [TestMethod]
        public void ItShouldAllowBasicTaglines()
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
        public void ItShouldNotAllowTaglinesUnder5Characters()
        {
            this.AssertMinLength(5);
        }

        [TestMethod]
        public void ItShouldNotAllowTaglinesOver55Characters()
        {
            this.AssertMaxLength(55);
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

        protected override ValidTagline Parse(string value, bool exact)
        {
            return ValidTagline.Parse(value);
        }

        protected override bool TryParse(string value, out ValidTagline parsedObject, bool exact)
        {
            return ValidTagline.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidTagline parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidTagline.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidTagline parsedObject)
        {
            return parsedObject.Value;
        }
    }
}