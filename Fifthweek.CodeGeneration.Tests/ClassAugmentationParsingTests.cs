namespace Fifthweek.CodeGeneration.Tests
{
    using System;

    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClassAugmentationParsingTests
    {
        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = this.NewInstanceOfObjectA();
            var parsed = data.Parse();

            Assert.AreEqual(parsed.SomeParsedString, ParsedString.Parse(data.SomeParsedString));
            Assert.AreEqual(parsed.OptionalParsedString, ParsedString.Parse(data.OptionalParsedString));

            Assert.AreEqual(parsed.SomeParsedInt, ParsedInt.Parse(data.SomeParsedInt));
            Assert.AreEqual(parsed.OptionalParsedInt, ParsedInt.Parse(data.OptionalParsedInt));
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
            var data = this.NewInstanceOfObjectA();
            data.OptionalParsedString = null;
            var parsed = data.Parse();
            Assert.IsNull(parsed.OptionalParsedString);
        }

        public void BadValue(Action<ClassAugmentationParsingDummy> setInvalidFieldValue)
        {
            try
            {
                var data = this.NewInstanceOfObjectA();
                setInvalidFieldValue(data);
                data.Parse();
                Assert.Fail("Expected model validation exception");
            }
            catch (ModelValidationException)
            {
            }
        }

        protected ClassAugmentationParsingDummy NewInstanceOfObjectA()
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