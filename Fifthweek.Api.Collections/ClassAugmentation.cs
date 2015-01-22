using System;
using System.Linq;



namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class CollectionId 
    {
        public CollectionId(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class CollectionOwnership 
    {
        public CollectionOwnership(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class CollectionSecurity 
    {
        public CollectionSecurity(
            Fifthweek.Api.Collections.ICollectionOwnership collectionOwnership)
        {
            if (collectionOwnership == null)
            {
                throw new ArgumentNullException("collectionOwnership");
            }

            this.collectionOwnership = collectionOwnership;
        }
    }

}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.CodeGeneration;
    public partial class CollectionController 
    {
        public CollectionController(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Collections.Queries.GetLiveDateOfNewQueuedPostQuery,System.DateTime> getLiveDateOfNewQueuedPost, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext)
        {
            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
            this.userContext = userContext;
        }
    }

}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class GetCollectionWeeklyReleaseTimesDbStatement 
    {
        public GetCollectionWeeklyReleaseTimesDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class GetLiveDateOfNewQueuedPostDbStatement 
    {
        public GetLiveDateOfNewQueuedPostDbStatement(
            Fifthweek.Api.Collections.IGetNewQueuedPostLiveDateLowerBoundDbStatement getNewQueuedPostLiveDateLowerBound, 
            Fifthweek.Api.Collections.IGetCollectionWeeklyReleaseTimesDbStatement getCollectionWeeklyReleaseTimes, 
            Fifthweek.Api.Collections.IQueuedPostLiveDateCalculator queuedPostLiveDateCalculator)
        {
            if (getNewQueuedPostLiveDateLowerBound == null)
            {
                throw new ArgumentNullException("getNewQueuedPostLiveDateLowerBound");
            }

            if (getCollectionWeeklyReleaseTimes == null)
            {
                throw new ArgumentNullException("getCollectionWeeklyReleaseTimes");
            }

            if (queuedPostLiveDateCalculator == null)
            {
                throw new ArgumentNullException("queuedPostLiveDateCalculator");
            }

            this.getNewQueuedPostLiveDateLowerBound = getNewQueuedPostLiveDateLowerBound;
            this.getCollectionWeeklyReleaseTimes = getCollectionWeeklyReleaseTimes;
            this.queuedPostLiveDateCalculator = queuedPostLiveDateCalculator;
        }
    }

}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class GetNewQueuedPostLiveDateLowerBoundDbStatement 
    {
        public GetNewQueuedPostLiveDateLowerBoundDbStatement(
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetLiveDateOfNewQueuedPostQuery 
    {
        public GetLiveDateOfNewQueuedPostQuery(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.Collections.CollectionId collectionId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            this.Requester = requester;
            this.CollectionId = collectionId;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetLiveDateOfNewQueuedPostQueryHandler 
    {
        public GetLiveDateOfNewQueuedPostQueryHandler(
            Fifthweek.Api.Collections.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Collections.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost)
        {
            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            this.collectionSecurity = collectionSecurity;
            this.requesterSecurity = requesterSecurity;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetCreatedChannelsAndCollectionsQuery 
    {
        public GetCreatedChannelsAndCollectionsQuery(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.Identity.Membership.UserId requestedCreatorId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedCreatorId == null)
            {
                throw new ArgumentNullException("requestedCreatorId");
            }

            this.Requester = requester;
            this.RequestedCreatorId = requestedCreatorId;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class ChannelsAndCollections
    {
        public partial class Channel 
        {
        public Channel(
            Fifthweek.Api.Subscriptions.ChannelId channelId, 
            System.String name, 
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Collections.Queries.ChannelsAndCollections.Collection> collections)
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class ChannelsAndCollections 
    {
        public ChannelsAndCollections(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Collections.Queries.ChannelsAndCollections.Channel> channels)
        {
            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.Channels = channels;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class ChannelsAndCollections
    {
        public partial class Collection 
        {
        public Collection(
            Fifthweek.Api.Collections.CollectionId collectionId, 
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class GetChannelsAndCollectionsDbStatement 
    {
        public GetChannelsAndCollectionsDbStatement(
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetCreatedChannelsAndCollectionsQueryHandler 
    {
        public GetCreatedChannelsAndCollectionsQueryHandler(
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Collections.IGetChannelsAndCollectionsDbStatement getChannelsAndCollections)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getChannelsAndCollections == null)
            {
                throw new ArgumentNullException("getChannelsAndCollections");
            }

            this.requesterSecurity = requesterSecurity;
            this.getChannelsAndCollections = getChannelsAndCollections;
        }
    }

}

namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class CollectionId 
    {
        public override string ToString()
        {
            return string.Format("CollectionId({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((CollectionId)obj);
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

        protected bool Equals(CollectionId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetLiveDateOfNewQueuedPostQuery 
    {
        public override string ToString()
        {
            return string.Format("GetLiveDateOfNewQueuedPostQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString());
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

            return this.Equals((GetLiveDateOfNewQueuedPostQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetLiveDateOfNewQueuedPostQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Api.Subscriptions;
    public partial class GetCreatedChannelsAndCollectionsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreatedChannelsAndCollectionsQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedCreatorId == null ? "null" : this.RequestedCreatorId.ToString());
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

            return this.Equals((GetCreatedChannelsAndCollectionsQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedCreatorId != null ? this.RequestedCreatorId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetCreatedChannelsAndCollectionsQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.RequestedCreatorId, other.RequestedCreatorId))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Queries;
    using System.Text;
    using Fifthweek.Api.Subscriptions;
    public partial class HourOfWeek 
    {
        public override string ToString()
        {
            return string.Format("HourOfWeek({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((HourOfWeek)obj);
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

        protected bool Equals(HourOfWeek other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}

