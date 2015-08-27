using System;
using System.Linq;

//// Generated on 27/08/2015 13:54:40 (UTC)
//// Mapped solution in 19.68s


namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    public partial class UserStateController 
    {
        public UserStateController(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Aggregations.Queries.GetUserStateQuery,Fifthweek.Api.Aggregations.Queries.UserState> getUserState,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
        {
            if (getUserState == null)
            {
                throw new ArgumentNullException("getUserState");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.getUserState = getUserState;
            this.timestampCreator = timestampCreator;
            this.requesterContext = requesterContext;
        }
    }
}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetUserStateQuery 
    {
        public GetUserStateQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
            System.Boolean impersonate,
            System.DateTime now)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (impersonate == null)
            {
                throw new ArgumentNullException("impersonate");
            }

            if (now == null)
            {
                throw new ArgumentNullException("now");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
            this.Impersonate = impersonate;
            this.Now = now;
        }
    }
}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetUserStateQueryHandler 
    {
        public GetUserStateQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.FileManagement.Queries.GetUserAccessSignaturesQuery,Fifthweek.Api.FileManagement.Queries.UserAccessSignatures> getUserAccessSignatures,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetCreatorStatusQuery,Fifthweek.Api.Blogs.CreatorStatus> getCreatorStatus,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.GetAccountSettingsQuery,Fifthweek.Api.Identity.Membership.GetAccountSettingsResult> getAccountSettings,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetBlogChannelsAndCollectionsQuery,Fifthweek.Api.Blogs.Queries.GetBlogChannelsAndCollectionsResult> getBlogChannelsAndCollections,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetUserSubscriptionsQuery,Fifthweek.Api.Blogs.Queries.GetUserSubscriptionsResult> getBlogSubscriptions,
            Fifthweek.Api.Identity.Membership.IImpersonateIfRequired impersonateIfRequired)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getUserAccessSignatures == null)
            {
                throw new ArgumentNullException("getUserAccessSignatures");
            }

            if (getCreatorStatus == null)
            {
                throw new ArgumentNullException("getCreatorStatus");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            if (getBlogChannelsAndCollections == null)
            {
                throw new ArgumentNullException("getBlogChannelsAndCollections");
            }

            if (getBlogSubscriptions == null)
            {
                throw new ArgumentNullException("getBlogSubscriptions");
            }

            if (impersonateIfRequired == null)
            {
                throw new ArgumentNullException("impersonateIfRequired");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserAccessSignatures = getUserAccessSignatures;
            this.getCreatorStatus = getCreatorStatus;
            this.getAccountSettings = getAccountSettings;
            this.getBlogChannelsAndCollections = getBlogChannelsAndCollections;
            this.getBlogSubscriptions = getBlogSubscriptions;
            this.impersonateIfRequired = impersonateIfRequired;
        }
    }
}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class UserState 
    {
        public UserState(
            Fifthweek.Api.FileManagement.Queries.UserAccessSignatures accessSignatures,
            Fifthweek.Api.Blogs.CreatorStatus creatorStatus,
            Fifthweek.Api.Identity.Membership.GetAccountSettingsResult accountSettings,
            Fifthweek.Api.Blogs.Queries.BlogWithFileInformation blog,
            Fifthweek.Api.Blogs.Queries.GetUserSubscriptionsResult subscriptions)
        {
            if (accessSignatures == null)
            {
                throw new ArgumentNullException("accessSignatures");
            }

            this.AccessSignatures = accessSignatures;
            this.CreatorStatus = creatorStatus;
            this.AccountSettings = accountSettings;
            this.Blog = blog;
            this.Subscriptions = subscriptions;
        }
    }
}

namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetUserStateQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserStateQuery({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.Impersonate == null ? "null" : this.Impersonate.ToString(), this.Now == null ? "null" : this.Now.ToString());
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
        
            return this.Equals((GetUserStateQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Impersonate != null ? this.Impersonate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Now != null ? this.Now.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetUserStateQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }
        
            if (!object.Equals(this.Impersonate, other.Impersonate))
            {
                return false;
            }
        
            if (!object.Equals(this.Now, other.Now))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class UserState 
    {
        public override string ToString()
        {
            return string.Format("UserState({0}, {1}, {2}, {3}, {4})", this.AccessSignatures == null ? "null" : this.AccessSignatures.ToString(), this.CreatorStatus == null ? "null" : this.CreatorStatus.ToString(), this.AccountSettings == null ? "null" : this.AccountSettings.ToString(), this.Blog == null ? "null" : this.Blog.ToString(), this.Subscriptions == null ? "null" : this.Subscriptions.ToString());
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
        
            return this.Equals((UserState)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.AccessSignatures != null ? this.AccessSignatures.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorStatus != null ? this.CreatorStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccountSettings != null ? this.AccountSettings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscriptions != null ? this.Subscriptions.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserState other)
        {
            if (!object.Equals(this.AccessSignatures, other.AccessSignatures))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorStatus, other.CreatorStatus))
            {
                return false;
            }
        
            if (!object.Equals(this.AccountSettings, other.AccountSettings))
            {
                return false;
            }
        
            if (!object.Equals(this.Blog, other.Blog))
            {
                return false;
            }
        
            if (!object.Equals(this.Subscriptions, other.Subscriptions))
            {
                return false;
            }
        
            return true;
        }
    }
}


