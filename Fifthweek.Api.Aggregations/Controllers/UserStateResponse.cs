namespace Fifthweek.Api.Aggregations.Controllers
{
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserStateResponse
    {
        [Optional]
        public CreatorStatusData CreatorStatus { get; private set; }
    }
}