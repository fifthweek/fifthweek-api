using System;
using System.Linq;




namespace Fifthweek.Api.Accounts.Commands
{
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsCommand 
    {
        public UpdateAccountSettingsCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername newUsername,
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail newEmail,
            Fifthweek.Api.Identity.Shared.Membership.ValidPassword newPassword,
            Fifthweek.Api.FileManagement.Shared.FileId newProfileImageId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
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

            this.Requester = requester;
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsCommandHandler 
    {
        public UpdateAccountSettingsCommandHandler(
            Fifthweek.Api.Accounts.IUpdateAccountSettingsDbStatement updateAccountSettings,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity)
        {
            if (updateAccountSettings == null)
            {
                throw new ArgumentNullException("updateAccountSettings");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            this.updateAccountSettings = updateAccountSettings;
            this.requesterSecurity = requesterSecurity;
            this.fileSecurity = fileSecurity;
        }
    }
}
namespace Fifthweek.Api.Accounts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class AccountSettingsController 
    {
        public AccountSettingsController(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Accounts.Commands.UpdateAccountSettingsCommand> updateAccountSettings,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Accounts.Queries.GetAccountSettingsQuery,Fifthweek.Api.Accounts.GetAccountSettingsResult> getAccountSettings)
        {
            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (updateAccountSettings == null)
            {
                throw new ArgumentNullException("updateAccountSettings");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            this.requesterContext = requesterContext;
            this.updateAccountSettings = updateAccountSettings;
            this.getAccountSettings = getAccountSettings;
        }
    }
}
namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

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
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class GetAccountSettingsDbStatement 
    {
        public GetAccountSettingsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.databaseContext = databaseContext;
        }
    }
}
namespace Fifthweek.Api.Accounts
{
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class GetAccountSettingsResult 
    {
        public GetAccountSettingsResult(
            Fifthweek.Api.Identity.Shared.Membership.Email email,
            Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId)
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class GetAccountSettingsQuery 
    {
        public GetAccountSettingsQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
        }
    }
}
namespace Fifthweek.Api.Accounts.Queries
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class GetAccountSettingsQueryHandler 
    {
        public GetAccountSettingsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Accounts.IGetAccountSettingsDbStatement getAccountSettings)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            this.requesterSecurity = requesterSecurity;
            this.getAccountSettings = getAccountSettings;
        }
    }
}
namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsDbStatement
    {
        public partial class UpdateAccountSettingsResult 
        {
            public UpdateAccountSettingsResult(
                System.Boolean emailConfirmed)
            {
                if (emailConfirmed == null)
                {
                    throw new ArgumentNullException("emailConfirmed");
                }

                this.EmailConfirmed = emailConfirmed;
            }
        }
    }
}
namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsDbStatement 
    {
        public UpdateAccountSettingsDbStatement(
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
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateAccountSettingsCommand({0}, {1}, {2}, {3}, {4}, {5})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.NewUsername == null ? "null" : this.NewUsername.ToString(), this.NewEmail == null ? "null" : this.NewEmail.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString(), this.NewProfileImageId == null ? "null" : this.NewProfileImageId.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
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
            if (!object.Equals(this.Requester, other.Requester))
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
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class GetAccountSettingsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetAccountSettingsQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetAccountSettingsQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class UpdateAccountSettingsDbStatement
    {
        public partial class UpdateAccountSettingsResult 
        {
            public override string ToString()
            {
                return string.Format("UpdateAccountSettingsResult({0})", this.EmailConfirmed == null ? "null" : this.EmailConfirmed.ToString());
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
            
                return this.Equals((UpdateAccountSettingsResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.EmailConfirmed != null ? this.EmailConfirmed.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(UpdateAccountSettingsResult other)
            {
                if (!object.Equals(this.EmailConfirmed, other.EmailConfirmed))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class UpdatedAccountSettings 
    {
        public class Parsed
        {
            public Parsed(
                ValidUsername newUsername,
                ValidEmail newEmail,
                ValidPassword newPassword,
                System.String newProfileImageId)
            {
                if (newUsername == null)
                {
                    throw new ArgumentNullException("newUsername");
                }

                if (newEmail == null)
                {
                    throw new ArgumentNullException("newEmail");
                }

                if (newProfileImageId == null)
                {
                    throw new ArgumentNullException("newProfileImageId");
                }

                this.NewUsername = newUsername;
                this.NewEmail = newEmail;
                this.NewPassword = newPassword;
                this.NewProfileImageId = newProfileImageId;
            }
        
        	public ValidUsername NewUsername { get; private set; }
        
        	public ValidEmail NewEmail { get; private set; }
        
        	public ValidPassword NewPassword { get; private set; }
        
        	public System.String NewProfileImageId { get; private set; }
        }
    }

    public static partial class UpdatedAccountSettingsExtensions
    {
        public static UpdatedAccountSettings.Parsed Parse(this UpdatedAccountSettings target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
        	ValidUsername parsed0 = null;
            if (true || !ValidUsername.IsEmpty(target.NewUsername))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ValidUsername.TryParse(target.NewUsername, out parsed0, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("NewUsername", modelState);
                }
            }
        
        	ValidEmail parsed1 = null;
            if (true || !ValidEmail.IsEmpty(target.NewEmail))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ValidEmail.TryParse(target.NewEmail, out parsed1, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("NewEmail", modelState);
                }
            }
        
        	ValidPassword parsed2 = null;
            if (false || !ValidPassword.IsEmpty(target.NewPassword))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ValidPassword.TryParse(target.NewPassword, out parsed2, out errorMessages))
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
        
        	return new UpdatedAccountSettings.Parsed(
                parsed0,
                parsed1,
                parsed2,
                target.NewProfileImageId);
        }    
    }
}
