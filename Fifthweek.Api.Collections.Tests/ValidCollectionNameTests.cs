namespace Fifthweek.Api.Collections.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ValidCollectionName = Fifthweek.Api.Collections.Shared.ValidCollectionName;

    [TestClass]
    public class ValidCollectionNameTests : ValidatedStringTests<ValidCollectionName>
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
        public void ItShouldAllowBasicCollectionNames()
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
        public void ItShouldNotAllowEmptyCollectionNames()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowCollectionNamesOver25Characters()
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

        protected override ValidCollectionName Parse(string value, bool exact)
        {
            return ValidCollectionName.Parse(value);
        }

        protected override bool TryParse(string value, out ValidCollectionName parsedObject, bool exact)
        {
            return ValidCollectionName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidCollectionName parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidCollectionName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidCollectionName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}