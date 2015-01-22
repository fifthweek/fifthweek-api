using System;
using System.Linq;



namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateController 
    {
        public UserStateController(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Aggregations.Queries.GetUserStateQuery,Fifthweek.Api.Aggregations.Queries.UserState> getUserState, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext)
        {
            if (getUserState == null)
            {
                throw new ArgumentNullException("getUserState");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.getUserState = getUserState;
            this.userContext = userContext;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse 
    {
        public UserStateResponse(
            Fifthweek.Api.Aggregations.Controllers.UserStateResponse.CreatorStatusResponse creatorStatus, 
            Fifthweek.Api.Aggregations.Controllers.UserStateResponse.ChannelsAndCollectionsResponse createdChannelsAndCollections)
        {
            this.CreatorStatus = creatorStatus;
            this.CreatedChannelsAndCollections = createdChannelsAndCollections;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Collections.Queries;
    public partial class GetUserStateQuery 
    {
        public GetUserStateQuery(
            Fifthweek.Api.Identity.Membership.UserId requestedUserId, 
            Fifthweek.Api.Identity.Membership.Requester requester, 
            System.Boolean isCreator)
        {
            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (isCreator == null)
            {
                throw new ArgumentNullException("isCreator");
            }

            this.RequestedUserId = requestedUserId;
            this.Requester = requester;
            this.IsCreator = isCreator;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Collections.Queries;
    public partial class UserState 
    {
        public UserState(
            Fifthweek.Api.Subscriptions.CreatorStatus creatorStatus, 
            Fifthweek.Api.Collections.Queries.ChannelsAndCollections createdChannelsAndCollections)
        {
            this.CreatorStatus = creatorStatus;
            this.CreatedChannelsAndCollections = createdChannelsAndCollections;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Collections.Queries;
    public partial class GetUserStateQueryHandler 
    {
        public GetUserStateQueryHandler(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Subscriptions.Queries.GetCreatorStatusQuery,Fifthweek.Api.Subscriptions.CreatorStatus> getCreatorStatus, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Collections.Queries.GetCreatedChannelsAndCollectionsQuery,Fifthweek.Api.Collections.Queries.ChannelsAndCollections> getCreatedChannelsAndCollections)
        {
            if (getCreatorStatus == null)
            {
                throw new ArgumentNullException("getCreatorStatus");
            }

            if (getCreatedChannelsAndCollections == null)
            {
                throw new ArgumentNullException("getCreatedChannelsAndCollections");
            }

            this.getCreatorStatus = getCreatorStatus;
            this.getCreatedChannelsAndCollections = getCreatedChannelsAndCollections;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse
    {
        public partial class ChannelsAndCollectionsResponse
        {
            public partial class ChannelResponse 
            {
        public ChannelResponse(
            System.String channelId, 
            System.String name, 
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Aggregations.Controllers.UserStateResponse.ChannelsAndCollectionsResponse.CollectionResponse> collections)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (collections == null)
            {
                throw new ArgumentNullException("collections");
            }

            this.ChannelId = channelId;
            this.Name = name;
            this.Collections = collections;
        }
            }

            }
        }
}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse
    {
        public partial class ChannelsAndCollectionsResponse 
        {
        public ChannelsAndCollectionsResponse(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Aggregations.Controllers.UserStateResponse.ChannelsAndCollectionsResponse.ChannelResponse> channels)
        {
            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.Channels = channels;
        }
        }

        }
}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse
    {
        public partial class ChannelsAndCollectionsResponse
        {
            public partial class CollectionResponse 
            {
        public CollectionResponse(
            System.String collectionId, 
            System.String name)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.CollectionId = collectionId;
            this.Name = name;
        }
            }

            }
        }
}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse
    {
        public partial class CreatorStatusResponse 
        {
        public CreatorStatusResponse(
            System.String subscriptionId, 
            System.Boolean mustWriteFirstPost)
        {
            if (mustWriteFirstPost == null)
            {
                throw new ArgumentNullException("mustWriteFirstPost");
            }

            this.SubscriptionId = subscriptionId;
            this.MustWriteFirstPost = mustWriteFirstPost;
        }
        }

        }
}

namespace Fifthweek.Api.Aggregations.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Collections.Queries;
    public partial class GetUserStateQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserStateQuery({0}, {1}, {2})", this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.Requester == null ? "null" : this.Requester.ToString(), this.IsCreator == null ? "null" : this.IsCreator.ToString());
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
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsCreator != null ? this.IsCreator.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetUserStateQuery other)
        {
            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }

            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.IsCreator, other.IsCreator))
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
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Collections.Queries;
    public partial class UserState 
    {
        public override string ToString()
        {
            return string.Format("UserState({0}, {1})", this.CreatorStatus == null ? "null" : this.CreatorStatus.ToString(), this.CreatedChannelsAndCollections == null ? "null" : this.CreatedChannelsAndCollections.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CreatorStatus != null ? this.CreatorStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatedChannelsAndCollections != null ? this.CreatedChannelsAndCollections.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UserState other)
        {
            if (!object.Equals(this.CreatorStatus, other.CreatorStatus))
            {
                return false;
            }

            if (!object.Equals(this.CreatedChannelsAndCollections, other.CreatedChannelsAndCollections))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Aggregations.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Aggregations.Queries;
    using System.Collections.Generic;
    public partial class UserStateResponse
    {
        public partial class CreatorStatusResponse 
        {
        public override string ToString()
        {
            return string.Format("CreatorStatusResponse(\"{0}\", {1})", this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.MustWriteFirstPost == null ? "null" : this.MustWriteFirstPost.ToString());
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

            return this.Equals((CreatorStatusResponse)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.MustWriteFirstPost != null ? this.MustWriteFirstPost.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(CreatorStatusResponse other)
        {
            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.MustWriteFirstPost, other.MustWriteFirstPost))
            {
                return false;
            }

            return true;
        }
        }

        }
}

