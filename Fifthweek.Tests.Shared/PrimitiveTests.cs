namespace Fifthweek.Tests.Shared
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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