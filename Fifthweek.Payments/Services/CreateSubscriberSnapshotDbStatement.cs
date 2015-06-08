namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateSubscriberSnapshotDbStatement : ICreateSubscriberSnapshotDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public Task ExecuteAsync(UserId subscriberId)
        {
            return Task.FromResult(0);
        }
    }
}