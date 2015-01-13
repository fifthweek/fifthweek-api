using System;
using System.Linq;



namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class CreateSubscriptionCommand 
	{
        public CreateSubscriptionCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Subscriptions.SubscriptionId newSubscriptionId, 
            Fifthweek.Api.Subscriptions.ValidSubscriptionName subscriptionName, 
            Fifthweek.Api.Subscriptions.ValidTagline tagline, 
            Fifthweek.Api.Subscriptions.ChannelPriceInUsCentsPerWeek basePrice)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newSubscriptionId == null)
            {
                throw new ArgumentNullException("newSubscriptionId");
            }

            if (subscriptionName == null)
            {
                throw new ArgumentNullException("subscriptionName");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (basePrice == null)
            {
                throw new ArgumentNullException("basePrice");
            }

            this.Requester = requester;
            this.NewSubscriptionId = newSubscriptionId;
            this.SubscriptionName = subscriptionName;
            this.Tagline = tagline;
            this.BasePrice = basePrice;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
	public partial class CreateSubscriptionCommandHandler 
	{
        public CreateSubscriptionCommandHandler(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext, 
            Fifthweek.Api.Subscriptions.ISubscriptionSecurity subscriptionSecurity)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            if (subscriptionSecurity == null)
            {
                throw new ArgumentNullException("subscriptionSecurity");
            }

            this.fifthweekDbContext = fifthweekDbContext;
            this.subscriptionSecurity = subscriptionSecurity;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class PromoteNewUserToCreatorCommand 
	{
        public PromoteNewUserToCreatorCommand(
            Fifthweek.Api.Identity.Membership.UserId newUserId)
        {
            if (newUserId == null)
            {
                throw new ArgumentNullException("newUserId");
            }

            this.NewUserId = newUserId;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	public partial class PromoteNewUserToCreatorCommandHandler 
	{
        public PromoteNewUserToCreatorCommandHandler(
            Fifthweek.Api.Persistence.IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership.Events;
	public partial class PromoteNewUserToCreatorCommandInitiator 
	{
        public PromoteNewUserToCreatorCommandInitiator(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Subscriptions.Commands.PromoteNewUserToCreatorCommand> promoteNewUserToCreator)
        {
            if (promoteNewUserToCreator == null)
            {
                throw new ArgumentNullException("promoteNewUserToCreator");
            }

            this.promoteNewUserToCreator = promoteNewUserToCreator;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateSubscriptionCommand 
	{
        public UpdateSubscriptionCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Subscriptions.SubscriptionId subscriptionId, 
            Fifthweek.Api.Subscriptions.ValidSubscriptionName subscriptionName, 
            Fifthweek.Api.Subscriptions.ValidTagline tagline, 
            Fifthweek.Api.Subscriptions.ValidIntroduction introduction, 
            Fifthweek.Api.Subscriptions.ValidDescription description, 
            Fifthweek.Api.FileManagement.FileId headerImageFileId, 
            Fifthweek.Api.Subscriptions.ValidExternalVideoUrl video)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (subscriptionName == null)
            {
                throw new ArgumentNullException("subscriptionName");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (introduction == null)
            {
                throw new ArgumentNullException("introduction");
            }

            this.Requester = requester;
            this.SubscriptionId = subscriptionId;
            this.SubscriptionName = subscriptionName;
            this.Tagline = tagline;
            this.Introduction = introduction;
            this.Description = description;
            this.HeaderImageFileId = headerImageFileId;
            this.Video = video;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Persistence;
	public partial class UpdateSubscriptionCommandHandler 
	{
        public UpdateSubscriptionCommandHandler(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext, 
            Fifthweek.Api.Subscriptions.ISubscriptionSecurity subscriptionSecurity, 
            Fifthweek.Api.FileManagement.IFileRepository fileRepository)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (subscriptionSecurity == null)
            {
                throw new ArgumentNullException("subscriptionSecurity");
            }

            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            this.databaseContext = databaseContext;
            this.subscriptionSecurity = subscriptionSecurity;
            this.fileRepository = fileRepository;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	public partial class CreatorStatusData 
	{
        public CreatorStatusData(
            System.Nullable<System.Guid> subscriptionId, 
            System.Boolean mustWriteFirstPost)
        {
            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (mustWriteFirstPost == null)
            {
                throw new ArgumentNullException("mustWriteFirstPost");
            }

            this.SubscriptionId = subscriptionId;
            this.MustWriteFirstPost = mustWriteFirstPost;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.OAuth;
	using Fifthweek.Api.Subscriptions.Commands;
	using Fifthweek.Api.Subscriptions.Queries;
	public partial class SubscriptionController 
	{
        public SubscriptionController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Subscriptions.Commands.CreateSubscriptionCommand> setMandatorySubscriptionFields, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Subscriptions.Queries.GetCreatorStatusQuery,Fifthweek.Api.Subscriptions.CreatorStatus> getCreatorStatus, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext, 
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (setMandatorySubscriptionFields == null)
            {
                throw new ArgumentNullException("setMandatorySubscriptionFields");
            }

            if (getCreatorStatus == null)
            {
                throw new ArgumentNullException("getCreatorStatus");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.setMandatorySubscriptionFields = setMandatorySubscriptionFields;
            this.getCreatorStatus = getCreatorStatus;
            this.userContext = userContext;
            this.guidCreator = guidCreator;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using Fifthweek.Api.Core;
	public partial class CreatorStatus 
	{
        public CreatorStatus(
            Fifthweek.Api.Subscriptions.SubscriptionId subscriptionId, 
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
namespace Fifthweek.Api.Subscriptions.Queries
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetCreatorStatusQuery 
	{
        public GetCreatorStatusQuery(
            Fifthweek.Api.Identity.Membership.UserId creatorId)
        {
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            this.CreatorId = creatorId;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Queries
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetCreatorStatusQueryHandler 
	{
        public GetCreatorStatusQueryHandler(
            Fifthweek.Api.Persistence.IFifthweekDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            this.dbContext = dbContext;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using Fifthweek.Api.Core;
	public partial class SubscriptionId 
	{
        public SubscriptionId(
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
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	using Fifthweek.Api.Persistence.Identity;
	public partial class SubscriptionSecurity 
	{
        public SubscriptionSecurity(
            Fifthweek.Api.Persistence.IUserManager userManager, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.userManager = userManager;
            this.databaseContext = databaseContext;
        }
	}

}

namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Api.Core;
	public partial class ChannelPriceInUsCentsPerWeek 
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

            return this.Equals((ChannelPriceInUsCentsPerWeek)obj);
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

        protected bool Equals(ChannelPriceInUsCentsPerWeek other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class CreateSubscriptionCommand 
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

            return this.Equals((CreateSubscriptionCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewSubscriptionId != null ? this.NewSubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(CreateSubscriptionCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.NewSubscriptionId, other.NewSubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.BasePrice, other.BasePrice))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class PromoteNewUserToCreatorCommand 
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

            return this.Equals((PromoteNewUserToCreatorCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NewUserId != null ? this.NewUserId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PromoteNewUserToCreatorCommand other)
        {
            if (!object.Equals(this.NewUserId, other.NewUserId))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	using Fifthweek.Api.Identity.Membership;
	public partial class UpdateSubscriptionCommand 
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

            return this.Equals((UpdateSubscriptionCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdateSubscriptionCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.Introduction, other.Introduction))
            {
                return false;
            }

            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }

            if (!object.Equals(this.HeaderImageFileId, other.HeaderImageFileId))
            {
                return false;
            }

            if (!object.Equals(this.Video, other.Video))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	public partial class CreatorStatusData 
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

            return this.Equals((CreatorStatusData)obj);
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

        protected bool Equals(CreatorStatusData other)
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
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using Fifthweek.Api.Core;
	public partial class NewSubscriptionData 
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

            return this.Equals((NewSubscriptionData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewSubscriptionData other)
        {
            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.BasePrice, other.BasePrice))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	public partial class UpdatedSubscriptionData 
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

            return this.Equals((UpdatedSubscriptionData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UpdatedSubscriptionData other)
        {
            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.Introduction, other.Introduction))
            {
                return false;
            }

            if (!object.Equals(this.HeaderImageFileId, other.HeaderImageFileId))
            {
                return false;
            }

            if (!object.Equals(this.Video, other.Video))
            {
                return false;
            }

            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using Fifthweek.Api.Core;
	public partial class CreatorStatus 
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

            return this.Equals((CreatorStatus)obj);
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

        protected bool Equals(CreatorStatus other)
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
namespace Fifthweek.Api.Subscriptions.Queries
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GetCreatorStatusQuery 
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

            return this.Equals((GetCreatorStatusQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetCreatorStatusQuery other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using Fifthweek.Api.Core;
	public partial class SubscriptionId 
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

            return this.Equals((SubscriptionId)obj);
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

        protected bool Equals(SubscriptionId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Api.Core;
	public partial class ValidDescription 
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

            return this.Equals((ValidDescription)obj);
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

        protected bool Equals(ValidDescription other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fifthweek.Api.Core;
	public partial class ValidSubscriptionName 
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

            return this.Equals((ValidSubscriptionName)obj);
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

        protected bool Equals(ValidSubscriptionName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fifthweek.Api.Core;
	public partial class ValidTagline 
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

            return this.Equals((ValidTagline)obj);
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

        protected bool Equals(ValidTagline other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}

namespace Fifthweek.Api.Subscriptions.Controllers
{
	using Fifthweek.Api.Core;
	public partial class NewSubscriptionData 
	{
		public ValidSubscriptionName SubscriptionNameObject { get; set; }
		public ValidTagline TaglineObject { get; set; }
		public ChannelPriceInUsCentsPerWeek BasePriceObject { get; set; }

		public void Parse()
		{
			NewSubscriptionDataExtensions.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	public static partial class NewSubscriptionDataExtensions
	{
		public static void Parse(NewSubscriptionData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (true || !ValidSubscriptionName.IsEmpty(target.SubscriptionName))
			{
				ValidSubscriptionName @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidSubscriptionName.TryParse(target.SubscriptionName, out @object, out errorMessages))
				{
					target.SubscriptionNameObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("SubscriptionName", modelState);
				}
			}

			if (true || !ValidTagline.IsEmpty(target.Tagline))
			{
				ValidTagline @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidTagline.TryParse(target.Tagline, out @object, out errorMessages))
				{
					target.TaglineObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Tagline", modelState);
				}
			}

			if (true || !ChannelPriceInUsCentsPerWeek.IsEmpty(target.BasePrice))
			{
				ChannelPriceInUsCentsPerWeek @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ChannelPriceInUsCentsPerWeek.TryParse(target.BasePrice, out @object, out errorMessages))
				{
					target.BasePriceObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("BasePrice", modelState);
				}
			}

			if (!modelStateDictionary.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
			}
		}	
	}
}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement;
	public partial class UpdatedSubscriptionData 
	{
		public SubscriptionId SubscriptionIdObject { get; set; }
		public ValidSubscriptionName SubscriptionNameObject { get; set; }
		public ValidTagline TaglineObject { get; set; }
		public ValidIntroduction IntroductionObject { get; set; }
		public FileId HeaderImageFileIdObject { get; set; }
		public ValidExternalVideoUrl VideoObject { get; set; }
		public ValidDescription DescriptionObject { get; set; }

		public void Parse()
		{
			UpdatedSubscriptionDataExtensions.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	public static partial class UpdatedSubscriptionDataExtensions
	{
		public static void Parse(UpdatedSubscriptionData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

		    if (target.SubscriptionId != null)
		    {
                target.SubscriptionIdObject = new SubscriptionId(target.SubscriptionId);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("SubscriptionId", modelState);
            }

			if (true || !ValidSubscriptionName.IsEmpty(target.SubscriptionName))
			{
				ValidSubscriptionName @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidSubscriptionName.TryParse(target.SubscriptionName, out @object, out errorMessages))
				{
					target.SubscriptionNameObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("SubscriptionName", modelState);
				}
			}

			if (true || !ValidTagline.IsEmpty(target.Tagline))
			{
				ValidTagline @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidTagline.TryParse(target.Tagline, out @object, out errorMessages))
				{
					target.TaglineObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Tagline", modelState);
				}
			}

			if (true || !ValidIntroduction.IsEmpty(target.Introduction))
			{
				ValidIntroduction @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidIntroduction.TryParse(target.Introduction, out @object, out errorMessages))
				{
					target.IntroductionObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Introduction", modelState);
				}
			}

		    if (target.HeaderImageFileId != null)
		    {
                target.HeaderImageFileIdObject = new FileId(target.HeaderImageFileId.Value);
		    }
		    else if (false)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("HeaderImageFileId", modelState);
            }

			if (false || !ValidExternalVideoUrl.IsEmpty(target.Video))
			{
				ValidExternalVideoUrl @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidExternalVideoUrl.TryParse(target.Video, out @object, out errorMessages))
				{
					target.VideoObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Video", modelState);
				}
			}

			if (true || !ValidDescription.IsEmpty(target.Description))
			{
				ValidDescription @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ValidDescription.TryParse(target.Description, out @object, out errorMessages))
				{
					target.DescriptionObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Description", modelState);
				}
			}

			if (!modelStateDictionary.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
			}
		}	
	}
}

