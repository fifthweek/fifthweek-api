namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class SetBacklogPostLiveDateDbStatement : ISetBacklogPostLiveDateDbStatement
    {
        private static readonly string WhereNotLive = string.Format(
            @"EXISTS (SELECT * 
                      FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                      WHERE {1} = @{1}
                      AND   {2} > @Now)",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate);

        private readonly IScheduledDateClippingFunction scheduledDateClipping;
        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(PostId postId, DateTime newTime, DateTime now)
        {
            postId.AssertNotNull("postId");
            newTime.AssertUtc("newTime");
            now.AssertUtc("now");

            var post = new Post(postId.Value)
            {
                LiveDate = this.scheduledDateClipping.Apply(now, newTime),
                ScheduledByQueue = false
            };

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                AdditionalParameters = new { Now = now },
                Conditions = new[] { WhereNotLive },
                UpdateMask = Post.Fields.LiveDate | Post.Fields.ScheduledByQueue
            };

            return this.databaseContext.Database.Connection.UpdateAsync(parameters);
        }
    }
}