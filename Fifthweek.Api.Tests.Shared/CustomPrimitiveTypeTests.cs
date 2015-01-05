using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    [TestClass]
    public abstract class CustomPrimitiveTypeTests<TParsed, TRaw>
    {
        public void TestEquality()
        {
            this.ItShouldRecogniseEqualObjects();
            this.ItShouldRecogniseDifferentObjects();
        }

        public void ItShouldRecogniseEqualObjects()
        {
            var value1 = this.Parse(this.ValueA);
            var value2 = this.Parse(this.ValueA);
            
            Assert.AreEqual(value1, value2);
        }

        public void ItShouldRecogniseDifferentObjects()
        {
            var value1 = this.Parse(this.ValueA);
            var value2 = this.Parse(this.ValueB);

            Assert.AreNotEqual(value1, value2);
        }

        protected abstract TRaw ValueA { get; }

        protected abstract TRaw ValueB { get; }

        protected abstract TParsed Parse(TRaw value);
    }
}