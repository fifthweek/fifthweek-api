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

        protected bool Equals(RegisterUserCommand other)
        {
            return Equals(this.RegistrationData, other.RegistrationData);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((RegisterUserCommand) obj);
        }

        public override int GetHashCode()
        {
            return (this.RegistrationData != null ? this.RegistrationData.GetHashCode() : 0);
        }
    }
}