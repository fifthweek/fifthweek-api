namespace Fifthweek.Shared.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeUtilsTests
    {
        [TestMethod]
        public void ItShouldSaveToIsoStringAndParseIsoString()
        {
            var dt = DateTime.UtcNow;
            var output = dt.ToIso8601String();

            var result = output.FromIso8601String();

            Assert.AreEqual(dt, result);
            Assert.IsTrue(result.Kind == DateTimeKind.Utc);
        }
    }
}