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

            Add(
                FilePurposes.PostImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        1890,
                        1440,
                        Shared.Constants.PostFeedImageThumbnailName,
                        ResizeBehaviour.MaintainAspectRatio),
                    new Thumbnail(
                        600,
                        600,
                        "preview",
                        ResizeBehaviour.MaintainAspectRatio),
                    new Thumbnail(
                        1200,
                        1200,
                        Shared.Constants.PostPreviewImageThumbnailName,
                        ResizeBehaviour.CropToAspectRatio,
                        ProcessingBehaviour.Lighten)));

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