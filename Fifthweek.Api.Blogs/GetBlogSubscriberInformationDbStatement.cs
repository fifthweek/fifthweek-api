namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogSubscriberInformationDbStatement : IGetBlogSubscriberInformationDbStatement
    {
        public static readonly string Sql = string.Format(
            @"SELECT u.{8} AS Username, u.{7} AS UserId, u.{11}, cs.{2}, cs.{9}, cs.{10}, fau.{14}, origin.{18},
                    CASE WHEN origin.{17} IS NULL OR origin.{20}={21} THEN 'False' ELSE 'True' END AS HasPaymentDetails
                FROM {0} cs
                INNER JOIN {1} c ON cs.{2}=c.{3}
                INNER JOIN {5} u ON cs.{6}=u.{7}
                LEFT OUTER JOIN {12} fau ON u.{13}=fau.{14} AND fau.{15}=@BlogId
                LEFT OUTER JOIN {16} origin 
                ON u.{7} = origin.{19}
                WHERE c.{4}=@BlogId",
            ChannelSubscription.Table,
            Channel.Table,
            ChannelSubscription.Fields.ChannelId,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            FifthweekUser.Table,
            ChannelSubscription.Fields.UserId,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName,
            ChannelSubscription.Fields.SubscriptionStartDate,
            ChannelSubscription.Fields.AcceptedPrice,
            FifthweekUser.Fields.ProfileImageFileId,
            FreeAccessUser.Table,
            FifthweekUser.Fields.Email,
            FreeAccessUser.Fields.Email,
            FreeAccessUser.Fields.BlogId,
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.PaymentOriginKey,
            UserPaymentOrigin.Fields.PaymentStatus,
            UserPaymentOrigin.Fields.UserId,
            UserPaymentOrigin.Fields.PaymentOriginKeyType,
            (int)PaymentOriginKeyType.None);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetBlogSubscriberInformationDbStatementResult> ExecuteAsync(BlogId blogId)
        {
            blogId.AssertNotNull("blogId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<InternalResult>(
                    Sql,
                    new 
                    {
                        BlogId = blogId.Value,
                    });

                return new GetBlogSubscriberInformationDbStatementResult(
                    result.Select(v =>
                        new GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            new Username(v.Username),
                            new UserId(v.UserId),
                            v.ProfileImageFileId == null ? null : new FileId(v.ProfileImageFileId.Value),
                            new ChannelId(v.ChannelId),
                            DateTime.SpecifyKind(v.SubscriptionStartDate, DateTimeKind.Utc),
                            v.AcceptedPrice,
                            v.Email == null ? null : new Email(v.Email),
                            v.PaymentStatus == null ? PaymentStatus.None : v.PaymentStatus.Value,
                            v.HasPaymentDetails)).ToList());
            }
        }

        public class InternalResult
        {
            public string Username { get; set; }

            public Guid UserId { get; set; }

            public Guid? ProfileImageFileId { get; set; }

            public Guid ChannelId { get; set; }

            public DateTime SubscriptionStartDate { get; set; }

            public int AcceptedPrice { get; set; }

            public string Email { get; set; }

            public PaymentStatus? PaymentStatus { get; set; }

            public bool HasPaymentDetails { get; set; }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class GetBlogSubscriberInformationDbStatementResult
        {
            public IReadOnlyList<Subscriber> Subscribers { get; private set; }

            [AutoConstructor, AutoEqualityMembers]
            public partial class Subscriber
            {
                public Username Username { get; private set; }

                public UserId UserId { get; private set; }

                [Optional]
                public FileId ProfileImageFileId { get; private set; }

                public ChannelId ChannelId { get; private set; }

                public DateTime SubscriptionStartDate { get; private set; }

                public int AcceptedPrice { get; private set; }

                [Optional]
                public Email FreeAccessEmail { get; private set; }

                public PaymentStatus PaymentStatus { get; private set; }

                public bool HasPaymentDetails { get; private set; }
            }
        }
    }
}