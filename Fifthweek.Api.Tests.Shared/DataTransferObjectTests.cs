using System;
using Fifthweek.Api.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    public abstract class DataTransferObjectTests<T> : EqualityTests<T>
    {
        public void BadValue(Action<T> setInvalidFieldValue)
        {
            try
            {
                var data = NewInstanceOfObjectA();
                setInvalidFieldValue(data);
                this.Parse(data);
                Assert.Fail("Expected model validation exception");
            }
            catch (ModelValidationException)
            {
            }
        }

        public void GoodValue(Action<T> setValidFieldValue)
        {
            var data = NewInstanceOfObjectA();
            setValidFieldValue(data);
            this.Parse(data);
        }

        protected abstract void Parse(T obj);
    }
}