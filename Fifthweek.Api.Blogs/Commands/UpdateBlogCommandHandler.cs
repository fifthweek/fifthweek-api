namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateBlogCommandHandler : ICommandHandler<UpdateBlogCommand>
    {
        private readonly IBlogSecurity blogSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(UpdateBlogCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.blogSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.BlogId);

            if (command.HeaderImageFileId != null)
            {
                await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, command.HeaderImageFileId);
            }

            await this.UpdateBlogAsync(command);
        }

        private async Task UpdateBlogAsync(UpdateBlogCommand command)
        {
            var blog = new Blog(
                command.BlogId.Value,
                default(Guid),
                null,
                command.BlogName == null ? null : command.BlogName.Value,
                command.Introduction == null ? null : command.Introduction.Value,
                command.Description == null ? null : command.Description.Value,
                command.Video == null ? null : command.Video.Value,
                command.HeaderImageFileId == null ? (Guid?)null : command.HeaderImageFileId.Value,
                null,
                default(DateTime));

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(
                    blog,
                    Blog.Fields.Name |
                    Blog.Fields.Introduction |
                    Blog.Fields.Description |
                    Blog.Fields.ExternalVideoUrl |
                    Blog.Fields.HeaderImageFileId);
            }
        }
    }
}