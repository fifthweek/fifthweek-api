namespace Fifthweek.Api.FileManagement
{
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.FileManagement.FileTasks;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Thumbnail = Fifthweek.Api.FileManagement.FileTasks.Thumbnail;
    using ThumbnailSetFileTask = Fifthweek.Api.FileManagement.FileTasks.ThumbnailSetFileTask;

    public class FilePurposeTasks : IFilePurposeTasks
    {
        private readonly Dictionary<string, IEnumerable<IFileTask>> mappings =
            new Dictionary<string, IEnumerable<IFileTask>>();

        public FilePurposeTasks()
        {
            this.Add(
                FilePurposes.ProfileImage,
                new ThumbnailSetFileTask(
                    new Thumbnail(
                        300, 
                        300, 
                        ResizeBehaviour.CropToAspectRatio, 
                        new Thumbnail(
                            150, 
                            150, 
                            ResizeBehaviour.CropToAspectRatio, 
                            new Thumbnail(
                                128, 
                                128, 
                                ResizeBehaviour.CropToAspectRatio, 
                                new Thumbnail(
                                    64, 
                                    64, 
                                    ResizeBehaviour.CropToAspectRatio,
                                     new Thumbnail(
                                         32, 
                                         32, 
                                         ResizeBehaviour.CropToAspectRatio)))))));

            this.Add(
                FilePurposes.ProfileHeaderImage,
                new ThumbnailSetFileTask(
                    new Thumbnail(
                        1500, 
                        400, 
                        ResizeBehaviour.CropToAspectRatio,
                        new Thumbnail(
                            480, 
                            128, 
                            ResizeBehaviour.CropToAspectRatio))));

            this.Add(
                FilePurposes.PostImage,
                new ThumbnailSetFileTask(
                    new Thumbnail(
                        600, 
                        8000, 
                        ResizeBehaviour.MaintainAspectRatio,
                        new Thumbnail(
                            300, 
                            300, 
                            ResizeBehaviour.MaintainAspectRatio))));
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