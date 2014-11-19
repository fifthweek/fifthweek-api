namespace Fifthweek.Api.Commands
{
    using Fifthweek.Api.Models;

    public class RegisterInternalUserCommand
    {
        public RegisterInternalUserCommand(InternalRegistrationData internalRegistrationData)
        {
            this.InternalRegistrationData = internalRegistrationData;
        }

        public InternalRegistrationData InternalRegistrationData { get; private set; }
    }
}