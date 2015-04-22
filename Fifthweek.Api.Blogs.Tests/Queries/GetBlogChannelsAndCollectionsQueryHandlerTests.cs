namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetBlogChannelsAndCollectionsQueryHandlerTests
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogName Name = new BlogName("name");
        private static readonly Tagline Tagline = new Tagline("tagline");
        private static readonly DateTime CreationDate = DateTime.UtcNow;
        private static readonly Introduction Introduction = new Introduction("intro");
        private static readonly BlogDescription Description = new BlogDescription("description");
        private static readonly ExternalVideoUrl ExternalVideoUrl = new ExternalVideoUrl("url");
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());

        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IGetBlogChannelsAndCollectionsDbStatement> getBlogChannelsAndCollections;

        private GetBlogChannelsAndCollectionsQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.getBlogChannelsAndCollections = new Mock<IGetBlogChannelsAndCollectionsDbStatement>();
            this.target = new GetBlogChannelsAndCollectionsQueryHandler(
                this.fileInformationAggregator.Object,
                this.getBlogChannelsAndCollections.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenNoHeaderImageExists_ItShouldReturnTheBlogData()
        {
            var databaseResult =
                new GetBlogChannelsAndCollectionsDbStatement.GetBlogChannelsAndCollectionsDbResult(
                    new GetBlogChannelsAndCollectionsDbStatement.BlogDbResult(
                        BlogId,
                        CreatorId,
                        Name,
                        Tagline,
                        Introduction,
                        CreationDate,
                        null,
                        null,
                        null),
                    new List<ChannelResult>());
            this.getBlogChannelsAndCollections.Setup(v => v.ExecuteAsync(BlogId))
                .ReturnsAsync(databaseResult);

            var result = await this.target.HandleAsync(new GetBlogChannelsAndCollectionsQuery(BlogId));

            Assert.AreEqual(BlogId, result.Blog.BlogId);
            Assert.AreEqual(Name, result.Blog.Name);
            Assert.AreEqual(Tagline, result.Blog.Tagline);
            Assert.AreEqual(Introduction, result.Blog.Introduction);
            Assert.AreEqual(CreationDate, result.Blog.CreationDate);
            Assert.AreEqual(null, result.Blog.HeaderImage);
            Assert.AreEqual(null, result.Blog.Video);
            Assert.AreEqual(null, result.Blog.Description);

            Assert.AreEqual(databaseResult.Channels, result.Blog.Channels);
        }

        [TestMethod]
        public async Task WhenHeaderImageExists_ItShouldReturnTheBlogData()
        {
            var databaseResult =
                new GetBlogChannelsAndCollectionsDbStatement.GetBlogChannelsAndCollectionsDbResult(
                    new GetBlogChannelsAndCollectionsDbStatement.BlogDbResult(
                        BlogId,
                        CreatorId,
                        Name,
                        Tagline,
                        Introduction,
                        CreationDate,
                        HeaderFileId,
                        ExternalVideoUrl,
                        Description),
                    new List<ChannelResult>());

            this.getBlogChannelsAndCollections.Setup(v => v.ExecuteAsync(BlogId))
                .ReturnsAsync(databaseResult);

            var fileInformation = new FileInformation(HeaderFileId, "container");
            this.fileInformationAggregator.Setup(
                v => v.GetFileInformationAsync(CreatorId, HeaderFileId, FilePurposes.ProfileHeaderImage))
                .ReturnsAsync(fileInformation);

            var result = await this.target.HandleAsync(new GetBlogChannelsAndCollectionsQuery(BlogId));

            Assert.AreEqual(BlogId, result.Blog.BlogId);
            Assert.AreEqual(Name, result.Blog.Name);
            Assert.AreEqual(Tagline, result.Blog.Tagline);
            Assert.AreEqual(Introduction, result.Blog.Introduction);
            Assert.AreEqual(CreationDate, result.Blog.CreationDate);
            Assert.AreEqual(fileInformation, result.Blog.HeaderImage);
            Assert.AreEqual(ExternalVideoUrl, result.Blog.Video);
            Assert.AreEqual(Description, result.Blog.Description);

            Assert.AreEqual(databaseResult.Channels, result.Blog.Channels);
        }
    }
}
