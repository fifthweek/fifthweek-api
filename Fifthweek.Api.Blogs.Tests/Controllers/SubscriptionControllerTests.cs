namespace Fifthweek.Api.Blogs.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Controllers;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscriptionControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private Mock<ICommandHandler<UpdateBlogSubscriptionsCommand>> updateBlogSubscriptions;
        private Mock<ICommandHandler<UnsubscribeFromChannelCommand>> unsubscribeFromChannel;
        private Mock<ICommandHandler<AcceptChannelSubscriptionPriceChangeCommand>> acceptPriceChange;
        private Mock<IRequesterContext> requesterContext;
        private SubscriptionController target;

        public virtual void Initialize()
        {
            this.updateBlogSubscriptions = new Mock<ICommandHandler<UpdateBlogSubscriptionsCommand>>(MockBehavior.Strict);
            this.unsubscribeFromChannel = new Mock<ICommandHandler<UnsubscribeFromChannelCommand>>(MockBehavior.Strict);
            this.acceptPriceChange = new Mock<ICommandHandler<AcceptChannelSubscriptionPriceChangeCommand>>(MockBehavior.Strict);
            this.requesterContext = new Mock<IRequesterContext>();
            this.target = new SubscriptionController(
                this.updateBlogSubscriptions.Object,
                this.unsubscribeFromChannel.Object,
                this.acceptPriceChange.Object,
                this.requesterContext.Object);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
        }

        [TestClass]
        public class PutBlogSubscriptionsTests : SubscriptionControllerTests
        {
            private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
            private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
            private static readonly ValidAcceptedChannelPrice AcceptedPrice1 = ValidAcceptedChannelPrice.Parse(10);
            private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
            private static readonly ValidAcceptedChannelPrice AcceptedPrice2 = ValidAcceptedChannelPrice.Parse(20);

            private static readonly UpdatedBlogSubscriptionData UpdatedBlogSubscriptionData
                = new UpdatedBlogSubscriptionData
                {
                    Subscriptions = new List<ChannelSubscriptionDataWithChannelId>
                    {
                        new ChannelSubscriptionDataWithChannelId
                        {
                            ChannelId = ChannelId1.Value.EncodeGuid(),
                            AcceptedPrice = AcceptedPrice1.Value
                        },
                        new ChannelSubscriptionDataWithChannelId
                        {
                            ChannelId = ChannelId2.Value.EncodeGuid(),
                            AcceptedPrice = AcceptedPrice2.Value
                        },
                    }
                };

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
            {
                await this.target.PutBlogSubscriptions(null, UpdatedBlogSubscriptionData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenSubscriptionDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PutBlogSubscriptions(BlogId.Value.EncodeGuid(), null);
            }

            [TestMethod]
            public async Task ItShouldUpdateBlogSubscriptions()
            {
                this.updateBlogSubscriptions.Setup(v => v.HandleAsync(
                    new UpdateBlogSubscriptionsCommand(
                        Requester,
                        BlogId,
                        new List<AcceptedChannelSubscription>
                        {
                            new AcceptedChannelSubscription(ChannelId1, AcceptedPrice1),
                            new AcceptedChannelSubscription(ChannelId2, AcceptedPrice2),
                        }))).Returns(Task.FromResult(0));

                await this.target.PutBlogSubscriptions(BlogId.Value.EncodeGuid(), UpdatedBlogSubscriptionData);
            }
        }

        [TestClass]
        public class DeleteChannelSubscriptionTests : SubscriptionControllerTests
        {
            private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenChannelIdIsNull_ItShouldThrowAnException()
            {
                await this.target.DeleteChannelSubscription(null);
            }

            [TestMethod]
            public async Task ItShouldUnsubscribeFromTheChannel()
            {
                this.unsubscribeFromChannel.Setup(v => v.HandleAsync(
                    new UnsubscribeFromChannelCommand(
                        Requester,
                        ChannelId))).Returns(Task.FromResult(0));

                await this.target.DeleteChannelSubscription(ChannelId.Value.EncodeGuid());
            }
        }

        [TestClass]
        public class PutChannelSubscription : SubscriptionControllerTests
        {
            private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
            private static readonly ValidAcceptedChannelPrice AcceptedPrice = ValidAcceptedChannelPrice.Parse(10);

            private static readonly ChannelSubscriptionDataWithoutChannelId UpdatedSubscriptionData = new ChannelSubscriptionDataWithoutChannelId
            {
                AcceptedPrice = AcceptedPrice.Value
            };

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
            {
                await this.target.PutChannelSubscription(null, UpdatedSubscriptionData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenSubscriptionDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PutChannelSubscription(ChannelId.Value.EncodeGuid(), null);
            }

            [TestMethod]
            public async Task ItShouldUpdateBlogSubscriptions()
            {
                this.acceptPriceChange.Setup(v => v.HandleAsync(
                    new AcceptChannelSubscriptionPriceChangeCommand(
                        Requester,
                        ChannelId,
                        AcceptedPrice))).Returns(Task.FromResult(0));

                await this.target.PutChannelSubscription(ChannelId.Value.EncodeGuid(), UpdatedSubscriptionData);
            }
        }
    }
}