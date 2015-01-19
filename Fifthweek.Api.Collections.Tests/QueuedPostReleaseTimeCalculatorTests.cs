namespace Fifthweek.Api.Collections.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueuedPostReleaseTimeCalculatorTests
    {
        public QueuedPostReleaseTimeCalculator target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new QueuedPostReleaseTimeCalculator();
        }
    }
}