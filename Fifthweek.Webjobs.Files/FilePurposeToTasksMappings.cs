namespace Fifthweek.Webjobs.Files
{
    using System.Collections.Generic;

    using Fifthweek.Shared;
    using Fifthweek.Webjobs.Files.Shared;
    using Fifthweek.Webjobs.Thumbnails.Shared;

    public class FilePurposeToTasksMappings
    {
        private readonly Dictionary<string, IEnumerable<IFileTask>> mappings =
            new Dictionary<string, IEnumerable<IFileTask>>();

        public FilePurposeToTasksMappings()
        {
            this.Add(
                FilePurposes.ProfileImage,
                new ThumbnailFileTask(300, 300),
                new ThumbnailFileTask(256, 256),
                new ThumbnailFileTask(128, 128),
                new ThumbnailFileTask(64, 64),
                new ThumbnailFileTask(32, 32));

            this.Add(
                FilePurposes.ProfileHeaderImage,
                new ThumbnailFileTask(1500, 400),
                new ThumbnailFileTask(480, 128));
        }

        private void Add(string purpose, params IFileTask[] tasks)
        {
            this.mappings.Add(purpose, tasks);
        }
    }
}