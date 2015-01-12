using System;
using System.Linq;



namespace Fifthweek.Api.Accounts.Commands
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateAccountSettingsCommand 
	{
        public UpdateAccountSettingsCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Identity.Membership.UserId requestedAccount, 
            System.String newUsername, 
            System.String newEmail, 
            Fifthweek.Api.FileManagement.FileId newProfileImageId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedAccount == null)
            {
                throw new ArgumentNullException("requestedAccount");
            }

            this.Requester = requester;
            this.RequestedAccount = requestedAccount;
            this.NewUsername = newUsername;
            this.NewEmail = newEmail;
            this.NewProfileImageId = newProfileImageId;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	public partial class AccountSettingsController 
	{
        public AccountSettingsController(
            Fifthweek.Api.Identity.OAuth.IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.userContext = userContext;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using Fifthweek.Api.Core;
	public partial class AccountSettingsData 
	{
        public AccountSettingsData(
            System.String email, 
            System.String profileImageId)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (profileImageId == null)
            {
                throw new ArgumentNullException("profileImageId");
            }

            this.Email = email;
            this.ProfileImageId = profileImageId;
        }
	}

}

namespace Fifthweek.Api.Accounts.Commands
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateAccountSettingsCommand 
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

            return this.Equals((UpdateAccountSettingsCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedAccount != null ? this.RequestedAccount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewUsername != null ? this.NewUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewEmail != null ? this.NewEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewProfileImageId != null ? this.NewProfileImageId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdateAccountSettingsCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.RequestedAccount, other.RequestedAccount))
            {
                return false;
            }

            if (!object.Equals(this.NewUsername, other.NewUsername))
            {
                return false;
            }

            if (!object.Equals(this.NewEmail, other.NewEmail))
            {
                return false;
            }

            if (!object.Equals(this.NewProfileImageId, other.NewProfileImageId))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using Fifthweek.Api.Core;
	public partial class AccountSettingsData 
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

            return this.Equals((AccountSettingsData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageId != null ? this.ProfileImageId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(AccountSettingsData other)
        {
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }

            if (!object.Equals(this.ProfileImageId, other.ProfileImageId))
            {
                return false;
            }

            return true;
        }
	}

}

