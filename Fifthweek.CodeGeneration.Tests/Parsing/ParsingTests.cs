namespace Fifthweek.CodeGeneration.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                ParsedNormalizedString.Parse(data.SomeParsedNormalizedString));
            Assert.AreEqual(
                parsed.OptionalParsedNormalizedString, 
                ParsedNormalizedString.Parse(data.OptionalParsedNormalizedString));
        }

        [TestMethod]
        public void ItShouldParseListsWithCustomElementType()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            CollectionAssert.AreEqual(parsed.SomeParsedIntList.Select(_ => _.Value).ToList(), data.SomeParsedIntList);
            CollectionAssert.AreEqual(parsed.OptionalParsedIntList.Select(_ => _.Value).ToList(), data.OptionalParsedIntList);
        }

        [TestMethod]
        public void ItShouldParseListsWithCustomElementTypeAndOuterType()
        {
            var data = this.NewInstance();
            var parsed = data.Parse();

            CollectionAssert.AreEqual(parsed.SomeParsedCollection.Value.ToList(), data.SomeParsedCollection);
            CollectionAssert.AreEqual(parsed.OptionalParsedCollection.Value.ToList(), data.OptionalParsedCollection);

            CollectionAssert.AreEqual(parsed.SomeParsedMappedCollection.Value.Select(_ => _.Value).ToList(), data.SomeParsedMappedCollection);
            CollectionAssert.AreEqual(parsed.OptionalParsedMappedCollection.Value.Select(_ => _.Value).ToList(), data.OptionalParsedMappedCollection);
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
        public void WhenParsingCustomPrimitives_ItShouldRequireNonOptionalObjects()
        {
            this.BadValue(_ => _.SomeParsedCollection = null);
            this.BadValue(_ => _.SomeParsedMappedCollection = null);
            this.BadValue(_ => _.SomeParsedIntList = null);
            this.BadValue(_ => _.SomeParsedNormalizedString = null);
            this.BadValue(_ => _.SomeParsedString = null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldInspectCustomEmptyChecks()
        {
            this.BadValue(_ => _.SomeParsedNormalizedString = "   ");
            this.BadValue(_ => _.SomeParsedCollection = new List<string>() { "Just One Cornetto!" });
            this.BadValue(_ => _.SomeParsedMappedCollection = new List<string>() { "Just One Cornetto!" });
            this.BadValue(_ => _.SomeParsedMappedCollection = new List<string>() { "   " });
            this.BadValue(_ => _.SomeParsedMappedCollection = new List<string>() { "Just One Cornetto!", "   " });
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForOptionalObjects()
        {
            this.GoodValue(_ => _.OptionalParsedInt = null, _ => _.OptionalParsedInt == null);
            this.GoodValue(_ => _.OptionalParsedString = null, _ => _.OptionalParsedString == null);
            this.GoodValue(_ => _.OptionalWeaklyTypedInt = null, _ => _.OptionalWeaklyTypedInt == null);
            this.GoodValue(_ => _.OptionalWeaklyTypedString = null, _ => _.OptionalWeaklyTypedString == null);
            this.GoodValue(_ => _.OptionalParsedIntList = null, _ => _.OptionalParsedIntList == null);
            this.GoodValue(_ => _.OptionalParsedCollection = null, _ => _.OptionalParsedCollection == null);
            this.GoodValue(_ => _.OptionalParsedMappedCollection = null, _ => _.OptionalParsedMappedCollection == null);
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
                OptionalParsedInt = 4,
                SomeParsedIntList = new List<int>() { 1, 2, 3 },
                OptionalParsedIntList = new List<int>() { 1, 2, 3 },
                SomeParsedCollection = new List<string>() { "Hello", "Phil" },
                OptionalParsedCollection = new List<string>() { "Hello!", "Phil!" },
                SomeParsedMappedCollection = new List<string>() { "Hello!!", "Phil!!" },
                OptionalParsedMappedCollection = new List<string>() { "Hello!!!", "Phil!!!" },
            };
        }
    }
}