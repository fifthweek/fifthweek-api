namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpsertNoteDbStatement : IUpsertNoteDbStatement
    {
        private readonly IScheduledDateClippingFunction scheduledDateClipping;
        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(PostId postId, ChannelId channelId, ValidNote note, DateTime? sheduledPostDate, DateTime now)
        {
            postId.AssertNotNull("postId");
            channelId.AssertNotNull("channelId");
            note.AssertNotNull("note");

            if (sheduledPostDate.HasValue)
            {
                sheduledPostDate.Value.AssertUtc("sheduledPostDate");    
            }
            
            now.AssertUtc("now");

            var liveDate = this.scheduledDateClipping.Apply(now, sheduledPostDate);
            var post = new Post(
                postId.Value,
                channelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                note.Value,
                false,
                liveDate,
                default(DateTime));

            var updatedFields = Post.Fields.ChannelId | Post.Fields.Comment | Post.Fields.LiveDate;

            return this.databaseContext.Database.Connection.UpdateAsync(post, updatedFields);
        }
    }
}