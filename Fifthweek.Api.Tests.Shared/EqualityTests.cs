using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    public abstract class EqualityTests<T>
    {
        public virtual void TestEquality()
        {
            this.ItShouldRecogniseEqualObjects();
            this.ItShouldRecogniseNullAsDifferent();
            this.ItShouldRecogniseDifferentObjects();
        }

        public virtual void ItShouldRecogniseEqualObjects()
        {
            Assert.AreEqual(this.ObjectA, this.ObjectA);
        }

        public virtual void ItShouldRecogniseNullAsDifferent()
        {
            Assert.AreNotEqual(this.ObjectA, null);
        }

        public virtual void ItShouldRecogniseDifferentObjects()
        {
            Assert.AreNotEqual(this.ObjectA, this.ObjectB);
        }

        protected abstract T ObjectA { get; }

        protected abstract T ObjectB { get; }
    }
}