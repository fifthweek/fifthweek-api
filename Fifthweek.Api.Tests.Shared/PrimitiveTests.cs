using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    public abstract class PrimitiveTests<T> : EqualityTests<T>
    {
        public override void TestEquality()
        {
            base.TestEquality();
            this.ItShouldRecogniseDifferentObjects();
        }

        public virtual void ItShouldRecogniseDifferentObjects()
        {
            Assert.AreNotEqual(this.ObjectA, this.ObjectB);
        }
        protected abstract T NewInstanceOfObjectB();

        protected T ObjectB
        {
            get { return this.NewInstanceOfObjectB(); }
        }
    }
}