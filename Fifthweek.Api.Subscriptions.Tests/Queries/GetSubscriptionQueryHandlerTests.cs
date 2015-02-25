namespace Fifthweek.Api.Subscriptions.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetSubscriptionQueryHandlerTests
    {
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionName Name = new SubscriptionName("name");
        private static readonly DateTime CreationDate = DateTime.UtcNow;
        private static readonly Introduction Introduction = new Introduction("intro");
        private static readonly SubscriptionDescription Description = new SubscriptionDescription("description");
        private static readonly ExternalVideoUrl ExternalVideoUrl = new ExternalVideoUrl("url");
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());

        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IGetSubscriptionDbStatement> getSubscription;

        private GetSubscriptionQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.getSubscription = new Mock<IGetSubscriptionDbStatement>();
            this.target = new GetSubscriptionQueryHandler(
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
            this.getSubscription.Setup(v => v.ExecuteAsync(SubscriptionId))
                .ReturnsAsync(new GetSubscriptionDbStatement.GetSubscriptionDataDbResult(
                    SubscriptionId,
                    CreatorId,
                    Name,
                    CreationDate,
                    Introduction,
                    null,
                    null,
                    null));

            var result = await this.target.HandleAsync(new GetSubscriptionQuery(SubscriptionId));

            Assert.AreEqual(SubscriptionId, result.SubscriptionId);
            Assert.AreEqual(CreatorId, result.CreatorId);
            Assert.AreEqual(Name, result.Name);
            Assert.AreEqual(CreationDate, result.CreationDate);
            Assert.AreEqual(Introduction, result.Introduction);
            Assert.AreEqual(null, result.Description);
            Assert.AreEqual(null, result.ExternalVideoUrl);
            Assert.AreEqual(null, result.HeaderImage);
        }

        [TestMethod]
        public async Task WhenHeaderImageExists_ItShouldReturnTheSubscriptionData()
        {
            this.getSubscription.Setup(v => v.ExecuteAsync(SubscriptionId))
                .ReturnsAsync(new GetSubscriptionDbStatement.GetSubscriptionDataDbResult(
                    SubscriptionId,
                    CreatorId,
                    Name,
                    CreationDate,
                    Introduction,
                    Description,
                    ExternalVideoUrl,
                    HeaderFileId));

            var fileInformation = new FileInformation(HeaderFileId, "container", "blob", "bloburi");
            this.fileInformationAggregator.Setup(
                v => v.GetFileInformationAsync(CreatorId, HeaderFileId, FilePurposes.ProfileHeaderImage))
                .ReturnsAsync(fileInformation);

            var result = await this.target.HandleAsync(new GetSubscriptionQuery(SubscriptionId));

            Assert.AreEqual(SubscriptionId, result.SubscriptionId);
            Assert.AreEqual(CreatorId, result.CreatorId);
            Assert.AreEqual(Name, result.Name);
            Assert.AreEqual(CreationDate, result.CreationDate);
            Assert.AreEqual(Introduction, result.Introduction);
            Assert.AreEqual(Description, result.Description);
            Assert.AreEqual(ExternalVideoUrl, result.ExternalVideoUrl);
            Assert.AreEqual(fileInformation, result.HeaderImage);
        }
    }
}
