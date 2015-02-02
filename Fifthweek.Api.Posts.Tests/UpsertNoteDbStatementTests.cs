namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpsertNoteDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime ScheduleDate = Now.AddDays(2);
        private static readonly DateTime ClippedScheduleDate = DateTime.UtcNow.AddDays(1);
        private static readonly ReviseNoteCommand Command = new ReviseNoteCommand(Requester, PostId, ChannelId, Note, ScheduleDate);
        private Mock<IScheduledDateClippingFunction> scheduledDateClipping;
        private UpsertNoteDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.scheduledDateClipping = new Mock<IScheduledDateClippingFunction>();
            this.scheduledDateClipping.Setup(_ => _.Apply(It.IsAny<DateTime>(), ScheduleDate)).Returns(ClippedScheduleDate);

            // Give side-effecting components strict mock behaviour.
            var databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new UpsertNoteDbStatement(this.scheduledDateClipping.Object, databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, ChannelId, Note, ScheduleDate, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireChannelId()
        {
            await this.target.ExecuteAsync(PostId, null, Note, ScheduleDate, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireNote()
        {
            await this.target.ExecuteAsync(PostId, ChannelId, null, ScheduleDate, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcScheduleDate()
        {
            await this.target.ExecuteAsync(PostId, ChannelId, Note, DateTime.Now, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireUtcNowDate()
        {
            await this.target.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: false);
                await this.target.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldInsertIfPostDoesNotExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    Note.Value,
                    false,
                    new SqlDateTime(ClippedScheduleDate).Value,
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return expectedPost;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateIfPostExists()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    Note.Value,
                    false,
                    new SqlDateTime(ClippedScheduleDate).Value,
                    default(DateTime));
            
                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return expectedPost;
                        }
                    }
                };
            });
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId, TestDatabaseContext testDatabase, bool createPost)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestChannelAsync(newUserId.Value, newChannelId.Value);

                if (createPost)
                {
                    var post = PostTests.UniqueNote(new Random());
                    post.Id = PostId.Value;
                    post.ChannelId = newChannelId.Value;
                    await databaseContext.Database.Connection.InsertAsync(post);
                }
            }
        }
    }
}