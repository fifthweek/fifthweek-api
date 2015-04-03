namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetSubscriptionDbStatement : IGetSubscriptionDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetSubscriptionDataDbResult> ExecuteAsync(SubscriptionId subscriptionId)
        {
            subscriptionId.AssertNotNull("subscriptionId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var items = await connection.QueryAsync<Subscription>(
                    string.Format(
                        @"SELECT * FROM {0} WHERE {1}=@{1}",
                        Subscription.Table,
                        Subscription.Fields.Id),
                    new { Id = subscriptionId.Value });

                var result = items.SingleOrDefault();

                if (result == null)
                {
                    throw new InvalidOperationException("The subscription " + subscriptionId + " couldn't be found.");
                }

                return new GetSubscriptionDataDbResult(
                    new SubscriptionId(result.Id), 
                    new UserId(result.CreatorId), 
                    new SubscriptionName(result.Name),
                    new Tagline(result.Tagline),
                    new Introduction(result.Introduction),
                    result.CreationDate,
                    result.HeaderImageFileId == null ? null : new FileId(result.HeaderImageFileId.Value),
                    result.ExternalVideoUrl == null ? null : new ExternalVideoUrl(result.ExternalVideoUrl),
                    result.Description == null ? null : new SubscriptionDescription(result.Description));
            }
        }

        [AutoConstructor]
        public partial class GetSubscriptionDataDbResult
        {
            public SubscriptionId SubscriptionId { get; private set; }

            public UserId CreatorId { get; private set; }

            public SubscriptionName SubscriptionName { get; private set; }

            public Tagline Tagline { get; private set; }

            public Introduction Introduction { get; private set; }

            public DateTime CreationDate { get; private set; }

            [Optional]
            public FileId HeaderImageFileId { get; private set; }

            [Optional]
            public ExternalVideoUrl Video { get; private set; }

            [Optional]
            public SubscriptionDescription Description { get; private set; }
        }
    }
}