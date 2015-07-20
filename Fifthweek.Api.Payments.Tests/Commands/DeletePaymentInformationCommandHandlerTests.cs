namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeletePaymentInformationCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly DeletePaymentInformationCommand Command = new DeletePaymentInformationCommand(Requester, UserId);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IDeleteUserPaymentInformationDbStatement> deleteUserPaymentInformation;

        private DeletePaymentInformationCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.deleteUserPaymentInformation = new Mock<IDeleteUserPaymentInformationDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new DeletePaymentInformationCommandHandler(
                this.requesterSecurity.Object,
                this.deleteUserPaymentInformation.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new DeletePaymentInformationCommand(
                Requester,
                UserId.Random()));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new DeletePaymentInformationCommand(
                Requester.Unauthenticated,
                UserId));
        }

        [TestMethod]
        public async Task ItShouldDeleteUserPaymentInformation()
        {
            this.deleteUserPaymentInformation.Setup(v => v.ExecuteAsync(Command.UserId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.deleteUserPaymentInformation.Verify();
        }
    }
}