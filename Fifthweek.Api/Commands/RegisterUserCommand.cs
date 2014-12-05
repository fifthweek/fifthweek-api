namespace Fifthweek.Api.Commands
{
    using Fifthweek.Api.Models;

    public class RegisterUserCommand
    {
        public RegisterUserCommand(RegistrationData registrationData)
        {
            this.RegistrationData = registrationData;
        }

        public RegistrationData RegistrationData { get; private set; }
    }
}