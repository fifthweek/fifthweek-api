namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetIsTestUserChannelDbStatement : IGetIsTestUserChannelDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT CASE WHEN EXISTS (
                SELECT * FROM {6} c INNER JOIN {0} blogs ON c.{8}=blogs.{9}
                        INNER JOIN {2} r ON blogs.{1} = r.{3}
                WHERE c.{7}=@ChannelId AND r.{4}='{5}'
              )
              THEN CAST(1 AS BIT)
              ELSE CAST(0 AS BIT) END",
            Blog.Table,
            Blog.Fields.CreatorId,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.UserId,
            FifthweekUserRole.Fields.RoleId,
            FifthweekRole.TestUserId.ToString(),
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            Blog.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(ChannelId channelId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                        {
                            ChannelId = channelId.Value
                        });

                return result;
            }
        }
    }
}