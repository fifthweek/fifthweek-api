namespace Fifthweek.Api.Identity.Membership.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand(UserId userId, string exampleWork, NormalizedEmail email, NormalizedUsername username, Password password)
        {
            this.UserId = userId;
            this.ExampleWork = exampleWork;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public UserId UserId { get; private set; }

        public string ExampleWork { get; private set; }

        public NormalizedEmail Email { get; private set; }

        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }

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

            return this.Equals((RegisterUserCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.UserId.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(RegisterUserCommand other)
        {
            return this.UserId.Equals(other.UserId) &&
                object.Equals(this.ExampleWork, other.ExampleWork) && 
                object.Equals(this.Email, other.Email) &&
                object.Equals(this.Username, other.Username) && 
                object.Equals(this.Password, other.Password);
        }
    }
}