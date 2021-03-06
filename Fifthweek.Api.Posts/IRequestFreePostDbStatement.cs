﻿namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface IRequestFreePostDbStatement
    {
        Task<bool> ExecuteAsync(UserId requestorId, PostId postId, DateTime timestamp, int maximumPosts);
    }
}