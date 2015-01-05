using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class TaglineTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var value1 = Tagline.Parse(this.stringA);
            var value2 = Tagline.Parse(this.stringA);

            Assert.AreEqual(value1, value2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var value1 = Tagline.Parse(this.stringA);
            var value2 = Tagline.Parse(this.stringB);

            Assert.AreNotEqual(value1, value2);
        }

        private readonly string stringA = "Tagline A";
        private readonly string stringB = "Tagline B";
    }
}