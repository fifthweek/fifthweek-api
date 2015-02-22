namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    using Microsoft.WindowsAzure.Storage.Blob;

    [AutoConstructor]
    public partial class ThumbnailProcessor : IThumbnailProcessor
    {
        public const string DefaultOutputMimeType = "image/jpeg";
        private const MagickFormat DefaultOutputFormat = MagickFormat.Jpeg;
        private static readonly List<string> SupportedOutputMimeTypes = new List<string> { DefaultOutputMimeType, "image/gif", "image/png" };

        private readonly IImageService imageService;

        public async Task CreateThumbnailSetAsync(
            CreateThumbnailSetMessage message,
            ICloudBlockBlob input,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();
            logger.Info("StartFileProcessor: " + sw.ElapsedMilliseconds);

            if (message.Items == null || message.Items.Count == 0)
            {
                return;
            }

            var cache = this.GetItemsCache(message, cloudStorageAccount);
            logger.Info("GetBlockBlobs: " + sw.ElapsedMilliseconds);
            await this.PopulateExists(message, cancellationToken, cache);
            logger.Info("CheckExists: " + sw.ElapsedMilliseconds);

            if (message.Overwrite == false && cache.Values.All(v => v.Exists))
            {
                return;
            }
            
            using (var inputStream = await input.OpenReadAsync(cancellationToken))
            {
                logger.Info("OpenStream: " + sw.ElapsedMilliseconds);
                using (var image = new MagickImage(inputStream))
                {
                    logger.Info("OpenImage: " + sw.ElapsedMilliseconds);
                    var outputMimeType = image.FormatInfo.MimeType;
                    if (!SupportedOutputMimeTypes.Contains(outputMimeType))
                    {
                        outputMimeType = DefaultOutputMimeType;
                        image.Format = DefaultOutputFormat;
                    }

                    var jobData = new JobData(outputMimeType);

                    logger.Info("Processing: " + sw.ElapsedMilliseconds);
                    await this.ProcessThumbnailItems(message.Items, cache, image, jobData, cancellationToken);
                    logger.Info("ProcessingComplete: " + sw.ElapsedMilliseconds);
                }
            }

            logger.Info("Disposed: " + sw.ElapsedMilliseconds);
        }

        public async Task CreatePoisonThumbnailSetAsync(
            CreateThumbnailSetMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            if (message.Items == null || message.Items.Count == 0)
            {
                return;
            }

            var cache = this.GetItemsCache(message, cloudStorageAccount);
            await this.PopulateExists(message, cancellationToken, cache);

            if (message.Overwrite == false && cache.Values.All(v => v.Exists))
            {
                return;
            }

            // Create a small black 1x1 png.
            using (var image = new MagickImage(new MagickColor(0, 0, 0), 1, 1))
            {
                image.Format = MagickFormat.Png;

                foreach (var itemData in cache.Values)
                {
                    if (itemData.ShouldCreate)
                    {
                        if (itemData.Exists)
                        {
                            await itemData.BlockBlob.FetchAttributesAsync(cancellationToken);
                        }

                        itemData.BlockBlob.Properties.ContentType = image.FormatInfo.MimeType;

                        using (var outputStream = await itemData.BlockBlob.OpenWriteAsync(cancellationToken))
                        {
                            image.Write(outputStream);
                            await Task.Factory.FromAsync(outputStream.BeginCommit(null, null), outputStream.EndCommit);
                        }
                    }
                }
            }
        }

        private async Task ProcessThumbnailItems(
            List<ThumbnailSetItemMessage> items, 
            Dictionary<ThumbnailSetItemMessage, ThumbnailSetItemData> cache, 
            MagickImage image, 
            JobData jobData, 
            CancellationToken cancellationToken)
        {
            if (items == null)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var itemData = cache[item];
                var itemImage = i == items.Count - 1 ? image : image.Clone();

                if (itemData.ShouldCreate)
                {
                    if (itemData.Exists)
                    {
                        await itemData.BlockBlob.FetchAttributesAsync(cancellationToken);
                    }

                    itemData.BlockBlob.Properties.ContentType = jobData.OutputMimeType;

                    using (var outputStream = await itemData.BlockBlob.OpenWriteAsync(cancellationToken))
                    {
                        this.imageService.Resize(itemImage, outputStream, item.Width, item.Height, item.ResizeBehaviour);

                        await Task.Factory.FromAsync(outputStream.BeginCommit(null, null), outputStream.EndCommit);
                    }
                }

                await this.ProcessThumbnailItems(item.Children, cache, itemImage, jobData, cancellationToken);
            }
        }

        private Dictionary<ThumbnailSetItemMessage, ThumbnailSetItemData> GetItemsCache(CreateThumbnailSetMessage message, ICloudStorageAccount cloudStorageAccount)
        {
            var client = cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(message.ContainerName);
            var cache = new Dictionary<ThumbnailSetItemMessage, ThumbnailSetItemData>();
            this.GetItemsCache(container, cache, message.Items);
            return cache;
        }

        private void GetItemsCache(
            ICloudBlobContainer container,
            Dictionary<ThumbnailSetItemMessage, ThumbnailSetItemData> data,
            IEnumerable<ThumbnailSetItemMessage> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                var blockBlob = container.GetBlockBlobReference(item.OutputBlobName);
                data.Add(item, new ThumbnailSetItemData(blockBlob));
                this.GetItemsCache(container, data, item.Children);
            }
        }

        private async Task PopulateExists(CreateThumbnailSetMessage message, CancellationToken cancellationToken, Dictionary<ThumbnailSetItemMessage, ThumbnailSetItemData> cache)
        {
            foreach (var item in cache.Values)
            {
                item.Exists = await item.BlockBlob.ExistsAsync(cancellationToken);
                item.ShouldCreate = message.Overwrite || !item.Exists;
            }
        }


        private class ThumbnailSetItemData
        {
            public ThumbnailSetItemData(ICloudBlockBlob blockBlob)
            {
                this.BlockBlob = blockBlob;
            }

            public ICloudBlockBlob BlockBlob { get; private set; }

            public bool Exists { get; set; }

            public bool ShouldCreate { get; set; }
        }

        private class JobData
        {
            public JobData(string outputMimeType)
            {
                this.OutputMimeType = outputMimeType;
            }

            public string OutputMimeType { get; private set; }
        }
    }
}