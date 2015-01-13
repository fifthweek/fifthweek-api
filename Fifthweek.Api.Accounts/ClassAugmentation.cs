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
	using System;
	using System.Linq;
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
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	using System.Threading.Tasks;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	public partial class UpdateAccountSettingsCommand 
	{
        public UpdateAccountSettingsCommand(
            Fifthweek.Api.Identity.Membership.UserId authenticatedUserId, 
            Fifthweek.Api.Identity.Membership.UserId requestedUserId, 
            Fifthweek.Api.Identity.Membership.Username newUsername, 
            Fifthweek.Api.Identity.Membership.Email newEmail, 
            Fifthweek.Api.Identity.Membership.Password newPassword, 
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
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Accounts.Queries;
	public partial class AccountSettingsController 
	{
        public AccountSettingsController(
            Fifthweek.Api.Identity.OAuth.IUserContext userContext, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Accounts.Commands.UpdateAccountSettingsCommand> updateAccountSettings, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Accounts.Queries.GetAccountSettingsQuery,Fifthweek.Api.Accounts.Controllers.AccountSettingsResult> getAccountSettings)
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
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.FileManagement;
	public partial class AccountSettingsResult 
	{
        public AccountSettingsResult(
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
namespace Fifthweek.Api.Accounts.Queries
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Threading.Tasks;
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
	using System;
	using System.Linq;
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Threading.Tasks;
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
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	using System.Threading.Tasks;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	public partial class UpdateAccountSettingsCommandHandler 
	{
        public UpdateAccountSettingsCommandHandler(
            Fifthweek.Api.Accounts.IAccountRepository accountRepository, 
            Fifthweek.Api.FileManagement.IFileRepository fileRepository)
        {
            if (accountRepository == null)
            {
                throw new ArgumentNullException("accountRepository");
            }

            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            this.accountRepository = accountRepository;
            this.fileRepository = fileRepository;
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
	using System.Threading.Tasks;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
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
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.FileManagement;
	public partial class AccountSettingsResult 
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

            return this.Equals((AccountSettingsResult)obj);
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

        protected bool Equals(AccountSettingsResult other)
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
	using System;
	using System.Linq;
	using Fifthweek.Api.Accounts.Controllers;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Threading.Tasks;
	public partial class GetAccountSettingsQuery 
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
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.FileManagement;
	public partial class UpdatedAccountSettings 
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

            return this.Equals((UpdatedAccountSettings)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NewUsernameObject != null ? this.NewUsernameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewEmailObject != null ? this.NewEmailObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPasswordObject != null ? this.NewPasswordObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewUsername != null ? this.NewUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewEmail != null ? this.NewEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewProfileImageId != null ? this.NewProfileImageId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdatedAccountSettings other)
        {
            if (!object.Equals(this.NewUsernameObject, other.NewUsernameObject))
            {
                return false;
            }

            if (!object.Equals(this.NewEmailObject, other.NewEmailObject))
            {
                return false;
            }

            if (!object.Equals(this.NewPasswordObject, other.NewPasswordObject))
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
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Identity.OAuth;
	using Fifthweek.Api.Accounts.Commands;
	using Fifthweek.Api.FileManagement;
	public partial class UpdatedAccountSettings 
	{
		public Username NewUsernameObject { get; set; }
		public Email NewEmailObject { get; set; }
		public Password NewPasswordObject { get; set; }

		public void Parse()
		{
			UpdatedAccountSettingsExtensions.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	public static partial class UpdatedAccountSettingsExtensions
	{
		public static void Parse(UpdatedAccountSettings target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (true || !Username.IsEmpty(target.NewUsername))
			{
				Username @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Username.TryParse(target.NewUsername, out @object, out errorMessages))
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

			if (true || !Email.IsEmpty(target.NewEmail))
			{
				Email @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Email.TryParse(target.NewEmail, out @object, out errorMessages))
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

			if (false || !Password.IsEmpty(target.NewPassword))
			{
				Password @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Password.TryParse(target.NewPassword, out @object, out errorMessages))
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
