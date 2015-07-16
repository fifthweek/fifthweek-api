namespace Fifthweek.Payments.Tests.Services.Credit.Taxamo
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class CommitTaxamoTransactionTests
    {
        private static readonly TaxamoTransactionResult TaxamoTransactionResult = new TaxamoTransactionResult(
            "transactionKey", new AmountInUsCents(10), new AmountInUsCents(12), new AmountInUsCents(2), 20.0m, "vat", "UK", "UK");

        private static readonly StripeTransactionResult StripeTransactionResult = new StripeTransactionResult(
            DateTime.UtcNow, Guid.NewGuid(), "chargeId");

        private static readonly string ApiKey = "apiKey";

        private Mock<ITaxamoApiKeyRepository> apiKeyRepository = new Mock<ITaxamoApiKeyRepository>();
        private Mock<ITaxamoService> taxamoService = new Mock<ITaxamoService>();

        private CommitTaxamoTransaction target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<ITaxamoApiKeyRepository>(MockBehavior.Strict);
            this.taxamoService = new Mock<ITaxamoService>(MockBehavior.Strict);

            this.target = new CommitTaxamoTransaction(
                this.apiKeyRepository.Object,
                this.taxamoService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionResultIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, StripeTransactionResult, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeTransactionResultIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(TaxamoTransactionResult, null, default(UserType));
        }

        [TestMethod]
        public async Task ItShouldCreateAPayment()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.StandardUser)).Returns(ApiKey);

            var expectedInput = new CreatePaymentIn
            {
                Amount = TaxamoTransactionResult.Amount.ToUsDollars(),
                PaymentTimestamp = StripeTransactionResult.Timestamp.ToIso8601String(),
                PaymentInformation = string.Format(
                    "Reference:{0}, StripeChargeId:{1}",
                    StripeTransactionResult.TransactionReference,
                    StripeTransactionResult.StripeChargeId),
            };

            CreatePaymentIn actualInput = null;
            this.taxamoService.Setup(v => v.CreatePaymentAsync(TaxamoTransactionResult.Key, It.IsAny<CreatePaymentIn>(), ApiKey))
                .Callback<string, CreatePaymentIn, string>((a, b, c) => actualInput = b)
                .ReturnsAsync(new CreatePaymentOut());

            await this.target.ExecuteAsync(TaxamoTransactionResult, StripeTransactionResult, UserType.StandardUser);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }

        [TestMethod]
        public async Task WhenCalledAsTestUser_ItShouldCreateAPayment()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(ApiKey);

            var expectedInput = new CreatePaymentIn
            {
                Amount = TaxamoTransactionResult.Amount.ToUsDollars(),
                PaymentTimestamp = StripeTransactionResult.Timestamp.ToIso8601String(),
                PaymentInformation = string.Format(
                    "Reference:{0}, StripeChargeId:{1}",
                    StripeTransactionResult.TransactionReference,
                    StripeTransactionResult.StripeChargeId),
            };

            CreatePaymentIn actualInput = null;
            this.taxamoService.Setup(v => v.CreatePaymentAsync(TaxamoTransactionResult.Key, It.IsAny<CreatePaymentIn>(), ApiKey))
                .Callback<string, CreatePaymentIn, string>((a, b, c) => actualInput = b)
                .ReturnsAsync(new CreatePaymentOut());

            await this.target.ExecuteAsync(TaxamoTransactionResult, StripeTransactionResult, UserType.TestUser);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }

    }
}