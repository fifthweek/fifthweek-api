using System;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    public class ConfirmPasswordResetCommand
    {
        public ConfirmPasswordResetCommand(UserId userId, string token, Password newPassword)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            if (newPassword == null)
            {
                throw new ArgumentNullException("newPassword");
            }

            this.UserId = userId;
            this.Token = token;
            this.NewPassword = newPassword;
        }

        public UserId UserId { get; private set; }

        public string Token { get; private set; }

        public Password NewPassword { get; private set; }

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

            return this.Equals((ConfirmPasswordResetCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.UserId != null ? this.UserId.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ConfirmPasswordResetCommand other)
        {
            return object.Equals(this.UserId, other.UserId) && 
                object.Equals(this.Token, other.Token) &&
                object.Equals(this.NewPassword, other.NewPassword);
        }
    }
}