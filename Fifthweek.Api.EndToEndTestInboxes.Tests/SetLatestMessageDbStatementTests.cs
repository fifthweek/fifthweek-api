namespace Fifthweek.Api.EndToEndTestMailboxes.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SetLatestMessageDbStatementTests : PersistenceTestsBase
    {
        private const string Subject = "Meow";
        private const string Body = "Paw-some";

        private static readonly MailboxName MailboxName = MailboxName.Parse("wd_1234567890123");

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private SetLatestMessageDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new SetLatestMessageDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireMailboxName()
        {
            await this.target.ExecuteAsync(null, Subject, Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireSubject()
        {
            await this.target.ExecuteAsync(MailboxName, null, Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireBody()
        {
            await this.target.ExecuteAsync(MailboxName, Subject, null);
        }

        [TestMethod]
        public async Task ItShouldInsertIfMailboxContainsNoMessages()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(MailboxName, Subject, Body);

                var expected = new EndToEndTestEmail(MailboxName.Value, Subject, Body, default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<EndToEndTestEmail>(expected)
                    {
                        Expected = actual =>
                        {
                            expected.DateReceived = actual.DateReceived;
                            return expected;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateIfMailboxAlreadyContainsMessage()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);

                using (var connection = testDatabase.CreateConnection())
                {
                    await connection.CreateEndToEndEmailAsync(MailboxName.Value);
                }

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(MailboxName, Subject, Body);

                var expected = new EndToEndTestEmail(MailboxName.Value, Subject, Body, default(DateTime));

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<EndToEndTestEmail>(expected)
                    {
                        Expected = actual =>
                        {
                            expected.DateReceived = actual.DateReceived;
                            return expected;
                        }
                    }
                };
            });
        }
    }
}