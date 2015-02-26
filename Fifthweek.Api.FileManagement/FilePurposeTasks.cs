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
        private readonly Dictionary<string, IEnumerable<IFileTask>> mappings =
            new Dictionary<string, IEnumerable<IFileTask>>();

        public FilePurposeTasks()
        {
            // NOTE: These are retina display resolutions, so twice what you might expect.
            this.Add(
                FilePurposes.ProfileImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        600, 
                        600, 
                        ResizeBehaviour.CropToAspectRatio, 
                        new Thumbnail(
                            300, 
                            300, 
                            ResizeBehaviour.CropToAspectRatio, 
                            new Thumbnail(
                                256, 
                                256, 
                                ResizeBehaviour.CropToAspectRatio, 
                                new Thumbnail(
                                    128, 
                                    128, 
                                    ResizeBehaviour.CropToAspectRatio,
                                     new Thumbnail(
                                         64, 
                                         64, 
                                         ResizeBehaviour.CropToAspectRatio)))))));

            this.Add(
                FilePurposes.ProfileHeaderImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        3000, 
                        800, 
                        ResizeBehaviour.CropToAspectRatio,
                        new Thumbnail(
                            960, 
                            256, 
                            ResizeBehaviour.CropToAspectRatio))));

            this.Add(
                FilePurposes.PostImage,
                new CreateThumbnailsTask(
                    new Thumbnail(
                        1200, 
                        16000, 
                        ResizeBehaviour.MaintainAspectRatio,
                        new Thumbnail(
                            600, 
                            600, 
                            ResizeBehaviour.MaintainAspectRatio,
                            new Thumbnail(
                                332,
                                250,
                                ResizeBehaviour.MaintainAspectRatio)))));
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