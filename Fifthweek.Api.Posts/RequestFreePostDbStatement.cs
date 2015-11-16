namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RequestFreePostDbStatement : IRequestFreePostDbStatement
    {
         private static readonly string Sql = string.Format(
          @"BEGIN TRY
                INSERT INTO {0}({1}, {2}, {3})
                    SELECT @UserId, @PostId, @Timestamp
                    WHERE (SELECT COUNT(*) FROM {0} WHERE {1}=@UserId AND {3}=@Timestamp) < @MaximumPosts;
                IF @@ROWCOUNT=0
                    SELECT 0;
                ELSE
                    SELECT 1;
            END TRY
            BEGIN CATCH
                IF ERROR_NUMBER() <> 2601 AND ERROR_NUMBER() <> 2627 /* Unique constraint violation */
                BEGIN
		            DECLARE @errorNumber nchar(5), @errorMessage nvarchar(2048);
		            SELECT
			            @errorNumber = RIGHT('00000' + ERROR_NUMBER(), 5),
			            @errorMessage = @errorNumber + ' ' + ERROR_MESSAGE();
		            RAISERROR (@errorMessage, 16, 1);
                END
                ELSE
                    SELECT 1;
            END CATCH",
            FreePost.Table,
            FreePost.Fields.UserId,
            FreePost.Fields.PostId,
            FreePost.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(UserId requestorId, PostId postId, DateTime timestamp, int maximumPosts)
        {
            requestorId.AssertNotNull("requestorId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                    {
                        PostId = postId.Value,
                        UserId = requestorId.Value,
                        Timestamp = timestamp,
                        MaximumPosts = maximumPosts
                    });

                return result;
            }
        }
    }
}