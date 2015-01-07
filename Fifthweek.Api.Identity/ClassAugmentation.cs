
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	public partial class PasswordResetRequestData
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

            return this.Equals((PasswordResetRequestData)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.EmailObj != null ? this.EmailObj.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.UsernameObj != null ? this.UsernameObj.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(PasswordResetRequestData other)
        {
			if (!object.Equals(this.Email, other.Email))
			{
				return false;
			}
			if (!object.Equals(this.Username, other.Username))
			{
				return false;
			}
			if (!object.Equals(this.EmailObj, other.EmailObj))
			{
				return false;
			}
			if (!object.Equals(this.UsernameObj, other.UsernameObj))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	public partial class PasswordResetConfirmationData
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

            return this.Equals((PasswordResetConfirmationData)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.UserIdObj != null ? this.UserIdObj.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.NewPasswordObj != null ? this.NewPasswordObj.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(PasswordResetConfirmationData other)
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
			if (!object.Equals(this.UserIdObj, other.UserIdObj))
			{
				return false;
			}
			if (!object.Equals(this.NewPasswordObj, other.NewPasswordObj))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	public partial class RegistrationData
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

            return this.Equals((RegistrationData)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.EmailObj != null ? this.EmailObj.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.UsernameObj != null ? this.UsernameObj.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.PasswordObj != null ? this.PasswordObj.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(RegistrationData other)
        {
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
			if (!object.Equals(this.EmailObj, other.EmailObj))
			{
				return false;
			}
			if (!object.Equals(this.UsernameObj, other.UsernameObj))
			{
				return false;
			}
			if (!object.Equals(this.PasswordObj, other.PasswordObj))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
	public partial class IsPasswordResetTokenValidQuery
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

            return this.Equals((IsPasswordResetTokenValidQuery)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(IsPasswordResetTokenValidQuery other)
        {
			if (!object.Equals(this.UserId, other.UserId))
			{
				return false;
			}
			if (!object.Equals(this.Token, other.Token))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
	public partial class IsUsernameAvailableQuery
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

            return this.Equals((IsUsernameAvailableQuery)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(IsUsernameAvailableQuery other)
        {
			if (!object.Equals(this.Username, other.Username))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
	public partial class GetUserQuery
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

            return this.Equals((GetUserQuery)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(GetUserQuery other)
        {
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
namespace Fifthweek.Api.Identity.Membership
{
	public partial class Password
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

            return this.Equals((Password)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(Password other)
        {
			if (!object.Equals(this.Value, other.Value))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership
{
	public partial class Email
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

            return this.Equals((Email)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(Email other)
        {
			if (!object.Equals(this.Value, other.Value))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership
{
	public partial class UserId
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

            return this.Equals((UserId)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(UserId other)
        {
			if (!object.Equals(this.Value, other.Value))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.Membership
{
	public partial class Username
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

            return this.Equals((Username)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(Username other)
        {
			if (!object.Equals(this.Value, other.Value))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
	public partial class GetRefreshTokenQuery
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

            return this.Equals((GetRefreshTokenQuery)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.HashedRefreshTokenId != null ? this.HashedRefreshTokenId.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(GetRefreshTokenQuery other)
        {
			if (!object.Equals(this.HashedRefreshTokenId, other.HashedRefreshTokenId))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
	public partial class GetClientQuery
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

            return this.Equals((GetClientQuery)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.ClientId != null ? this.ClientId.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(GetClientQuery other)
        {
			if (!object.Equals(this.ClientId, other.ClientId))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
	public partial class RemoveRefreshTokenCommand
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

            return this.Equals((RemoveRefreshTokenCommand)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.HashedRefreshTokenId != null ? this.HashedRefreshTokenId.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(RemoveRefreshTokenCommand other)
        {
			if (!object.Equals(this.HashedRefreshTokenId, other.HashedRefreshTokenId))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
	public partial class AddRefreshTokenCommand
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

            return this.Equals((AddRefreshTokenCommand)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.RefreshToken != null ? this.RefreshToken.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(AddRefreshTokenCommand other)
        {
			if (!object.Equals(this.RefreshToken, other.RefreshToken))
			{
				return false;
			}
			return true;
        }
	}
}
namespace Fifthweek.Api.Identity.OAuth
{
	public partial class ClientId
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

            return this.Equals((ClientId)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(ClientId other)
        {
			if (!object.Equals(this.Value, other.Value))
			{
				return false;
			}
			return true;
        }
	}
}

