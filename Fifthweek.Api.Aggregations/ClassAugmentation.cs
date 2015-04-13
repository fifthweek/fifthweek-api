using System;
using System.Linq;

//// Generated on 13/04/2015 14:07:15 (UTC)
//// Mapped solution in 7.2s


namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    public partial class UserStateController 
    {
        public UserStateController(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Aggregations.Queries.GetUserStateQuery,Fifthweek.Api.Aggregations.Queries.UserState> getUserState,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
        {
            if (getUserState == null)
            {
                throw new ArgumentNullException("getUserState");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.getUserState = getUserState;
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;

    public partial class GetUserStateQuery 
    {
        public GetUserStateQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Shared;

    public partial class GetUserStateQueryHandler 
    {
        public GetUserStateQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.FileManagement.Queries.GetUserAccessSignaturesQuery,Fifthweek.Api.FileManagement.Queries.UserAccessSignatures> getUserAccessSignatures,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetCreatorStatusQuery,Fifthweek.Api.Blogs.CreatorStatus> getCreatorStatus,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Collections.Queries.GetCreatedChannelsAndCollectionsQuery,Fifthweek.Api.Collections.Queries.ChannelsAndCollections> getCreatedChannelsAndCollections,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Identity.Membership.Queries.GetAccountSettingsQuery,Fifthweek.Api.Identity.Membership.GetAccountSettingsResult> getAccountSettings,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetBlogQuery,Fifthweek.Api.Blogs.Queries.GetBlogResult> getBlog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetBlogSubscriptionsQuery,Fifthweek.Api.Blogs.Queries.GetBlogSubscriptionsResult> getBlogSubscriptions)
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

            if (getCreatedChannelsAndCollections == null)
            {
                throw new ArgumentNullException("getCreatedChannelsAndCollections");
            }

            if (getAccountSettings == null)
            {
                throw new ArgumentNullException("getAccountSettings");
            }

            if (getBlog == null)
            {
                throw new ArgumentNullException("getBlog");
            }

            if (getBlogSubscriptions == null)
            {
                throw new ArgumentNullException("getBlogSubscriptions");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserAccessSignatures = getUserAccessSignatures;
            this.getCreatorStatus = getCreatorStatus;
            this.getCreatedChannelsAndCollections = getCreatedChannelsAndCollections;
            this.getAccountSettings = getAccountSettings;
            this.getBlog = getBlog;
            this.getBlogSubscriptions = getBlogSubscriptions;
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;

    public partial class UserState 
    {
        public UserState(
            Fifthweek.Api.FileManagement.Queries.UserAccessSignatures accessSignatures,
            Fifthweek.Api.Blogs.CreatorStatus creatorStatus,
            Fifthweek.Api.Collections.Queries.ChannelsAndCollections createdChannelsAndCollections,
            Fifthweek.Api.Identity.Membership.GetAccountSettingsResult accountSettings,
            Fifthweek.Api.Blogs.Queries.GetBlogResult blog,
            Fifthweek.Api.Blogs.Queries.GetBlogSubscriptionsResult blogSubscriptions)
        {
            if (accessSignatures == null)
            {
                throw new ArgumentNullException("accessSignatures");
            }

            this.AccessSignatures = accessSignatures;
            this.CreatorStatus = creatorStatus;
            this.CreatedChannelsAndCollections = createdChannelsAndCollections;
            this.AccountSettings = accountSettings;
            this.Blog = blog;
            this.BlogSubscriptions = blogSubscriptions;
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;

    public partial class GetUserStateQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserStateQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Membership.Queries;

    public partial class UserState 
    {
        public override string ToString()
        {
            return string.Format("UserState({0}, {1}, {2}, {3}, {4}, {5})", this.AccessSignatures == null ? "null" : this.AccessSignatures.ToString(), this.CreatorStatus == null ? "null" : this.CreatorStatus.ToString(), this.CreatedChannelsAndCollections == null ? "null" : this.CreatedChannelsAndCollections.ToString(), this.AccountSettings == null ? "null" : this.AccountSettings.ToString(), this.Blog == null ? "null" : this.Blog.ToString(), this.BlogSubscriptions == null ? "null" : this.BlogSubscriptions.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CreatedChannelsAndCollections != null ? this.CreatedChannelsAndCollections.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccountSettings != null ? this.AccountSettings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogSubscriptions != null ? this.BlogSubscriptions.GetHashCode() : 0);
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
        
            if (!object.Equals(this.CreatedChannelsAndCollections, other.CreatedChannelsAndCollections))
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
        
            if (!object.Equals(this.BlogSubscriptions, other.BlogSubscriptions))
            {
                return false;
            }
        
            return true;
        }
    }
}


