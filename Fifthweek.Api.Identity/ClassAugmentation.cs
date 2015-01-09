
using System;





namespace Fifthweek.Api.Identity.Membership.Commands
{
	using System;
	using Fifthweek.Api.Core;
	public partial class ConfirmPasswordResetCommand
	{
        public ConfirmPasswordResetCommand(
            Fifthweek.Api.Identity.Membership.UserId userId, 
            System.String token, 
            Fifthweek.Api.Identity.Membership.Password newPassword)
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
	using Fifthweek.Api.Core;
	public partial class RegisterUserCommand
	{
        public RegisterUserCommand(
            Fifthweek.Api.Identity.Membership.UserId userId, 
            System.String exampleWork, 
            Fifthweek.Api.Identity.Membership.NormalizedEmail email, 
            Fifthweek.Api.Identity.Membership.Username username, 
            Fifthweek.Api.Identity.Membership.Password password)
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
	using Fifthweek.Api.Core;
	public partial class RequestPasswordResetCommand
	{
        public RequestPasswordResetCommand(
            Fifthweek.Api.Identity.Membership.NormalizedEmail email, 
            Fifthweek.Api.Identity.Membership.Username username)
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
	public partial class UpdateLastAccessTokenDateCommand
	{
        public UpdateLastAccessTokenDateCommand(
            Fifthweek.Api.Identity.Membership.Username username, 
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
namespace Fifthweek.Api.Identity.Membership.Queries
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class GetUserQuery
	{
        public GetUserQuery(
            Fifthweek.Api.Identity.Membership.Username username, 
            Fifthweek.Api.Identity.Membership.Password password)
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class IsPasswordResetTokenValidQuery
	{
        public IsPasswordResetTokenValidQuery(
            Fifthweek.Api.Identity.Membership.UserId userId, 
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class IsUsernameAvailableQuery
	{
        public IsUsernameAvailableQuery(
            Fifthweek.Api.Identity.Membership.Username username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            this.Username = username;
        }
	}

}
namespace Fifthweek.Api.Identity.OAuth
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
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
namespace Fifthweek.Api.Identity.Membership
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
	public partial class UserId
	{
        public UserId(
            System.Guid value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
	}

}
namespace Fifthweek.Api.Identity.Membership
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
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

namespace Fifthweek.Api.Identity.Membership.Commands
{
	using System;
	using Fifthweek.Api.Core;
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
namespace Fifthweek.Api.Identity.Membership.Commands
{
	using System;
	using Fifthweek.Api.Core;
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
	using System;
	using Fifthweek.Api.Core;
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
	using System;
	using Fifthweek.Api.Core;
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
namespace Fifthweek.Api.Identity.Membership.Queries
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
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
namespace Fifthweek.Api.Identity.Membership.Queries
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
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
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
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
namespace Fifthweek.Api.Identity.OAuth
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
	public partial class Client
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
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
namespace Fifthweek.Api.Identity.OAuth.Commands
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
namespace Fifthweek.Api.Identity.OAuth.Commands
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
namespace Fifthweek.Api.Identity.OAuth
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
	public partial class HashedRefreshTokenId
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
namespace Fifthweek.Api.Identity.OAuth.Queries
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
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
namespace Fifthweek.Api.Identity.OAuth
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership.Commands;
	using Fifthweek.Api.Identity.Membership.Queries;
	using Fifthweek.Api.Identity.OAuth.Queries;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	using Microsoft.Owin.Security;
	using Microsoft.Owin.Security.OAuth;
	public partial class RefreshTokenId
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
namespace Fifthweek.Api.Identity.Membership
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
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
                hashCode = (hashCode * 397) ^ (this.UserIdObject != null ? this.UserIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPasswordObject != null ? this.NewPasswordObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PasswordResetConfirmationData other)
        {
            if (!object.Equals(this.UserIdObject, other.UserIdObject))
            {
                return false;
            }
            if (!object.Equals(this.NewPasswordObject, other.NewPasswordObject))
            {
                return false;
            }
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
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
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
                hashCode = (hashCode * 397) ^ (this.EmailObject != null ? this.EmailObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UsernameObject != null ? this.UsernameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PasswordResetRequestData other)
        {
            if (!object.Equals(this.EmailObject, other.EmailObject))
            {
                return false;
            }
            if (!object.Equals(this.UsernameObject, other.UsernameObject))
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
            return true;
        }
	}

}
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
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
                hashCode = (hashCode * 397) ^ (this.EmailObject != null ? this.EmailObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UsernameObject != null ? this.UsernameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PasswordObject != null ? this.PasswordObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(RegistrationData other)
        {
            if (!object.Equals(this.EmailObject, other.EmailObject))
            {
                return false;
            }
            if (!object.Equals(this.UsernameObject, other.UsernameObject))
            {
                return false;
            }
            if (!object.Equals(this.PasswordObject, other.PasswordObject))
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
namespace Fifthweek.Api.Identity.Membership
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
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
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
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
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using Fifthweek.Api.Core;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence;
	using System.Linq;
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

namespace Fifthweek.Api.Identity.Membership.Controllers
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
	public partial class PasswordResetConfirmationData
	{
		public UserId UserIdObject { get; set; }
		public Password NewPasswordObject { get; set; }

		public void Parse()
		{
			PasswordResetConfirmationData_Companion.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	internal class PasswordResetConfirmationData_Companion
	{
		public static void Parse(PasswordResetConfirmationData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

		    if (target.UserId != null)
		    {
                target.UserIdObject = new UserId(target.UserId);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("UserId", modelState);
            }

			if (true || !Password.IsEmpty(target.NewPassword))
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
namespace Fifthweek.Api.Identity.Membership.Controllers
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
	public partial class PasswordResetRequestData
	{
		public Email EmailObject { get; set; }
		public Username UsernameObject { get; set; }

		public void Parse()
		{
			PasswordResetRequestData_Companion.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	internal class PasswordResetRequestData_Companion
	{
		public static void Parse(PasswordResetRequestData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (false || !Email.IsEmpty(target.Email))
			{
				Email @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Email.TryParse(target.Email, out @object, out errorMessages))
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

			if (false || !Username.IsEmpty(target.Username))
			{
				Username @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Username.TryParse(target.Username, out @object, out errorMessages))
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
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Http.ModelBinding;
	using Fifthweek.Api.Core;
	public partial class RegistrationData
	{
		public Email EmailObject { get; set; }
		public Username UsernameObject { get; set; }
		public Password PasswordObject { get; set; }

		public void Parse()
		{
			RegistrationData_Companion.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	internal class RegistrationData_Companion
	{
		public static void Parse(RegistrationData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (true || !Email.IsEmpty(target.Email))
			{
				Email @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Email.TryParse(target.Email, out @object, out errorMessages))
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

			if (true || !Username.IsEmpty(target.Username))
			{
				Username @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Username.TryParse(target.Username, out @object, out errorMessages))
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

			if (true || !Password.IsEmpty(target.Password))
			{
				Password @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Password.TryParse(target.Password, out @object, out errorMessages))
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
