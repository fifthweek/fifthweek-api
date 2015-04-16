namespace Fifthweek.Api.Blogs.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Controllers;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BlogAccessControllerTests
    {
        private static readonly Requester Requester = Requester.Authenticated(new UserId(Guid.NewGuid()));
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());

        private Mock<IRequesterContext> requesterContext;
        private Mock<ICommandHandler<UpdateFreeAccessUsersCommand>> updateFreeAccessUsers;
        private Mock<IQueryHandler<GetFreeAccessUsersQuery, GetFreeAccessUsersResult>> getFreeAccessUsers;

        private BlogAccessController target;

        public virtual void TestInitialize()
        {
            this.requesterContext = new Mock<IRequesterContext>();
            this.updateFreeAccessUsers = new Mock<ICommandHandler<UpdateFreeAccessUsersCommand>>();
            this.getFreeAccessUsers = new Mock<IQueryHandler<GetFreeAccessUsersQuery, GetFreeAccessUsersResult>>();

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.target = new BlogAccessController(this.requesterContext.Object, this.updateFreeAccessUsers.Object, this.getFreeAccessUsers.Object);
        }

        [TestClass]
        public class PutFreeAccessListTests : BlogAccessControllerTests
        {
            private static readonly FreeAccessUsersData EmptyData = new FreeAccessUsersData();

            [TestInitialize]
            public override void TestInitialize()
            {
                base.TestInitialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
            {
                await this.target.PutFreeAccessList(null, EmptyData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PutFreeAccessList(BlogId.Value.EncodeGuid(), null);
            }

            [TestMethod]
            public async Task ItShouldCallUpdateFreeAccessUsers()
            {
                var emailList = new List<string> { "one@test.com", "two@test.com" };
                var freeAccessUsersData = new FreeAccessUsersData { Emails = emailList };
               
                var validEmailList = emailList.Select(ValidEmail.Parse).ToList();
                var invalidEmailList = new List<Email>();
                
                var expectedResult = new PutFreeAccessUsersResult(invalidEmailList);

                this.updateFreeAccessUsers
                    .Setup(v => v.HandleAsync(new UpdateFreeAccessUsersCommand(Requester, BlogId, validEmailList)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                var result = await this.target.PutFreeAccessList(BlogId.Value.EncodeGuid(), freeAccessUsersData);

                this.updateFreeAccessUsers.Verify();
                Assert.AreEqual(expectedResult, result);
            }

            [TestMethod]
            public async Task WhenSomeEmailAddressesAreInvalid_ItShouldCallUpdateFreeAccessUsersAndItShouldReturnValidEmailAddresses()
            {
                var emailList = new List<string> { "one@test.com", "two.test.com" };
                var freeAccessUsersData = new FreeAccessUsersData { Emails = emailList };

                var validEmailList = new List<ValidEmail> { ValidEmail.Parse(emailList[0]) };
                var invalidEmailList = new List<Email> { new Email(emailList[1]) };

                var expectedResult = new PutFreeAccessUsersResult(invalidEmailList);

                this.updateFreeAccessUsers
                    .Setup(v => v.HandleAsync(new UpdateFreeAccessUsersCommand(Requester, BlogId, validEmailList)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                var result = await this.target.PutFreeAccessList(BlogId.Value.EncodeGuid(), freeAccessUsersData);

                this.updateFreeAccessUsers.Verify();
                Assert.AreEqual(expectedResult, result);
            }

            [TestMethod]
            public async Task WhenAllEmailAddressesAreInvalid_ItShouldCallUpdateFreeAccessUsersAndItShouldReturnValidEmailAddresses()
            {
                var emailList = new List<string> { "one.test.com", "two.test.com" };
                var freeAccessUsersData = new FreeAccessUsersData { Emails = emailList };

                var validEmailList = new List<ValidEmail>();
                var invalidEmailList = emailList.Select(v => new Email(v)).ToList();

                var expectedResult = new PutFreeAccessUsersResult(invalidEmailList);

                this.updateFreeAccessUsers
                    .Setup(v => v.HandleAsync(new UpdateFreeAccessUsersCommand(Requester, BlogId, validEmailList)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                var result = await this.target.PutFreeAccessList(BlogId.Value.EncodeGuid(), freeAccessUsersData);

                this.updateFreeAccessUsers.Verify();
                Assert.AreEqual(expectedResult, result);
            }

            [TestMethod]
            public async Task WhenSomeEmailAddressesWhitespace_ItShouldCallUpdateFreeAccessUsersAndItShouldReturnValidEmailAddresses()
            {
                var emailList = new List<string> { "one@test.com", "    ", "\t", "two@test.com" };
                var freeAccessUsersData = new FreeAccessUsersData { Emails = emailList };

                var validEmailList = new List<ValidEmail> { ValidEmail.Parse(emailList[0]), ValidEmail.Parse(emailList[3]) };
                var invalidEmailList = new List<Email>();

                var expectedResult = new PutFreeAccessUsersResult(invalidEmailList);

                this.updateFreeAccessUsers
                    .Setup(v => v.HandleAsync(new UpdateFreeAccessUsersCommand(Requester, BlogId, validEmailList)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                var result = await this.target.PutFreeAccessList(BlogId.Value.EncodeGuid(), freeAccessUsersData);

                this.updateFreeAccessUsers.Verify();
                Assert.AreEqual(expectedResult, result);
            }
        }

        [TestClass]
        public class GetFreeAccessListTests : BlogAccessControllerTests
        {
            [TestInitialize]
            public override void TestInitialize()
            {
                base.TestInitialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
            {
                await this.target.GetFreeAccessList(null);
            }

            [TestMethod]
            public async Task WhenBlogIdIsProvided_ItShouldCallGetFreeAccessUsers()
            {
                var expectedResult = new GetFreeAccessUsersResult(new List<GetFreeAccessUsersResult.FreeAccessUser>());

                this.getFreeAccessUsers.Setup(v => v.HandleAsync(new GetFreeAccessUsersQuery(Requester, BlogId)))
                    .ReturnsAsync(expectedResult);

                var result = await this.target.GetFreeAccessList(BlogId.Value.EncodeGuid());

                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}