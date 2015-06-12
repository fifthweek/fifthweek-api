using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Payments.Tests.Pipeline
{
    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BillingWeekUtilitiesTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        [TestMethod]
        public void ItShouldCalculateBillingWeekEndDate1()
        {
            var result = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(3));
            Assert.AreEqual(Now.AddDays(7), result);
        }

        [TestMethod]
        public void ItShouldCalculateBillingWeekEndDate2()
        {
            var result = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7).AddTicks(-1));
            Assert.AreEqual(Now.AddDays(7), result);
        }

        [TestMethod]
        public void ItShouldCalculateBillingWeekEndDate3()
        {
            var result = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7));
            Assert.AreEqual(Now.AddDays(14), result);
        }

        [TestMethod]
        public void ItShouldCalculateBillingWeekEndDate4()
        {
            var result = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7).AddTicks(1));
            Assert.AreEqual(Now.AddDays(14), result);
        }

        [TestMethod]
        public void ItShouldCalculateBillingWeekEndDate5()
        {
            var result = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(29).AddTicks(1));
            Assert.AreEqual(Now.AddDays(35), result);
        }
    }
}
