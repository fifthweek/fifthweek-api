namespace Dexter.Api.Commands
{
    using Dexter.Api.Models;

    public class RegisterInternalUserCommand
    {
        public RegisterInternalUserCommand(InternalRegistrationData internalRegistrationData)
        {
            this.InternalRegistrationData = internalRegistrationData;
        }

        public InternalRegistrationData InternalRegistrationData { get; private set; }
    }
}