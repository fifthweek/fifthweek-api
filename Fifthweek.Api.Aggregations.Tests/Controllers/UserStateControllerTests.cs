namespace Fifthweek.Api.Aggregations.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Controllers;
    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserStateControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private UserStateController target;
        private Mock<IQueryHandler<GetUserStateQuery, UserState>> getUserState;
        private Mock<IUserContext> userContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserState = new Mock<IQueryHandler<GetUserStateQuery, UserState>>();
            this.userContext = new Mock<IUserContext>();
            this.target = new UserStateController(this.getUserState.Object, this.userContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId);
            this.userContext.Setup(v => v.IsUserInRole(FifthweekRole.Creator)).Returns(true);
            
            var userState = new UserState(
                    new CreatorStatus(new SubscriptionId(Guid.NewGuid()), true),
                    new ChannelsAndCollections(
                        new List<ChannelsAndCollections.Channel>
                        {
                            new ChannelsAndCollections.Channel(
                                new ChannelId(
                                    Guid.NewGuid()), 
                                    Guid.NewGuid().ToString(),
                                    new List<ChannelsAndCollections.Collection>()),
                            new ChannelsAndCollections.Channel(
                                new ChannelId(
                                    Guid.NewGuid()), 
                                    Guid.NewGuid().ToString(),
                                    new List<ChannelsAndCollections.Collection>
                                    {
                                        new ChannelsAndCollections.Collection(
                                            new CollectionId(Guid.NewGuid()),
                                            Guid.NewGuid().ToString())
                                    }),
                            new ChannelsAndCollections.Channel(
                                new ChannelId(
                                    Guid.NewGuid()), 
                                    Guid.NewGuid().ToString(),
                                    new List<ChannelsAndCollections.Collection>
                                    {
                                        new ChannelsAndCollections.Collection(
                                            new CollectionId(Guid.NewGuid()),
                                            Guid.NewGuid().ToString()),
                                        new ChannelsAndCollections.Collection(
                                            new CollectionId(Guid.NewGuid()),
                                            Guid.NewGuid().ToString())
                                    })
                        }));

            var query = new GetUserStateQuery(UserId, Requester.Authenticated(UserId), true);
            this.getUserState.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(userState);

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatorStatus.SubscriptionId, 
                userState.CreatorStatus.SubscriptionId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatorStatus.MustWriteFirstPost, 
                userState.CreatorStatus.MustWriteFirstPost);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[0].ChannelId,
                userState.CreatedChannelsAndCollections.Channels[0].ChannelId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[0].Name,
                userState.CreatedChannelsAndCollections.Channels[0].Name);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[0].Collections.Count,
                userState.CreatedChannelsAndCollections.Channels[0].Collections.Count);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[1].ChannelId,
                userState.CreatedChannelsAndCollections.Channels[1].ChannelId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[1].Name,
                userState.CreatedChannelsAndCollections.Channels[1].Name);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[1].Collections.Count,
                userState.CreatedChannelsAndCollections.Channels[1].Collections.Count);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[1].Collections[0].CollectionId,
                userState.CreatedChannelsAndCollections.Channels[1].Collections[0].CollectionId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[1].Collections[0].Name,
                userState.CreatedChannelsAndCollections.Channels[1].Collections[0].Name);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].ChannelId,
                userState.CreatedChannelsAndCollections.Channels[2].ChannelId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Name,
                userState.CreatedChannelsAndCollections.Channels[2].Name);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Collections.Count,
                userState.CreatedChannelsAndCollections.Channels[2].Collections.Count);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Collections[0].CollectionId,
                userState.CreatedChannelsAndCollections.Channels[2].Collections[0].CollectionId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Collections[0].Name,
                userState.CreatedChannelsAndCollections.Channels[2].Collections[0].Name);

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Collections[1].CollectionId,
                userState.CreatedChannelsAndCollections.Channels[2].Collections[1].CollectionId.Value.EncodeGuid());

            Assert.AreEqual(
                result.CreatedChannelsAndCollections.Channels[2].Collections[1].Name,
                userState.CreatedChannelsAndCollections.Channels[2].Collections[1].Name);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery2()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId);
            this.userContext.Setup(v => v.IsUserInRole(FifthweekRole.Creator)).Returns(false);
            
            var query = new GetUserStateQuery(UserId, Requester.Authenticated(UserId), false);
            this.getUserState.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(new UserState(null, null));

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.IsNull(result.CreatorStatus);
            Assert.IsNull(result.CreatedChannelsAndCollections);
        }
    }
}