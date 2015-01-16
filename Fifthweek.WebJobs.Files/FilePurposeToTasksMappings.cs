namespace Fifthweek.WebJobs.Files
{
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    public class FilePurposeToTasksMappings : IFilePurposeToTasksMappings
    {
        private readonly Dictionary<string, IEnumerable<IFileTask>> mappings =
            new Dictionary<string, IEnumerable<IFileTask>>();

        public FilePurposeToTasksMappings()
        {
            this.Add(
                FilePurposes.ProfileImage,
                new ThumbnailFileTask(300, 300, ResizeBehaviour.CropToAspectRatio),
                new ThumbnailFileTask(150, 150, ResizeBehaviour.CropToAspectRatio),
                new ThumbnailFileTask(128, 128, ResizeBehaviour.CropToAspectRatio),
                new ThumbnailFileTask(64, 64, ResizeBehaviour.CropToAspectRatio),
                new ThumbnailFileTask(32, 32, ResizeBehaviour.CropToAspectRatio));

            this.Add(
                FilePurposes.ProfileHeaderImage,
                new ThumbnailFileTask(1500, 400, ResizeBehaviour.CropToAspectRatio),
                new ThumbnailFileTask(480, 128, ResizeBehaviour.CropToAspectRatio));
        }

        public IEnumerable<IFileTask> GetTasks(string purpose)
        {
            IEnumerable<IFileTask> result;
            return this.mappings.TryGetValue(purpose, out result) ? result : Enumerable.Empty<IFileTask>();
        }

        private void Add(string purpose, params IFileTask[] tasks)
        {
            this.mappings.Add(purpose, tasks);
        }
    }
}