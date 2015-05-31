namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlobNameCreatorTests
    {
        private readonly FileId fileId = new FileId(Guid.NewGuid());
        
        private readonly FileId fileId2 = new FileId(Guid.NewGuid());

        private readonly ChannelId channelId = new ChannelId(Guid.NewGuid());

        private readonly ChannelId channelId2 = new ChannelId(Guid.NewGuid());

        private readonly FilePurpose validPublicPurpose = FilePurposes.GetAll().First(v => v.IsPublic);
        private readonly FilePurpose validPrivatePurpose = FilePurposes.GetAll().First(v => !v.IsPublic);

        private BlobLocationGenerator target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new BlobLocationGenerator();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalledWithNullPublicPurpose_ItShouldThrowAnException()
        {
            this.target.GetBlobLocation(this.channelId, this.fileId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalledWithNullFileIdAndPublicPurpose_ItShouldThrowAnException()
        {
            this.target.GetBlobLocation(this.channelId, null, this.validPublicPurpose.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalledWithNullFileIdAndPrivatePurpose_ItShouldThrowAnException()
        {
            this.target.GetBlobLocation(this.channelId, null, this.validPrivatePurpose.Name);
        }

        [TestMethod]
        public void WhenCalledWithNullUserIdAndPublicPurpose_ItShouldNotThrowAnException()
        {
            this.target.GetBlobLocation(this.channelId, this.fileId, this.validPublicPurpose.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalledWithNullUserIdAndPrivatePurpose_ItShouldThrowAnException()
        {
            this.target.GetBlobLocation(null, this.fileId, this.validPrivatePurpose.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void WhenCalledWithUnknownPurpose_ItShouldThrowAnException()
        {
            this.target.GetBlobLocation(this.channelId, this.fileId, "Unknown_Purpose");
        }

        [TestMethod]
        public void ItShouldGenerateValidLocations()
        {
            foreach (var filePurpose in FilePurposes.GetAll())
            {
                this.ItShouldGenerateAUrlFriendlyName(filePurpose.Name);
                this.ItShouldGenerateGuidStyleContainerNames(filePurpose.Name, filePurpose.IsPublic);
                this.ItShouldGenerateEncodeStyleBlobNames(filePurpose.Name);
                this.ItShouldGenerateTheSameOutputGivenTheSameInputs(filePurpose.Name);
                this.ItShouldGenerateDifferentOutputsGivenDifferentInputs(filePurpose.Name, filePurpose.IsPublic);
                this.ItShouldGeneratePublicAndPrivateUrlsCorrectly(filePurpose.Name, filePurpose.IsPublic);
            }
        }

        private void ItShouldGenerateAUrlFriendlyName(string purpose)
        {
            var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);

            var encodedContainerName = HttpUtility.UrlEncode(result.ContainerName);
            var encodedBlobName = HttpUtility.UrlEncode(result.BlobName);

            Assert.AreEqual(encodedContainerName, result.ContainerName);
            Assert.AreEqual(encodedBlobName, result.BlobName);
        }

        private void ItShouldGenerateGuidStyleContainerNames(string purpose, bool isPublic)
        {
            if (!isPublic)
            {
                var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);
                Assert.AreEqual(this.channelId.Value.ToString("N"), result.ContainerName);
            }
        }

        private void ItShouldGenerateEncodeStyleBlobNames(string purpose)
        {
            var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);
            Assert.AreEqual(this.fileId.Value.EncodeGuid(), result.BlobName);
        }

        private void ItShouldGenerateTheSameOutputGivenTheSameInputs(string purpose)
        {
            var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);
            var result2 = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);
            Assert.AreEqual(result2, result);
        }

        private void ItShouldGenerateDifferentOutputsGivenDifferentInputs(string purpose, bool isPublic)
        {
            var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);
            var result2 = this.target.GetBlobLocation(this.channelId, this.fileId2, purpose);
            var result3 = this.target.GetBlobLocation(this.channelId2, this.fileId, purpose);
            
            Assert.AreNotEqual(result2, result);

            if (isPublic)
            {
                Assert.AreEqual(result3, result);
            }
            else
            {
                Assert.AreNotEqual(result3, result);
            }
        }

        private void ItShouldGeneratePublicAndPrivateUrlsCorrectly(string purpose, bool isPublic)
        {
            var result = this.target.GetBlobLocation(this.channelId, this.fileId, purpose);

            if (isPublic)
            {
                Assert.AreEqual(FileManagement.Constants.PublicFileBlobContainerName, result.ContainerName);
            }
            else
            {
                Assert.AreNotEqual(FileManagement.Constants.PublicFileBlobContainerName, result.ContainerName);
            }
        }
    }
}