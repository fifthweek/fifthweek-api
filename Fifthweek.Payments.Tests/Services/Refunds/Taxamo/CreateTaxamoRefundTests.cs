namespace Fifthweek.Payments.Tests.Services.Refunds.Taxamo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class CreateTaxamoRefundTests
    {
        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        private static readonly PositiveInt RefundCreditAmount = PositiveInt.Parse(10);
        private static readonly string ApiKey = "apiKey";

        private Mock<ITaxamoApiKeyRepository> apiKeyRepository = new Mock<ITaxamoApiKeyRepository>();
        private Mock<ITaxamoService> taxamoService = new Mock<ITaxamoService>();

        private CreateTaxamoRefund target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<ITaxamoApiKeyRepository>(MockBehavior.Strict);
            this.taxamoService = new Mock<ITaxamoService>(MockBehavior.Strict);

            this.target = new CreateTaxamoRefund(
                this.apiKeyRepository.Object,
                this.taxamoService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionKeyIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, RefundCreditAmount, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(TaxamoTransactionKey, null, default(UserType));
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

            var expectedInput = new CreateRefundIn
            {
                Amount = AmountInMinorDenomination.Create(RefundCreditAmount).ToMajorDenomination(),
                CustomId = CreateTaxamoTransaction.CustomId
            };

            CreateRefundIn actualInput = null;
            this.taxamoService.Setup(v => v.CreateRefundAsync(TaxamoTransactionKey, It.IsAny<CreateRefundIn>(), ApiKey))
                .Callback<string, CreateRefundIn, string>((a, b, c) => actualInput = b)
                .ReturnsAsync(new CreateRefundOut
                {
                    TotalAmount = 0.12m,
                    TaxAmount = 0.02m,
                });

            var result = await this.target.ExecuteAsync(TaxamoTransactionKey, RefundCreditAmount, userType);

            Assert.AreEqual(
                new CreateTaxamoRefund.TaxamoRefundResult(
                    PositiveInt.Parse(12),
                    NonNegativeInt.Parse(2)),
                result);

            Assert.AreEqual(
                JsonConvert.SerializeObject(expectedInput, Formatting.None),
                JsonConvert.SerializeObject(actualInput, Formatting.None));
        }
    }
}