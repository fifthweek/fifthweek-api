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
    public class GetTaxInformationTests
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

        private GetTaxInformation target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<ITaxamoApiKeyRepository>(MockBehavior.Strict);
            this.taxamoService = new Mock<ITaxamoService>(MockBehavior.Strict);

            this.target = new GetTaxInformation(
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
        public async Task WhenCountryDetected_ItShouldReturnResult()
        {
            await this.PerformCountryDetectedTests(UserType.StandardUser);
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_WhenCountryDetected_ItShouldReturnResult()
        {
            await this.PerformCountryDetectedTests(UserType.TestUser);
        }

        [TestMethod]
        public async Task WhenCountryNotDetected_ItShouldReturnResult()
        {
            await this.PerformCountryNotDetectedTest(UserType.StandardUser);
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_WhenCountryNotDetected_ItShouldReturnResult()
        {
            await this.PerformCountryNotDetectedTest(UserType.TestUser);
        }

        private async Task PerformCountryDetectedTests(UserType userType)
        {
            await this.PerformCountryDetectedTest(
                userType,
                new Evidence
                {
                    ByCc = new EvidenceSchema { Used = true },
                    ByIp = new EvidenceSchema { Used = true },
                    ByBilling = null,
                });
            await this.PerformCountryDetectedTest(
                userType,
                new Evidence
                {
                    ByCc = null,
                    ByIp = new EvidenceSchema { Used = true },
                    ByBilling = new EvidenceSchema { Used = true },
                });
            await this.PerformCountryDetectedTest(
                userType,
                new Evidence
                {
                    ByCc = new EvidenceSchema { Used = true },
                    ByIp = null,
                    ByBilling = new EvidenceSchema { Used = true },
                });
            await this.PerformCountryDetectedTest(
                userType,
                new Evidence
                {
                    ByCc = new EvidenceSchema { Used = true },
                    ByIp = new EvidenceSchema { Used = true },
                    ByBilling = new EvidenceSchema { Used = true },
                });
        }

        private async Task PerformCountryDetectedTest(UserType userType, Evidence evidence)
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(userType)).Returns(ApiKey);

            var expectedInput = new CalculateTaxIn
            {
                Transaction = new InputTransaction
                {
                    CurrencyCode = PaymentConstants.Usd,
                    TransactionLines = new List<InputTransactionLine>
                    {
                        new InputTransactionLine
                        {
                            CustomId = CreateTaxamoTransaction.CustomId,
                            Amount = new AmountInMinorDenomination(Amount.Value).ToMajorDenomination()
                        }
                    },
                    BuyerCreditCardPrefix = CreditCardPrefix,
                    BuyerIp = IpAddress,
                    BillingCountryCode = CountryCode,
                    OriginalTransactionKey = OriginalTaxamoTransactionKey
                }
            };

            CalculateTaxIn actualInput = null;
            this.taxamoService.Setup(v => v.CalculateTaxAsync(It.IsAny<CalculateTaxIn>(), ApiKey))
                .Callback<CalculateTaxIn, string>((b, c) => actualInput = b)
                .ReturnsAsync(new CalculateTaxOut
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
                        },
                        Evidence = evidence
                    }
                });

            var result = await this.target.ExecuteAsync(Amount, CountryCode, CreditCardPrefix, IpAddress, OriginalTaxamoTransactionKey, userType);

            Assert.AreEqual(
                new TaxamoCalculationResult(
                    new AmountInMinorDenomination(10),
                    new AmountInMinorDenomination(12),
                    new AmountInMinorDenomination(2),
                    20m,
                    "VAT",
                    "England",
                    "UK",
                    null),
                result);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }

        private async Task PerformCountryNotDetectedTest(UserType userType)
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(userType)).Returns(ApiKey);

            var expectedInput = new CalculateTaxIn
            {
                Transaction = new InputTransaction
                {
                    CurrencyCode = PaymentConstants.Usd,
                    TransactionLines = new List<InputTransactionLine>
                    {
                        new InputTransactionLine
                        {
                            CustomId = CreateTaxamoTransaction.CustomId,
                            Amount = new AmountInMinorDenomination(Amount.Value).ToMajorDenomination()
                        }
                    },
                    BuyerCreditCardPrefix = CreditCardPrefix,
                    BuyerIp = IpAddress,
                    BillingCountryCode = CountryCode,
                    OriginalTransactionKey = OriginalTaxamoTransactionKey
                }
            };

            CalculateTaxIn actualInput = null;
            this.taxamoService.Setup(v => v.CalculateTaxAsync(It.IsAny<CalculateTaxIn>(), ApiKey))
                .Callback<CalculateTaxIn, string>((b, c) => actualInput = b)
                .ReturnsAsync(new CalculateTaxOut
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
                        },
                        Evidence = new Evidence
                        {
                            ByCc = new EvidenceSchema
                            {
                                Used = true
                            },
                            ByIp = new EvidenceSchema
                            {
                                Used = false
                            },
                            ByBilling = null,
                        },
                        Countries = new Countries
                        {
                            ByCc = new CountrySchema
                            {
                                Name = "United Kingdom",
                                Code = "GB"
                            },
                            ByIp = new CountrySchema
                            {
                                Name = "France",
                                Code = "FR"
                            },
                            ByBilling = new CountrySchema
                            {
                                Name = "United States",
                                Code = "US"
                            },
                        }
                    }
                });

            var result = await this.target.ExecuteAsync(Amount, CountryCode, CreditCardPrefix, IpAddress, OriginalTaxamoTransactionKey, userType);

            Assert.AreEqual(
                new TaxamoCalculationResult(
                    new AmountInMinorDenomination(10),
                    new AmountInMinorDenomination(12),
                    new AmountInMinorDenomination(2),
                    20m,
                    "VAT",
                    "England",
                    null,
                    new List<TaxamoCalculationResult.PossibleCountry>
                    {
                        new TaxamoCalculationResult.PossibleCountry("United Kingdom", "GB"),
                        new TaxamoCalculationResult.PossibleCountry("France", "FR"),
                        new TaxamoCalculationResult.PossibleCountry("United States", "US"),
                    }),
                result);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }
    }
}