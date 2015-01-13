using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Identity.OAuth;
using Fifthweek.Api.Subscriptions.Commands;
using Fifthweek.Api.Subscriptions.Controllers;
using Fifthweek.Api.Subscriptions.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests.Controllers
{
    [TestClass]
    public class SubscriptionControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private Mock<ICommandHandler<CreateSubscriptionCommand>> setMandatorySubscriptionFields;
        private Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>> getCreatorStatus;
        private Mock<IUserContext> userContext;
        private Mock<IGuidCreator> guidCreator;
        private SubscriptionController target;

        [TestInitialize]
        public void Initialize()
        {
            this.setMandatorySubscriptionFields = new Mock<ICommandHandler<CreateSubscriptionCommand>>();
            this.getCreatorStatus = new Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>>();
            this.userContext = new Mock<IUserContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new SubscriptionController(
                this.setMandatorySubscriptionFields.Object,
                this.getCreatorStatus.Object,
                this.userContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPuttingMandatorySubscriptionFields_ItShouldIssueSetMandatorySubscriptionFieldsCommand()
        {
            var data = NewMandatorySubscriptionData();
            var command = NewCreateSubscriptionCommand(UserId, SubscriptionId, data);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(SubscriptionId.Value);
            this.setMandatorySubscriptionFields.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostNewSubscription(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.setMandatorySubscriptionFields.Verify();
        }

        [TestMethod]
        public async Task WhenGettingCreatorStatus_ItShouldReturnResultFromCreatorStatusQuery()
        {
            var query = new GetCreatorStatusQuery(UserId);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.getCreatorStatus.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(new CreatorStatus(SubscriptionId, false)));

            var result = await this.target.GetCurrentCreatorStatus();

            Assert.AreEqual(result.SubscriptionId, SubscriptionId.Value);
            Assert.AreEqual(result.MustWriteFirstPost, false);
        }

        [TestMethod]
        public async Task WhenGettingCreatorStatus_ItShouldReturnResultFromCreatorStatusQuery2()
        {
            var query = new GetCreatorStatusQuery(UserId);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.getCreatorStatus.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(new CreatorStatus(SubscriptionId, true)));

            var result = await this.target.GetCurrentCreatorStatus();

            Assert.AreEqual(result.SubscriptionId, SubscriptionId.Value);
            Assert.AreEqual(result.MustWriteFirstPost, true);
        }


        public static NewSubscriptionData NewMandatorySubscriptionData()
        {
            return new NewSubscriptionData
            {
                SubscriptionName = "Captain Phil",
                Tagline = "Web Comics And More",
                BasePrice = 50
            };
        }

        public static CreateSubscriptionCommand NewCreateSubscriptionCommand(
            UserId userId,
            SubscriptionId subscriptionId,
            NewSubscriptionData data)
        {
            return new CreateSubscriptionCommand(
                userId,
                subscriptionId,
                ValidSubscriptionName.Parse(data.SubscriptionName),
                ValidTagline.Parse(data.Tagline),
                ChannelPriceInUsCentsPerWeek.Parse(data.BasePrice));
        }
    }
}