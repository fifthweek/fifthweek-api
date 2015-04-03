namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.ComponentModel;
    using System.Data.Entity.Core;
    using System.Data.SqlClient;
    using System.IO;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using OptimisticConcurrencyException = System.Data.Entity.Core.OptimisticConcurrencyException;

    [TestClass]
    public class TransientErrorDetectionStrategyTests
    {
        private FifthweekTransientErrorDetectionStrategy target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new FifthweekTransientErrorDetectionStrategy();
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlDeadlock()
        {
            Assert.IsTrue(this.target.IsTransient(SqlExceptionMocker.MakeSqlException(RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode)));
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlTimeout()
        {
            Assert.IsTrue(this.target.IsTransient(SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode)));
        }

        [TestMethod]
        public async Task ItShouldRetryWhenEntityFrameworkThinksThereIsATransientFailure()
        {
            Assert.IsTrue(this.target.IsTransient(new EntityException("An exception has been raised that is likely due to a transient failure. If you are connecting to a SQL Azure database consider using SqlAzureExecutionStrategy.")));
        }

        [TestMethod]
        public async Task ItShouldRetryOnTimeout()
        {
            Assert.IsTrue(this.target.IsTransient(new TimeoutException("A timeout occurred")));
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlAzureTransientError()
        {
            Assert.IsTrue(this.target.IsTransient(SqlExceptionMocker.MakeSqlException(
                            40501)));
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlAzureTransientError2()
        {
            Assert.IsTrue(this.target.IsTransient(SqlExceptionMocker.MakeSqlException(
                            40143)));
        }

        [TestMethod]
        public async Task ItShouldRetryOnAzureStorageTransientError()
        {
            Assert.IsTrue(this.target.IsTransient(new IOException()));
        }

        [TestMethod]
        public async Task ItShouldRetryOnWin32ExceptionsContainingTimeOutTransientError()
        {
            Assert.IsTrue(this.target.IsTransient(new Win32Exception("The wait operation timed out")));
        }

        [TestMethod]
        public async Task ItShouldRetryOnOptimisticConcurrencyExceptions()
        {
            Assert.IsTrue(this.target.IsTransient(new OptimisticConcurrencyException()));
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedSqlError()
        {
            Assert.IsTrue(this.target.IsTransient(new InvalidOperationException(
                            "blah",
                            new DivideByZeroException(
                                "blah",
                                SqlExceptionMocker.MakeSqlException(
                                    RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode)))));
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedAggregateSqlError()
        {
            Assert.IsTrue(this.target.IsTransient(new AggregateException(
                            "blah",
                            new DivideByZeroException(
                                "blah",
                                new Exception("Dead end")),
                            new DivideByZeroException(
                                "blah",
                                new AggregateException(
                                    "blah",
                                    new ExternalErrorException("blah"),
                                    new Exception(
                                        "blah",
                                        SqlExceptionMocker.MakeSqlException(RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode)),
                                    new EntryPointNotFoundException("blah"))))));
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnStandardExceptions()
        {
            Assert.IsFalse(this.target.IsTransient(new DivideByZeroException()));
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnUnsupportedSqlExceptionErrorNumbers()
        {
            Assert.IsFalse(this.target.IsTransient(SqlExceptionMocker.MakeSqlException(0)));
        }
    }
}