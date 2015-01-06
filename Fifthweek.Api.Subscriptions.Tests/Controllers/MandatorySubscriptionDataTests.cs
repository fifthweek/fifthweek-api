using System;
using Fifthweek.Api.Subscriptions.Controllers;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests.Controllers
{
    [TestClass]
    public class MandatorySubscriptionDataTests : DataTransferObjectTests<MandatorySubscriptionData>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentSubscriptionId()
        {
            this.AssertDifference(_ => _.SubscriptionId = Guid.NewGuid());
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentSubscriptionName()
        {
            this.AssertDifference(_ => _.SubscriptionName = "different");
            this.AssertDifference(_ => _.SubscriptionName = null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentTagline()
        {
            this.AssertDifference(_ => _.Tagline = "different");
            this.AssertDifference(_ => _.Tagline = null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentBasePrice()
        {
            this.AssertDifference(_ => _.BasePrice = 12345);
        }

        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = this.NewInstanceOfObjectA();

            Assert.IsNull(data.BasePriceObject);
            Assert.IsNull(data.SubscriptionIdObject);
            Assert.IsNull(data.SubscriptionNameObject);
            Assert.IsNull(data.TaglineObject);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = this.NewInstanceOfObjectA();
            data.Parse();

            Assert.AreEqual(data.BasePriceObject, ChannelPriceInUsCentsPerWeek.Parse(data.BasePrice));
            Assert.AreEqual(data.SubscriptionIdObject, SubscriptionId.Parse(data.SubscriptionId));
            Assert.AreEqual(data.SubscriptionNameObject, SubscriptionName.Parse(data.SubscriptionName));
            Assert.AreEqual(data.TaglineObject, Tagline.Parse(data.Tagline));
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
        {
            this.BadValue(_ => _.SubscriptionName = SubscriptionNameTests.InvalidValue);
            this.BadValue(_ => _.Tagline = TaglineTests.InvalidValue);
            this.BadValue(_ => _.BasePrice = ChannelPriceInUsCentsPerWeekTests.InvalidValue);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfAnyAreNull()
        {
            this.BadValue(_ => _.SubscriptionName = null);
            this.BadValue(_ => _.Tagline = null);
        }

        protected override MandatorySubscriptionData NewInstanceOfObjectA()
        {
            return new MandatorySubscriptionData
            {
                SubscriptionId = Guid.Parse("{3224E372-A9B2-4225-9E6F-0A27C6AEF169}"),
                SubscriptionName = "Lawrence",
                Tagline = "Web Comics and More",
                BasePrice = 50
            };
        }

        protected override void Parse(MandatorySubscriptionData obj)
        {
            obj.Parse();
        }
    }
}