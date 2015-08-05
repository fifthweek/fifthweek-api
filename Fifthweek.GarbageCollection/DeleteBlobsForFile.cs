namespace Fifthweek.GarbageCollection
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteBlobsForFile : IDeleteBlobsForFile
    {
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly ICloudStorageAccount cloudStorageAccount;

        public async Task ExecuteAsync(OrphanedFileData file)
        {
            file.AssertNotNull("file");

            var location = this.blobLocationGenerator.GetBlobLocation(file.ChannelId, file.FileId, file.Purpose);

            var blobClient = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(location.ContainerName);
            var parentBlob = container.GetBlockBlobReference(location.BlobName);

            if (await parentBlob.ExistsAsync())
            {
                await parentBlob.DeleteAsync();
            }

            var blobDirectory = container.GetDirectoryReference(location.BlobName);

            var childBlobs = await blobDirectory.ListCloudBlockBlobsAsync(true);
            foreach (var blob in childBlobs)
            {
                await blob.DeleteAsync();
            }
        }
    }
}