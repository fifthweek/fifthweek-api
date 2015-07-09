namespace Fifthweek.Api.Payments.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentsControllerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private Mock<IRequesterContext> requesterContext;
        private Mock<IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary>> getCreditRequestSummary;
        private Mock<ICommandHandler<UpdatePaymentOriginCommand>> updatePaymentsOrigin;
        private Mock<ICommandHandler<ApplyCreditRequestCommand>> applyCreditRequest;

        private PaymentsController target;

        public virtual void Initialize()
        {
            this.requesterContext = new Mock<IRequesterContext>(MockBehavior.Strict);
            this.getCreditRequestSummary = new Mock<IQueryHandler<GetCreditRequestSummaryQuery,CreditRequestSummary>>(MockBehavior.Strict);
            this.updatePaymentsOrigin = new Mock<ICommandHandler<UpdatePaymentOriginCommand>>(MockBehavior.Strict);
            this.applyCreditRequest = new Mock<ICommandHandler<ApplyCreditRequestCommand>>(MockBehavior.Strict);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.target = new PaymentsController(
                this.requesterContext.Object,
                this.getCreditRequestSummary.Object,
                this.updatePaymentsOrigin.Object,
                this.applyCreditRequest.Object);
        }

        [TestClass]
        public class PutPaymentOriginAsync : PaymentsControllerTests
        {
            private static readonly ValidCountryCode CountryCode = ValidCountryCode.Parse("GB");
            private static readonly ValidCreditCardPrefix CreditCardPrefix = ValidCreditCardPrefix.Parse("123456");
            private static readonly ValidIpAddress IpAddress = ValidIpAddress.Parse("1.1.1.1");
            private static readonly ValidStripeToken StripeToken = ValidStripeToken.Parse(Guid.NewGuid().ToString());
            private static readonly PaymentOriginData PaymentOriginData = new PaymentOriginData(
                StripeToken.Value,
                CountryCode.Value,
                CreditCardPrefix.Value,
                IpAddress.Value);

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsNull_ItShouldThrowAnException()
            {
                await this.target.PutPaymentOriginAsync(null, PaymentOriginData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.PutPaymentOriginAsync(" ", PaymentOriginData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenPaymentOriginDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PutPaymentOriginAsync(UserId.Value.EncodeGuid(), null);
            }

            [TestMethod]
            public async Task ItShouldCallUpdatePaymentsOrigin()
            {
                this.updatePaymentsOrigin.Setup(v => v.HandleAsync(
                    new UpdatePaymentOriginCommand(Requester, UserId, StripeToken, CountryCode, CreditCardPrefix, IpAddress)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.PutPaymentOriginAsync(UserId.Value.EncodeGuid(), PaymentOriginData);

                this.updatePaymentsOrigin.Verify();
            }
        }

        [TestClass]
        public class PostCreditRequestAsync : PaymentsControllerTests
        {
            private static readonly PositiveInt Amount = PositiveInt.Parse(10);
            private static readonly PositiveInt ExpectedTotalAmount = PositiveInt.Parse(12);
            private static readonly CreditRequestData CreditRequestData = new CreditRequestData(Amount.Value, ExpectedTotalAmount.Value);

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsNull_ItShouldThrowAnException()
            {
                await this.target.PostCreditRequestAsync(null, CreditRequestData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.PostCreditRequestAsync(" ", CreditRequestData);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenPaymentOriginDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PostCreditRequestAsync(UserId.Value.EncodeGuid(), null);
            }

            [TestMethod]
            public async Task ItShouldCallApplyCreditRequest()
            {
                this.applyCreditRequest.Setup(
                    v => v.HandleAsync(new ApplyCreditRequestCommand(Requester, UserId, Amount, ExpectedTotalAmount)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.PostCreditRequestAsync(UserId.Value.EncodeGuid(), CreditRequestData);

                this.applyCreditRequest.Verify();
            }
        }

        [TestClass]
        public class GetCreditRequestSummaryAsync : PaymentsControllerTests
        {
            private static readonly PositiveInt Amount = PositiveInt.Parse(10);

            private static readonly CreditRequestSummary CreditRequestSummary = new Payments.Controllers.CreditRequestSummary(
                10, 12, 2, 0.2m, "VAT", "GB", "UK");

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsNull_ItShouldThrowAnException()
            {
                await this.target.GetCreditRequestSummaryAsync(null);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.GetCreditRequestSummaryAsync(" ");
            }

            [TestMethod]
            public async Task ItShouldGetCreditRequestSummery()
            {
                this.getCreditRequestSummary.Setup(
                    v => v.HandleAsync(new GetCreditRequestSummaryQuery(Requester, UserId)))
                    .ReturnsAsync(CreditRequestSummary);

                var result = await this.target.GetCreditRequestSummaryAsync(UserId.Value.EncodeGuid());

                Assert.AreEqual(CreditRequestSummary, result);
            }
        }
    }
}