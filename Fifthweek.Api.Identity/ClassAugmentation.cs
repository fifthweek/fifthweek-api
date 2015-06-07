using System;
using System.Linq;

//// Generated on 05/06/2015 14:48:49 (UTC)
//// Mapped solution in 10.26s


namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class ConfirmPasswordResetCommand 
    {
        public ConfirmPasswordResetCommand(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.String token,
            Fifthweek.Api.Identity.Shared.Membership.ValidPassword newPassword)
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
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterInterestCommand 
    {
        public RegisterInterestCommand(
            System.String name,
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail email)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.Name = name;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterInterestCommandHandler 
    {
        public RegisterInterestCommandHandler(
            Fifthweek.Api.Core.IFifthweekActivityReporter activityReporter,
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler)
        {
            if (activityReporter == null)
            {
                throw new ArgumentNullException("activityReporter");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.activityReporter = activityReporter;
            this.exceptionHandler = exceptionHandler;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterUserCommand 
    {
        public RegisterUserCommand(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.String exampleWork,
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail email,
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username,
            Fifthweek.Api.Identity.Shared.Membership.ValidPassword password,
            System.Boolean regiserAsCreator,
            Fifthweek.Api.Identity.Shared.Membership.ValidCreatorName creatorName)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (regiserAsCreator == null)
            {
                throw new ArgumentNullException("regiserAsCreator");
            }

            this.UserId = userId;
            this.ExampleWork = exampleWork;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.RegiserAsCreator = regiserAsCreator;
            this.CreatorName = creatorName;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterUserCommandHandler 
    {
        public RegisterUserCommandHandler(
            Fifthweek.Api.Identity.Membership.IRegisterUserDbStatement registerUser,
            Fifthweek.Api.Identity.Membership.IReservedUsernameService reservedUsernames,
            Fifthweek.Api.Persistence.IUserManager userManager)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
            }

            if (reservedUsernames == null)
            {
                throw new ArgumentNullException("reservedUsernames");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.registerUser = registerUser;
            this.reservedUsernames = reservedUsernames;
            this.userManager = userManager;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RequestPasswordResetCommand 
    {
        public RequestPasswordResetCommand(
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail email,
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username)
        {
            this.Email = email;
            this.Username = username;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RequestPasswordResetCommandHandler 
    {
        public RequestPasswordResetCommandHandler(
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Shared.IHtmlLinter htmlLinter)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (htmlLinter == null)
            {
                throw new ArgumentNullException("htmlLinter");
            }

            this.userManager = userManager;
            this.htmlLinter = htmlLinter;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class SendIdentifiedUserInformationCommand 
    {
        public SendIdentifiedUserInformationCommand(
            System.Boolean isUpdate,
            Fifthweek.Api.Identity.Shared.Membership.Email email,
            System.String name,
            Fifthweek.Api.Identity.Shared.Membership.Username username)
        {
            if (isUpdate == null)
            {
                throw new ArgumentNullException("isUpdate");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.IsUpdate = isUpdate;
            this.Email = email;
            this.Name = name;
            this.Username = username;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class SendIdentifiedUserInformationCommandHandler 
    {
        public SendIdentifiedUserInformationCommandHandler(
            Fifthweek.Api.Core.IFifthweekActivityReporter activityReporter,
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler)
        {
            if (activityReporter == null)
            {
                throw new ArgumentNullException("activityReporter");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.activityReporter = activityReporter;
            this.exceptionHandler = exceptionHandler;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateAccountSettingsCommand 
    {
        public UpdateAccountSettingsCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername newUsername,
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail newEmail,
            System.String newSecurityStamp,
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

            if (newSecurityStamp == null)
            {
                throw new ArgumentNullException("newSecurityStamp");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
            this.NewUsername = newUsername;
            this.NewEmail = newEmail;
            this.NewSecurityStamp = newSecurityStamp;
            this.NewPassword = newPassword;
            this.NewProfileImageId = newProfileImageId;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateAccountSettingsCommandHandler 
    {
        public UpdateAccountSettingsCommandHandler(
            Fifthweek.Api.Identity.Membership.IUpdateAccountSettingsDbStatement updateAccountSettings,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Membership.IReservedUsernameService reservedUsernames)
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

            if (reservedUsernames == null)
            {
                throw new ArgumentNullException("reservedUsernames");
            }

            this.updateAccountSettings = updateAccountSettings;
            this.requesterSecurity = requesterSecurity;
            this.fileSecurity = fileSecurity;
            this.reservedUsernames = reservedUsernames;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateCreatorAccountSettingsCommand 
    {
        public UpdateCreatorAccountSettingsCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
            Fifthweek.Api.Identity.Shared.Membership.ValidCreatorName name)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
            this.Name = name;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateCreatorAccountSettingsCommandHandler 
    {
        public UpdateCreatorAccountSettingsCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.requesterSecurity = requesterSecurity;
            this.userManager = userManager;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class AccountSettingsController 
    {
        public AccountSettingsController(
            Fifthweek.Api.Core.IGuidCreator guidCreator,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.UpdateAccountSettingsCommand> updateAccountSettings,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.GetAccountSettingsQuery,Fifthweek.Api.Identity.Membership.GetAccountSettingsResult> getAccountSettings,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.UpdateCreatorAccountSettingsCommand> updateCreatorAccountSettings)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

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

            if (updateCreatorAccountSettings == null)
            {
                throw new ArgumentNullException("updateCreatorAccountSettings");
            }

            this.guidCreator = guidCreator;
            this.requesterContext = requesterContext;
            this.updateAccountSettings = updateAccountSettings;
            this.getAccountSettings = getAccountSettings;
            this.updateCreatorAccountSettings = updateCreatorAccountSettings;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class MembershipController 
    {
        public MembershipController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.RegisterUserCommand> registerUser,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.RequestPasswordResetCommand> requestPasswordReset,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.ConfirmPasswordResetCommand> confirmPasswordReset,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.RegisterInterestCommand> registerInterest,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.SendIdentifiedUserInformationCommand> sendIdentifiedUserInformation,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.IsUsernameAvailableQuery,System.Boolean> isUsernameAvailable,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.IsPasswordResetTokenValidQuery,System.Boolean> isPasswordResetTokenValid,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
            }

            if (requestPasswordReset == null)
            {
                throw new ArgumentNullException("requestPasswordReset");
            }

            if (confirmPasswordReset == null)
            {
                throw new ArgumentNullException("confirmPasswordReset");
            }

            if (registerInterest == null)
            {
                throw new ArgumentNullException("registerInterest");
            }

            if (sendIdentifiedUserInformation == null)
            {
                throw new ArgumentNullException("sendIdentifiedUserInformation");
            }

            if (isUsernameAvailable == null)
            {
                throw new ArgumentNullException("isUsernameAvailable");
            }

            if (isPasswordResetTokenValid == null)
            {
                throw new ArgumentNullException("isPasswordResetTokenValid");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.registerUser = registerUser;
            this.requestPasswordReset = requestPasswordReset;
            this.confirmPasswordReset = confirmPasswordReset;
            this.registerInterest = registerInterest;
            this.sendIdentifiedUserInformation = sendIdentifiedUserInformation;
            this.isUsernameAvailable = isUsernameAvailable;
            this.isPasswordResetTokenValid = isPasswordResetTokenValid;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class GetAccountSettingsDbStatement 
    {
        public GetAccountSettingsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class GetAccountSettingsDbResult 
    {
        public GetAccountSettingsDbResult(
            Fifthweek.Api.Identity.Shared.Membership.CreatorName name,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            Fifthweek.Api.Identity.Shared.Membership.Email email,
            Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.ProfileImageFileId = profileImageFileId;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class GetAccountSettingsResult 
    {
        public GetAccountSettingsResult(
            Fifthweek.Api.Identity.Shared.Membership.CreatorName name,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            Fifthweek.Api.Identity.Shared.Membership.Email email,
            Fifthweek.Api.FileManagement.Shared.FileInformation profileImage)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.ProfileImage = profileImage;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

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
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetAccountSettingsQueryHandler 
    {
        public GetAccountSettingsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Identity.Membership.IGetAccountSettingsDbStatement getAccountSettings,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            this.requesterSecurity = requesterSecurity;
            this.getAccountSettings = getAccountSettings;
            this.fileInformationAggregator = fileInformationAggregator;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class IsPasswordResetTokenValidQuery 
    {
        public IsPasswordResetTokenValidQuery(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.String token)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            this.UserId = userId;
            this.Token = token;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class IsUsernameAvailableQuery 
    {
        public IsUsernameAvailableQuery(
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            this.Username = username;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class IsUsernameAvailableQueryHandler 
    {
        public IsUsernameAvailableQueryHandler(
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Api.Identity.Membership.IReservedUsernameService reservedUsernames)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (reservedUsernames == null)
            {
                throw new ArgumentNullException("reservedUsernames");
            }

            this.userManager = userManager;
            this.reservedUsernames = reservedUsernames;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class RegisterUserDbStatement 
    {
        public RegisterUserDbStatement(
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.userManager = userManager;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class RequesterContext 
    {
        public RequesterContext(
            Fifthweek.Api.Core.IRequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            this.requestContext = requestContext;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;
    using Fifthweek.Payments.Services;

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
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;
    using Fifthweek.Payments.Services;

    public partial class UpdateAccountSettingsDbStatement 
    {
        public UpdateAccountSettingsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Payments.Services.IRequestSnapshotService requestSnapshot)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (requestSnapshot == null)
            {
                throw new ArgumentNullException("requestSnapshot");
            }

            this.connectionFactory = connectionFactory;
            this.userManager = userManager;
            this.requestSnapshot = requestSnapshot;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class Client 
    {
        public Client(
            Fifthweek.Api.Identity.OAuth.ClientId clientId,
            System.String secret,
            System.String name,
            Fifthweek.Api.Identity.OAuth.ApplicationType applicationType,
            System.Boolean active,
            System.Int32 refreshTokenLifeTimeMinutes,
            System.String allowedOriginRegex,
            System.String defaultAllowedOrigin)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            if (secret == null)
            {
                throw new ArgumentNullException("secret");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (applicationType == null)
            {
                throw new ArgumentNullException("applicationType");
            }

            if (active == null)
            {
                throw new ArgumentNullException("active");
            }

            if (refreshTokenLifeTimeMinutes == null)
            {
                throw new ArgumentNullException("refreshTokenLifeTimeMinutes");
            }

            if (allowedOriginRegex == null)
            {
                throw new ArgumentNullException("allowedOriginRegex");
            }

            if (defaultAllowedOrigin == null)
            {
                throw new ArgumentNullException("defaultAllowedOrigin");
            }

            this.ClientId = clientId;
            this.Secret = secret;
            this.Name = name;
            this.ApplicationType = applicationType;
            this.Active = active;
            this.RefreshTokenLifeTimeMinutes = refreshTokenLifeTimeMinutes;
            this.AllowedOriginRegex = allowedOriginRegex;
            this.DefaultAllowedOrigin = defaultAllowedOrigin;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class ClientId 
    {
        public ClientId(
            System.String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class CreateRefreshTokenCommand 
    {
        public CreateRefreshTokenCommand(
            Fifthweek.Api.Identity.OAuth.RefreshTokenId refreshTokenId,
            Fifthweek.Api.Identity.OAuth.ClientId clientId,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            System.String protectedTicket,
            System.DateTime issuedDate,
            System.DateTime expiresDate)
        {
            if (refreshTokenId == null)
            {
                throw new ArgumentNullException("refreshTokenId");
            }

            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (protectedTicket == null)
            {
                throw new ArgumentNullException("protectedTicket");
            }

            if (issuedDate == null)
            {
                throw new ArgumentNullException("issuedDate");
            }

            if (expiresDate == null)
            {
                throw new ArgumentNullException("expiresDate");
            }

            this.RefreshTokenId = refreshTokenId;
            this.ClientId = clientId;
            this.Username = username;
            this.ProtectedTicket = protectedTicket;
            this.IssuedDate = issuedDate;
            this.ExpiresDate = expiresDate;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class CreateRefreshTokenCommandHandler 
    {
        public CreateRefreshTokenCommandHandler(
            Fifthweek.Api.Identity.OAuth.IUpsertRefreshTokenDbStatement upsertRefreshToken)
        {
            if (upsertRefreshToken == null)
            {
                throw new ArgumentNullException("upsertRefreshToken");
            }

            this.upsertRefreshToken = upsertRefreshToken;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class RemoveRefreshTokenCommand 
    {
        public RemoveRefreshTokenCommand(
            Fifthweek.Api.Identity.OAuth.HashedRefreshTokenId hashedRefreshTokenId)
        {
            if (hashedRefreshTokenId == null)
            {
                throw new ArgumentNullException("hashedRefreshTokenId");
            }

            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class RemoveRefreshTokenCommandHandler 
    {
        public RemoveRefreshTokenCommandHandler(
            Fifthweek.Api.Identity.OAuth.IRemoveRefreshTokenDbStatement removeRefreshToken)
        {
            if (removeRefreshToken == null)
            {
                throw new ArgumentNullException("removeRefreshToken");
            }

            this.removeRefreshToken = removeRefreshToken;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class UpdateLastAccessTokenDateCommand 
    {
        public UpdateLastAccessTokenDateCommand(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.DateTime timestamp,
            Fifthweek.Api.Identity.OAuth.Commands.UpdateLastAccessTokenDateCommand.AccessTokenCreationType creationType)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creationType == null)
            {
                throw new ArgumentNullException("creationType");
            }

            this.UserId = userId;
            this.Timestamp = timestamp;
            this.CreationType = creationType;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class FifthweekAuthorizationServerHandler 
    {
        public FifthweekAuthorizationServerHandler(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.OAuth.Queries.GetValidatedClientQuery,Fifthweek.Api.Identity.OAuth.Client> getValidatedClient,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.OAuth.Queries.GetUserClaimsIdentityQuery,Fifthweek.Api.Identity.OAuth.Queries.UserClaimsIdentity> getUserClaimsIdentity,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.OAuth.Commands.UpdateLastAccessTokenDateCommand> updateLastAccessTokenDate,
            Fifthweek.Api.Core.IOwinExceptionHandler exceptionHandler)
        {
            if (getValidatedClient == null)
            {
                throw new ArgumentNullException("getValidatedClient");
            }

            if (getUserClaimsIdentity == null)
            {
                throw new ArgumentNullException("getUserClaimsIdentity");
            }

            if (updateLastAccessTokenDate == null)
            {
                throw new ArgumentNullException("updateLastAccessTokenDate");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.getValidatedClient = getValidatedClient;
            this.getUserClaimsIdentity = getUserClaimsIdentity;
            this.updateLastAccessTokenDate = updateLastAccessTokenDate;
            this.exceptionHandler = exceptionHandler;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class FifthweekRefreshTokenHandler 
    {
        public FifthweekRefreshTokenHandler(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.OAuth.Commands.CreateRefreshTokenCommand> createRefreshToken,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.OAuth.Commands.RemoveRefreshTokenCommand> removeRefreshToken,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.OAuth.Queries.TryGetRefreshTokenQuery,Fifthweek.Api.Persistence.RefreshToken> tryGetRefreshToken)
        {
            if (createRefreshToken == null)
            {
                throw new ArgumentNullException("createRefreshToken");
            }

            if (removeRefreshToken == null)
            {
                throw new ArgumentNullException("removeRefreshToken");
            }

            if (tryGetRefreshToken == null)
            {
                throw new ArgumentNullException("tryGetRefreshToken");
            }

            this.createRefreshToken = createRefreshToken;
            this.removeRefreshToken = removeRefreshToken;
            this.tryGetRefreshToken = tryGetRefreshToken;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class GetUserAndRolesFromCredentialsDbStatement 
    {
        public GetUserAndRolesFromCredentialsDbStatement(
            Fifthweek.Api.Persistence.IUserManager userManager,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.userManager = userManager;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class GetUserAndRolesFromUserIdDbStatement 
    {
        public GetUserAndRolesFromUserIdDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class HashedRefreshTokenId 
    {
        public HashedRefreshTokenId(
            System.String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class GetUserClaimsIdentityQuery 
    {
        public GetUserClaimsIdentityQuery(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            Fifthweek.Api.Identity.Shared.Membership.Password password,
            System.String authenticationType)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException("authenticationType");
            }

            this.UserId = userId;
            this.Username = username;
            this.Password = password;
            this.AuthenticationType = authenticationType;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class GetUserClaimsIdentityQueryHandler 
    {
        public GetUserClaimsIdentityQueryHandler(
            Fifthweek.Api.Identity.OAuth.IGetUserAndRolesFromCredentialsDbStatement getUserAndRolesFromCredentials,
            Fifthweek.Api.Identity.OAuth.IGetUserAndRolesFromUserIdDbStatement getUserAndRolesFromUserId)
        {
            if (getUserAndRolesFromCredentials == null)
            {
                throw new ArgumentNullException("getUserAndRolesFromCredentials");
            }

            if (getUserAndRolesFromUserId == null)
            {
                throw new ArgumentNullException("getUserAndRolesFromUserId");
            }

            this.getUserAndRolesFromCredentials = getUserAndRolesFromCredentials;
            this.getUserAndRolesFromUserId = getUserAndRolesFromUserId;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class GetValidatedClientQuery 
    {
        public GetValidatedClientQuery(
            Fifthweek.Api.Identity.OAuth.ClientId clientId,
            System.String clientSecret)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class TryGetRefreshTokenQuery 
    {
        public TryGetRefreshTokenQuery(
            Fifthweek.Api.Identity.OAuth.HashedRefreshTokenId hashedRefreshTokenId)
        {
            if (hashedRefreshTokenId == null)
            {
                throw new ArgumentNullException("hashedRefreshTokenId");
            }

            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class TryGetRefreshTokenQueryHandler 
    {
        public TryGetRefreshTokenQueryHandler(
            Fifthweek.Api.Identity.OAuth.ITryGetRefreshTokenDbStatement tryGetRefreshToken)
        {
            if (tryGetRefreshToken == null)
            {
                throw new ArgumentNullException("tryGetRefreshToken");
            }

            this.tryGetRefreshToken = tryGetRefreshToken;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class UserClaimsIdentity 
    {
        public UserClaimsIdentity(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            System.Security.Claims.ClaimsIdentity claimsIdentity)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            this.UserId = userId;
            this.Username = username;
            this.ClaimsIdentity = claimsIdentity;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class RefreshTokenId 
    {
        public RefreshTokenId(
            System.String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class RemoveRefreshTokenDbStatement 
    {
        public RemoveRefreshTokenDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class TryGetRefreshTokenDbStatement 
    {
        public TryGetRefreshTokenDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UpdateUserTimeStampsDbStatement 
    {
        public UpdateUserTimeStampsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UpsertRefreshTokenDbStatement 
    {
        public UpsertRefreshTokenDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UserIdAndRoles 
    {
        public UserIdAndRoles(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.Collections.Generic.IReadOnlyList<System.String> roles)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            this.UserId = userId;
            this.Roles = roles;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UsernameAndRoles 
    {
        public UsernameAndRoles(
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            System.Collections.Generic.IReadOnlyList<System.String> roles)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            this.Username = username;
            this.Roles = roles;
        }
    }
}

namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class ConfirmPasswordResetCommand 
    {
        public override string ToString()
        {
            return string.Format("ConfirmPasswordResetCommand({0}, \"{1}\", {2})", this.UserId == null ? "null" : this.UserId.ToString(), this.Token == null ? "null" : this.Token.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString());
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
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterInterestCommand 
    {
        public override string ToString()
        {
            return string.Format("RegisterInterestCommand(\"{0}\", {1})", this.Name == null ? "null" : this.Name.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((RegisterInterestCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RegisterInterestCommand other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RegisterUserCommand 
    {
        public override string ToString()
        {
            return string.Format("RegisterUserCommand({0}, \"{1}\", {2}, {3}, {4}, {5}, {6})", this.UserId == null ? "null" : this.UserId.ToString(), this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString(), this.RegiserAsCreator == null ? "null" : this.RegiserAsCreator.ToString(), this.CreatorName == null ? "null" : this.CreatorName.ToString());
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
                hashCode = (hashCode * 397) ^ (this.RegiserAsCreator != null ? this.RegiserAsCreator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorName != null ? this.CreatorName.GetHashCode() : 0);
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
        
            if (!object.Equals(this.RegiserAsCreator, other.RegiserAsCreator))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorName, other.CreatorName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class RequestPasswordResetCommand 
    {
        public override string ToString()
        {
            return string.Format("RequestPasswordResetCommand({0}, {1})", this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString());
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class SendIdentifiedUserInformationCommand 
    {
        public override string ToString()
        {
            return string.Format("SendIdentifiedUserInformationCommand({0}, {1}, \"{2}\", {3})", this.IsUpdate == null ? "null" : this.IsUpdate.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Username == null ? "null" : this.Username.ToString());
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
        
            return this.Equals((SendIdentifiedUserInformationCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.IsUpdate != null ? this.IsUpdate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SendIdentifiedUserInformationCommand other)
        {
            if (!object.Equals(this.IsUpdate, other.IsUpdate))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateAccountSettingsCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateAccountSettingsCommand({0}, {1}, {2}, {3}, \"{4}\", {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.NewUsername == null ? "null" : this.NewUsername.ToString(), this.NewEmail == null ? "null" : this.NewEmail.ToString(), this.NewSecurityStamp == null ? "null" : this.NewSecurityStamp.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString(), this.NewProfileImageId == null ? "null" : this.NewProfileImageId.ToString());
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
                hashCode = (hashCode * 397) ^ (this.NewSecurityStamp != null ? this.NewSecurityStamp.GetHashCode() : 0);
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
        
            if (!object.Equals(this.NewSecurityStamp, other.NewSecurityStamp))
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
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using System.Web;
    using System.Text;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateCreatorAccountSettingsCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateCreatorAccountSettingsCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.Name == null ? "null" : this.Name.ToString());
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
        
            return this.Equals((UpdateCreatorAccountSettingsCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateCreatorAccountSettingsCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class GetAccountSettingsDbResult 
    {
        public override string ToString()
        {
            return string.Format("GetAccountSettingsDbResult({0}, {1}, {2}, {3})", this.Name == null ? "null" : this.Name.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString());
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
        
            return this.Equals((GetAccountSettingsDbResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetAccountSettingsDbResult other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
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
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;

    public partial class GetAccountSettingsResult 
    {
        public override string ToString()
        {
            return string.Format("GetAccountSettingsResult({0}, {1}, {2}, {3})", this.Name == null ? "null" : this.Name.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.ProfileImage == null ? "null" : this.ProfileImage.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImage != null ? this.ProfileImage.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetAccountSettingsResult other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImage, other.ProfileImage))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

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
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class IsPasswordResetTokenValidQuery 
    {
        public override string ToString()
        {
            return string.Format("IsPasswordResetTokenValidQuery({0}, \"{1}\")", this.UserId == null ? "null" : this.UserId.ToString(), this.Token == null ? "null" : this.Token.ToString());
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public partial class IsUsernameAvailableQuery 
    {
        public override string ToString()
        {
            return string.Format("IsUsernameAvailableQuery({0})", this.Username == null ? "null" : this.Username.ToString());
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
namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Azure;
    using System.Data.SqlTypes;
    using Fifthweek.Api.Persistence.Identity;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Text;
    using Fifthweek.Payments.Services;

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
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class Client 
    {
        public override string ToString()
        {
            return string.Format("Client({0}, \"{1}\", \"{2}\", {3}, {4}, {5}, \"{6}\", \"{7}\")", this.ClientId == null ? "null" : this.ClientId.ToString(), this.Secret == null ? "null" : this.Secret.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.ApplicationType == null ? "null" : this.ApplicationType.ToString(), this.Active == null ? "null" : this.Active.ToString(), this.RefreshTokenLifeTimeMinutes == null ? "null" : this.RefreshTokenLifeTimeMinutes.ToString(), this.AllowedOriginRegex == null ? "null" : this.AllowedOriginRegex.ToString(), this.DefaultAllowedOrigin == null ? "null" : this.DefaultAllowedOrigin.ToString());
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
        
            return this.Equals((Client)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ClientId != null ? this.ClientId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Secret != null ? this.Secret.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ApplicationType != null ? this.ApplicationType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Active != null ? this.Active.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RefreshTokenLifeTimeMinutes != null ? this.RefreshTokenLifeTimeMinutes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AllowedOriginRegex != null ? this.AllowedOriginRegex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.DefaultAllowedOrigin != null ? this.DefaultAllowedOrigin.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Client other)
        {
            if (!object.Equals(this.ClientId, other.ClientId))
            {
                return false;
            }
        
            if (!object.Equals(this.Secret, other.Secret))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.ApplicationType, other.ApplicationType))
            {
                return false;
            }
        
            if (!object.Equals(this.Active, other.Active))
            {
                return false;
            }
        
            if (!object.Equals(this.RefreshTokenLifeTimeMinutes, other.RefreshTokenLifeTimeMinutes))
            {
                return false;
            }
        
            if (!object.Equals(this.AllowedOriginRegex, other.AllowedOriginRegex))
            {
                return false;
            }
        
            if (!object.Equals(this.DefaultAllowedOrigin, other.DefaultAllowedOrigin))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class ClientId 
    {
        public override string ToString()
        {
            return string.Format("ClientId(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class CreateRefreshTokenCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateRefreshTokenCommand({0}, {1}, {2}, \"{3}\", {4}, {5})", this.RefreshTokenId == null ? "null" : this.RefreshTokenId.ToString(), this.ClientId == null ? "null" : this.ClientId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ProtectedTicket == null ? "null" : this.ProtectedTicket.ToString(), this.IssuedDate == null ? "null" : this.IssuedDate.ToString(), this.ExpiresDate == null ? "null" : this.ExpiresDate.ToString());
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
        
            return this.Equals((CreateRefreshTokenCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.RefreshTokenId != null ? this.RefreshTokenId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ClientId != null ? this.ClientId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProtectedTicket != null ? this.ProtectedTicket.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IssuedDate != null ? this.IssuedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpiresDate != null ? this.ExpiresDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateRefreshTokenCommand other)
        {
            if (!object.Equals(this.RefreshTokenId, other.RefreshTokenId))
            {
                return false;
            }
        
            if (!object.Equals(this.ClientId, other.ClientId))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.ProtectedTicket, other.ProtectedTicket))
            {
                return false;
            }
        
            if (!object.Equals(this.IssuedDate, other.IssuedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpiresDate, other.ExpiresDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class RemoveRefreshTokenCommand 
    {
        public override string ToString()
        {
            return string.Format("RemoveRefreshTokenCommand({0})", this.HashedRefreshTokenId == null ? "null" : this.HashedRefreshTokenId.ToString());
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;

    public partial class UpdateLastAccessTokenDateCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateLastAccessTokenDateCommand({0}, {1}, {2})", this.UserId == null ? "null" : this.UserId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreationType == null ? "null" : this.CreationType.ToString());
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
        
            return this.Equals((UpdateLastAccessTokenDateCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationType != null ? this.CreationType.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateLastAccessTokenDateCommand other)
        {
            if (!object.Equals(this.UserId, other.UserId))
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
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class HashedRefreshTokenId 
    {
        public override string ToString()
        {
            return string.Format("HashedRefreshTokenId(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((HashedRefreshTokenId)obj);
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
        
        protected bool Equals(HashedRefreshTokenId other)
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class GetUserClaimsIdentityQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserClaimsIdentityQuery({0}, {1}, {2}, \"{3}\")", this.UserId == null ? "null" : this.UserId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString(), this.AuthenticationType == null ? "null" : this.AuthenticationType.ToString());
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
        
            return this.Equals((GetUserClaimsIdentityQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AuthenticationType != null ? this.AuthenticationType.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetUserClaimsIdentityQuery other)
        {
            if (!object.Equals(this.UserId, other.UserId))
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
        
            if (!object.Equals(this.AuthenticationType, other.AuthenticationType))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class GetValidatedClientQuery 
    {
        public override string ToString()
        {
            return string.Format("GetValidatedClientQuery({0}, \"{1}\")", this.ClientId == null ? "null" : this.ClientId.ToString(), this.ClientSecret == null ? "null" : this.ClientSecret.ToString());
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
        
            return this.Equals((GetValidatedClientQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ClientId != null ? this.ClientId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ClientSecret != null ? this.ClientSecret.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetValidatedClientQuery other)
        {
            if (!object.Equals(this.ClientId, other.ClientId))
            {
                return false;
            }
        
            if (!object.Equals(this.ClientSecret, other.ClientSecret))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class TryGetRefreshTokenQuery 
    {
        public override string ToString()
        {
            return string.Format("TryGetRefreshTokenQuery({0})", this.HashedRefreshTokenId == null ? "null" : this.HashedRefreshTokenId.ToString());
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
        
            return this.Equals((TryGetRefreshTokenQuery)obj);
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
        
        protected bool Equals(TryGetRefreshTokenQuery other)
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class UserClaimsIdentity 
    {
        public override string ToString()
        {
            return string.Format("UserClaimsIdentity({0}, {1}, {2})", this.UserId == null ? "null" : this.UserId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ClaimsIdentity == null ? "null" : this.ClaimsIdentity.ToString());
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
        
            return this.Equals((UserClaimsIdentity)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ClaimsIdentity != null ? this.ClaimsIdentity.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserClaimsIdentity other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.ClaimsIdentity, other.ClaimsIdentity))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class RefreshTokenId 
    {
        public override string ToString()
        {
            return string.Format("RefreshTokenId(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((RefreshTokenId)obj);
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
        
        protected bool Equals(RefreshTokenId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UserIdAndRoles 
    {
        public override string ToString()
        {
            return string.Format("UserIdAndRoles({0}, {1})", this.UserId == null ? "null" : this.UserId.ToString(), this.Roles == null ? "null" : this.Roles.ToString());
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
        
            return this.Equals((UserIdAndRoles)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Roles != null 
        			? this.Roles.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserIdAndRoles other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (this.Roles != null && other.Roles != null)
            {
                if (!this.Roles.SequenceEqual(other.Roles))
                {
                    return false;    
                }
            }
            else if (this.Roles != null || other.Roles != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Diagnostics;
    using Microsoft.Owin.Security.Infrastructure;
    using Dapper;
    using Microsoft.AspNet.Identity;

    public partial class UsernameAndRoles 
    {
        public override string ToString()
        {
            return string.Format("UsernameAndRoles({0}, {1})", this.Username == null ? "null" : this.Username.ToString(), this.Roles == null ? "null" : this.Roles.ToString());
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
        
            return this.Equals((UsernameAndRoles)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Roles != null 
        			? this.Roles.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UsernameAndRoles other)
        {
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (this.Roles != null && other.Roles != null)
            {
                if (!this.Roles.SequenceEqual(other.Roles))
                {
                    return false;    
                }
            }
            else if (this.Roles != null || other.Roles != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class CreatorInformation 
    {
        public override string ToString()
        {
            return string.Format("CreatorInformation(\"{0}\")", this.Name == null ? "null" : this.Name.ToString());
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
        
            return this.Equals((CreatorInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorInformation other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class IdentifiedUserData 
    {
        public override string ToString()
        {
            return string.Format("IdentifiedUserData({0}, \"{1}\", \"{2}\", \"{3}\")", this.IsUpdate == null ? "null" : this.IsUpdate.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((IdentifiedUserData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.IsUpdate != null ? this.IsUpdate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(IdentifiedUserData other)
        {
            if (!object.Equals(this.IsUpdate, other.IsUpdate))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class PasswordResetConfirmationData 
    {
        public override string ToString()
        {
            return string.Format("PasswordResetConfirmationData({0}, \"{1}\", \"{2}\")", this.UserId == null ? "null" : this.UserId.ToString(), this.NewPassword == null ? "null" : this.NewPassword.ToString(), this.Token == null ? "null" : this.Token.ToString());
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
        
            return this.Equals((PasswordResetConfirmationData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PasswordResetConfirmationData other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.NewPassword, other.NewPassword))
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class PasswordResetRequestData 
    {
        public override string ToString()
        {
            return string.Format("PasswordResetRequestData(\"{0}\", \"{1}\")", this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString());
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
        
            return this.Equals((PasswordResetRequestData)obj);
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class RegisterInterestData 
    {
        public override string ToString()
        {
            return string.Format("RegisterInterestData(\"{0}\", \"{1}\")", this.Name == null ? "null" : this.Name.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((RegisterInterestData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RegisterInterestData other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class RegistrationData 
    {
        public override string ToString()
        {
            return string.Format("RegistrationData(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\")", this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString(), this.CreatorName == null ? "null" : this.CreatorName.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CreatorName != null ? this.CreatorName.GetHashCode() : 0);
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
        
            if (!object.Equals(this.CreatorName, other.CreatorName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class CreatorInformation 
    {
        public class Parsed
        {
            public Parsed(
                ValidCreatorName name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                this.Name = name;
            }
        
            public ValidCreatorName Name { get; private set; }
        }
    }

    public static partial class CreatorInformationExtensions
    {
        public static CreatorInformation.Parsed Parse(this CreatorInformation target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidCreatorName parsed0 = null;
            if (!ValidCreatorName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidCreatorName.TryParse(target.Name, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Name", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Name", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CreatorInformation.Parsed(
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class PasswordResetConfirmationData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                ValidPassword newPassword,
                System.String token)
            {
                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (newPassword == null)
                {
                    throw new ArgumentNullException("newPassword");
                }

                if (token == null)
                {
                    throw new ArgumentNullException("token");
                }

                this.UserId = userId;
                this.NewPassword = newPassword;
                this.Token = token;
            }
        
            public Fifthweek.Api.Identity.Shared.Membership.UserId UserId { get; private set; }
        
            public ValidPassword NewPassword { get; private set; }
        
            public System.String Token { get; private set; }
        }
    }

    public static partial class PasswordResetConfirmationDataExtensions
    {
        public static PasswordResetConfirmationData.Parsed Parse(this PasswordResetConfirmationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidPassword parsed0 = null;
            if (!ValidPassword.IsEmpty(target.NewPassword))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidPassword.TryParse(target.NewPassword, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("NewPassword", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("NewPassword", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new PasswordResetConfirmationData.Parsed(
                target.UserId,
                parsed0,
                target.Token);
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class PasswordResetRequestData 
    {
        public class Parsed
        {
            public Parsed(
                ValidEmail email,
                ValidUsername username)
            {
                this.Email = email;
                this.Username = username;
            }
        
            public ValidEmail Email { get; private set; }
        
            public ValidUsername Username { get; private set; }
        }
    }

    public static partial class PasswordResetRequestDataExtensions
    {
        public static PasswordResetRequestData.Parsed Parse(this PasswordResetRequestData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidEmail parsed0 = null;
            if (!ValidEmail.IsEmpty(target.Email))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidEmail.TryParse(target.Email, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Email", modelState);
                }
            }

            ValidUsername parsed1 = null;
            if (!ValidUsername.IsEmpty(target.Username))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidUsername.TryParse(target.Username, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Username", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new PasswordResetRequestData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class RegisterInterestData 
    {
        public class Parsed
        {
            public Parsed(
                System.String name,
                ValidEmail email)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (email == null)
                {
                    throw new ArgumentNullException("email");
                }

                this.Name = name;
                this.Email = email;
            }
        
            public System.String Name { get; private set; }
        
            public ValidEmail Email { get; private set; }
        }
    }

    public static partial class RegisterInterestDataExtensions
    {
        public static RegisterInterestData.Parsed Parse(this RegisterInterestData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidEmail parsed0 = null;
            if (!ValidEmail.IsEmpty(target.Email))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidEmail.TryParse(target.Email, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Email", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Email", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RegisterInterestData.Parsed(
                target.Name,
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

    public partial class RegistrationData 
    {
        public class Parsed
        {
            public Parsed(
                System.String exampleWork,
                ValidEmail email,
                ValidUsername username,
                ValidPassword password,
                ValidCreatorName creatorName)
            {
                if (email == null)
                {
                    throw new ArgumentNullException("email");
                }

                if (username == null)
                {
                    throw new ArgumentNullException("username");
                }

                if (password == null)
                {
                    throw new ArgumentNullException("password");
                }

                this.ExampleWork = exampleWork;
                this.Email = email;
                this.Username = username;
                this.Password = password;
                this.CreatorName = creatorName;
            }
        
            public System.String ExampleWork { get; private set; }
        
            public ValidEmail Email { get; private set; }
        
            public ValidUsername Username { get; private set; }
        
            public ValidPassword Password { get; private set; }
        
            public ValidCreatorName CreatorName { get; private set; }
        }
    }

    public static partial class RegistrationDataExtensions
    {
        public static RegistrationData.Parsed Parse(this RegistrationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidEmail parsed0 = null;
            if (!ValidEmail.IsEmpty(target.Email))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidEmail.TryParse(target.Email, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Email", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Email", modelState);
            }

            ValidUsername parsed1 = null;
            if (!ValidUsername.IsEmpty(target.Username))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidUsername.TryParse(target.Username, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Username", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Username", modelState);
            }

            ValidPassword parsed2 = null;
            if (!ValidPassword.IsEmpty(target.Password))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidPassword.TryParse(target.Password, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Password", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Password", modelState);
            }

            ValidCreatorName parsed3 = null;
            if (!ValidCreatorName.IsEmpty(target.CreatorName))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed3Errors;
                if (!ValidCreatorName.TryParse(target.CreatorName, out parsed3, out parsed3Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed3Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("CreatorName", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RegistrationData.Parsed(
                target.ExampleWork,
                parsed0,
                parsed1,
                parsed2,
                parsed3);
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Web.Http.Description;
    using Fifthweek.Api.Blogs.Shared;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Shared;

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
            if (!ValidUsername.IsEmpty(target.NewUsername))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidUsername.TryParse(target.NewUsername, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("NewUsername", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("NewUsername", modelState);
            }

            ValidEmail parsed1 = null;
            if (!ValidEmail.IsEmpty(target.NewEmail))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidEmail.TryParse(target.NewEmail, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("NewEmail", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("NewEmail", modelState);
            }

            ValidPassword parsed2 = null;
            if (!ValidPassword.IsEmpty(target.NewPassword))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidPassword.TryParse(target.NewPassword, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
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


