using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Core.Tests
{
    [TestClass]
    public class ClassAugmentationParsingTests : DataTransferObjectTests<ClassAugmentationParsingDummy>
    {
//        [TestMethod]
//        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
//        {
//            var data = this.NewInstanceOfObjectA();
//
//            Assert.IsNull(data.SomeUnconditionalStringObj);
//            Assert.IsNull(data.OptionalUnconditionalStringObj);
//            Assert.IsNull(data.SomeConditionalStringObj);
//            Assert.IsNull(data.OptionalConditionalStringObj);
//            Assert.IsNull(data.SomeUnconditionalIntObj);
//            Assert.IsNull(data.SomeConditionalIntObj);
//            Assert.IsNull(data.OptionalConditionalIntObj);
//        }
//
//        [TestMethod]
//        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
//        {
//            var data = this.NewInstanceOfObjectA();
//            data.Parse();
//
//            Assert.AreEqual(data.EmailObj, Email.Parse(data.Email));
//            Assert.AreEqual(data.PasswordObj, Password.Parse(data.Password));
//            Assert.AreEqual(data.UsernameObj, Username.Parse(data.Username));
//        }
//
//        [TestMethod]
//        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
//        {
//            this.BadValue(_ => _.Email = EmailTests.InvalidValue);
//            this.BadValue(_ => _.Password = PasswordTests.InvalidValue);
//            this.BadValue(_ => _.Username = UsernameTests.InvalidValue);
//        }
//
//        [TestMethod]
//        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfAnyAreNull()
//        {
//            this.BadValue(_ => _.Email = null);
//            this.BadValue(_ => _.Password = null);
//            this.BadValue(_ => _.Username = null);
//        }

        protected override void Parse(ClassAugmentationParsingDummy obj)
        {
            obj.Parse();
        }

        protected override ClassAugmentationParsingDummy NewInstanceOfObjectA()
        {
            return new ClassAugmentationParsingDummy
            {
                NotStrongTyped = 123,
                SomeUnconditionalString = "valid: !null",
                OptionalUnconditionalString = "value: true",
                SomeConditionalString = "valid: !empty && custom_rules",
                OptionalConditionalString = "valid: empty || custom_rules",
                SomeUnconditionalInt = 123, // valid: true
                SomeConditionalInt = 123, // valid: !empty && custom_rules
                OptionalConditionalInt = 123 // valid: empty || custom_rules
            };
        }
    }
}