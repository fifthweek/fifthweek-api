namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetFileProcessingCompleteDbStatement : ISetFileProcessingCompleteDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(FileId fileId, int dequeueCount, DateTime processingStartedDate, DateTime processingCompletedDate, int renderWidth, int renderHeight)
        {
            fileId.AssertNotNull("fileId");

            var newFile = new File
            {
                State = FileState.ProcessingComplete,
                ProcessingAttempts = dequeueCount,
                ProcessingStartedDate = processingStartedDate,
                ProcessingCompletedDate = processingCompletedDate,
                RenderWidth = renderWidth,
                RenderHeight = renderHeight,
                Id = fileId.Value,
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                const File.Fields States =
                    File.Fields.State | 
                    File.Fields.ProcessingAttempts |
                    File.Fields.ProcessingStartedDate |
                    File.Fields.ProcessingCompletedDate |
                    File.Fields.RenderWidth | 
                    File.Fields.RenderHeight;

                await connection.UpdateAsync(newFile, States);
            }
        }
    }
}