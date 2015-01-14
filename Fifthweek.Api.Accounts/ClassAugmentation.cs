using System;
using System.Linq;



namespace Fifthweek.Api.Accounts
{
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class AccountRepository 
	{
        public AccountRepository(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext, 
            Fifthweek.Api.Persistence.IUserManager userManager)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }
	}

}
namespace Fifthweek.Api.Accounts.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateAccountSettingsCommand 
	{
        public UpdateAccountSettingsCommand(
            Fifthweek.Api.Identity.Membership.UserId authenticatedUserId, 
            Fifthweek.Api.Identity.Membership.UserId requestedUserId, 
            Fifthweek.Api.Identity.Membership.ValidUsername newUsername, 
            Fifthweek.Api.Identity.Membership.ValidEmail newEmail, 
            Fifthweek.Api.Identity.Membership.ValidPassword newPassword, 
            Fifthweek.Api.FileManagement.FileId newProfileImageId)
        {
            if (authenticatedUserId == null)
            {
                throw new ArgumentNullException("authenticatedUserId");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            if (newUsername == null)
            {
                throw new ArgumentNullException("newUsername");
            }

            if (newEmail == null)
            {
                throw new ArgumentNullException("newEmail");
            }

            if (newPassword == null)
            {
                throw new ArgumentNullException("newPassword");
            }

            if (newProfileImageId == null)
            {
                throw new ArgumentNullException("newProfileImageId");
            }

            this.AuthenticatedUserId = authenticatedUserId;
            this.RequestedUserId = requestedUserId;
            this.NewUsername = newUsername;
            this.NewEmail = newEmail;
            this.NewPassword = newPassword;
            this.NewProfileImageId = newProfileImageId;
        }
	}

}
namespace Fifthweek.Api.Accounts.Commands
{
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	public partial class UpdateAccountSettingsCommandHandler 
	{
        public UpdateAccountSettingsCommandHandler(
            Fifthweek.Api.Accounts.IAccountRepository accountRepository, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity)
        {
            if (accountRepository == null)
            {
                throw new ArgumentNullException("accountRepository");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            this.accountRepository = accountRepository;
            this.fileSecurity = fileSecurity;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.Accounts.Queries;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	public partial class AccountSettingsController 
	{
        public AccountSettingsController(
            Fifthweek.Api.Identity.OAuth.IUserContext userContext, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Accounts.Commands.UpdateAccountSettingsCommand> updateAccountSettings, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Accounts.Queries.GetAccountSettingsQuery,Fifthweek.Api.Accounts.GetAccountSettingsResult> getAccountSettings)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            if (updateAccountSettings == null)
            {
                throw new ArgumentNullException("updateAccountSettings");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            this.userContext = userContext;
            this.updateAccountSettings = updateAccountSettings;
            this.getAccountSettings = getAccountSettings;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using Fifthweek.Api.Core;
	public partial class AccountSettingsResponse 
	{
        public AccountSettingsResponse(
            System.String email, 
            System.String profileImageFileId)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.Email = email;
            this.ProfileImageFileId = profileImageFileId;
        }
	}

}
namespace Fifthweek.Api.Accounts
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsResult 
	{
        public GetAccountSettingsResult(
            Fifthweek.Api.Identity.Membership.Email email, 
            Fifthweek.Api.FileManagement.FileId profileImageFileId)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (profileImageFileId == null)
            {
                throw new ArgumentNullException("profileImageFileId");
            }

            this.Email = email;
            this.ProfileImageFileId = profileImageFileId;
        }
	}

}
namespace Fifthweek.Api.Accounts.Queries
{
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsQuery 
	{
        public GetAccountSettingsQuery(
            Fifthweek.Api.Identity.Membership.UserId authenticatedUserId, 
            Fifthweek.Api.Identity.Membership.UserId requestedUserId)
        {
            if (authenticatedUserId == null)
            {
                throw new ArgumentNullException("authenticatedUserId");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            this.AuthenticatedUserId = authenticatedUserId;
            this.RequestedUserId = requestedUserId;
        }
	}

}
namespace Fifthweek.Api.Accounts.Queries
{
	using System.Threading.Tasks;
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsQueryHandler 
	{
        public GetAccountSettingsQueryHandler(
            Fifthweek.Api.Accounts.IAccountRepository accountRepository)
        {
            if (accountRepository == null)
            {
                throw new ArgumentNullException("accountRepository");
            }

            this.accountRepository = accountRepository;
        }
	}

}

namespace Fifthweek.Api.Accounts.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateAccountSettingsCommand 
	{
		public override string ToString()
        {
			return string.Format("UpdateAccountSettingsCommand({0}, {1}, {2}, {3}, {4}, {5})", this.AuthenticatedUserId == null ? "null" : this.AuthenticatedUserId.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.NewUsername == null ? "null" : this.NewUsername.ToString(), this.NewEmail == null ? "null" : this.NewEmail.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString(), this.NewProfileImageId == null ? "null" : this.NewProfileImageId.ToString());
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

            return this.Equals((UpdateAccountSettingsCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.AuthenticatedUserId != null ? this.AuthenticatedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewUsername != null ? this.NewUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewEmail != null ? this.NewEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewProfileImageId != null ? this.NewProfileImageId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdateAccountSettingsCommand other)
        {
            if (!object.Equals(this.AuthenticatedUserId, other.AuthenticatedUserId))
            {
                return false;
            }

            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
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

            if (!object.Equals(this.NewPassword, other.NewPassword))
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
	public partial class AccountSettingsResponse 
	{
		public override string ToString()
        {
			return string.Format("AccountSettingsResponse(\"{0}\", \"{1}\")", this.Email == null ? "null" : this.Email.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString());
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

            return this.Equals((AccountSettingsResponse)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(AccountSettingsResponse other)
        {
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }

            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Accounts.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdatedAccountSettings 
	{
		public override string ToString()
        {
			return string.Format("UpdatedAccountSettings(\"{0}\", \"{1}\", \"{2}\", \"{3}\")", this.NewUsername == null ? "null" : this.NewUsername.ToString(), this.NewEmail == null ? "null" : this.NewEmail.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString(), this.NewProfileImageId == null ? "null" : this.NewProfileImageId.ToString());
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

            return this.Equals((UpdatedAccountSettings)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NewUsername != null ? this.NewUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewEmail != null ? this.NewEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewProfileImageId != null ? this.NewProfileImageId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdatedAccountSettings other)
        {
            if (!object.Equals(this.NewUsername, other.NewUsername))
            {
                return false;
            }

            if (!object.Equals(this.NewEmail, other.NewEmail))
            {
                return false;
            }

            if (!object.Equals(this.NewPassword, other.NewPassword))
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
namespace Fifthweek.Api.Accounts
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsResult 
	{
		public override string ToString()
        {
			return string.Format("GetAccountSettingsResult({0}, {1})", this.Email == null ? "null" : this.Email.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString());
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

            return this.Equals((GetAccountSettingsResult)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetAccountSettingsResult other)
        {
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }

            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Accounts.Queries
{
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsQuery 
	{
		public override string ToString()
        {
			return string.Format("GetAccountSettingsQuery({0}, {1})", this.AuthenticatedUserId == null ? "null" : this.AuthenticatedUserId.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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

            return this.Equals((GetAccountSettingsQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.AuthenticatedUserId != null ? this.AuthenticatedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetAccountSettingsQuery other)
        {
            if (!object.Equals(this.AuthenticatedUserId, other.AuthenticatedUserId))
            {
                return false;
            }

            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }

            return true;
        }
	}

}

namespace Fifthweek.Api.Accounts.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdatedAccountSettings 
	{
		public ValidUsername NewUsernameObject { get; set; }
		public ValidEmail NewEmailObject { get; set; }
		public ValidPassword NewPasswordObject { get; set; }
	}

	public static partial class UpdatedAccountSettingsExtensions
	{
		public static void Parse(this UpdatedAccountSettings target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (true || !ValidUsername.IsEmpty(target.NewUsername))
			{
				ValidUsername @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidUsername.TryParse(target.NewUsername, out @object, out errorMessages))
				{
					target.NewUsernameObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("NewUsername", modelState);
				}
			}

			if (true || !ValidEmail.IsEmpty(target.NewEmail))
			{
				ValidEmail @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidEmail.TryParse(target.NewEmail, out @object, out errorMessages))
				{
					target.NewEmailObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("NewEmail", modelState);
				}
			}

			if (false || !ValidPassword.IsEmpty(target.NewPassword))
			{
				ValidPassword @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidPassword.TryParse(target.NewPassword, out @object, out errorMessages))
				{
					target.NewPasswordObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("NewPassword", modelState);
				}
			}

			if (!modelStateDictionary.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
			}
		}	
	}
}
namespace Fifthweek.Api.Accounts
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetAccountSettingsDapperResult 
	{
		public Email EmailObject { get; set; }
		public FileId ProfileImageFileIdObject { get; set; }
	}

	public static partial class GetAccountSettingsDapperResultExtensions
	{
		public static void Parse(this GetAccountSettingsDapperResult target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

		    if (target.Email != null)
		    {
                target.EmailObject = new Email(target.Email);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Email", modelState);
            }

		    if (target.ProfileImageFileId != null)
		    {
                target.ProfileImageFileIdObject = new FileId(target.ProfileImageFileId);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("ProfileImageFileId", modelState);
            }

			if (!modelStateDictionary.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
			}
		}	
	}
}
