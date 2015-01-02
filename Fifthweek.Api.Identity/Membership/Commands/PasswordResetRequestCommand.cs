namespace Fifthweek.Api.Identity.Membership.Commands
{
    public class PasswordResetRequestCommand
    {
        public PasswordResetRequestCommand(string email, string username)
        {
            this.Email = email;
            this.Username = username;
        }

        public string Email { get; private set; }

        public string Username { get; private set; }

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

            return this.Equals((PasswordResetRequestCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Email != null ? this.Email.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PasswordResetRequestCommand other)
        {
            return string.Equals(this.Email, other.Email) && 
                string.Equals(this.Username, other.Username);
        }
    }
}