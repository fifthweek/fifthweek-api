namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class AddNewFileDbStatement : IAddNewFileDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task ExecuteAsync(
            Shared.FileId fileId,
            UserId userId,
            string fileNameWithoutExtension,
            string fileExtension,
            string purpose,
            DateTime timeStamp)
        {
            fileId.AssertNotNull("fileId");
            userId.AssertNotNull("userId");
            fileNameWithoutExtension.AssertNotNull("fileNameWithoutExtension");
            fileExtension.AssertNotNull("fileExtension");
            purpose.AssertNotNull("purpose");

            var file = new File(
                fileId.Value,
                null,
                userId.Value,
                FileState.WaitingForUpload,
                timeStamp,
                null,
                null,
                null,
                fileNameWithoutExtension,
                fileExtension,
                0L,
                purpose);

            return this.fifthweekDbContext.Database.Connection.InsertAsync(file, true);
        }
    }
}