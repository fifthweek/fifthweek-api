namespace Dexter.Api.Commands
{
    using Dexter.Api.Models;

    public class RegisterExternalUserCommand
    {
        public RegisterExternalUserCommand(ExternalRegistrationData externalRegistrationData)
        {
            this.ExternalRegistrationData = externalRegistrationData;
        }

        public ExternalRegistrationData ExternalRegistrationData { get; private set; }
    }
}