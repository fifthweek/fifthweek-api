using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    [TestClass]
    public class ClassAugmentationParsingTests : DataTransferObjectTests<ClassAugmentation.ClassAugmentationParsingDummy>
    {
        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = this.NewInstanceOfObjectA();

            Assert.IsNull(data.SomeConstructedNullableStringObject);
            Assert.IsNull(data.OptionalConstructedNullableStringObject);

            Assert.IsNull(data.SomeConstructedNonNullableStringObject);
            Assert.IsNull(data.OptionalConstructedNonNullableStringObject);

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

            Assert.AreEqual(data.SomeConstructedNullableStringObject, new ConstructedNullableString(data.SomeConstructedNullableString));
            Assert.AreEqual(data.OptionalConstructedNullableStringObject, new ConstructedNullableString(data.OptionalConstructedNullableString));

            Assert.AreEqual(data.SomeConstructedNonNullableStringObject, new ConstructedNonNullableString(data.SomeConstructedNonNullableString));
            Assert.AreEqual(data.OptionalConstructedNonNullableStringObject, new ConstructedNonNullableString(data.OptionalConstructedNonNullableString));

            Assert.AreEqual(data.SomeParsedStringObject, ParsedString.Parse(data.SomeParsedString));
            Assert.AreEqual(data.OptionalParsedStringObject, ParsedString.Parse(data.OptionalParsedString));

            Assert.AreEqual(data.SomeConstructedIntObject, new ConstructedInt(data.SomeConstructedInt));

            Assert.AreEqual(data.SomeParsedIntObject, ParsedInt.Parse(data.SomeParsedInt));
            Assert.AreEqual(data.OptionalParsedIntObject, ParsedInt.Parse(data.OptionalParsedInt));
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
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForNullableRequiredObjects()
        {
            this.GoodValue(
                _ => _.SomeConstructedNullableString = null,
                _ => _.SomeConstructedNullableStringObject.Value == null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowTreatNullAsValueSubmissionForOptionalObjects()
        {
            this.GoodValue(
                _ => _.OptionalConstructedNullableString = null,
                _ => _.OptionalConstructedNullableStringObject.Value == null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldNotAllowNullForRequiredParsedObject()
        {
            this.BadValue(_ => _.SomeConstructedNonNullableString = null);
            this.BadValue(_ => _.SomeParsedString = null);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForOptionalNonNullableObjects()
        {
            this.GoodValue(
                _ => _.OptionalConstructedNonNullableString = null,
                _ => _.OptionalConstructedNonNullableStringObject == null);

            this.GoodValue(
                _ => _.OptionalParsedString = null,
                _ => _.OptionalParsedStringObject == null);
        }

        protected override void Parse(ClassAugmentation.ClassAugmentationParsingDummy obj)
        {
            obj.Parse();
        }

        protected override ClassAugmentation.ClassAugmentationParsingDummy NewInstanceOfObjectA()
        {
            return new ClassAugmentation.ClassAugmentationParsingDummy
            {
                NotStrongTyped = 123,
                SomeConstructedNullableString = "Captain Phil",
                OptionalConstructedNullableString = "Captain Phil",
                SomeConstructedNonNullableString = "Captain Phil",
                OptionalConstructedNonNullableString = "Captain Phil",
                SomeParsedString = "Captain Phil",
                OptionalParsedString = "Captain Phil",
                SomeConstructedInt = 123,
                SomeParsedInt = 123, 
                OptionalParsedInt = 123 
            };
        }
    }
}