
namespace Fifthweek.Api.Identity.Membership.Commands
{
	public partial class UpdateLastAccessTokenDateCommand
	{
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

            return this.Equals((UpdateLastAccessTokenDateCommand)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.CreationType != null ? this.CreationType.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(UpdateLastAccessTokenDateCommand other)
        {
			if (!object.Equals(this.Username, other.Username))
			{
				return false;
			}
			if (!object.Equals(this.Timestamp, other.Timestamp))
			{
				return false;
			}
			if (!object.Equals(this.CreationType, other.CreationType))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
	public partial class RegisterUserCommand
	{
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
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(RegisterUserCommand other)
        {
			if (!object.Equals(this.UserId, other.UserId))
			{
				return false;
			}
			if (!object.Equals(this.ExampleWork, other.ExampleWork))
			{
				return false;
			}
			if (!object.Equals(this.Email, other.Email))
			{
				return false;
			}
			if (!object.Equals(this.Username, other.Username))
			{
				return false;
			}
			if (!object.Equals(this.Password, other.Password))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
	public partial class RequestPasswordResetCommand
	{
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

            return this.Equals((RequestPasswordResetCommand)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(RequestPasswordResetCommand other)
        {
			if (!object.Equals(this.Email, other.Email))
			{
				return false;
			}
			if (!object.Equals(this.Username, other.Username))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
	public partial class ConfirmPasswordResetCommand
	{
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
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(ConfirmPasswordResetCommand other)
        {
			if (!object.Equals(this.UserId, other.UserId))
			{
				return false;
			}
			if (!object.Equals(this.Token, other.Token))
			{
				return false;
			}
			if (!object.Equals(this.NewPassword, other.NewPassword))
			{
				return false;
			}
			return true;
        }
	}
}
