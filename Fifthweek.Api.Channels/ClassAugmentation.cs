using System;
using System.Linq;




namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    public partial class ChannelOwnership 
    {
        public ChannelOwnership(
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
namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using Dapper;
    using Fifthweek.Api.Persistence;
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    public partial class CreateChannelCommand 
    {
        public CreateChannelCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
            Fifthweek.Api.Channels.Shared.ChannelId newChannelId, 
            Fifthweek.Api.Subscriptions.Shared.SubscriptionId subscriptionId, 
            Fifthweek.Api.Channels.Shared.ValidChannelName name, 
            Fifthweek.Api.Channels.Shared.ValidChannelPriceInUsCentsPerWeek price)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newChannelId == null)
            {
                throw new ArgumentNullException("newChannelId");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            this.Requester = requester;
            this.NewChannelId = newChannelId;
            this.SubscriptionId = subscriptionId;
            this.Name = name;
            this.Price = price;
        }
    }

}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Commands;
    public partial class ChannelController 
    {
        public ChannelController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Channels.Commands.CreateChannelCommand> createChannel, 
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext, 
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (createChannel == null)
            {
                throw new ArgumentNullException("createChannel");
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
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
        }
    }

}
namespace Fifthweek.Api.Channels.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Commands;
    public partial class NewChannelData 
    {
        public NewChannelData(
            Fifthweek.Api.Channels.Shared.ValidChannelName nameObject, 
            Fifthweek.Api.Channels.Shared.ValidChannelPriceInUsCentsPerWeek priceObject, 
            Fifthweek.Api.Subscriptions.Shared.SubscriptionId subscriptionId, 
            System.String name, 
            System.Int32 price)
        {
            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            this.NameObject = nameObject;
            this.PriceObject = priceObject;
            this.SubscriptionId = subscriptionId;
            this.Name = name;
            this.Price = price;
        }
    }

}
namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    public partial class CreateChannelCommandHandler 
    {
        public CreateChannelCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Subscriptions.Shared.ISubscriptionSecurity subscriptionSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (subscriptionSecurity == null)
            {
                throw new ArgumentNullException("subscriptionSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.requesterSecurity = requesterSecurity;
            this.subscriptionSecurity = subscriptionSecurity;
            this.databaseContext = databaseContext;
        }
    }

}

namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    public partial class CreateChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateChannelCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewChannelId == null ? "null" : this.NewChannelId.ToString(), this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Price == null ? "null" : this.Price.ToString());
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
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
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

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }

            if (!object.Equals(this.Price, other.Price))
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
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Commands;
    public partial class NewChannelData 
    {
        public override string ToString()
        {
            return string.Format("NewChannelData({0}, {1}, {2}, \"{3}\", {4})", this.NameObject == null ? "null" : this.NameObject.ToString(), this.PriceObject == null ? "null" : this.PriceObject.ToString(), this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Price == null ? "null" : this.Price.ToString());
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
                hashCode = (hashCode * 397) ^ (this.NameObject != null ? this.NameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceObject != null ? this.PriceObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewChannelData other)
        {
            if (!object.Equals(this.NameObject, other.NameObject))
            {
                return false;
            }

            if (!object.Equals(this.PriceObject, other.PriceObject))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }

            if (!object.Equals(this.Price, other.Price))
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
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Commands;
    public partial class NewChannelData 
    {
		[Optional]
        public ValidChannelName NameObject { get; set; }
		[Optional]
        public ValidChannelPriceInUsCentsPerWeek PriceObject { get; set; }
    }

    public static partial class NewChannelDataExtensions
    {
        public static void Parse(this NewChannelData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (true || !ValidChannelName.IsEmpty(target.Name))
            {
                ValidChannelName @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidChannelName.TryParse(target.Name, out @object, out errorMessages))
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

            if (true || !ValidChannelPriceInUsCentsPerWeek.IsEmpty(target.Price))
            {
                ValidChannelPriceInUsCentsPerWeek @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidChannelPriceInUsCentsPerWeek.TryParse(target.Price, out @object, out errorMessages))
                {
                    target.PriceObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Price", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}

