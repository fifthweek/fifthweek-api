namespace Fifthweek.Api.Blogs.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Controllers;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserBlogAccessControllerTests
    {
        private static readonly Requester Requester = Requester.Authenticated(new UserId(Guid.NewGuid()));

        private Mock<IRequesterContext> requesterContext;
        private Mock<IQueryHandler<GetBlogAccessStatusQuery, GetBlogAccessStatusResult>> getBlogAccessStatus;

        private UserBlogAccessController target;

        public virtual void TestInitialize()
        {
            this.requesterContext = new Mock<IRequesterContext>();
            this.getBlogAccessStatus = new Mock<IQueryHandler<GetBlogAccessStatusQuery, GetBlogAccessStatusResult>>();

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.target = new UserBlogAccessController(this.requesterContext.Object, this.getBlogAccessStatus.Object);
        }

        [TestClass]
        public class GetBlogAccessStatusTests : UserBlogAccessControllerTests
        {
            private static readonly BlogId BlogId = new Shared.BlogId(Guid.NewGuid());

            [TestInitialize]
            public override void TestInitialize()
            {
                base.TestInitialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
            {
                await this.target.GetBlogAccessStatus(null);
            }

            [TestMethod]
            public async Task WhenBlogIdIsProvided_ItShouldCallGetBlogAccessStatus()
            {
                var expectedResult = new GetBlogAccessStatusResult(true);
                this.getBlogAccessStatus.Setup(v => v.HandleAsync(new GetBlogAccessStatusQuery(Requester, BlogId)))
                    .ReturnsAsync(expectedResult);

                var result = await this.target.GetBlogAccessStatus(BlogId.Value.EncodeGuid());

                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}