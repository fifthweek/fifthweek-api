using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Core.Tests
{
    [TestClass]
    public class ClassAugmentationParsingTests : DataTransferObjectTests<ClassAugmentationParsingDummy>
    {
        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = this.NewInstanceOfObjectA();

            Assert.IsNull(data.SomeConstructedStringObject);
            Assert.IsNull(data.OptionalConstructedStringObject);

            Assert.IsNull(data.SomeParsedStringObject);
            Assert.IsNull(data.OptionalParsedStringObject);

            Assert.IsNull(data.SomeConstructedIntObject);

            Assert.IsNull(data.SomeParsedIntObject);
            Assert.IsNull(data.OptionalParsedIntObject);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = this.NewInstanceOfObjectA();
            data.Parse();

            Assert.AreEqual(data.SomeConstructedStringObject, new ClassAugmentationParsingDummy.ConstructedString(data.SomeConstructedString));
            Assert.AreEqual(data.OptionalConstructedStringObject, new ClassAugmentationParsingDummy.ConstructedString(data.OptionalConstructedString));

            Assert.AreEqual(data.SomeParsedStringObject, ClassAugmentationParsingDummy.ParsedString.Parse(data.SomeParsedString));
            Assert.AreEqual(data.OptionalParsedStringObject, ClassAugmentationParsingDummy.ParsedString.Parse(data.OptionalParsedString));

            Assert.AreEqual(data.SomeConstructedIntObject, new ClassAugmentationParsingDummy.ConstructedInt(data.SomeConstructedInt));

            Assert.AreEqual(data.SomeParsedIntObject, ClassAugmentationParsingDummy.ParsedInt.Parse(data.SomeParsedInt));
            Assert.AreEqual(data.OptionalParsedIntObject, ClassAugmentationParsingDummy.ParsedInt.Parse(data.OptionalParsedInt));
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
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForConstructedObjects()
        {
            this.GoodValue(_ => _.SomeConstructedString = null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForOptionalProperties()
        {
            this.GoodValue(_ => _.OptionalConstructedString = null);
            this.GoodValue(_ => _.OptionalParsedString = null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseValidationExceptionIfRequiredParsedObjectsAreNull()
        {
            this.BadValue(_ => _.SomeParsedString = null);
        }

        protected override void Parse(ClassAugmentationParsingDummy obj)
        {
            obj.Parse();
        }

        protected override ClassAugmentationParsingDummy NewInstanceOfObjectA()
        {
            return new ClassAugmentationParsingDummy
            {
                NotStrongTyped = 123,
                SomeConstructedString = "valid: !null",
                OptionalConstructedString = "value: true",
                SomeParsedString = "valid: !empty && custom_rules",
                OptionalParsedString = "valid: empty || custom_rules",
                SomeConstructedInt = 123, // valid: true
                SomeParsedInt = 123, // valid: !empty && custom_rules
                OptionalParsedInt = 123 // valid: empty || custom_rules
            };
        }
    }
}