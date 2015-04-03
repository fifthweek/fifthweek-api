namespace Fifthweek.Api.Core.Tests
{
    using System;

    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void WhenAssertNotNullCalled_ItShouldThrowAnArgumentNullExceptionIfTheTargetIsNull()
        {
            Exception exception = null;
            try
            {
                ((object)null).AssertNotNull("test");
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
            Assert.AreEqual(((ArgumentNullException)exception).ParamName, "test");
        }

        [TestMethod]
        public void WhenAssertNotNullCalled_ItShouldReturnIfTheTargetIsNotNull()
        {
            Exception exception = null;
            try
            {
                new object().AssertNotNull("test");
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }
    }
}