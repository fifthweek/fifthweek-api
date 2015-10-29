namespace Fifthweek.Api.FileManagement
{
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.FileManagement.FileTasks;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Thumbnail = Fifthweek.Api.FileManagement.FileTasks.Thumbnail;

    public class FilePurposeTasks : IFilePurposeTasks
    {
        private static readonly Dictionary<string, IEnumerable<IFileTask>> Mappings =
            new Dictionary<string, IEnumerable<IFileTask>>();

        static FilePurposeTasks()
        {
            // NOTE: These are retina display resolutions, so twice what you might expect.
            Add(
                FilePurposes.ProfileImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        300, 
                        300,
                        "preview",
                        ResizeBehaviour.CropToAspectRatio),
                    new Thumbnail(
                        216,
                        216,
                        "header",
                        ResizeBehaviour.CropToAspectRatio),
                    new Thumbnail(
                        64,
                        64,
                        "footer",
                        ResizeBehaviour.CropToAspectRatio)));

            Add(
                FilePurposes.ProfileHeaderImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        3000, 
                        1000,
                        "header",
                        ResizeBehaviour.CropToAspectRatio),
                    new Thumbnail( // Having this as a child was causing the image to be cropped incorrectly to 960x160 (using e2e/sample-image.jpg).
                        960,
                        320,
                        "preview",
                        ResizeBehaviour.CropToAspectRatio)));

            const int PostFeedImageThumbnailWidth = 1890;
            const int PostFeedImageThumbnailHeight = 1440;
            Add(
                FilePurposes.PostImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        PostFeedImageThumbnailWidth,
                        PostFeedImageThumbnailHeight,
                        Shared.Constants.PostFeedImageThumbnailName,
                        ResizeBehaviour.MaintainAspectRatio,
                        new Thumbnail(
                            PostFeedImageThumbnailWidth,
                            PostFeedImageThumbnailHeight,
                            Shared.Constants.PostPreviewImageThumbnailName,
                            ResizeBehaviour.MaintainAspectRatio,
                            ProcessingBehaviour.Lighten)),
                    new Thumbnail(
                        600,
                        600,
                        "preview",
                        ResizeBehaviour.MaintainAspectRatio)));

            Add(FilePurposes.PostFile);
        }

        public IEnumerable<IFileTask> GetTasks(string purpose)
        {
            IEnumerable<IFileTask> result;
            return Mappings.TryGetValue(purpose, out result) ? result : Enumerable.Empty<IFileTask>();
        }

        private static void Add(string purpose, params IFileTask[] tasks)
        {
            Mappings.Add(purpose, tasks);
        }
    }
}