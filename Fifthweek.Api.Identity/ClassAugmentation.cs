using System;
using System.Linq;




namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class RegisterUserCommand 
    {
        public RegisterUserCommand(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId, 
            System.String exampleWork, 
            Fifthweek.Api.Identity.Shared.Membership.ValidEmail email, 
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username, 
            Fifthweek.Api.Identity.Shared.Membership.ValidPassword password)
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

            this.UserId = userId;
            this.ExampleWork = exampleWork;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    public partial class RegisterUserCommandHandler 
    {
        public RegisterUserCommandHandler(
            Fifthweek.Api.Persistence.IUserManager userManager, 
            Fifthweek.Api.Core.IEventHandler<Fifthweek.Api.Identity.Membership.Events.UserRegisteredEvent> userRegistered)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (userRegistered == null)
            {
                throw new ArgumentNullException("userRegistered");
            }

            this.userManager = userManager;
            this.userRegistered = userRegistered;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UpdateLastAccessTokenDateCommand 
    {
        public UpdateLastAccessTokenDateCommand(
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username, 
            System.DateTime timestamp, 
            Fifthweek.Api.Identity.Membership.Commands.UpdateLastAccessTokenDateCommand.AccessTokenCreationType creationType)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creationType == null)
            {
                throw new ArgumentNullException("creationType");
            }

            this.Username = username;
            this.Timestamp = timestamp;
            this.CreationType = creationType;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class MembershipController 
    {
        public MembershipController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.RegisterUserCommand> registerUser, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.RequestPasswordResetCommand> requestPasswordReset, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.ConfirmPasswordResetCommand> confirmPasswordReset, 
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
            this.isUsernameAvailable = isUsernameAvailable;
            this.isPasswordResetTokenValid = isPasswordResetTokenValid;
            this.guidCreator = guidCreator;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Events
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UserRegisteredEvent 
    {
        public UserRegisteredEvent(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.UserId = userId;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetUserQuery 
    {
        public GetUserQuery(
            Fifthweek.Api.Identity.Shared.Membership.ValidUsername username, 
            Fifthweek.Api.Identity.Shared.Membership.ValidPassword password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            this.Username = username;
            this.Password = password;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Core;
    using System;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UserRepository 
    {
        public UserRepository(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}
namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class AddRefreshTokenCommand 
    {
        public AddRefreshTokenCommand(
            Fifthweek.Api.Persistence.RefreshToken refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken");
            }

            this.RefreshToken = refreshToken;
        }
    }

}
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Identity.Membership;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    public partial class FifthweekAuthorizationServerHandler 
    {
        public FifthweekAuthorizationServerHandler(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.OAuth.Queries.GetClientQuery,Fifthweek.Api.Identity.OAuth.Client> getClient, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.GetUserQuery,Fifthweek.Api.Persistence.Identity.FifthweekUser> getUser, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Identity.Membership.Commands.UpdateLastAccessTokenDateCommand> updateLastAccessTokenDate, 
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler)
        {
            if (getClient == null)
            {
                throw new ArgumentNullException("getClient");
            }

            if (getUser == null)
            {
                throw new ArgumentNullException("getUser");
            }

            if (updateLastAccessTokenDate == null)
            {
                throw new ArgumentNullException("updateLastAccessTokenDate");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.getClient = getClient;
            this.getUser = getUser;
            this.updateLastAccessTokenDate = updateLastAccessTokenDate;
            this.exceptionHandler = exceptionHandler;
        }
    }

}
namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetClientQuery 
    {
        public GetClientQuery(
            Fifthweek.Api.Identity.OAuth.ClientId clientId)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            this.ClientId = clientId;
        }
    }

}
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetRefreshTokenQuery 
    {
        public GetRefreshTokenQuery(
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
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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

namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class RegisterUserCommand 
    {
        public override string ToString()
        {
            return string.Format("RegisterUserCommand({0}, \"{1}\", {2}, {3}, {4})", this.UserId == null ? "null" : this.UserId.ToString(), this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString());
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UpdateLastAccessTokenDateCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateLastAccessTokenDateCommand({0}, {1}, {2})", this.Username == null ? "null" : this.Username.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreationType == null ? "null" : this.CreationType.ToString());
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class RegistrationData 
    {
        public override string ToString()
        {
            return string.Format("RegistrationData(\"{0}\", \"{1}\", \"{2}\", \"{3}\")", this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString());
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

            return true;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Events
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UserRegisteredEvent 
    {
        public override string ToString()
        {
            return string.Format("UserRegisteredEvent({0})", this.UserId == null ? "null" : this.UserId.ToString());
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

            return this.Equals((UserRegisteredEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UserRegisteredEvent other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetUserQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserQuery({0}, {1})", this.Username == null ? "null" : this.Username.ToString(), this.Password == null ? "null" : this.Password.ToString());
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
namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class AddRefreshTokenCommand 
    {
        public override string ToString()
        {
            return string.Format("AddRefreshTokenCommand({0})", this.RefreshToken == null ? "null" : this.RefreshToken.ToString());
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
namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetClientQuery 
    {
        public override string ToString()
        {
            return string.Format("GetClientQuery({0})", this.ClientId == null ? "null" : this.ClientId.ToString());
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
namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GetRefreshTokenQuery 
    {
        public override string ToString()
        {
            return string.Format("GetRefreshTokenQuery({0})", this.HashedRefreshTokenId == null ? "null" : this.HashedRefreshTokenId.ToString());
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
namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class PasswordResetConfirmationData 
    {
		[Optional]
        public ValidPassword NewPasswordObject { get; set; }
    }

    public static partial class PasswordResetConfirmationDataExtensions
    {
        public static void Parse(this PasswordResetConfirmationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (true || !ValidPassword.IsEmpty(target.NewPassword))
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class PasswordResetRequestData 
    {
		[Optional]
        public ValidEmail EmailObject { get; set; }
		[Optional]
        public ValidUsername UsernameObject { get; set; }
    }

    public static partial class PasswordResetRequestDataExtensions
    {
        public static void Parse(this PasswordResetRequestData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (false || !ValidEmail.IsEmpty(target.Email))
            {
                ValidEmail @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidEmail.TryParse(target.Email, out @object, out errorMessages))
                {
                    target.EmailObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Email", modelState);
                }
            }

            if (false || !ValidUsername.IsEmpty(target.Username))
            {
                ValidUsername @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidUsername.TryParse(target.Username, out @object, out errorMessages))
                {
                    target.UsernameObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
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
        }    
    }
}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class RegistrationData 
    {
		[Optional]
        public ValidEmail EmailObject { get; set; }
		[Optional]
        public ValidUsername UsernameObject { get; set; }
		[Optional]
        public ValidPassword PasswordObject { get; set; }
    }

    public static partial class RegistrationDataExtensions
    {
        public static void Parse(this RegistrationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (true || !ValidEmail.IsEmpty(target.Email))
            {
                ValidEmail @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidEmail.TryParse(target.Email, out @object, out errorMessages))
                {
                    target.EmailObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Email", modelState);
                }
            }

            if (true || !ValidUsername.IsEmpty(target.Username))
            {
                ValidUsername @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidUsername.TryParse(target.Username, out @object, out errorMessages))
                {
                    target.UsernameObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Username", modelState);
                }
            }

            if (true || !ValidPassword.IsEmpty(target.Password))
            {
                ValidPassword @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidPassword.TryParse(target.Password, out @object, out errorMessages))
                {
                    target.PasswordObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Password", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}

