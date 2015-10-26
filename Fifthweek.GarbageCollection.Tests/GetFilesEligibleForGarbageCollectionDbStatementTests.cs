namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetFilesEligibleForGarbageCollectionDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime EndDate = Now.AddDays(-1);
        private static readonly DateTime IncludedDate = EndDate.AddDays(-1);
        private static readonly UserId UserId = UserId.Random();
        private static readonly FileId OrphanedFileId1 = FileId.Random();
        private static readonly FileId OrphanedFileId2 = FileId.Random();
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly string Purpose = "purpose";

        private static readonly IReadOnlyList<ChannelId> ChannelIds
            = Enumerable.Repeat(0, 10).Select(v => ChannelId.Random()).ToList();

        private GetFilesEligibleForGarbageCollectionDbStatement target;

        [TestMethod]
        public async Task WhenDeletingFile_ItShouldRemoveTheRequestedPostFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new GetFilesEligibleForGarbageCollectionDbStatement(testDatabase);
                var initialFiles = await this.target.ExecuteAsync(EndDate);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var finalFiles = await this.target.ExecuteAsync(EndDate);

                Assert.AreEqual(2, finalFiles.Count - initialFiles.Count);

                CollectionAssert.AreEquivalent(
                    initialFiles.Concat(new List<OrphanedFileData>
                    {
                        new OrphanedFileData(OrphanedFileId1, ChannelId, Purpose),
                        new OrphanedFileData(OrphanedFileId2, ChannelId, Purpose),
                    }).ToList(),
                    finalFiles.ToList());

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                // Create header file
                var blogId = BlogId.Random();
                var headerImageFileId = FileId.Random();
                await databaseContext.CreateTestBlogAsync(UserId.Value, blogId.Value, headerImageFileId.Value);
                var headerImageFile = databaseContext.Files.First(v => v.Id == headerImageFileId.Value);
                headerImageFile.UploadStartedDate = IncludedDate;

                var channelId = ChannelId.Random();
                var channel = ChannelTests.UniqueEntity(Random);
                channel.BlogId = blogId.Value;
                channel.Blog = databaseContext.Blogs.First(v => v.Id == blogId.Value);
                channel.Id = channelId.Value;
                databaseContext.Channels.Add(channel);

                // Create profile image file
                var user = databaseContext.Users.First(v => v.Id == UserId.Value);
                var profileImageFileId = FileId.Random();
                var profileImageFile = this.CreateTestFileWithExistingUserAsync(UserId.Value, profileImageFileId.Value, IncludedDate);
                databaseContext.Files.Add(profileImageFile);
                user.ProfileImageFileId = profileImageFileId.Value;

                // Create image post file.
                var post1 = PostTests.UniqueFileOrImage(Random);
                var imageFileId = FileId.Random();
                var image = this.CreateTestFileWithExistingUserAsync(UserId.Value, imageFileId.Value, IncludedDate);
                databaseContext.Files.Add(image);
                post1.PreviewImageId = imageFileId.Value;
                post1.PreviewImage = image;
                post1.ChannelId = channelId.Value;
                post1.Channel = channel;
                databaseContext.Posts.Add(post1);

                var post1File = new PostFile(post1.Id, post1, imageFileId.Value, image);
                databaseContext.PostFiles.Add(post1File);

                // Create file post file.
                var post2 = PostTests.UniqueFileOrImage(Random);
                var fileFileId = FileId.Random();
                var file = this.CreateTestFileWithExistingUserAsync(UserId.Value, fileFileId.Value, IncludedDate);
                databaseContext.Files.Add(file);
                post2.ChannelId = channelId.Value;
                post2.Channel = channel;
                databaseContext.Posts.Add(post2);

                var post2File = new PostFile(post2.Id, post2, fileFileId.Value, file);
                databaseContext.PostFiles.Add(post2File);

                // Create files excluded because of date.
                var lateFile1 = this.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Random().Value, EndDate);
                databaseContext.Files.Add(lateFile1);
                var lateFile2 = this.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Random().Value, Now);
                databaseContext.Files.Add(lateFile2);

                // Create orphaned files.
                var orphanedFile1 = this.CreateTestFileWithExistingUserAsync(UserId.Value, OrphanedFileId1.Value, IncludedDate);
                databaseContext.Files.Add(orphanedFile1);
                var orphanedFile2 = this.CreateTestFileWithExistingUserAsync(UserId.Value, OrphanedFileId2.Value, IncludedDate);
                databaseContext.Files.Add(orphanedFile2);

                await databaseContext.SaveChangesAsync();
            }
        }
  
        public File CreateTestFileWithExistingUserAsync(Guid existingUserId, Guid newFileId, DateTime uploadStartedDate)
        {
            var file = FileTests.UniqueEntity(Random);
            file.Id = newFileId;
            file.UserId = existingUserId;
            file.UploadStartedDate = uploadStartedDate;
            file.ChannelId = ChannelId.Value;
            file.Purpose = Purpose;
            return file;
        }
  }
}