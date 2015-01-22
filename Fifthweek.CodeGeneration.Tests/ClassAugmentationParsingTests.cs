namespace Fifthweek.CodeGeneration.Tests
{
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClassAugmentationParsingTests : DataTransferObjectTests<ClassAugmentationParsingDummy>
    {
        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = this.NewInstanceOfObjectA();

            Assert.IsNull(data.SomeParsedStringObject);
            Assert.IsNull(data.OptionalParsedStringObject);

            Assert.IsNull(data.SomeParsedIntObject);
            Assert.IsNull(data.OptionalParsedIntObject);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = this.NewInstanceOfObjectA();
            data.Parse();

            Assert.AreEqual<ParsedString>(data.SomeParsedStringObject, ParsedString.Parse(data.SomeParsedString));
            Assert.AreEqual<ParsedString>(data.OptionalParsedStringObject, ParsedString.Parse(data.OptionalParsedString));

            Assert.AreEqual<ParsedInt>(data.SomeParsedIntObject, ParsedInt.Parse(data.SomeParsedInt));
            Assert.AreEqual<ParsedInt>(data.OptionalParsedIntObject, ParsedInt.Parse(data.OptionalParsedInt));
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
        public void WhenParsingCustomPrimitives_ItShouldAllowNullForOptionalNonNullableObjects()
        {
            this.GoodValue(
                _ => _.OptionalParsedString = null,
                _ => _.OptionalParsedStringObject == null);
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
                SomeParsedString = "Captain Phil",
                OptionalParsedString = "Captain Phil",
                SomeParsedInt = 123, 
                OptionalParsedInt = 123 
            };
        }
    }
}