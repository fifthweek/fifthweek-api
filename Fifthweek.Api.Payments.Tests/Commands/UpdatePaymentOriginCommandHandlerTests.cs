namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdatePaymentOriginCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly ValidCountryCode CountryCode = ValidCountryCode.Parse("GB");
        private static readonly ValidCreditCardPrefix CreditCardPrefix = ValidCreditCardPrefix.Parse("123456");
        private static readonly ValidIpAddress IpAddress = ValidIpAddress.Parse("1.1.1.1");
        private static readonly ValidStripeToken StripeToken = ValidStripeToken.Parse(Guid.NewGuid().ToString());

        private static readonly UpdatePaymentOriginCommand Command = new UpdatePaymentOriginCommand(
            Requester, UserId, StripeToken, CountryCode, CreditCardPrefix, IpAddress);

        private static readonly UserPaymentOriginResult OriginWithCustomer = new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk");
        private static readonly UserPaymentOriginResult OriginWithoutCustomer = new UserPaymentOriginResult(null, "GB", "12345", "1.1.1.1", "ttk");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ISetUserPaymentOriginDbStatement> setUserPaymentOrigin;
        private Mock<IGetUserPaymentOriginDbStatement> getUserPaymentOrigin;
        private Mock<ICreateStripeCustomer> createStripeCustomer;
        private Mock<IUpdateStripeCustomerCreditCard> updateStripeCustomerCreditCard;

        private UpdatePaymentOriginCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.setUserPaymentOrigin = new Mock<ISetUserPaymentOriginDbStatement>(MockBehavior.Strict);
            this.getUserPaymentOrigin = new Mock<IGetUserPaymentOriginDbStatement>(MockBehavior.Strict);
            this.createStripeCustomer = new Mock<ICreateStripeCustomer>(MockBehavior.Strict);
            this.updateStripeCustomerCreditCard = new Mock<IUpdateStripeCustomerCreditCard>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new UpdatePaymentOriginCommandHandler(
                this.requesterSecurity.Object,
                this.setUserPaymentOrigin.Object,
                this.getUserPaymentOrigin.Object,
                this.createStripeCustomer.Object,
                this.updateStripeCustomerCreditCard.Object);
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
            await this.target.HandleAsync(new UpdatePaymentOriginCommand(
                Requester, UserId.Random(), StripeToken, CountryCode, CreditCardPrefix, IpAddress));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new UpdatePaymentOriginCommand(
                Requester.Unauthenticated, UserId, StripeToken, CountryCode, CreditCardPrefix, IpAddress));
        }

        [TestMethod]
        public async Task WhenStripeCustomerIdAlreadyExists_ItShouldUpdateTheCustomer()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(OriginWithCustomer);

            this.updateStripeCustomerCreditCard.Setup(v => v.ExecuteAsync(OriginWithCustomer.StripeCustomerId, StripeToken.Value))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.setUserPaymentOrigin.Setup(v => v.ExecuteAsync(
                UserId, OriginWithCustomer.StripeCustomerId, CountryCode, CreditCardPrefix, IpAddress))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.updateStripeCustomerCreditCard.Verify();
            this.setUserPaymentOrigin.Verify();
        }

        [TestMethod]
        public async Task WhenStripeCustomerIdDoesNotAlreadyExist_ItShouldCreateTheCustomer()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(OriginWithoutCustomer);

            var stripeCustomerId = Guid.NewGuid().ToString();
            this.createStripeCustomer.Setup(v => v.ExecuteAsync(UserId, StripeToken.Value))
                .Returns(Task.FromResult(stripeCustomerId));

            this.setUserPaymentOrigin.Setup(v => v.ExecuteAsync(
                UserId, stripeCustomerId, CountryCode, CreditCardPrefix, IpAddress))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.setUserPaymentOrigin.Verify();
        }
    }
}