namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AggregateCostPeriodsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private AggregateCostPeriodsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new AggregateCostPeriodsExecutor();
        }

        [TestMethod]
        public void WhenNoCostPeriods_ItShouldReturnZeroCost()
        {
            var result = this.Execute();
            Assert.AreEqual(0, result.Cost);
        }

        [TestMethod]
        public void ItShouldReturnCost1()
        {
            var result = this.Execute(new CostPeriod(Now, Now.AddDays(7), 100));
            Assert.AreEqual(100, result.Cost);
        }

        [TestMethod]
        public void ItShouldReturnCost2()
        {
            var result = this.Execute(new CostPeriod(Now, Now.AddDays(14), 50));
            Assert.AreEqual(100, result.Cost);
        }

        [TestMethod]
        public void ItShouldReturnCost3()
        {
            var result = this.Execute(new CostPeriod(Now, Now.AddDays(1), 700));
            Assert.AreEqual(100, Math.Round(result.Cost));
        }

        [TestMethod]
        public void ItShouldReturnCost4()
        {
            var result = this.Execute(
                new CostPeriod(Now, Now.AddDays(7), 100), 
                new CostPeriod(Now, Now.AddDays(14), 50),
                new CostPeriod(Now, Now.AddDays(1), 700),
                new CostPeriod(Now, Now.AddDays(0.5), 700));

            Assert.AreEqual(350, Math.Round(result.Cost));
        }

        private static IReadOnlyList<CostPeriod> Create(params CostPeriod[] periods)
        {
            return periods.ToList();
        }

        private AggregateCostSummary Execute(params CostPeriod[] periods)
        {
            return this.target.Execute(Create(periods));
        }
    }
}