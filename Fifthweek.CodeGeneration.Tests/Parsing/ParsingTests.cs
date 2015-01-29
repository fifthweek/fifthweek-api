namespace Fifthweek.CodeGeneration.Tests.Parsing
{
    using System;

    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        public void ItShouldParseCustomPrimitiveValueTypes()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            Assert.AreEqual(parsed.SomeParsedInt, ParsedInt.Parse(data.SomeParsedInt));
            Assert.AreEqual(parsed.OptionalParsedInt, ParsedInt.Parse(data.OptionalParsedInt.Value));
        }

        [TestMethod]
        public void ItShouldParseCustomPrimitiveReferenceTypes()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            Assert.AreEqual(parsed.SomeParsedString, ParsedString.Parse(data.SomeParsedString));
            Assert.AreEqual(parsed.OptionalParsedString, ParsedString.Parse(data.OptionalParsedString));

            Assert.AreEqual(
                parsed.SomeParsedNormalizedString, 
                ParsedNormalizedString.Parse(ParsedNormalizedString.Normalize(data.SomeParsedNormalizedString)));
            Assert.AreEqual(
                parsed.OptionalParsedNormalizedString, 
                ParsedNormalizedString.Parse(ParsedNormalizedString.Normalize(data.OptionalParsedNormalizedString)));
        }

        [TestMethod]
        public void ItShouldRetainNonParsedValueTypes()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            Assert.AreEqual(parsed.SomeWeaklyTypedInt, data.SomeWeaklyTypedInt);
            Assert.AreEqual(parsed.OptionalWeaklyTypedInt, data.OptionalWeaklyTypedInt);
        }

        [TestMethod]
        public void ItShouldRetainNonParsedReferenceTypes()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            Assert.AreEqual(parsed.SomeWeaklyTypedString, data.SomeWeaklyTypedString);
            Assert.AreEqual(parsed.OptionalWeaklyTypedString, data.OptionalWeaklyTypedString);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
        {
            this.BadValue(_ => _.SomeParsedString = "!");
            this.BadValue(_ => _.OptionalParsedString = "!");
            this.BadValue(_ => _.SomeParsedInt = -1);
            this.BadValue(_ => _.OptionalParsedInt = -1);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForOptionalObjects()
        {
            this.GoodValue(_ => _.OptionalParsedInt = null, _ => _.OptionalParsedInt == null);
            this.GoodValue(_ => _.OptionalParsedString = null, _ => _.OptionalParsedString == null);
            this.GoodValue(_ => _.OptionalWeaklyTypedInt = null, _ => _.OptionalWeaklyTypedInt == null);
            this.GoodValue(_ => _.OptionalWeaklyTypedString = null, _ => _.OptionalWeaklyTypedString == null);
        }

        public void BadValue(Action<AutoCounterpart> setInvalidFieldValue)
        {
            try
            {
                var data = this.NewInstance();
                setInvalidFieldValue(data);
                data.Parse();
                Assert.Fail("Expected model validation exception");
            }
            catch (ModelValidationException)
            {
            }
        }

        public void GoodValue(Action<AutoCounterpart> setValidFieldValue, Predicate<AutoCounterpart.Parsed> isValid)
        {
            var data = this.NewInstance();
            setValidFieldValue(data);
            var parsed = data.Parse();
            Assert.IsTrue(isValid(parsed));
        }

        protected AutoCounterpart NewInstance()
        {
            return new AutoCounterpart
            {
                SomeWeaklyTypedInt = 1,
                OptionalWeaklyTypedInt = 2,
                SomeWeaklyTypedString = "Captain Phil 1",
                OptionalWeaklyTypedString = "Captain Phil 2",
                SomeParsedString = "Captain Phil 3",
                OptionalParsedString = "Captain Phil 4",
                SomeParsedNormalizedString = "Captain Phil 5",
                OptionalParsedNormalizedString = "Captain Phil 6",
                SomeParsedInt = 3, 
                OptionalParsedInt = 4 
            };
        }
    }
}