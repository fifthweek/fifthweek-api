using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
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
            var data1 = NewInstanceOfObjectA();
            var data2 = NewInstanceOfObjectA();
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