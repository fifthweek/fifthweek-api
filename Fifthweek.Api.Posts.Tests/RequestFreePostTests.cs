namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestFreePostTests
    {
        private static readonly UserId RequestorId = UserId.Random();
        private static readonly PostId PostId = PostId.Random();
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime Timestamp = Now.AddDays(-1);

        private Mock<IGetFreePostTimestamp> getFreePostTimestamp;
        private Mock<IRequestFreePostDbStatement> requestFreePost;

        private RequestFreePost target;

        [TestInitialize]
        public void Initialize()
        {
            this.getFreePostTimestamp = new Mock<IGetFreePostTimestamp>();
            this.requestFreePost = new Mock<IRequestFreePostDbStatement>(MockBehavior.Strict);

            this.getFreePostTimestamp.Setup(v => v.Execute(Now)).Returns(Timestamp);

            this.target = new RequestFreePost(this.getFreePostTimestamp.Object, this.requestFreePost.Object);
        }

        [TestMethod]
        public async Task WhenFreePostsAvailable_ItShouldCompleteSuccessfully()
        {
            this.requestFreePost.Setup(v => v.ExecuteAsync(RequestorId, PostId, Timestamp, PostConstants.MaximumFreePostsPerInterval)).ReturnsAsync(true)
                .Verifiable();
         
            await this.target.ExecuteAsync(RequestorId, PostId, Now);
            
            this.requestFreePost.Verify();
        }

        [TestMethod]
        public async Task WhenFreePostsNotAvailable_ItShouldCompleteSuccessfully()
        {
            this.requestFreePost.Setup(v => v.ExecuteAsync(RequestorId, PostId, Timestamp, PostConstants.MaximumFreePostsPerInterval)).ReturnsAsync(false)
                .Verifiable();
           
            await ExpectedException.AssertExceptionAsync<RecoverableException>(
                () => this.target.ExecuteAsync(RequestorId, PostId, Now));

            this.requestFreePost.Verify();
        }
    }
}