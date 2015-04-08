namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetSubscriptionQueryHandlerTests
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
        private Mock<IGetBlogDbStatement> getSubscription;

        private GetBlogQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.getSubscription = new Mock<IGetBlogDbStatement>();
            this.target = new GetBlogQueryHandler(
                this.fileInformationAggregator.Object,
                this.getSubscription.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenNoHeaderImageExists_ItShouldReturnTheSubscriptionData()
        {
            this.getSubscription.Setup(v => v.ExecuteAsync(BlogId))
                .ReturnsAsync(new GetBlogDbStatement.GetSubscriptionDataDbResult(
                    BlogId,
                    CreatorId,
                    Name,
                    Tagline,
                    Introduction,
                    CreationDate,
                    null,
                    null,
                    null));

            var result = await this.target.HandleAsync(new GetBlogQuery(BlogId));

            Assert.AreEqual(BlogId, result.BlogId);
            Assert.AreEqual(CreatorId, result.CreatorId);
            Assert.AreEqual(Name, result.BlogName);
            Assert.AreEqual(Tagline, result.Tagline);
            Assert.AreEqual(Introduction, result.Introduction);
            Assert.AreEqual(CreationDate, result.CreationDate);
            Assert.AreEqual(null, result.HeaderImage);
            Assert.AreEqual(null, result.Video);
            Assert.AreEqual(null, result.Description);
        }

        [TestMethod]
        public async Task WhenHeaderImageExists_ItShouldReturnTheSubscriptionData()
        {
            this.getSubscription.Setup(v => v.ExecuteAsync(BlogId))
                .ReturnsAsync(new GetBlogDbStatement.GetSubscriptionDataDbResult(
                    BlogId,
                    CreatorId,
                    Name,
                    Tagline,
                    Introduction,
                    CreationDate,
                    HeaderFileId,
                    ExternalVideoUrl,
                    Description));

            var fileInformation = new FileInformation(HeaderFileId, "container");
            this.fileInformationAggregator.Setup(
                v => v.GetFileInformationAsync(CreatorId, HeaderFileId, FilePurposes.ProfileHeaderImage))
                .ReturnsAsync(fileInformation);

            var result = await this.target.HandleAsync(new GetBlogQuery(BlogId));

            Assert.AreEqual(BlogId, result.BlogId);
            Assert.AreEqual(CreatorId, result.CreatorId);
            Assert.AreEqual(Name, result.BlogName);
            Assert.AreEqual(Tagline, result.Tagline);
            Assert.AreEqual(Introduction, result.Introduction);
            Assert.AreEqual(CreationDate, result.CreationDate);
            Assert.AreEqual(fileInformation, result.HeaderImage);
            Assert.AreEqual(ExternalVideoUrl, result.Video);
            Assert.AreEqual(Description, result.Description);
        }
    }
}
