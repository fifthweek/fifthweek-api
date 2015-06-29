namespace Fifthweek.Api.Availability.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AvailabilityResultTests
    {
        [TestMethod]
        public void WhenAllContentIsTrue_IsOkShouldReturnTrue()
        {
            var item = new AvailabilityResult(true, true, true);
            Assert.IsTrue(item.IsOk());
        }

        [TestMethod]
        public void WhenAnyContentIsFalse_IsOkShouldReturnFalse()
        {
            var item = new AvailabilityResult(false, true, true);
            Assert.IsFalse(item.IsOk());

            item = new AvailabilityResult(true, false, true);
            Assert.IsFalse(item.IsOk());

            item = new AvailabilityResult(true, true, false);
            Assert.IsFalse(item.IsOk());

            item = new AvailabilityResult(false, false, false);
            Assert.IsFalse(item.IsOk());
        }
    }
}