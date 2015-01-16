namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostImageCommandHandler : ICommandHandler<PostImageCommand>
    {
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(PostImageCommand command)
        {
            command.AssertNotNull("command");

            await this.collectionSecurity.AssertPostingAllowedAsync(command.Requester, command.CollectionId);

            await this.CreatePostAsync(command);
        }

        private Task CreatePostAsync(PostImageCommand command)
        {
            var now = DateTime.UtcNow;

            int? queuePosition = null;
            DateTime? liveDate = now;

            if (command.IsQueued)
            {
                liveDate = null;

                throw new NotImplementedException("Need to get queue position from database. Consider atomicity here.");
            }
            else if (command.ScheduledPostDate != null)
            {
                var scheduledPostDate = command.ScheduledPostDate.Value;
                if (scheduledPostDate > now)
                {
                    liveDate = scheduledPostDate;
                }
            }

            throw new NotImplementedException("Need to get channel ID from database.");

//            var post = new Post(
//                command.NewPostId.Value,
//                command.ChannelId.Value,
//                null,
//                command.CollectionId.Value,
//                null,
//                null,
//                null,
//                command.ImageFileId.Value,
//                null,
//                command.Comment == null ? null : command.Comment.Value,
//                queuePosition,
//                liveDate,
//                now);
//
//            return this.databaseContext.Database.Connection.InsertAsync(post);
        }
    }
}