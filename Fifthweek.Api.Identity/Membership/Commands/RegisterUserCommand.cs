namespace Fifthweek.Api.Identity.Membership.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand(string exampleWork, NormalizedEmail email, string username, string password)
        {
            this.ExampleWork = exampleWork;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public string ExampleWork { get; private set; }

        public NormalizedEmail Email { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

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
                return hashCode;
            }
        }

        protected bool Equals(RegisterUserCommand other)
        {
            return string.Equals(this.ExampleWork, other.ExampleWork) && string.Equals(this.Email, other.Email) && string.Equals(this.Username, other.Username) && string.Equals(this.Password, other.Password);
        }
    }
}