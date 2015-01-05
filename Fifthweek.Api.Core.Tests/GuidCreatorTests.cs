namespace Fifthweek.Api.Core.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GuidCreatorTests
    {
        [TestMethod]
        public void ShouldBeSequentialAndNotEqual()
        {
            var guidCreator = new GuidCreator();
            Guid lastGuid = Guid.Empty;

            for (int i = 0; i < 1000000; i++)
            {
                var guid = guidCreator.CreateClrSequential();
                Assert.IsTrue(guid.CompareTo(lastGuid) > 0);
                lastGuid = guid;
            }
        }
    }
}