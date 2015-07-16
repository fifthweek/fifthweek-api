namespace Fifthweek.Payments.Tests.Services.Credit.Taxamo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;

    using global::Taxamo.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class DeleteTaxamoTransactionTests
    {
        private static readonly string TaxamoTransactionKey = "transactionKey";
        private static readonly string ApiKey = "apiKey";

        private Mock<ITaxamoApiKeyRepository> apiKeyRepository = new Mock<ITaxamoApiKeyRepository>();
        private Mock<ITaxamoService> taxamoService = new Mock<ITaxamoService>();

        private DeleteTaxamoTransaction target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<ITaxamoApiKeyRepository>(MockBehavior.Strict);
            this.taxamoService = new Mock<ITaxamoService>(MockBehavior.Strict);

            this.target = new DeleteTaxamoTransaction(
                this.apiKeyRepository.Object,
                this.taxamoService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, default(UserType));
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

            this.taxamoService.Setup(v => v.CancelTransactionAsync(TaxamoTransactionKey, ApiKey))
                .ReturnsAsync(new CancelTransactionOut())
                .Verifiable();

            await this.target.ExecuteAsync(TaxamoTransactionKey, userType);

            this.taxamoService.Verify();
        }
    }
}