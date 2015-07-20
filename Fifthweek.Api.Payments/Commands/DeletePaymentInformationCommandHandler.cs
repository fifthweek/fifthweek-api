namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeletePaymentInformationCommandHandler : ICommandHandler<DeletePaymentInformationCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IDeleteUserPaymentInformationDbStatement deleteUserPaymentInformation;

        public async Task HandleAsync(DeletePaymentInformationCommand command)
        {
            command.AssertNotNull("command");

            await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.UserId);

            await this.deleteUserPaymentInformation.ExecuteAsync(command.UserId);
        }
    }
}