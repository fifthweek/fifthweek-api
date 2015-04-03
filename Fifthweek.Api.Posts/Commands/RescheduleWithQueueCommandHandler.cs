﻿namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RescheduleWithQueueCommandHandler : ICommandHandler<RescheduleWithQueueCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ITryGetUnqueuedPostCollectionDbStatement tryGetUnqueuedPostCollection;
        private readonly IMovePostToQueueDbStatement movePostToQueue;

        public async Task HandleAsync(RescheduleWithQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            await this.RescheduleWithQueueAsync(command);
        }

        private async Task RescheduleWithQueueAsync(RescheduleWithQueueCommand command)
        {
            var collectionId = await this.tryGetUnqueuedPostCollection.ExecuteAsync(command.PostId);
            if (collectionId == null)
            {
                // Post is already queued within the collection, or not within a collection.
                return;
            }

            await this.movePostToQueue.ExecuteAsync(command.PostId, collectionId);
        }
    }
}