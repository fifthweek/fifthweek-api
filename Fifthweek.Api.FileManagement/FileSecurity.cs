namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class FileSecurity : IFileSecurity
    {
        private readonly IFileOwnership fileOwnership;

        public Task<bool> IsUsageAllowedAsync(UserId requester, Shared.FileId fileId)
        {
            requester.AssertNotNull("requester");
            fileId.AssertNotNull("fileId");

            return this.fileOwnership.IsOwnerAsync(requester, fileId);
        }

        public async Task AssertUsageAllowedAsync(UserId requester, Shared.FileId fileId)
        {
            requester.AssertNotNull("requester");
            fileId.AssertNotNull("fileId");

            var isUsageAllowed = await this.IsUsageAllowedAsync(requester, fileId);

            if (!isUsageAllowed)
            {
                throw new UnauthorizedException("The user " + requester.Value + " does not have permission to access file " + fileId.Value);
            }
        }
    }
}