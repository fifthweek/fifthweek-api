namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsPostSubscriberDbStatement : IIsPostSubscriberDbStatement
    {
        private static readonly string DeclareAccountBalance = string.Format(
            @"DECLARE @AccountBalance int = (SELECT COALESCE(({0}), 0));",
            CalculatedAccountBalance.GetUserAccountBalanceQuery("RequestorId", CalculatedAccountBalance.Fields.Amount));

        private static readonly string DeclarePaymentStatus = string.Format(@"
            DECLARE @PaymentStatus int = (SELECT TOP 1 {0} FROM {1} WHERE {2}=@RequestorId);
            DECLARE @IsRetryingPayment bit = CASE WHEN @PaymentStatus>{3} AND @PaymentStatus<{4} THEN 'True' ELSE 'False' END;",
            UserPaymentOrigin.Fields.PaymentStatus,
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.UserId,
            (int)PaymentStatus.None,
            (int)PaymentStatus.Failed);

        private static readonly string SelectAccountBalance = @"
            SELECT @AccountBalance;";

        private static readonly string SubscriptionFilter = string.Format(
            @"
            AND post.{14} IN 
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
            Channel.Table,
            Post.Fields.ChannelId);

        private static readonly string Sql = string.Format(
            @"
            {4}
            IF EXISTS
            (
                SELECT * FROM {0} post
                WHERE post.{1} = @PostId
                AND post.{2} <= @Now
                {3}
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate,
            SubscriptionFilter,
            GetPaymentDeclarations());

        public static string GetPaymentDeclarations()
        {
            return DeclareAccountBalance + DeclarePaymentStatus;
        }

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(UserId userId, PostId postId, DateTime now)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                    {
                        PostId = postId.Value,
                        RequestorId = userId.Value,
                        Now = now
                    });
            }
        }
    }
}