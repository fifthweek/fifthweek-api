namespace Fifthweek.Api.Collections.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        protected override ValidCollectionName Parse(string value)
        {
            return ValidCollectionName.Parse(value);
        }

        protected override bool TryParse(string value, out ValidCollectionName parsedObject)
        {
            return ValidCollectionName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidCollectionName parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidCollectionName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidCollectionName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}