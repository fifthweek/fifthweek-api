namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;

    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlobNameCreatorTests
    {
        private readonly FileId fileId = new FileId(Guid.Parse("05FD3AE8-AE77-4E8E-8D4A-56C461A78367"));
        
        private readonly FileId fileId2 = new FileId(Guid.Parse("15FD3AE8-AE77-4E8E-8D4A-56C461A78367"));

        [TestMethod]
        public void ItShouldGenerateAUrlFriendlyName()
        {
            var creator = new BlobNameCreator();
            var result = creator.CreateFileName(this.fileId);

            var encoded = HttpUtility.UrlEncode(result);

            Assert.AreEqual(encoded, result);
        }

        [TestMethod]
        public void ItShouldGenerateTheSameOutputGivenTheSameInputs()
        {
            var creator = new BlobNameCreator();
            var result = creator.CreateFileName(this.fileId);
            var result2 = creator.CreateFileName(this.fileId);
            Assert.AreEqual(result2, result);
        }

        [TestMethod]
        public void ItShouldGenerateDifferentOutputsGivenDifferentInputs()
        {
            var creator = new BlobNameCreator();
            var result = creator.CreateFileName(this.fileId);
            var result2 = creator.CreateFileName(this.fileId2);
            Assert.AreNotEqual(result2, result);
        }
    }
}