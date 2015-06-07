using System;
using System.Linq;

//// Generated on 05/06/2015 14:49:14 (UTC)
//// Mapped solution in 6.08s


namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using System.Data.Entity;

    public partial class ChannelOwnership 
    {
        public ChannelOwnership(
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
namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
    using System.Linq;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Data.Entity;

    public partial class ChannelSecurity 
    {
        public ChannelSecurity(
            Fifthweek.Api.Channels.IChannelOwnership channelOwnership)
        {
            if (channelOwnership == null)
            {
                throw new ArgumentNullException("channelOwnership");
            }

            this.channelOwnership = channelOwnership;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class CreateChannelCommand 
    {
        public CreateChannelCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId newChannelId,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Channels.Shared.ValidChannelName name,
            Fifthweek.Api.Channels.Shared.ValidChannelDescription description,
            Fifthweek.Api.Channels.Shared.ValidChannelPriceInUsCentsPerWeek price,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newChannelId == null)
            {
                throw new ArgumentNullException("newChannelId");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.Requester = requester;
            this.NewChannelId = newChannelId;
            this.BlogId = blogId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class CreateChannelCommandHandler 
    {
        public CreateChannelCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.Services.IRequestSnapshotService requestSnapshot)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (requestSnapshot == null)
            {
                throw new ArgumentNullException("requestSnapshot");
            }

            this.requesterSecurity = requesterSecurity;
            this.blogSecurity = blogSecurity;
            this.connectionFactory = connectionFactory;
            this.requestSnapshot = requestSnapshot;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateChannelCommand 
    {
        public UpdateChannelCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Channels.Shared.ValidChannelName name,
            Fifthweek.Api.Channels.Shared.ValidChannelDescription description,
            Fifthweek.Api.Channels.Shared.ValidChannelPriceInUsCentsPerWeek price,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateChannelCommandHandler 
    {
        public UpdateChannelCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Channels.IUpdateChannelDbStatement updateChannel)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (updateChannel == null)
            {
                throw new ArgumentNullException("updateChannel");
            }

            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
            this.updateChannel = updateChannel;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class ChannelController 
    {
        public ChannelController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Channels.Commands.CreateChannelCommand> createChannel,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Channels.Commands.UpdateChannelCommand> updateChannel,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Channels.Commands.DeleteChannelCommand> deleteChannel,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (createChannel == null)
            {
                throw new ArgumentNullException("createChannel");
            }

            if (updateChannel == null)
            {
                throw new ArgumentNullException("updateChannel");
            }

            if (deleteChannel == null)
            {
                throw new ArgumentNullException("deleteChannel");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.createChannel = createChannel;
            this.updateChannel = updateChannel;
            this.deleteChannel = deleteChannel;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewChannelData 
    {
        public NewChannelData(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String name,
            System.String description,
            System.Int32 price,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.BlogId = blogId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedChannelData 
    {
        public UpdatedChannelData(
            System.String name,
            System.String description,
            System.Int32 price,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class DeleteChannelCommand 
    {
        public DeleteChannelCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class DeleteChannelCommandHandler 
    {
        public DeleteChannelCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity collectionSecurity,
            Fifthweek.Api.Channels.IDeleteChannelDbStatement deleteChannel,
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
            }

            if (deleteChannel == null)
            {
                throw new ArgumentNullException("deleteChannel");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            this.requesterSecurity = requesterSecurity;
            this.collectionSecurity = collectionSecurity;
            this.deleteChannel = deleteChannel;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
        }
    }
}
namespace Fifthweek.Api.Channels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;
    using System.Data.Entity;
    using Fifthweek.Payments.Services;

    public partial class DeleteChannelDbStatement 
    {
        public DeleteChannelDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.Services.IRequestSnapshotService requestSnapshot)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (requestSnapshot == null)
            {
                throw new ArgumentNullException("requestSnapshot");
            }

            this.connectionFactory = connectionFactory;
            this.requestSnapshot = requestSnapshot;
        }
    }
}
namespace Fifthweek.Api.Channels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;
    using System.Data.Entity;
    using Fifthweek.Payments.Services;

    public partial class UpdateChannelDbStatement 
    {
        public UpdateChannelDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.Services.IRequestSnapshotService requestSnapshot)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (requestSnapshot == null)
            {
                throw new ArgumentNullException("requestSnapshot");
            }

            this.connectionFactory = connectionFactory;
            this.requestSnapshot = requestSnapshot;
        }
    }
}

namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class CreateChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateChannelCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewChannelId == null ? "null" : this.NewChannelId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.Price == null ? "null" : this.Price.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString());
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
        
            return this.Equals((CreateChannelCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewChannelId != null ? this.NewChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateChannelCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewChannelId, other.NewChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            if (!object.Equals(this.IsVisibleToNonSubscribers, other.IsVisibleToNonSubscribers))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class UpdateChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateChannelCommand({0}, {1}, {2}, {3}, {4}, {5})", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.Price == null ? "null" : this.Price.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString());
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
        
            return this.Equals((UpdateChannelCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateChannelCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            if (!object.Equals(this.IsVisibleToNonSubscribers, other.IsVisibleToNonSubscribers))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewChannelData 
    {
        public override string ToString()
        {
            return string.Format("NewChannelData({0}, \"{1}\", \"{2}\", {3}, {4})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.Price == null ? "null" : this.Price.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString());
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
        
            return this.Equals((NewChannelData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewChannelData other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            if (!object.Equals(this.IsVisibleToNonSubscribers, other.IsVisibleToNonSubscribers))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedChannelData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedChannelData(\"{0}\", \"{1}\", {2}, {3})", this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.Price == null ? "null" : this.Price.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString());
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
        
            return this.Equals((UpdatedChannelData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatedChannelData other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            if (!object.Equals(this.IsVisibleToNonSubscribers, other.IsVisibleToNonSubscribers))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Data.Entity;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Services;

    public partial class DeleteChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("DeleteChannelCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString());
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
        
            return this.Equals((DeleteChannelCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeleteChannelCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewChannelData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                ValidChannelName name,
                ValidChannelDescription description,
                ValidChannelPriceInUsCentsPerWeek price,
                System.Boolean isVisibleToNonSubscribers)
            {
                if (blogId == null)
                {
                    throw new ArgumentNullException("blogId");
                }

                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (description == null)
                {
                    throw new ArgumentNullException("description");
                }

                if (price == null)
                {
                    throw new ArgumentNullException("price");
                }

                if (isVisibleToNonSubscribers == null)
                {
                    throw new ArgumentNullException("isVisibleToNonSubscribers");
                }

                this.BlogId = blogId;
                this.Name = name;
                this.Description = description;
                this.Price = price;
                this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
            }
        
            public Fifthweek.Api.Blogs.Shared.BlogId BlogId { get; private set; }
        
            public ValidChannelName Name { get; private set; }
        
            public ValidChannelDescription Description { get; private set; }
        
            public ValidChannelPriceInUsCentsPerWeek Price { get; private set; }
        
            public System.Boolean IsVisibleToNonSubscribers { get; private set; }
        }
    }

    public static partial class NewChannelDataExtensions
    {
        public static NewChannelData.Parsed Parse(this NewChannelData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidChannelName parsed0 = null;
            if (!ValidChannelName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidChannelName.TryParse(target.Name, out parsed0, out parsed0Errors))
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

            ValidChannelDescription parsed1 = null;
            if (!ValidChannelDescription.IsEmpty(target.Description))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidChannelDescription.TryParse(target.Description, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Description", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Description", modelState);
            }

            ValidChannelPriceInUsCentsPerWeek parsed2 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
            if (!ValidChannelPriceInUsCentsPerWeek.TryParse(target.Price, out parsed2, out parsed2Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed2Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Price", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewChannelData.Parsed(
                target.BlogId,
                parsed0,
                parsed1,
                parsed2,
                target.IsVisibleToNonSubscribers);
        }    
    }
}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedChannelData 
    {
        public class Parsed
        {
            public Parsed(
                ValidChannelName name,
                ValidChannelDescription description,
                ValidChannelPriceInUsCentsPerWeek price,
                System.Boolean isVisibleToNonSubscribers)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (description == null)
                {
                    throw new ArgumentNullException("description");
                }

                if (price == null)
                {
                    throw new ArgumentNullException("price");
                }

                if (isVisibleToNonSubscribers == null)
                {
                    throw new ArgumentNullException("isVisibleToNonSubscribers");
                }

                this.Name = name;
                this.Description = description;
                this.Price = price;
                this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
            }
        
            public ValidChannelName Name { get; private set; }
        
            public ValidChannelDescription Description { get; private set; }
        
            public ValidChannelPriceInUsCentsPerWeek Price { get; private set; }
        
            public System.Boolean IsVisibleToNonSubscribers { get; private set; }
        }
    }

    public static partial class UpdatedChannelDataExtensions
    {
        public static UpdatedChannelData.Parsed Parse(this UpdatedChannelData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidChannelName parsed0 = null;
            if (!ValidChannelName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidChannelName.TryParse(target.Name, out parsed0, out parsed0Errors))
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

            ValidChannelDescription parsed1 = null;
            if (!ValidChannelDescription.IsEmpty(target.Description))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidChannelDescription.TryParse(target.Description, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Description", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Description", modelState);
            }

            ValidChannelPriceInUsCentsPerWeek parsed2 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
            if (!ValidChannelPriceInUsCentsPerWeek.TryParse(target.Price, out parsed2, out parsed2Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed2Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Price", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new UpdatedChannelData.Parsed(
                parsed0,
                parsed1,
                parsed2,
                target.IsVisibleToNonSubscribers);
        }    
    }
}


