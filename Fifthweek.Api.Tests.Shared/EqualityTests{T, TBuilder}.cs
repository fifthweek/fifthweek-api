using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    public abstract class EqualityTests<T, TBuilder> : EqualityTests<T>
    {
        public void AssertDifference(Action<TBuilder> applyDifference)
        {
            var objectAClone = this.NewInstanceOfBuilderForObjectA();
            applyDifference(objectAClone);
            var objectB = this.FromBuilder(objectAClone);

            Assert.AreNotEqual(this.ObjectA, objectB);
        }

        protected abstract TBuilder NewInstanceOfBuilderForObjectA();

        protected abstract T FromBuilder(TBuilder builder);

        protected override T NewInstanceOfObjectA()
        {
            return this.FromBuilder(this.NewInstanceOfBuilderForObjectA());
        }
    }
}