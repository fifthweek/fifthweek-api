namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class SetBacklogPostLiveDateToNowDbStatement : ISetBacklogPostLiveDateToNowDbStatement
    {
        private static readonly string WhereNotLive = string.Format(
            @"EXISTS (SELECT * 
                      FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                      WHERE {1} = @{1}
                      AND   {2} > @{2})", // Equivalent to `LiveDate > Now`
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate);

        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(PostId postId, DateTime now)
        {
            postId.AssertNotNull("postId");
            now.AssertUtc("now");

            var post = new Post(postId.Value)
            {
                LiveDate = now,
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