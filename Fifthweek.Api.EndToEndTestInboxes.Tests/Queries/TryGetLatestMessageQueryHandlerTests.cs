namespace Fifthweek.Api.EndToEndTestMailboxes.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetLatestMessageQueryHandlerTests : PersistenceTestsBase
    {
        private const string Subject = "Meow";
        private const string Body = "Paw-some";

        private static readonly MailboxName MailboxName = MailboxName.Parse("wd_1234567890123");
        private static readonly TryGetLatestMessageQuery Query = new TryGetLatestMessageQuery(MailboxName);

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private TryGetLatestMessageQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new TryGetLatestMessageQueryHandler(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireQuery()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenNoMessageExists_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenMessagesExist_ItShouldRemoveAllMessagesOnlyForThisMailbox()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);

                using (var connection = testDatabase.CreateConnection())
                {
                    var entity = new EndToEndTestEmail(MailboxName.Value, Subject, Body, DateTime.UtcNow);
                    await connection.InsertAsync(entity);
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(new Message(Subject, Body), result);

                return ExpectedSideEffects.None;
            });
        }
    }
}