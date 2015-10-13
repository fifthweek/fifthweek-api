namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetNewsfeedDbStatement : IGetNewsfeedDbStatement
    {
        private static readonly string DeclareAccountBalance = string.Format(
            @"DECLARE @AccountBalance int = (SELECT COALESCE(({0}), 0));",
            CalculatedAccountBalance.GetUserAccountBalanceQuery("RequestorId", CalculatedAccountBalance.Fields.Amount));

        private static readonly string SelectAccountBalance = @"
            SELECT @AccountBalance;";

        private static readonly string DeclarePaymentStatus = string.Format(@"
            DECLARE @PaymentStatus int = (SELECT TOP 1 {0} FROM {1} WHERE {2}=@RequestorId);
            DECLARE @IsRetryingPayment bit = CASE WHEN @PaymentStatus>{3} AND @PaymentStatus<{4} THEN 'True' ELSE 'False' END;",
            UserPaymentOrigin.Fields.PaymentStatus,
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.UserId,
            (int)PaymentStatus.None,
            (int)PaymentStatus.Failed);

        private static readonly string SqlSelectPartial = string.Format(@"
                blog.{12} AS BlogId, blog.{13} AS CreatorId, post.{1} AS PostId, 
                post.{2}, {6}, {7}, {3}, post.{21}, 
                [file].{16} as FileName, [file].{17} as FileExtension, [file].{18} as FileSize, 
                image.{16} as ImageName, image.{17} as ImageExtension, image.{18} as ImageSize, 
                image.{19} as ImageRenderWidth, image.{20} as ImageRenderHeight,
                COALESCE(likes.LikesCount, 0) AS LikesCount,
                COALESCE(comments.CommentsCount, 0) AS CommentsCount",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.ChannelId,
            Post.Fields.LiveDate,
            Post.Fields.QueueId,
            Post.Fields.Comment,
            Post.Fields.FileId,
            Post.Fields.ImageId,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            Blog.Table,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            File.Table,
            File.Fields.Id,
            File.Fields.FileNameWithoutExtension,
            File.Fields.FileExtension,
            File.Fields.BlobSizeBytes,
            File.Fields.RenderWidth,
            File.Fields.RenderHeight,
            Post.Fields.CreationDate,
            Like.Fields.PostId,
            Like.Table,
            Comment.Fields.PostId,
            Comment.Table,
            Like.Fields.UserId);

        private static readonly string SqlHasLikedSelect = string.Format(@",
            CASE WHEN likedPosts.{0} IS NOT NULL THEN 1 ELSE 0 END AS HasLikedPost",
            Like.Fields.PostId);

        private static readonly string SqlPreviewInformationSelect = string.Format(@",
            u.{0} AS Username,
            blog.{1} AS BlogName,
            channel.{2} AS ChannelName,
            u.{3}",
            FifthweekUser.Fields.UserName,
            Blog.Fields.Name,
            Channel.Fields.Name,
            FifthweekUser.Fields.ProfileImageFileId);

        private static readonly string SqlFromPartial = string.Format(@"
            FROM {0} post
            INNER JOIN  {8} channel ON post.{2} = channel.{9}
            INNER JOIN  {11} blog ON channel.{10} = blog.{12}
            LEFT OUTER JOIN {14} [file] ON post.{6} = [file].{15}
            LEFT OUTER JOIN {14} image ON post.{7} = image.{15}
            LEFT OUTER JOIN (SELECT {22}, COUNT(*) AS LikesCount FROM {23} GROUP BY {22}) likes ON post.{1} = likes.{22}
            LEFT OUTER JOIN (SELECT {24}, COUNT(*) AS CommentsCount FROM {25} GROUP BY {24}) comments ON post.{1} = comments.{24}",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.ChannelId,
            Post.Fields.LiveDate,
            Post.Fields.QueueId,
            Post.Fields.Comment,
            Post.Fields.FileId,
            Post.Fields.ImageId,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            Blog.Table,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            File.Table,
            File.Fields.Id,
            File.Fields.FileNameWithoutExtension,
            File.Fields.FileExtension,
            File.Fields.BlobSizeBytes,
            File.Fields.RenderWidth,
            File.Fields.RenderHeight,
            Post.Fields.CreationDate,
            Like.Fields.PostId,
            Like.Table,
            Comment.Fields.PostId,
            Comment.Table,
            Like.Fields.UserId);

        private static readonly string SqlHasLikedFromClause = string.Format(@"
            LEFT OUTER JOIN (SELECT {1} FROM {2} WHERE {3}=@RequestorId) likedPosts ON post.{0} = likedPosts.{1}",
            Post.Fields.Id,
            Like.Fields.PostId,
            Like.Table,
            Like.Fields.UserId);

        private static readonly string SqlPreviewInformationClause = string.Format(@"
            LEFT OUTER JOIN {0} u ON blog.{1}=u.{2}",
            FifthweekUser.Table,
            Blog.Fields.CreatorId,
            FifthweekUser.Fields.Id);

        private static readonly string NowDateFilter = string.Format(@"
            WHERE post.{0} <= @Now",
            Post.Fields.LiveDate);

        private static readonly string SubscriptionFilter = string.Format(
            @"
            AND channel.{0} IN 
            (
                SELECT sub.{3} 
                FROM {2} sub 
                INNER JOIN {13} subChannel ON sub.{3} = subChannel.{0}
                WHERE 
                    sub.{4} = @RequestorId 
                    AND 
                    (
                        ((@AccountBalance > 0 OR @IsRetryingPayment = 1) AND sub.{5} >= subChannel.{6})
                        OR
                        subChannel.{1} IN
                        (
                            SELECT fau.{8} 
                            FROM {7} fau
                            INNER JOIN {10} u ON u.{11} = fau.{9}
                            WHERE u.{12} = @RequestorId
                        )
                    )
            )",
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.UserId,
            ChannelSubscription.Fields.AcceptedPrice,
            Channel.Fields.Price,
            FreeAccessUser.Table,
            FreeAccessUser.Fields.BlogId,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Table,
            FifthweekUser.Fields.Email,
            FifthweekUser.Fields.Id,
            Channel.Table);

        private static readonly string CreatorFilter = string.Format(
            @"
            AND blog.{0} = @CreatorId",
            Blog.Fields.CreatorId);

        private static readonly string ChannelFilter = string.Format(
            @"
            AND channel.{0} IN @RequestedChannelIds",
            Channel.Fields.Id);

        private static readonly string DiscoverableFilter = string.Format(
            @"
            AND channel.{0}=1",
            Channel.Fields.IsDiscoverable);

        private static readonly string PreviewFilter = string.Format(
            @"
            AND channel.{0}=1",
            Channel.Fields.IsVisibleToNonSubscribers);

        private static readonly string SqlEndBackwardsWithOrigin = string.Format(
            @"
            AND post.{0} <= @Origin",
            Post.Fields.LiveDate);

        private static readonly string SqlEndBackwards = string.Format(
            @"
            ORDER BY    post.{0} DESC, post.{1} DESC
            OFFSET      @StartIndex ROWS
            FETCH NEXT  @Count ROWS ONLY",
            Post.Fields.LiveDate,
            Post.Fields.CreationDate);

        private static readonly string SqlEndForwards = string.Format(
            @"
            AND post.{0} > @Origin
            ORDER BY    post.{0} ASC, post.{1} ASC
            OFFSET      @StartIndex ROWS
            FETCH NEXT  @Count ROWS ONLY",
            Post.Fields.LiveDate,
            Post.Fields.CreationDate);

        private static readonly string SqlEnd = ";";

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public static string GetSqlStart(UserId requesterId, PositiveInt maxCommentLength = null)
        {
            bool isPreview = maxCommentLength != null;

            var result = new StringBuilder();

            if (maxCommentLength == null)
            {
                result.Append(string.Format("SELECT {0},", Post.Fields.Comment));
            }
            else
            {
                result.Append(string.Format("SELECT LEFT({0}, {1}) AS Comment,", Post.Fields.Comment, maxCommentLength.Value));
            }

            result.Append(SqlSelectPartial);

            if (requesterId != null)
            {
                result.Append(SqlHasLikedSelect);
            }

            if (isPreview)
            {
                result.Append(SqlPreviewInformationSelect);
            }

            result.Append(SqlFromPartial);

            if (requesterId != null)
            {
                result.Append(SqlHasLikedFromClause);
            }

            if (isPreview)
            {
                result.Append(SqlPreviewInformationClause);
            }

            return result.ToString();
        }

        public static string CreateFilter(
            UserId requestorId,
            UserId creatorId,
            IReadOnlyList<ChannelId> requestedChannelIds,
            DateTime now,
            DateTime origin,
            bool searchForwards,
            NonNegativeInt startIndex,
            PositiveInt count,
            bool isPreview)
        {
            var filter = new StringBuilder();

            var onlyDiscoverable = isPreview;

            filter.Append(NowDateFilter);

            if (requestedChannelIds != null && requestedChannelIds.Count > 0)
            {
                filter.Append(ChannelFilter);
                onlyDiscoverable = false;
            }

            if (creatorId != null)
            {
                filter.Append(CreatorFilter);
                onlyDiscoverable = false;
            }

            if (requestorId != null && !requestorId.Equals(creatorId))
            {
                filter.Append(SubscriptionFilter);
                onlyDiscoverable = false;
            }

            if (onlyDiscoverable)
            {
                filter.Append(DiscoverableFilter);
            }

            if (isPreview)
            {
                filter.Append(PreviewFilter);
            }

            if (searchForwards)
            {
                filter.Append(SqlEndForwards);
            }
            else
            {
                if (now != origin)
                {
                    filter.Append(SqlEndBackwardsWithOrigin);
                }

                filter.Append(SqlEndBackwards);
            }

            return filter.ToString();
        }

        public async Task<GetNewsfeedDbResult> ExecuteAsync(
            UserId requestorId,
            UserId creatorId,
            IReadOnlyList<ChannelId> requestedChannelIds,
            DateTime now,
            DateTime origin,
            bool searchForwards,
            NonNegativeInt startIndex,
            PositiveInt count)
        {
            requestorId.AssertNotNull("requestorId");
            startIndex.AssertNotNull("startIndex");
            count.AssertNotNull("count");

            var parameters = new
            {
                RequestorId = requestorId.Value,
                CreatorId = creatorId == null ? null : (Guid?)creatorId.Value,
                RequestedChannelIds = requestedChannelIds == null ? null : requestedChannelIds.Select(v => v.Value).ToList(),
                Now = now,
                Origin = origin,
                StartIndex = startIndex.Value,
                Count = count.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var query = new StringBuilder();
                query.Append(DeclareAccountBalance);

                if (!requestorId.Equals(creatorId))
                {
                    query.Append(DeclarePaymentStatus);
                }

                query.Append(GetSqlStart(requestorId));

                query.Append(CreateFilter(requestorId, creatorId, requestedChannelIds, now, origin, searchForwards, startIndex, count, false));

                query.Append(SqlEnd);
                query.Append(SelectAccountBalance);

                using (var multi = await connection.QueryMultipleAsync(query.ToString(), parameters))
                {
                    var entities = (await multi.ReadAsync<NewsfeedPost.Builder>()).ToList();
                    
                    ProcessNewsfeedResults(entities);

                    var accountBalance = (await multi.ReadAsync<int>()).Single();

                    return new GetNewsfeedDbResult(entities.Select(_ => _.Build()).ToList(), accountBalance);
                }
            }
        }

        private static void ProcessNewsfeedResults(List<NewsfeedPost.Builder> entities)
        {
            foreach (var entity in entities)
            {
                entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
                entity.CreationDate = DateTime.SpecifyKind(entity.CreationDate, DateTimeKind.Utc);
            }
        }
    }
}