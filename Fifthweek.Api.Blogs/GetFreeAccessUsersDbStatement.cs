namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetFreeAccessUsersDbStatement : IGetFreeAccessUsersDbStatement
    {
        private static readonly IReadOnlyList<ChannelId> EmptyChannelIds = new List<ChannelId>().AsReadOnlyList();

        private static readonly string Query = string.Format(
            @"SELECT guest.{3}, u.{4}, u.{5}, channel.{11} AS ChannelId
            FROM {0} guest
            LEFT OUTER JOIN {1} u ON guest.{3} = u.{6}
            LEFT OUTER JOIN {7} subscription ON u.{4} = subscription.{8}
            LEFT OUTER JOIN {10} channel ON subscription.{9} = channel.{11}
            WHERE guest.{2}=@BlogId AND (channel.{12} IS NULL OR channel.{12} = @BlogId)",
            FreeAccessUser.Table,
            FifthweekUser.Table,
            FreeAccessUser.Fields.BlogId,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.Email,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.UserId,
            ChannelSubscription.Fields.ChannelId,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.BlogId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetFreeAccessUsersResult> ExecuteAsync(BlogId blogId)
        {
            blogId.AssertNotNull("blogId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var queryResult = (await connection.QueryAsync<QueryResult>(
                    Query,
                    new { BlogId = blogId.Value })).ToList();

                var result = new List<GetFreeAccessUsersResult.FreeAccessUser>();
                foreach (var groupedItem in queryResult.GroupBy(v => v.Email))
                {
                    var email = groupedItem.Key;
                    var firstItem = groupedItem.First();

                    if (firstItem.Id.HasValue)
                    {
                        var channelIds
                            = firstItem.ChannelId == null
                            ? EmptyChannelIds
                            : groupedItem.Select(v => new ChannelId(v.ChannelId.Value)).ToList();

                        var output = new GetFreeAccessUsersResult.FreeAccessUser(
                            new Email(email),
                            new UserId(firstItem.Id.Value),
                            new Username(firstItem.UserName),
                            channelIds);

                        result.Add(output);
                    }
                    else
                    {
                        var output = new GetFreeAccessUsersResult.FreeAccessUser(
                            new Email(email), 
                            null, 
                            null,
                            EmptyChannelIds);

                        result.Add(output);
                    }
                }

                return new GetFreeAccessUsersResult(result);
            }
        }

        public class QueryResult
        {
            public string Email { get; set; }
            
            public Guid? Id { get; set; }

            public string UserName { get; set; }

            public Guid? ChannelId { get; set; }
        }
    }
}