namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;

    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AesEncryptionServiceTests
    {
        private AesEncryptionService target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new AesEncryptionService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenEncryptArgumentIsNull_ItShouldThrowAnException()
        {
            this.target.Encrypt(null, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenDecryptArgumentIsNull_ItShouldThrowAnException()
        {
            this.target.Decrypt(null, false);
        }

        [TestMethod]
        public void ItShouldEncryptDataWithRandomInitializationVector()
        {
            var input = Guid.NewGuid().ToByteArray();
            var encrypted1 = this.target.Encrypt(input, false);
            var decrypted1 = this.target.Decrypt(encrypted1, false);
            var encrypted2 = this.target.Encrypt(input, false);
            var decrypted2 = this.target.Decrypt(encrypted2, false);
            Assert.AreEqual(new Guid(input), new Guid(decrypted1));
            Assert.AreEqual(new Guid(input), new Guid(decrypted2));
            Assert.AreNotEqual(encrypted1, encrypted2);
        }

        [TestMethod]
        public void ItShouldEncryptDataWithEmptyInitializationVector()
        {
            var input = Guid.NewGuid().ToByteArray();
            var encrypted = this.target.Encrypt(input, true);
            var decrypted = this.target.Decrypt(encrypted, true);
            Assert.AreEqual(new Guid(input), new Guid(decrypted));
            Assert.IsTrue(Convert.ToBase64String(encrypted).Length <= 48);
        }
    }
}