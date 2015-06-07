namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public partial class UpdateFreeAccessUsersDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly IEnumerable<FreeAccessUser> InitialFreeAccessUsers = new List<FreeAccessUser> {
            new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
            new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
            new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
        };

        private MockRequestSnapshotService requestSnapshot;
        private UpdateFreeAccessUsersDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requestSnapshot = new MockRequestSnapshotService();
            this.target = new UpdateFreeAccessUsersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object, this.requestSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(CreatorId, null, new List<ValidEmail>());
        }

        [TestMethod]
        public async Task WhenNewEmailListIsNull_ItShouldDeleteAllEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId, BlogId, null);

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task ItShouldRequestSnapshotAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                this.target = new UpdateFreeAccessUsersDbStatement(trackingDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.VerifyConnectionDisposed(trackingDatabase);

                await this.target.ExecuteAsync(CreatorId, BlogId, null);

                this.requestSnapshot.VerifyCalledWith(CreatorId, Payments.Services.SnapshotType.CreatorGuestList);

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.ExecuteAsync(CreatorId, BlogId, null));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsEmpty_ItShouldDeleteAllEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId, BlogId, new List<ValidEmail>());

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsEmpty_AndEmailsForBlogIsEmpty_ItShouldNotAddEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId, BlogId, new List<ValidEmail>());

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsSubset_ItShouldDeleteOldEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("two@test.com"), 
                    });

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsSuperset_ItShouldAddNewEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);
                
                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("zero@test.com"), 
                        ValidEmail.Parse("one@test.com"), 
                        ValidEmail.Parse("two@test.com"), 
                        ValidEmail.Parse("three@test.com"), 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                return new ExpectedSideEffects
                {
                    Inserts = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "zero@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "four@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "five@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsSame_ItShouldNotModifyEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("one@test.com"), 
                        ValidEmail.Parse("two@test.com"), 
                        ValidEmail.Parse("three@test.com"), 
                    });
                
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsSame_AndOrderIsDifferent_ItShouldNotModifyEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("one@test.com"), 
                        ValidEmail.Parse("three@test.com"), 
                        ValidEmail.Parse("two@test.com"), 
                    });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListIsNewList_ItShouldModifyEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "three@test.com" },
                    },
                    Inserts = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "four@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "five@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenNewEmailListHasEmailsRemovedAndAdded_ItShouldModifyEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("three@test.com"), 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                return new ExpectedSideEffects
                {
                    Deletes = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "one@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "two@test.com" },
                    },
                    Inserts = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "four@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "five@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenNewEmailList_AndEmailsForBlogIsEmpty_ItShouldModifyEmailsForBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                return new ExpectedSideEffects
                {
                    Inserts = new List<FreeAccessUser>
                    {
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "four@test.com" },
                        new FreeAccessUser { BlogId = BlogId.Value, Email = "five@test.com" },
                    },
                };
            });
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateFreeAccessUsersDbStatement(testDatabase, this.requestSnapshot);

                await this.CreateDataAsync(testDatabase, true);

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("three@test.com"), 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    CreatorId, 
                    BlogId,
                    new List<ValidEmail>
                    { 
                        ValidEmail.Parse("three@test.com"), 
                        ValidEmail.Parse("four@test.com"), 
                        ValidEmail.Parse("five@test.com") 
                    });

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase, bool addEmailAddress)
        {
            await this.CreateUserAsync(testDatabase);
            await this.CreateBlogAsync(testDatabase);

            if (addEmailAddress)
            {
                using (var connection = testDatabase.CreateConnection())
                {
                    foreach (var item in InitialFreeAccessUsers)
                    {
                        await connection.InsertAsync(item);
                    }
                }
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = CreatorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateBlogAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var blog = BlogTests.UniqueEntity(random);
            blog.Id = BlogId.Value;
            blog.CreatorId = CreatorId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(blog);
            }
        }
    }
}