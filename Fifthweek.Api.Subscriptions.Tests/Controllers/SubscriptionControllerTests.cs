using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Identity.OAuth;
using Fifthweek.Api.Subscriptions.Commands;
using Fifthweek.Api.Subscriptions.Controllers;
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
        private Mock<IUserContext> userContext;
        private SubscriptionController target;

        [TestInitialize]
        public void Initialize()
        {
            this.setMandatorySubscriptionFields = new Mock<ICommandHandler<CreateSubscriptionCommand>>();
            this.userContext = new Mock<IUserContext>();
            this.target = new SubscriptionController(
                this.setMandatorySubscriptionFields.Object,
                this.userContext.Object);
        }

        [TestMethod]
        public async Task WhenPuttingMandatorySubscriptionFields_ItShouldIssueSetMandatorySubscriptionFieldsCommand()
        {
            var data = NewMandatorySubscriptionData();
            var command = NewSetMandatorySubscriptionFieldsCommand(UserId, SubscriptionId, data);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.setMandatorySubscriptionFields.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutMandatorySubscriptionFields(SubscriptionId.Value, data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.setMandatorySubscriptionFields.Verify();
        }

        public static MandatorySubscriptionData NewMandatorySubscriptionData()
        {
            return new MandatorySubscriptionData
            {
                SubscriptionName = "Captain Phil",
                Tagline = "Web Comics And More",
                BasePrice = 50
            };
        }

        public static CreateSubscriptionCommand NewSetMandatorySubscriptionFieldsCommand(
            UserId userId,
            SubscriptionId subscriptionId,
            MandatorySubscriptionData data)
        {
            return new CreateSubscriptionCommand(
                userId,
                subscriptionId,
                SubscriptionName.Parse(data.SubscriptionName),
                Tagline.Parse(data.Tagline),
                ChannelPriceInUsCentsPerWeek.Parse(data.BasePrice));
        }
    }
}