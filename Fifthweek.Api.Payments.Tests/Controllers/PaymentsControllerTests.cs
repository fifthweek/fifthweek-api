namespace Fifthweek.Api.Payments.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
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
        private Mock<ICommandHandler<DeletePaymentInformationCommand>> deletePaymentInformation;

        private PaymentsController target;

        public virtual void Initialize()
        {
            this.requesterContext = new Mock<IRequesterContext>(MockBehavior.Strict);
            this.getCreditRequestSummary = new Mock<IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary>>(MockBehavior.Strict);
            this.updatePaymentsOrigin = new Mock<ICommandHandler<UpdatePaymentOriginCommand>>(MockBehavior.Strict);
            this.applyCreditRequest = new Mock<ICommandHandler<ApplyCreditRequestCommand>>(MockBehavior.Strict);
            this.deletePaymentInformation = new Mock<ICommandHandler<DeletePaymentInformationCommand>>(MockBehavior.Strict);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.target = new PaymentsController(
                this.requesterContext.Object,
                this.getCreditRequestSummary.Object,
                this.updatePaymentsOrigin.Object,
                this.applyCreditRequest.Object,
                this.deletePaymentInformation.Object);
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

            private static readonly PaymentLocationData PaymentLocationData = new PaymentLocationData("GB", "123456", "1.1.1.1");

            private static readonly CreditRequestSummary CreditRequestSummary = 
                new CreditRequestSummary(
                    5, 
                    new TaxamoCalculationResult(
                        new AmountInUsCents(10),
                        new AmountInUsCents(12), 
                        new AmountInUsCents(2),
                        0.2m,
                        "VAT", 
                        "GB",
                        "UK",
                        null));

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsNull_ItShouldThrowAnException()
            {
                await this.target.GetCreditRequestSummaryAsync(null, PaymentLocationData.CountryCode, PaymentLocationData.CreditCardPrefix, PaymentLocationData.IpAddress);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.GetCreditRequestSummaryAsync(" ", PaymentLocationData.CountryCode, PaymentLocationData.CreditCardPrefix, PaymentLocationData.IpAddress);
            }

            [TestMethod]
            public async Task WhenLocationDataIsNull_ItShouldGetCreditRequestSummery()
            {
                this.getCreditRequestSummary.Setup(
                    v => v.HandleAsync(new GetCreditRequestSummaryQuery(Requester, UserId, null)))
                    .ReturnsAsync(CreditRequestSummary);

                var result = await this.target.GetCreditRequestSummaryAsync(UserId.Value.EncodeGuid(), null, null, null);

                Assert.AreEqual(CreditRequestSummary, result);
            }

            [TestMethod]
            public async Task WhenLocationDataIsProvided_ItShouldGetCreditRequestSummery()
            {
                this.getCreditRequestSummary.Setup(
                    v => v.HandleAsync(new GetCreditRequestSummaryQuery(Requester, UserId, new GetCreditRequestSummaryQuery.LocationData(PaymentLocationData.Parse().CountryCode, PaymentLocationData.Parse().CreditCardPrefix, PaymentLocationData.Parse().IpAddress))))
                    .ReturnsAsync(CreditRequestSummary);

                var result = await this.target.GetCreditRequestSummaryAsync(UserId.Value.EncodeGuid(), PaymentLocationData.CountryCode, PaymentLocationData.CreditCardPrefix, PaymentLocationData.IpAddress);

                Assert.AreEqual(CreditRequestSummary, result);
            }
        }

        [TestClass]
        public class DeletePaymentInformationAsync : PaymentsControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsNull_ItShouldThrowAnException()
            {
                await this.target.DeletePaymentInformationAsync(null);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenUserIdIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.DeletePaymentInformationAsync(" ");
            }

            [TestMethod]
            public async Task ItShouldCallUpdatePaymentsOrigin()
            {
                this.deletePaymentInformation.Setup(v => v.HandleAsync(
                    new DeletePaymentInformationCommand(Requester, UserId)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.DeletePaymentInformationAsync(UserId.Value.EncodeGuid());

                this.updatePaymentsOrigin.Verify();
            }
        }
    }
}