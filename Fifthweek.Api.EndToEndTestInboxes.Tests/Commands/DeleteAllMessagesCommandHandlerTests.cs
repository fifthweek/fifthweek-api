namespace Fifthweek.Api.EndToEndTestMailboxes.Tests.Commands
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteAllMessagesCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly MailboxName MailboxName = MailboxName.Parse("wd_1234567890123");
        private static readonly DeleteAllMessagesCommand Command = new DeleteAllMessagesCommand(MailboxName);

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private DeleteAllMessagesCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteAllMessagesCommandHandler(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCommand()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenNoMessagesExist_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

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
                    await connection.CreateEndToEndEmailAsync(MailboxName.Value);
                }

                var mailboxName = MailboxName.Value;
                EndToEndTestEmail expectedDeletion;
                using (var dbContext = testDatabase.CreateContext())
                {
                    expectedDeletion = await dbContext.EndToEndTestEmails.FirstAsync(_ => _.Mailbox == mailboxName);
                }

                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return new ExpectedSideEffects
                {
                    Delete = expectedDeletion
                };
            });
        }
    }
}