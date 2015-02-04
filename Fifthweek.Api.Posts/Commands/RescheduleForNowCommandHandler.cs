namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RescheduleForNowCommandHandler : ICommandHandler<RescheduleForNowCommand>
    {
        private static readonly string WhereNotLive = string.Format(
            @"EXISTS (SELECT * 
                      FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                      WHERE {1} = @{1}
                      AND   {2} > @{2})", // Equivalent to `LiveDate > Now`
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(RescheduleForNowCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            await this.RescheduleForNowAsync(command);
        }

        private Task RescheduleForNowAsync(RescheduleForNowCommand command)
        {
            var post = new Post(command.PostId.Value)
            {
                LiveDate = DateTime.UtcNow,
                ScheduledByQueue = false
            };

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                Conditions = new[] { WhereNotLive },
                UpdateMask = Post.Fields.LiveDate | Post.Fields.ScheduledByQueue
            };

            return this.databaseContext.Database.Connection.UpdateAsync(parameters);
        }
    }
}