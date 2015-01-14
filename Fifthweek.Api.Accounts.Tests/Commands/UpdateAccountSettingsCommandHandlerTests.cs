using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Accounts.Tests.Commands
{
    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.FileManagement;

    using Moq;

    [TestClass]
    public class UpdateAccountSettingsCommandHandlerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private Mock<IAccountRepository> accountRepository;

        private Mock<IFileSecurity> fileSecurity;

        private UpdateAccountSettingsCommandHandler target;
    }
}
