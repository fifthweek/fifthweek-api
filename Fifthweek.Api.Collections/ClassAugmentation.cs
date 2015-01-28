using System;
using System.Linq;




namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class CreateCollectionCommand 
    {
        public CreateCollectionCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
            Fifthweek.Api.Collections.Shared.CollectionId newCollectionId, 
            Fifthweek.Api.Channels.Shared.ChannelId channelId, 
            Fifthweek.Api.Collections.Shared.ValidCollectionName name)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newCollectionId == null)
            {
                throw new ArgumentNullException("newCollectionId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.Requester = requester;
            this.NewCollectionId = newCollectionId;
            this.ChannelId = channelId;
            this.Name = name;
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
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    public partial class CollectionController 
    {
        public CollectionController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Collections.Commands.CreateCollectionCommand> createCollection, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Collections.Queries.GetLiveDateOfNewQueuedPostQuery,System.DateTime> getLiveDateOfNewQueuedPost, 
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext, 
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (createCollection == null)
            {
                throw new ArgumentNullException("createCollection");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.createCollection = createCollection;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    public partial class NewCollectionData 
    {
        public NewCollectionData(
            Fifthweek.Api.Collections.Shared.ValidCollectionName nameObject, 
            Fifthweek.Api.Channels.Shared.ChannelId channelId, 
            System.String name)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.NameObject = nameObject;
            this.ChannelId = channelId;
            this.Name = name;
        }
    }

}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
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
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class ChannelsAndCollections
    {
        public partial class Channel 
        {
        public Channel(
            Fifthweek.Api.Channels.Shared.ChannelId channelId, 
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
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
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
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class ChannelsAndCollections
    {
        public partial class Collection 
        {
        public Collection(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId, 
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class GetCreatedChannelsAndCollectionsQuery 
    {
        public GetCreatedChannelsAndCollectionsQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedCreatorId)
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
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class GetCreatedChannelsAndCollectionsQueryHandler 
    {
        public GetCreatedChannelsAndCollectionsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity, 
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class GetLiveDateOfNewQueuedPostQuery 
    {
        public GetLiveDateOfNewQueuedPostQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
            Fifthweek.Api.Collections.Shared.CollectionId collectionId)
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
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    public partial class GetLiveDateOfNewQueuedPostQueryHandler 
    {
        public GetLiveDateOfNewQueuedPostQueryHandler(
            Fifthweek.Api.Collections.Shared.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Collections.Shared.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost)
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
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class CreateCollectionCommandHandler 
    {
        public CreateCollectionCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext, 
            Fifthweek.Shared.IRandom random)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (random == null)
            {
                throw new ArgumentNullException("random");
            }

            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
            this.databaseContext = databaseContext;
            this.random = random;
        }
    }

}

namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class CreateCollectionCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateCollectionCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewCollectionId == null ? "null" : this.NewCollectionId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString());
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

            return this.Equals((CreateCollectionCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewCollectionId != null ? this.NewCollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(CreateCollectionCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.NewCollectionId, other.NewCollectionId))
            {
                return false;
            }

            if (!object.Equals(this.ChannelId, other.ChannelId))
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
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    public partial class NewCollectionData 
    {
        public override string ToString()
        {
            return string.Format("NewCollectionData({0}, {1}, \"{2}\")", this.NameObject == null ? "null" : this.NameObject.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString());
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

            return this.Equals((NewCollectionData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NameObject != null ? this.NameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewCollectionData other)
        {
            if (!object.Equals(this.NameObject, other.NameObject))
            {
                return false;
            }

            if (!object.Equals(this.ChannelId, other.ChannelId))
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
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
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    public partial class NewCollectionData 
    {
		[Optional]
        public ValidCollectionName NameObject { get; set; }
    }

    public static partial class NewCollectionDataExtensions
    {
        public static void Parse(this NewCollectionData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (true || !ValidCollectionName.IsEmpty(target.Name))
            {
                ValidCollectionName @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidCollectionName.TryParse(target.Name, out @object, out errorMessages))
                {
                    target.NameObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Name", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}

