namespace Fifthweek.Api.Commands
{
    using Fifthweek.Api.Models;

    public class RegisterExternalUserCommand
    {
        public RegisterExternalUserCommand(ExternalRegistrationData externalRegistrationData)
        {
            this.ExternalRegistrationData = externalRegistrationData;
        }

        public ExternalRegistrationData ExternalRegistrationData { get; private set; }
    }
}