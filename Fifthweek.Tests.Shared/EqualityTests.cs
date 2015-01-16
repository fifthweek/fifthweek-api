namespace Fifthweek.Tests.Shared
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class EqualityTests<T>
    {
        public virtual void TestEquality()
        {
            this.ItShouldRecogniseEqualObjects();
            this.ItShouldRecogniseNullAsDifferent();
        }

        public virtual void ItShouldRecogniseEqualObjects()
        {
            Assert.AreEqual(this.ObjectA, this.ObjectA);
        }

        public virtual void ItShouldRecogniseNullAsDifferent()
        {
            Assert.AreNotEqual(this.ObjectA, null);
        }

        public void AssertDifference(Action<T> applyDifference)
        {
            var data1 = this.NewInstanceOfObjectA();
            var data2 = this.NewInstanceOfObjectA();
            applyDifference(data2);

            Assert.AreNotEqual(data1, data2);
        }

        protected abstract T NewInstanceOfObjectA();

        protected T ObjectA
        {
            get { return this.NewInstanceOfObjectA(); }
        }
    }
}