namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Linq;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RefreshTokenIdEncryptionServiceTests
    {
        private static readonly string EncryptedString = "1234abcd1234abcd";

        private static readonly byte[] EncryptedBytes = Convert.FromBase64String(EncryptedString);

        private Mock<IAesEncryptionService> encryptionService;

        private RefreshTokenIdEncryptionService target;

        public virtual void Initialize()
        {
            this.encryptionService = new Mock<IAesEncryptionService>(MockBehavior.Strict);

            this.target = new RefreshTokenIdEncryptionService(this.encryptionService.Object);
        }

        [TestClass]
        public class RefreshTokenIdEncryptionServiceTests_EncryptRefreshTokenId : RefreshTokenIdEncryptionServiceTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void WhenRefreshTokenIdIsNull_ItShouldThrowAnException()
            {
                this.target.EncryptRefreshTokenId(null);
            }

            [TestMethod]
            public void ItShouldEncryptRefreshTokenId()
            {
                var id = Guid.NewGuid();
                this.encryptionService
                    .Setup(v => v.Encrypt(It.Is<byte[]>(b => b.SequenceEqual(id.ToByteArray())), true))
                    .Returns(EncryptedBytes);

                var result = this.target.EncryptRefreshTokenId(new RefreshTokenId(id.EncodeGuid()));

                Assert.AreEqual(EncryptedString, result.Value);
            }
        }

        [TestClass]
        public class RefreshTokenIdEncryptionServiceTests_DecryptRefreshTokenId : RefreshTokenIdEncryptionServiceTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void WhenCalledWithObject_WhenRefreshTokenIdIsNull_ItShouldThrowAnException()
            {
                this.target.DecryptRefreshTokenId(null);
            }

            [TestMethod]
            public void ItShouldDecryptRefreshTokenId()
            {
                var id = Guid.NewGuid();
                this.encryptionService.Setup(v => v.Decrypt(EncryptedBytes, true)).Returns(id.ToByteArray());

                var result = this.target.DecryptRefreshTokenId(new EncryptedRefreshTokenId(EncryptedString));

                Assert.AreEqual(id.EncodeGuid(), result.Value);
            }
        }
    }
}