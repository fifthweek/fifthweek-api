namespace Fifthweek.Payments.Tests.Services.Credit.Taxamo
{
    using System;
    using System.Collections.Generic;
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
    public class CreateTaxamoTransactionTests
    {
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);
        private static readonly string CountryCode = "GB";
        private static readonly string CreditCardPrefix = "123456";
        private static readonly string IpAddress = "1.1.1.1";
        private static readonly string OriginalTaxamoTransactionKey = "transactionKey";
        private static readonly string NewTaxamoTransactionKey = "newTransactionKey";
        private static readonly string ApiKey = "apiKey";

        private Mock<ITaxamoApiKeyRepository> apiKeyRepository = new Mock<ITaxamoApiKeyRepository>();
        private Mock<ITaxamoService> taxamoService = new Mock<ITaxamoService>();

        private CreateTaxamoTransaction target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<ITaxamoApiKeyRepository>(MockBehavior.Strict);
            this.taxamoService = new Mock<ITaxamoService>(MockBehavior.Strict);

            this.target = new CreateTaxamoTransaction(
                this.apiKeyRepository.Object,
                this.taxamoService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, CountryCode, CreditCardPrefix, IpAddress, OriginalTaxamoTransactionKey, default(UserType));
        }

        [TestMethod]
        public async Task ItShouldCreateATransaction()
        {
            await this.PerformTest(UserType.StandardUser);
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_ItShouldCreateATransaction()
        {
            await this.PerformTest(UserType.TestUser);
        }

        private async Task PerformTest(UserType userType)
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(userType)).Returns(ApiKey);

            var expectedInput = new CreateTransactionIn
            {
                Transaction = new InputTransaction
                {
                    CurrencyCode = PaymentConstants.Usd,
                    TransactionLines = new List<InputTransactionLine>
                    {
                        new InputTransactionLine
                        {
                            CustomId = CreateTaxamoTransaction.CustomId,
                            Amount = new AmountInUsCents(Amount.Value).ToUsDollars()
                        }
                    },
                    BuyerCreditCardPrefix = CreditCardPrefix,
                    BuyerIp = IpAddress,
                    BillingCountryCode = CountryCode,
                    OriginalTransactionKey = OriginalTaxamoTransactionKey
                }
            };

            CreateTransactionIn actualInput = null;
            this.taxamoService.Setup(v => v.CreateTransactionAsync(It.IsAny<CreateTransactionIn>(), ApiKey))
                .Callback<CreateTransactionIn, string>((b, c) => actualInput = b)
                .ReturnsAsync(new CreateTransactionOut
                {
                    Transaction = new Transaction
                    {
                        Key = NewTaxamoTransactionKey,
                        Amount = 0.1m,
                        TotalAmount = 0.12m,
                        TaxAmount = 0.02m,
                        TaxEntityName = "England",
                        CountryName = "UK",
                        TransactionLines = new List<TransactionLines>
                        {
                            new TransactionLines { TaxRate = 20m, TaxName = "VAT" }
                        }
                    }
                });

            var result = await this.target.ExecuteAsync(Amount, CountryCode, CreditCardPrefix, IpAddress, OriginalTaxamoTransactionKey, userType);

            Assert.AreEqual(
                new TaxamoTransactionResult(
                    NewTaxamoTransactionKey,
                    new AmountInUsCents(10),
                    new AmountInUsCents(12),
                    new AmountInUsCents(2),
                    20m,
                    "VAT",
                    "England",
                    "UK"),
                result);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }
    }
}