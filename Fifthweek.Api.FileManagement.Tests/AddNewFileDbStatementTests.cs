﻿namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AddNewFileDbStatementTests : PersistenceTestsBase
    {
        private const string FileNameWithoutExtension = "myfile";
        private const string LongFileNameWithoutExtension = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        private const string FileExtension = "jpeg";
        private const string LongFileExtension = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        private const string Purpose = "profile-picture";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly DateTime TimeStamp = new SqlDateTime(DateTime.UtcNow).Value;

        private AddNewFileDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new AddNewFileDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenAddingANewFile_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AddNewFileDbStatement(testDatabase);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId, UserId, ChannelId, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);

                var expectedFile = new File(
                    FileId.Value,
                    UserId.Value,
                    ChannelId.Value,
                    FileState.WaitingForUpload,
                    TimeStamp,
                    null,
                    null,
                    null,
                    null,
                    FileNameWithoutExtension,
                    FileExtension,
                    0,
                    Purpose,
                    null,
                    null);

                return new ExpectedSideEffects
                {
                    Insert = expectedFile
                };
            });
        }

        [TestMethod]
        public async Task WhenAddingANewFileWithLongFilename_ItShouldTruncateAndUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AddNewFileDbStatement(testDatabase);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId, UserId, ChannelId, LongFileNameWithoutExtension, LongFileExtension, Purpose, TimeStamp);

                var expectedFile = new File(
                    FileId.Value,
                    UserId.Value,
                    ChannelId.Value,
                    FileState.WaitingForUpload,
                    TimeStamp,
                    null,
                    null,
                    null,
                    null,
                    LongFileNameWithoutExtension.Substring(0, File.MaximumFileNameLength),
                    LongFileExtension.Substring(0, File.MaximumFileExtensionLength),
                    0,
                    Purpose,
                    null,
                    null);

                return new ExpectedSideEffects
                {
                    Insert = expectedFile
                };
            });
        }

        [TestMethod]
        public async Task WhenAddingANewFileWithNoChannelId_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AddNewFileDbStatement(testDatabase);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId, UserId, null, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);

                var expectedFile = new File(
                    FileId.Value,
                    UserId.Value,
                    null,
                    FileState.WaitingForUpload,
                    TimeStamp,
                    null,
                    null,
                    null,
                    null,
                    FileNameWithoutExtension,
                    FileExtension,
                    0,
                    Purpose,
                    null,
                    null);

                return new ExpectedSideEffects
                {
                    Insert = expectedFile
                };
            });
        }

        [TestMethod]
        public async Task WhenAddingANewFileTwice_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AddNewFileDbStatement(testDatabase);
                await this.CreateUserAsync(testDatabase);
                await this.target.ExecuteAsync(FileId, UserId, ChannelId, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);
                await testDatabase.TakeSnapshotAsync();

                await target.ExecuteAsync(FileId, UserId, ChannelId, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileId()
        {
            await this.target.ExecuteAsync(null, UserId, ChannelId, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAUserId()
        {
            await this.target.ExecuteAsync(FileId, null, ChannelId, FileNameWithoutExtension, FileExtension, Purpose, TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileNameWithoutExtension()
        {
            await this.target.ExecuteAsync(FileId, UserId, ChannelId, null, FileExtension, Purpose, TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileExtension()
        {
            await this.target.ExecuteAsync(FileId, UserId, ChannelId, FileNameWithoutExtension, null, Purpose, TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAPurpose()
        {
            await this.target.ExecuteAsync(FileId, UserId, ChannelId, FileNameWithoutExtension, FileExtension, null, TimeStamp);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
