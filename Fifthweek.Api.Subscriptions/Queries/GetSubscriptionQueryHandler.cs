namespace Fifthweek.Api.Subscriptions.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetSubscriptionQueryHandler : IQueryHandler<GetSubscriptionQuery, GetSubscriptionResult>
    {
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IGetSubscriptionDbStatement getSubscription;

        public async Task<GetSubscriptionResult> HandleAsync(GetSubscriptionQuery query)
        {
            query.AssertNotNull("query");

            var subscription = await this.getSubscription.ExecuteAsync(query.NewSubscriptionId);

            FileInformation headerFileInformation = null;

            if (subscription.HeaderImageFileId != null)
            {
                headerFileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                        subscription.CreatorId,
                        subscription.HeaderImageFileId,
                        FilePurposes.ProfileHeaderImage);
            }

            return new GetSubscriptionResult(
                subscription.SubscriptionId,
                subscription.CreatorId,
                subscription.SubscriptionName,
                subscription.Tagline,
                subscription.Introduction,
                subscription.CreationDate,
                headerFileInformation,
                subscription.Video,
                subscription.Description);
        }
    }
}