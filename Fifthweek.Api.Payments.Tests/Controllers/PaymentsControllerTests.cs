namespace Fifthweek.Api.Payments.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
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
        private Mock<ICommandHandler<CreateCreditRefundCommand>> createCreditRefund;
        private Mock<ICommandHandler<CreateTransactionRefundCommand>> createTransactionRefund;
        private Mock<IQueryHandler<GetTransactionsQuery, GetTransactionsResult>> getTransactions;
        private Mock<ICommandHandler<BlockPaymentProcessingCommand>> blockPaymentProcessing;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGuidCreator> guidCreator;

        private PaymentsController target;

        public virtual void Initialize()
        {
            this.requesterContext = new Mock<IRequesterContext>(MockBehavior.Strict);
            this.getCreditRequestSummary = new Mock<IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary>>(MockBehavior.Strict);
            this.updatePaymentsOrigin = new Mock<ICommandHandler<UpdatePaymentOriginCommand>>(MockBehavior.Strict);
            this.applyCreditRequest = new Mock<ICommandHandler<ApplyCreditRequestCommand>>(MockBehavior.Strict);
            this.deletePaymentInformation = new Mock<ICommandHandler<DeletePaymentInformationCommand>>(MockBehavior.Strict);
            this.createCreditRefund = new Mock<ICommandHandler<CreateCreditRefundCommand>>(MockBehavior.Strict);
            this.createTransactionRefund = new Mock<ICommandHandler<CreateTransactionRefundCommand>>(MockBehavior.Strict);
            this.getTransactions = new Mock<IQueryHandler<GetTransactionsQuery, GetTransactionsResult>>(MockBehavior.Strict);
            this.blockPaymentProcessing = new Mock<ICommandHandler<BlockPaymentProcessingCommand>>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.guidCreator = new Mock<IGuidCreator>(MockBehavior.Strict);
            
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);

            this.target = new PaymentsController(
                this.requesterContext.Object,
                this.getCreditRequestSummary.Object,
                this.updatePaymentsOrigin.Object,
                this.applyCreditRequest.Object,
                this.deletePaymentInformation.Object,
                this.createCreditRefund.Object,
                this.createTransactionRefund.Object,
                this.getTransactions.Object,
                this.blockPaymentProcessing.Object,
                this.timestampCreator.Object,
                this.guidCreator.Object);
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
            private static readonly DateTime Now = DateTime.UtcNow;
            private static readonly TransactionReference TransactionReference = TransactionReference.Random();
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
                this.timestampCreator.Setup(v => v.Now()).Returns(Now);

                this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(TransactionReference.Value);

                this.applyCreditRequest.Setup(
                    v => v.HandleAsync(new ApplyCreditRequestCommand(Requester, UserId, Now, TransactionReference, Amount, ExpectedTotalAmount)))
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
                        new AmountInMinorDenomination(10),
                        new AmountInMinorDenomination(12), 
                        new AmountInMinorDenomination(2),
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

        [TestClass]
        public class PostTransactionRefundAsync : PaymentsControllerTests
        {
            private static readonly DateTime Now = DateTime.UtcNow;
            private static readonly TransactionReference TransactionReference = TransactionReference.Random();
            private static readonly TransactionRefundData Data = new TransactionRefundData("comment");

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
            {
                await this.target.PostTransactionRefundAsync(null, Data);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenTransactionReferenceIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.PostTransactionRefundAsync(" ", Data);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PostTransactionRefundAsync(TransactionReference.Value.EncodeGuid(), null);
            }


            [TestMethod]
            public async Task ItShouldCallCreateTransactionRefund()
            {
                this.timestampCreator.Setup(v => v.Now()).Returns(Now);

                this.createTransactionRefund.Setup(v => v.HandleAsync(
                    new CreateTransactionRefundCommand(Requester, TransactionReference, Now, Data.Comment)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.PostTransactionRefundAsync(TransactionReference.Value.EncodeGuid(), Data);

                this.createTransactionRefund.Verify();
            }
        }

        [TestClass]
        public class PostCreditRefundAsync : PaymentsControllerTests
        {
            private static readonly DateTime Now = DateTime.UtcNow;
            private static readonly TransactionReference TransactionReference = TransactionReference.Random();
            private static readonly CreditRefundData Data = new CreditRefundData(123, RefundCreditReason.Fraudulent, "comment");

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
            {
                await this.target.PostCreditRefundAsync(null, Data);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenTransactionReferenceIsWhitespace_ItShouldThrowAnException()
            {
                await this.target.PostCreditRefundAsync(" ", Data);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException))]
            public async Task WhenDataIsNull_ItShouldThrowAnException()
            {
                await this.target.PostCreditRefundAsync(TransactionReference.Value.EncodeGuid(), null);
            }


            [TestMethod]
            public async Task ItShouldCallCreateCreditRefund()
            {
                this.timestampCreator.Setup(v => v.Now()).Returns(Now);

                this.createCreditRefund.Setup(v => v.HandleAsync(
                    new CreateCreditRefundCommand(Requester, TransactionReference, Now, PositiveInt.Parse(Data.RefundCreditAmount), Data.Reason, Data.Comment)))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.PostCreditRefundAsync(TransactionReference.Value.EncodeGuid(), Data);

                this.createCreditRefund.Verify();
            }
        }

        [TestClass]
        public class GetTransactionsAsync : PaymentsControllerTests
        {
            private static readonly UserId UserId = UserId.Random();
            private static readonly DateTime StartTime = DateTime.UtcNow.AddDays(-5);
            private static readonly DateTime EndTime = DateTime.UtcNow;
            private static readonly GetTransactionsResult GetTransactionsResult =
                new GetTransactionsResult(new List<GetTransactionsResult.Item>());

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task WhenDataIsNull_ItShouldGetTransactions()
            {
                this.getTransactions.Setup(
                    v => v.HandleAsync(new GetTransactionsQuery(Requester, null, null, null)))
                    .ReturnsAsync(GetTransactionsResult);

                var result = await this.target.GetTransactionsAsync(null, null, null);

                Assert.AreEqual(GetTransactionsResult, result);
            }

            [TestMethod]
            public async Task WhenDataIsProvided_ItShouldGetTransactions()
            {
                this.getTransactions.Setup(
                    v => v.HandleAsync(new GetTransactionsQuery(Requester, UserId, StartTime, EndTime)))
                    .ReturnsAsync(GetTransactionsResult);

                var result = await this.target.GetTransactionsAsync(UserId.Value.EncodeGuid(), StartTime, EndTime);

                Assert.AreEqual(GetTransactionsResult, result);
            }
        }

        [TestClass]
        public class GetPaymentProcessingLeaseAsync : PaymentsControllerTests
        {
            private static readonly UserId UserId = UserId.Random();
            private static readonly DateTime StartTime = DateTime.UtcNow.AddDays(-5);
            private static readonly DateTime EndTime = DateTime.UtcNow;
            private static readonly GetTransactionsResult GetTransactionsResult =
                new GetTransactionsResult(new List<GetTransactionsResult.Item>());

            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task WhenLeaseIdIsNull_ItShouldGetNewLease()
            {
                var leaseId = Guid.NewGuid();
                this.guidCreator.Setup(v => v.Create()).Returns(leaseId);

                this.blockPaymentProcessing.Setup(
                    v => v.HandleAsync(new BlockPaymentProcessingCommand(Requester, null, leaseId.ToString())))
                    .Returns(Task.FromResult(0)).Verifiable();

                var result = await this.target.GetPaymentProcessingLeaseAsync(null);

                this.blockPaymentProcessing.Verify();

                Assert.AreEqual(
                    new BlockPaymentProcessingResult(
                        (int)BlockPaymentProcessingCommandHandler.LeaseLength.TotalSeconds,
                        leaseId.ToString()),
                    result);
            }

            [TestMethod]
            public async Task WhenLeaseIdIsProvided_ItShouldRenewLease()
            {
                var leaseId = Guid.NewGuid();

                this.blockPaymentProcessing.Setup(
                    v => v.HandleAsync(new BlockPaymentProcessingCommand(Requester, leaseId.ToString(), null)))
                    .Returns(Task.FromResult(0)).Verifiable();

                var result = await this.target.GetPaymentProcessingLeaseAsync(leaseId.ToString());

                this.blockPaymentProcessing.Verify();

                Assert.AreEqual(
                    new BlockPaymentProcessingResult(
                        (int)BlockPaymentProcessingCommandHandler.LeaseLength.TotalSeconds,
                        leaseId.ToString()),
                    result);
            }

            [TestMethod]
            public async Task WhenLeaseAcquisitionFails_ItShouldReturnNull()
            {
                var leaseId = Guid.NewGuid();

                this.blockPaymentProcessing.Setup(
                    v => v.HandleAsync(new BlockPaymentProcessingCommand(Requester, leaseId.ToString(), null)))
                    .Throws(new LeaseConflictException(new Exception()));

                var result = await this.target.GetPaymentProcessingLeaseAsync(leaseId.ToString());

                Assert.IsNull(result);
            }
        }
    }
}