namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

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
    public class RescheduleForNowCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly RescheduleForNowCommand Command = new RescheduleForNowCommand(Requester, PostId);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IFifthweekDbContext> databaseContext;
        private RescheduleForNowCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            // Mock potentially side-effecting components with strict behaviour.            
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new RescheduleForNowCommandHandler(this.requesterSecurity.Object, this.postSecurity.Object, databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new RescheduleForNowCommand(Requester.Unauthenticated, PostId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToPost_ItShouldThrowUnauthorizedException()
        {
            this.postSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, PostId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: true);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task PostIsAlreadyLive_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: false, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });

            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: false, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostIsNotScheduledByQueue_ItShouldSetLiveDateToNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                var post = await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Post>(post)
                    {
                        Expected = actual =>
                        {
                            if (actual.LiveDate < post.LiveDate)
                            {
                                // Ensure we're only 'OK' with the value if it looks to be 'Now'. 
                                // Checking to see if it's less than the future date seems sufficient.
                                post.LiveDate = actual.LiveDate;
                            }

                            return post;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenPostIsScheduledByQueue_ItShouldSetLiveDateToNowAndUnscheduledFromQueue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                var post = await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                post.ScheduledByQueue = false;

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Post>(post)
                    {
                        Expected = actual =>
                        {
                            if (actual.LiveDate < post.LiveDate)
                            {
                                // Ensure we're only 'OK' with the value if it looks to be 'Now'. 
                                // Checking to see if it's less than the future date seems sufficient.
                                post.LiveDate = actual.LiveDate;    
                            }
                            
                            return post;
                        }
                    }
                };
            });
        }

        private async Task<Post> CreateEntitiesAsync(TestDatabaseContext testDatabase, bool liveDateInFuture, bool scheduledByQueue)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var channelId = Guid.NewGuid();
                var collectionId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(UserId.Value, channelId, collectionId);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = channelId;
                post.CollectionId = collectionId;
                post.ScheduledByQueue = scheduledByQueue;
                post.LiveDate = Now.AddDays(liveDateInFuture ? 1 : -1);
                await databaseContext.Database.Connection.InsertAsync(post);
            }

            using (var databaseContext = testDatabase.NewContext())
            {
                var postId = PostId.Value;
                return await databaseContext.Posts.FirstAsync(_ => _.Id == postId);
            }
        }
    }
}