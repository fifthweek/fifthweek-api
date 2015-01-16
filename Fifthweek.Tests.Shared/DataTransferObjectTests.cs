namespace Fifthweek.Tests.Shared
{
    using System;

    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class DataTransferObjectTests<T> : EqualityTests<T>
    {
        public void BadValue(Action<T> setInvalidFieldValue)
        {
            try
            {
                var data = this.NewInstanceOfObjectA();
                setInvalidFieldValue(data);
                this.Parse(data);
                Assert.Fail("Expected model validation exception");
            }
            catch (ModelValidationException)
            {
            }
        }

        public void GoodValue(Action<T> setValidFieldValue, Func<T, bool> assert = null)
        {
            var data = this.NewInstanceOfObjectA();
            setValidFieldValue(data);
            this.Parse(data);

            if (assert != null)
            {
                Assert.IsTrue(assert(data));
            }
        }

        protected abstract void Parse(T obj);
    }
}