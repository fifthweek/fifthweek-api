using System;
using System.Linq;

//// Generated on 21/09/2015 08:49:21 (UTC)
//// Mapped solution in 70.45s


namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class AcceptChannelSubscriptionPriceChangeDbStatement 
    {
        public AcceptChannelSubscriptionPriceChangeDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.SnapshotCreation.IRequestSnapshotService requestSnapshot)
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class BlogOwnership 
    {
        public BlogOwnership(
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
namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class BlogSecurity 
    {
        public BlogSecurity(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IBlogOwnership blogOwnership)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (blogOwnership == null)
            {
                throw new ArgumentNullException("blogOwnership");
            }

            this.requesterSecurity = requesterSecurity;
            this.blogOwnership = blogOwnership;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class BlogSubscriptionDbResult 
    {
        public BlogSubscriptionDbResult(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String name,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Identity.Shared.Membership.Username creatorUsername,
            Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId,
            System.Boolean freeAccess,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelSubscriptionStatus> channels)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (creatorUsername == null)
            {
                throw new ArgumentNullException("creatorUsername");
            }

            if (freeAccess == null)
            {
                throw new ArgumentNullException("freeAccess");
            }

            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.BlogId = blogId;
            this.Name = name;
            this.CreatorId = creatorId;
            this.CreatorUsername = creatorUsername;
            this.ProfileImageFileId = profileImageFileId;
            this.FreeAccess = freeAccess;
            this.Channels = channels;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class AcceptChannelSubscriptionPriceChangeCommand 
    {
        public AcceptChannelSubscriptionPriceChangeCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Channels.Shared.ValidAcceptedChannelPrice acceptedPrice)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
            this.AcceptedPrice = acceptedPrice;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class AcceptChannelSubscriptionPriceChangeCommandHandler 
    {
        public AcceptChannelSubscriptionPriceChangeCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IAcceptChannelSubscriptionPriceChangeDbStatement acceptPriceChange)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (acceptPriceChange == null)
            {
                throw new ArgumentNullException("acceptPriceChange");
            }

            this.requesterSecurity = requesterSecurity;
            this.acceptPriceChange = acceptPriceChange;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class AcceptedChannelSubscription 
    {
        public AcceptedChannelSubscription(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Channels.Shared.ValidAcceptedChannelPrice acceptedPrice)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            this.ChannelId = channelId;
            this.AcceptedPrice = acceptedPrice;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreateBlogCommand 
    {
        public CreateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId newBlogId,
            Fifthweek.Api.Channels.Shared.ChannelId firstChannelId,
            Fifthweek.Api.Blogs.Shared.ValidBlogName blogName,
            Fifthweek.Api.Channels.Shared.ValidChannelPrice basePrice)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newBlogId == null)
            {
                throw new ArgumentNullException("newBlogId");
            }

            if (firstChannelId == null)
            {
                throw new ArgumentNullException("firstChannelId");
            }

            if (blogName == null)
            {
                throw new ArgumentNullException("blogName");
            }

            if (basePrice == null)
            {
                throw new ArgumentNullException("basePrice");
            }

            this.Requester = requester;
            this.NewBlogId = newBlogId;
            this.FirstChannelId = firstChannelId;
            this.BlogName = blogName;
            this.BasePrice = basePrice;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreateBlogCommandHandler 
    {
        public CreateBlogCommandHandler(
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.SnapshotCreation.IRequestSnapshotService requestSnapshot)
        {
            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (requestSnapshot == null)
            {
                throw new ArgumentNullException("requestSnapshot");
            }

            this.blogSecurity = blogSecurity;
            this.requesterSecurity = requesterSecurity;
            this.connectionFactory = connectionFactory;
            this.requestSnapshot = requestSnapshot;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UnsubscribeFromChannelCommand 
    {
        public UnsubscribeFromChannelCommand(
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UnsubscribeFromChannelCommandHandler 
    {
        public UnsubscribeFromChannelCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IUnsubscribeFromChannelDbStatement unsubscribeFromChannel)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (unsubscribeFromChannel == null)
            {
                throw new ArgumentNullException("unsubscribeFromChannel");
            }

            this.requesterSecurity = requesterSecurity;
            this.unsubscribeFromChannel = unsubscribeFromChannel;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogCommand 
    {
        public UpdateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Blogs.Shared.ValidBlogName blogName,
            Fifthweek.Api.Blogs.Shared.ValidIntroduction introduction,
            Fifthweek.Api.Blogs.Shared.ValidBlogDescription description,
            Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
            Fifthweek.Api.Blogs.Shared.ValidExternalVideoUrl video)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (blogName == null)
            {
                throw new ArgumentNullException("blogName");
            }

            this.Requester = requester;
            this.BlogId = blogId;
            this.BlogName = blogName;
            this.Introduction = introduction;
            this.Description = description;
            this.HeaderImageFileId = headerImageFileId;
            this.Video = video;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogCommandHandler 
    {
        public UpdateBlogCommandHandler(
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.blogSecurity = blogSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogSubscriptionsCommand 
    {
        public UpdateBlogSubscriptionsCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Commands.AcceptedChannelSubscription> channels)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.Requester = requester;
            this.BlogId = blogId;
            this.Channels = channels;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogSubscriptionsCommandHandler 
    {
        public UpdateBlogSubscriptionsCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IUpdateBlogSubscriptionsDbStatement updateBlogSubscriptions,
            Fifthweek.Api.Blogs.IGetIsTestUserBlogDbStatement getIsTestUserBlog)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (updateBlogSubscriptions == null)
            {
                throw new ArgumentNullException("updateBlogSubscriptions");
            }

            if (getIsTestUserBlog == null)
            {
                throw new ArgumentNullException("getIsTestUserBlog");
            }

            this.requesterSecurity = requesterSecurity;
            this.updateBlogSubscriptions = updateBlogSubscriptions;
            this.getIsTestUserBlog = getIsTestUserBlog;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateFreeAccessUsersCommand 
    {
        public UpdateFreeAccessUsersCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Identity.Shared.Membership.ValidEmail> emailAddresses)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (emailAddresses == null)
            {
                throw new ArgumentNullException("emailAddresses");
            }

            this.Requester = requester;
            this.BlogId = blogId;
            this.EmailAddresses = emailAddresses;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateFreeAccessUsersCommandHandler 
    {
        public UpdateFreeAccessUsersCommandHandler(
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IUpdateFreeAccessUsersDbStatement updateFreeAccessUsers)
        {
            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (updateFreeAccessUsers == null)
            {
                throw new ArgumentNullException("updateFreeAccessUsers");
            }

            this.blogSecurity = blogSecurity;
            this.requesterSecurity = requesterSecurity;
            this.updateFreeAccessUsers = updateFreeAccessUsers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class BlogAccessController 
    {
        public BlogAccessController(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.UpdateFreeAccessUsersCommand> updateFreeAccessUsers,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetFreeAccessUsersQuery,Fifthweek.Api.Blogs.Queries.GetFreeAccessUsersResult> getFreeAccessUsers)
        {
            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (updateFreeAccessUsers == null)
            {
                throw new ArgumentNullException("updateFreeAccessUsers");
            }

            if (getFreeAccessUsers == null)
            {
                throw new ArgumentNullException("getFreeAccessUsers");
            }

            this.requesterContext = requesterContext;
            this.updateFreeAccessUsers = updateFreeAccessUsers;
            this.getFreeAccessUsers = getFreeAccessUsers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class BlogController 
    {
        public BlogController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.CreateBlogCommand> createBlog,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.UpdateBlogCommand> updateBlog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetLandingPageQuery,Fifthweek.Api.Blogs.Queries.GetLandingPageResult> getLandingPage,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetBlogSubscriberInformationQuery,Fifthweek.Api.Blogs.Queries.BlogSubscriberInformation> getBlogSubscriberInformation,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetAllCreatorRevenuesQuery,Fifthweek.Api.Blogs.GetAllCreatorRevenuesResult> getAllCreatorRevenues,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Shared.IGuidCreator guidCreator)
        {
            if (createBlog == null)
            {
                throw new ArgumentNullException("createBlog");
            }

            if (updateBlog == null)
            {
                throw new ArgumentNullException("updateBlog");
            }

            if (getLandingPage == null)
            {
                throw new ArgumentNullException("getLandingPage");
            }

            if (getBlogSubscriberInformation == null)
            {
                throw new ArgumentNullException("getBlogSubscriberInformation");
            }

            if (getAllCreatorRevenues == null)
            {
                throw new ArgumentNullException("getAllCreatorRevenues");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.createBlog = createBlog;
            this.updateBlog = updateBlog;
            this.getLandingPage = getLandingPage;
            this.getBlogSubscriberInformation = getBlogSubscriberInformation;
            this.getAllCreatorRevenues = getAllCreatorRevenues;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PutFreeAccessUsersResult 
    {
        public PutFreeAccessUsersResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Identity.Shared.Membership.Email> invalidEmailAddresses)
        {
            if (invalidEmailAddresses == null)
            {
                throw new ArgumentNullException("invalidEmailAddresses");
            }

            this.InvalidEmailAddresses = invalidEmailAddresses;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class SubscriptionController 
    {
        public SubscriptionController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.UpdateBlogSubscriptionsCommand> updateBlogSubscriptions,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.UnsubscribeFromChannelCommand> unsubscribeFromChannel,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.AcceptChannelSubscriptionPriceChangeCommand> acceptPriceChange,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
        {
            if (updateBlogSubscriptions == null)
            {
                throw new ArgumentNullException("updateBlogSubscriptions");
            }

            if (unsubscribeFromChannel == null)
            {
                throw new ArgumentNullException("unsubscribeFromChannel");
            }

            if (acceptPriceChange == null)
            {
                throw new ArgumentNullException("acceptPriceChange");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.updateBlogSubscriptions = updateBlogSubscriptions;
            this.unsubscribeFromChannel = unsubscribeFromChannel;
            this.acceptPriceChange = acceptPriceChange;
            this.requesterContext = requesterContext;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class CreatorStatus 
    {
        public CreatorStatus(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.Boolean mustWriteFirstPost)
        {
            if (mustWriteFirstPost == null)
            {
                throw new ArgumentNullException("mustWriteFirstPost");
            }

            this.BlogId = blogId;
            this.MustWriteFirstPost = mustWriteFirstPost;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetAllCreatorRevenuesDbStatement 
    {
        public GetAllCreatorRevenuesDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetAllCreatorRevenuesResult
    {
        public partial class Creator 
        {
            public Creator(
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                System.Int32 unreleasedRevenue,
                System.Int32 releasedRevenue,
                System.Int32 releasableRevenue,
                Fifthweek.Api.Identity.Shared.Membership.Username username,
                Fifthweek.Api.Identity.Shared.Membership.Email email,
                System.Boolean emailConfirmed)
            {
                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (unreleasedRevenue == null)
                {
                    throw new ArgumentNullException("unreleasedRevenue");
                }

                if (releasedRevenue == null)
                {
                    throw new ArgumentNullException("releasedRevenue");
                }

                if (releasableRevenue == null)
                {
                    throw new ArgumentNullException("releasableRevenue");
                }

                if (emailConfirmed == null)
                {
                    throw new ArgumentNullException("emailConfirmed");
                }

                this.UserId = userId;
                this.UnreleasedRevenue = unreleasedRevenue;
                this.ReleasedRevenue = releasedRevenue;
                this.ReleasableRevenue = releasableRevenue;
                this.Username = username;
                this.Email = email;
                this.EmailConfirmed = emailConfirmed;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetAllCreatorRevenuesResult 
    {
        public GetAllCreatorRevenuesResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.GetAllCreatorRevenuesResult.Creator> creators)
        {
            if (creators == null)
            {
                throw new ArgumentNullException("creators");
            }

            this.Creators = creators;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogChannelsAndQueuesDbStatement
    {
        public partial class GetBlogChannelsAndQueuesDbResult 
        {
            public GetBlogChannelsAndQueuesDbResult(
                Fifthweek.Api.Blogs.GetBlogChannelsAndQueuesDbStatement.BlogDbResult blog,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelResult> channels)
            {
                if (blog == null)
                {
                    throw new ArgumentNullException("blog");
                }

                if (channels == null)
                {
                    throw new ArgumentNullException("channels");
                }

                this.Blog = blog;
                this.Channels = channels;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogChannelsAndQueuesDbStatement 
    {
        public GetBlogChannelsAndQueuesDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogChannelsAndQueuesDbStatement
    {
        public partial class BlogDbResult 
        {
            public BlogDbResult(
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Blogs.Shared.BlogName blogName,
                Fifthweek.Api.Blogs.Shared.Introduction introduction,
                System.DateTime creationDate,
                Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
                Fifthweek.Api.Blogs.Shared.ExternalVideoUrl video,
                Fifthweek.Api.Blogs.Shared.BlogDescription description,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.QueueResult> queues)
            {
                if (blogId == null)
                {
                    throw new ArgumentNullException("blogId");
                }

                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                if (blogName == null)
                {
                    throw new ArgumentNullException("blogName");
                }

                if (creationDate == null)
                {
                    throw new ArgumentNullException("creationDate");
                }

                if (queues == null)
                {
                    throw new ArgumentNullException("queues");
                }

                this.BlogId = blogId;
                this.CreatorId = creatorId;
                this.BlogName = blogName;
                this.Introduction = introduction;
                this.CreationDate = creationDate;
                this.HeaderImageFileId = headerImageFileId;
                this.Video = video;
                this.Description = description;
                this.Queues = queues;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogSubscriberInformationDbStatement
    {
        public partial class GetBlogSubscriberInformationDbStatementResult
        {
            public partial class Subscriber 
            {
                public Subscriber(
                    Fifthweek.Api.Identity.Shared.Membership.Username username,
                    Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                    Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId,
                    Fifthweek.Api.Channels.Shared.ChannelId channelId,
                    System.DateTime subscriptionStartDate,
                    System.Int32 acceptedPrice,
                    Fifthweek.Api.Identity.Shared.Membership.Email freeAccessEmail,
                    Fifthweek.Api.Persistence.Payments.PaymentStatus paymentStatus,
                    System.Boolean hasPaymentInformation)
                {
                    if (username == null)
                    {
                        throw new ArgumentNullException("username");
                    }

                    if (userId == null)
                    {
                        throw new ArgumentNullException("userId");
                    }

                    if (channelId == null)
                    {
                        throw new ArgumentNullException("channelId");
                    }

                    if (subscriptionStartDate == null)
                    {
                        throw new ArgumentNullException("subscriptionStartDate");
                    }

                    if (acceptedPrice == null)
                    {
                        throw new ArgumentNullException("acceptedPrice");
                    }

                    if (paymentStatus == null)
                    {
                        throw new ArgumentNullException("paymentStatus");
                    }

                    if (hasPaymentInformation == null)
                    {
                        throw new ArgumentNullException("hasPaymentInformation");
                    }

                    this.Username = username;
                    this.UserId = userId;
                    this.ProfileImageFileId = profileImageFileId;
                    this.ChannelId = channelId;
                    this.SubscriptionStartDate = subscriptionStartDate;
                    this.AcceptedPrice = acceptedPrice;
                    this.FreeAccessEmail = freeAccessEmail;
                    this.PaymentStatus = paymentStatus;
                    this.HasPaymentInformation = hasPaymentInformation;
                }
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogSubscriberInformationDbStatement
    {
        public partial class GetBlogSubscriberInformationDbStatementResult 
        {
            public GetBlogSubscriberInformationDbStatementResult(
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber> subscribers)
            {
                if (subscribers == null)
                {
                    throw new ArgumentNullException("subscribers");
                }

                this.Subscribers = subscribers;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogSubscriberInformationDbStatement 
    {
        public GetBlogSubscriberInformationDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetCreatorRevenueDbStatement
    {
        public partial class GetCreatorRevenueDbStatementResult 
        {
            public GetCreatorRevenueDbStatementResult(
                System.Int32 unreleasedRevenue,
                System.Int32 releasedRevenue,
                System.Int32 releasableRevenue)
            {
                if (unreleasedRevenue == null)
                {
                    throw new ArgumentNullException("unreleasedRevenue");
                }

                if (releasedRevenue == null)
                {
                    throw new ArgumentNullException("releasedRevenue");
                }

                if (releasableRevenue == null)
                {
                    throw new ArgumentNullException("releasableRevenue");
                }

                this.UnreleasedRevenue = unreleasedRevenue;
                this.ReleasedRevenue = releasedRevenue;
                this.ReleasableRevenue = releasableRevenue;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetCreatorRevenueDbStatement 
    {
        public GetCreatorRevenueDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetFreeAccessUsersDbStatement 
    {
        public GetFreeAccessUsersDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetIsTestUserBlogDbStatement 
    {
        public GetIsTestUserBlogDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetLandingPageDbStatement
    {
        public partial class GetLandingPageDbResult 
        {
            public GetLandingPageDbResult(
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId,
                Fifthweek.Api.Blogs.GetBlogChannelsAndQueuesDbStatement.BlogDbResult blog,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelResult> channels)
            {
                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (blog == null)
                {
                    throw new ArgumentNullException("blog");
                }

                if (channels == null)
                {
                    throw new ArgumentNullException("channels");
                }

                this.UserId = userId;
                this.ProfileImageFileId = profileImageFileId;
                this.Blog = blog;
                this.Channels = channels;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetLandingPageDbStatement 
    {
        public GetLandingPageDbStatement(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetUserSubscriptionsDbStatement 
    {
        public GetUserSubscriptionsDbStatement(
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation
    {
        public partial class Subscriber 
        {
            public Subscriber(
                Fifthweek.Api.Identity.Shared.Membership.Username username,
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                Fifthweek.Api.FileManagement.Shared.FileInformation profileImage,
                Fifthweek.Api.Identity.Shared.Membership.Email freeAccessEmail,
                Fifthweek.Api.Persistence.Payments.PaymentStatus paymentStatus,
                System.Boolean hasPaymentInformation,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.BlogSubscriberInformation.SubscriberChannel> channels)
            {
                if (username == null)
                {
                    throw new ArgumentNullException("username");
                }

                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (paymentStatus == null)
                {
                    throw new ArgumentNullException("paymentStatus");
                }

                if (hasPaymentInformation == null)
                {
                    throw new ArgumentNullException("hasPaymentInformation");
                }

                if (channels == null)
                {
                    throw new ArgumentNullException("channels");
                }

                this.Username = username;
                this.UserId = userId;
                this.ProfileImage = profileImage;
                this.FreeAccessEmail = freeAccessEmail;
                this.PaymentStatus = paymentStatus;
                this.HasPaymentInformation = hasPaymentInformation;
                this.Channels = channels;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation 
    {
        public BlogSubscriberInformation(
            System.Int32 unreleasedRevenue,
            System.Int32 releasedRevenue,
            System.Int32 releasableRevenue,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.BlogSubscriberInformation.Subscriber> subscribers)
        {
            if (unreleasedRevenue == null)
            {
                throw new ArgumentNullException("unreleasedRevenue");
            }

            if (releasedRevenue == null)
            {
                throw new ArgumentNullException("releasedRevenue");
            }

            if (releasableRevenue == null)
            {
                throw new ArgumentNullException("releasableRevenue");
            }

            if (subscribers == null)
            {
                throw new ArgumentNullException("subscribers");
            }

            this.UnreleasedRevenue = unreleasedRevenue;
            this.ReleasedRevenue = releasedRevenue;
            this.ReleasableRevenue = releasableRevenue;
            this.Subscribers = subscribers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation
    {
        public partial class SubscriberChannel 
        {
            public SubscriberChannel(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                System.DateTime subscriptionStartDate,
                System.Int32 acceptedPrice)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (subscriptionStartDate == null)
                {
                    throw new ArgumentNullException("subscriptionStartDate");
                }

                if (acceptedPrice == null)
                {
                    throw new ArgumentNullException("acceptedPrice");
                }

                this.ChannelId = channelId;
                this.SubscriptionStartDate = subscriptionStartDate;
                this.AcceptedPrice = acceptedPrice;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriptionStatus 
    {
        public BlogSubscriptionStatus(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String name,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Identity.Shared.Membership.Username username,
            Fifthweek.Api.FileManagement.Shared.FileInformation profileImage,
            System.Boolean freeAccess,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelSubscriptionStatus> channels)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (freeAccess == null)
            {
                throw new ArgumentNullException("freeAccess");
            }

            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.BlogId = blogId;
            this.Name = name;
            this.CreatorId = creatorId;
            this.Username = username;
            this.ProfileImage = profileImage;
            this.FreeAccess = freeAccess;
            this.Channels = channels;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogWithFileInformation 
    {
        public BlogWithFileInformation(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Blogs.Shared.BlogName name,
            Fifthweek.Api.Blogs.Shared.Introduction introduction,
            System.DateTime creationDate,
            Fifthweek.Api.FileManagement.Shared.FileInformation headerImage,
            Fifthweek.Api.Blogs.Shared.ExternalVideoUrl video,
            Fifthweek.Api.Blogs.Shared.BlogDescription description,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelResult> channels,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.QueueResult> queues)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            if (queues == null)
            {
                throw new ArgumentNullException("queues");
            }

            this.BlogId = blogId;
            this.Name = name;
            this.Introduction = introduction;
            this.CreationDate = creationDate;
            this.HeaderImage = headerImage;
            this.Video = video;
            this.Description = description;
            this.Channels = channels;
            this.Queues = queues;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class ChannelResult 
    {
        public ChannelResult(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String name,
            System.Int32 price,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.ChannelId = channelId;
            this.Name = name;
            this.Price = price;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class ChannelSubscriptionStatus 
    {
        public ChannelSubscriptionStatus(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String name,
            System.Int32 acceptedPrice,
            System.Int32 price,
            System.Boolean isDefault,
            System.DateTime priceLastSetDate,
            System.DateTime subscriptionStartDate,
            System.Boolean isVisibleToNonSubscribers)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (isDefault == null)
            {
                throw new ArgumentNullException("isDefault");
            }

            if (priceLastSetDate == null)
            {
                throw new ArgumentNullException("priceLastSetDate");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            this.ChannelId = channelId;
            this.Name = name;
            this.AcceptedPrice = acceptedPrice;
            this.Price = price;
            this.IsDefault = isDefault;
            this.PriceLastSetDate = priceLastSetDate;
            this.SubscriptionStartDate = subscriptionStartDate;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetAllCreatorRevenuesQuery 
    {
        public GetAllCreatorRevenuesQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.Requester = requester;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetAllCreatorRevenuesQueryHandler 
    {
        public GetAllCreatorRevenuesQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IGetAllCreatorRevenuesDbStatement getAllCreatorRevenuesDbStatement,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getAllCreatorRevenuesDbStatement == null)
            {
                throw new ArgumentNullException("getAllCreatorRevenuesDbStatement");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.getAllCreatorRevenuesDbStatement = getAllCreatorRevenuesDbStatement;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogChannelsAndQueuesQuery 
    {
        public GetBlogChannelsAndQueuesQuery(
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogChannelsAndQueuesQueryHandler 
    {
        public GetBlogChannelsAndQueuesQueryHandler(
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Blogs.IGetBlogChannelsAndQueuesDbStatement getBlogChannelsAndQueues)
        {
            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (getBlogChannelsAndQueues == null)
            {
                throw new ArgumentNullException("getBlogChannelsAndQueues");
            }

            this.fileInformationAggregator = fileInformationAggregator;
            this.getBlogChannelsAndQueues = getBlogChannelsAndQueues;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogChannelsAndQueuesResult 
    {
        public GetBlogChannelsAndQueuesResult(
            Fifthweek.Api.Blogs.Queries.BlogWithFileInformation blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException("blog");
            }

            this.Blog = blog;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogSubscriberInformationQuery 
    {
        public GetBlogSubscriberInformationQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            this.Requester = requester;
            this.BlogId = blogId;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogSubscriberInformationQueryHandler 
    {
        public GetBlogSubscriberInformationQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Blogs.IGetBlogSubscriberInformationDbStatement getBlogSubscriberInformation,
            Fifthweek.Api.Blogs.IGetCreatorRevenueDbStatement getCreatorRevenue,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (getBlogSubscriberInformation == null)
            {
                throw new ArgumentNullException("getBlogSubscriberInformation");
            }

            if (getCreatorRevenue == null)
            {
                throw new ArgumentNullException("getCreatorRevenue");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.blogSecurity = blogSecurity;
            this.fileInformationAggregator = fileInformationAggregator;
            this.getBlogSubscriberInformation = getBlogSubscriberInformation;
            this.getCreatorRevenue = getCreatorRevenue;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersQuery 
    {
        public GetFreeAccessUsersQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            this.Requester = requester;
            this.BlogId = blogId;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersQueryHandler 
    {
        public GetFreeAccessUsersQueryHandler(
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IGetFreeAccessUsersDbStatement getFreeAccessUsers)
        {
            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getFreeAccessUsers == null)
            {
                throw new ArgumentNullException("getFreeAccessUsers");
            }

            this.blogSecurity = blogSecurity;
            this.requesterSecurity = requesterSecurity;
            this.getFreeAccessUsers = getFreeAccessUsers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersResult
    {
        public partial class FreeAccessUser 
        {
            public FreeAccessUser(
                Fifthweek.Api.Identity.Shared.Membership.Email email,
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                Fifthweek.Api.Identity.Shared.Membership.Username username,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Channels.Shared.ChannelId> channelIds)
            {
                if (email == null)
                {
                    throw new ArgumentNullException("email");
                }

                if (channelIds == null)
                {
                    throw new ArgumentNullException("channelIds");
                }

                this.Email = email;
                this.UserId = userId;
                this.Username = username;
                this.ChannelIds = channelIds;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersResult 
    {
        public GetFreeAccessUsersResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.GetFreeAccessUsersResult.FreeAccessUser> freeAccessUsers)
        {
            if (freeAccessUsers == null)
            {
                throw new ArgumentNullException("freeAccessUsers");
            }

            this.FreeAccessUsers = freeAccessUsers;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetLandingPageQuery 
    {
        public GetLandingPageQuery(
            Fifthweek.Api.Identity.Shared.Membership.Username creatorUsername)
        {
            if (creatorUsername == null)
            {
                throw new ArgumentNullException("creatorUsername");
            }

            this.CreatorUsername = creatorUsername;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetLandingPageQueryHandler 
    {
        public GetLandingPageQueryHandler(
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Blogs.IGetLandingPageDbStatement getLandingPageDbStatement)
        {
            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (getLandingPageDbStatement == null)
            {
                throw new ArgumentNullException("getLandingPageDbStatement");
            }

            this.fileInformationAggregator = fileInformationAggregator;
            this.getLandingPageDbStatement = getLandingPageDbStatement;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetLandingPageResult 
    {
        public GetLandingPageResult(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.FileManagement.Shared.FileInformation profileImage,
            Fifthweek.Api.Blogs.Queries.BlogWithFileInformation blog)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (blog == null)
            {
                throw new ArgumentNullException("blog");
            }

            this.UserId = userId;
            this.ProfileImage = profileImage;
            this.Blog = blog;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetUserSubscriptionsQuery 
    {
        public GetUserSubscriptionsQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetUserSubscriptionsQueryHandler 
    {
        public GetUserSubscriptionsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IGetUserSubscriptionsDbStatement getUserSubscriptions,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getUserSubscriptions == null)
            {
                throw new ArgumentNullException("getUserSubscriptions");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserSubscriptions = getUserSubscriptions;
            this.fileInformationAggregator = fileInformationAggregator;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetUserSubscriptionsResult 
    {
        public GetUserSubscriptionsResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.BlogSubscriptionStatus> blogs)
        {
            if (blogs == null)
            {
                throw new ArgumentNullException("blogs");
            }

            this.Blogs = blogs;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class QueueResult 
    {
        public QueueResult(
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            System.String name,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Collections.Shared.HourOfWeek> weeklyReleaseSchedule)
        {
            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (weeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("weeklyReleaseSchedule");
            }

            this.QueueId = queueId;
            this.Name = name;
            this.WeeklyReleaseSchedule = weeklyReleaseSchedule;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class UnsubscribeFromChannelDbStatement 
    {
        public UnsubscribeFromChannelDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.SnapshotCreation.IRequestSnapshotService requestSnapshot)
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class UpdateBlogSubscriptionsDbStatement 
    {
        public UpdateBlogSubscriptionsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.SnapshotCreation.IRequestSnapshotService requestSnapshot)
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class UpdateFreeAccessUsersDbStatement 
    {
        public UpdateFreeAccessUsersDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Payments.SnapshotCreation.IRequestSnapshotService requestSnapshot)
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewBlogResult 
    {
        public NewBlogResult(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            this.BlogId = blogId;
            this.ChannelId = channelId;
        }
    }
}

namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class BlogSubscriptionDbResult 
    {
        public override string ToString()
        {
            return string.Format("BlogSubscriptionDbResult({0}, \"{1}\", {2}, {3}, {4}, {5}, {6})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.CreatorUsername == null ? "null" : this.CreatorUsername.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.FreeAccess == null ? "null" : this.FreeAccess.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
        
            return this.Equals((BlogSubscriptionDbResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorUsername != null ? this.CreatorUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FreeAccess != null ? this.FreeAccess.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Channels != null 
        			? this.Channels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlogSubscriptionDbResult other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorUsername, other.CreatorUsername))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
            {
                return false;
            }
        
            if (!object.Equals(this.FreeAccess, other.FreeAccess))
            {
                return false;
            }
        
            if (this.Channels != null && other.Channels != null)
            {
                if (!this.Channels.SequenceEqual(other.Channels))
                {
                    return false;    
                }
            }
            else if (this.Channels != null || other.Channels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class AcceptChannelSubscriptionPriceChangeCommand 
    {
        public override string ToString()
        {
            return string.Format("AcceptChannelSubscriptionPriceChangeCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString());
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
        
            return this.Equals((AcceptChannelSubscriptionPriceChangeCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AcceptChannelSubscriptionPriceChangeCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class AcceptedChannelSubscription 
    {
        public override string ToString()
        {
            return string.Format("AcceptedChannelSubscription({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString());
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
        
            return this.Equals((AcceptedChannelSubscription)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AcceptedChannelSubscription other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateBlogCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewBlogId == null ? "null" : this.NewBlogId.ToString(), this.FirstChannelId == null ? "null" : this.FirstChannelId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
        
            return this.Equals((CreateBlogCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewBlogId != null ? this.NewBlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FirstChannelId != null ? this.FirstChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateBlogCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewBlogId, other.NewBlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.FirstChannelId, other.FirstChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogName, other.BlogName))
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UnsubscribeFromChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("UnsubscribeFromChannelCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString());
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
        
            return this.Equals((UnsubscribeFromChannelCommand)obj);
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
        
        protected bool Equals(UnsubscribeFromChannelCommand other)
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateBlogCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString());
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
        
            return this.Equals((UpdateBlogCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateBlogCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogName, other.BlogName))
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateBlogSubscriptionsCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateBlogSubscriptionsCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
        
            return this.Equals((UpdateBlogSubscriptionsCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Channels != null 
        			? this.Channels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateBlogSubscriptionsCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (this.Channels != null && other.Channels != null)
            {
                if (!this.Channels.SequenceEqual(other.Channels))
                {
                    return false;    
                }
            }
            else if (this.Channels != null || other.Channels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using System.Transactions;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateFreeAccessUsersCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateFreeAccessUsersCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.EmailAddresses == null ? "null" : this.EmailAddresses.ToString());
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
        
            return this.Equals((UpdateFreeAccessUsersCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EmailAddresses != null 
        			? this.EmailAddresses.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateFreeAccessUsersCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (this.EmailAddresses != null && other.EmailAddresses != null)
            {
                if (!this.EmailAddresses.SequenceEqual(other.EmailAddresses))
                {
                    return false;    
                }
            }
            else if (this.EmailAddresses != null || other.EmailAddresses != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PutFreeAccessUsersResult 
    {
        public override string ToString()
        {
            return string.Format("PutFreeAccessUsersResult({0})", this.InvalidEmailAddresses == null ? "null" : this.InvalidEmailAddresses.ToString());
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
        
            return this.Equals((PutFreeAccessUsersResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.InvalidEmailAddresses != null 
        			? this.InvalidEmailAddresses.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PutFreeAccessUsersResult other)
        {
            if (this.InvalidEmailAddresses != null && other.InvalidEmailAddresses != null)
            {
                if (!this.InvalidEmailAddresses.SequenceEqual(other.InvalidEmailAddresses))
                {
                    return false;    
                }
            }
            else if (this.InvalidEmailAddresses != null || other.InvalidEmailAddresses != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class CreatorStatus 
    {
        public override string ToString()
        {
            return string.Format("CreatorStatus({0}, {1})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.MustWriteFirstPost == null ? "null" : this.MustWriteFirstPost.ToString());
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
        
            return this.Equals((CreatorStatus)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.MustWriteFirstPost != null ? this.MustWriteFirstPost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorStatus other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetAllCreatorRevenuesResult
    {
        public partial class Creator 
        {
            public override string ToString()
            {
                return string.Format("Creator({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.UserId == null ? "null" : this.UserId.ToString(), this.UnreleasedRevenue == null ? "null" : this.UnreleasedRevenue.ToString(), this.ReleasedRevenue == null ? "null" : this.ReleasedRevenue.ToString(), this.ReleasableRevenue == null ? "null" : this.ReleasableRevenue.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.EmailConfirmed == null ? "null" : this.EmailConfirmed.ToString());
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
            
                return this.Equals((Creator)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.UnreleasedRevenue != null ? this.UnreleasedRevenue.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ReleasedRevenue != null ? this.ReleasedRevenue.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ReleasableRevenue != null ? this.ReleasableRevenue.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.EmailConfirmed != null ? this.EmailConfirmed.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(Creator other)
            {
                if (!object.Equals(this.UserId, other.UserId))
                {
                    return false;
                }
            
                if (!object.Equals(this.UnreleasedRevenue, other.UnreleasedRevenue))
                {
                    return false;
                }
            
                if (!object.Equals(this.ReleasedRevenue, other.ReleasedRevenue))
                {
                    return false;
                }
            
                if (!object.Equals(this.ReleasableRevenue, other.ReleasableRevenue))
                {
                    return false;
                }
            
                if (!object.Equals(this.Username, other.Username))
                {
                    return false;
                }
            
                if (!object.Equals(this.Email, other.Email))
                {
                    return false;
                }
            
                if (!object.Equals(this.EmailConfirmed, other.EmailConfirmed))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetAllCreatorRevenuesResult 
    {
        public override string ToString()
        {
            return string.Format("GetAllCreatorRevenuesResult({0})", this.Creators == null ? "null" : this.Creators.ToString());
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
        
            return this.Equals((GetAllCreatorRevenuesResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Creators != null 
        			? this.Creators.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetAllCreatorRevenuesResult other)
        {
            if (this.Creators != null && other.Creators != null)
            {
                if (!this.Creators.SequenceEqual(other.Creators))
                {
                    return false;    
                }
            }
            else if (this.Creators != null || other.Creators != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogSubscriberInformationDbStatement
    {
        public partial class GetBlogSubscriberInformationDbStatementResult
        {
            public partial class Subscriber 
            {
                public override string ToString()
                {
                    return string.Format("Subscriber({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.Username == null ? "null" : this.Username.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.FreeAccessEmail == null ? "null" : this.FreeAccessEmail.ToString(), this.PaymentStatus == null ? "null" : this.PaymentStatus.ToString(), this.HasPaymentInformation == null ? "null" : this.HasPaymentInformation.ToString());
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
                
                    return this.Equals((Subscriber)obj);
                }
                
                public override int GetHashCode()
                {
                    unchecked
                    {
                        int hashCode = 0;
                        hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.FreeAccessEmail != null ? this.FreeAccessEmail.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.PaymentStatus != null ? this.PaymentStatus.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.HasPaymentInformation != null ? this.HasPaymentInformation.GetHashCode() : 0);
                        return hashCode;
                    }
                }
                
                protected bool Equals(Subscriber other)
                {
                    if (!object.Equals(this.Username, other.Username))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.UserId, other.UserId))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.ChannelId, other.ChannelId))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.FreeAccessEmail, other.FreeAccessEmail))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.PaymentStatus, other.PaymentStatus))
                    {
                        return false;
                    }
                
                    if (!object.Equals(this.HasPaymentInformation, other.HasPaymentInformation))
                    {
                        return false;
                    }
                
                    return true;
                }
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetBlogSubscriberInformationDbStatement
    {
        public partial class GetBlogSubscriberInformationDbStatementResult 
        {
            public override string ToString()
            {
                return string.Format("GetBlogSubscriberInformationDbStatementResult({0})", this.Subscribers == null ? "null" : this.Subscribers.ToString());
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
            
                return this.Equals((GetBlogSubscriberInformationDbStatementResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Subscribers != null 
            			? this.Subscribers.Aggregate(0, (previous, current) => 
            				{ 
            				    unchecked
            				    {
            				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
            				    }
            				})
            			: 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(GetBlogSubscriberInformationDbStatementResult other)
            {
                if (this.Subscribers != null && other.Subscribers != null)
                {
                    if (!this.Subscribers.SequenceEqual(other.Subscribers))
                    {
                        return false;    
                    }
                }
                else if (this.Subscribers != null || other.Subscribers != null)
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetCreatorRevenueDbStatement
    {
        public partial class GetCreatorRevenueDbStatementResult 
        {
            public override string ToString()
            {
                return string.Format("GetCreatorRevenueDbStatementResult({0}, {1}, {2})", this.UnreleasedRevenue == null ? "null" : this.UnreleasedRevenue.ToString(), this.ReleasedRevenue == null ? "null" : this.ReleasedRevenue.ToString(), this.ReleasableRevenue == null ? "null" : this.ReleasableRevenue.ToString());
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
            
                return this.Equals((GetCreatorRevenueDbStatementResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.UnreleasedRevenue != null ? this.UnreleasedRevenue.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ReleasedRevenue != null ? this.ReleasedRevenue.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ReleasableRevenue != null ? this.ReleasableRevenue.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(GetCreatorRevenueDbStatementResult other)
            {
                if (!object.Equals(this.UnreleasedRevenue, other.UnreleasedRevenue))
                {
                    return false;
                }
            
                if (!object.Equals(this.ReleasedRevenue, other.ReleasedRevenue))
                {
                    return false;
                }
            
                if (!object.Equals(this.ReleasableRevenue, other.ReleasableRevenue))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Collections.Shared;
    using System.Transactions;

    public partial class GetLandingPageDbStatement
    {
        public partial class GetLandingPageDbResult 
        {
            public override string ToString()
            {
                return string.Format("GetLandingPageDbResult({0}, {1}, {2}, {3})", this.UserId == null ? "null" : this.UserId.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.Blog == null ? "null" : this.Blog.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
            
                return this.Equals((GetLandingPageDbResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Channels != null 
            			? this.Channels.Aggregate(0, (previous, current) => 
            				{ 
            				    unchecked
            				    {
            				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
            				    }
            				})
            			: 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(GetLandingPageDbResult other)
            {
                if (!object.Equals(this.UserId, other.UserId))
                {
                    return false;
                }
            
                if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
                {
                    return false;
                }
            
                if (!object.Equals(this.Blog, other.Blog))
                {
                    return false;
                }
            
                if (this.Channels != null && other.Channels != null)
                {
                    if (!this.Channels.SequenceEqual(other.Channels))
                    {
                        return false;    
                    }
                }
                else if (this.Channels != null || other.Channels != null)
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation
    {
        public partial class Subscriber 
        {
            public override string ToString()
            {
                return string.Format("Subscriber({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Username == null ? "null" : this.Username.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.ProfileImage == null ? "null" : this.ProfileImage.ToString(), this.FreeAccessEmail == null ? "null" : this.FreeAccessEmail.ToString(), this.PaymentStatus == null ? "null" : this.PaymentStatus.ToString(), this.HasPaymentInformation == null ? "null" : this.HasPaymentInformation.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
            
                return this.Equals((Subscriber)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ProfileImage != null ? this.ProfileImage.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FreeAccessEmail != null ? this.FreeAccessEmail.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PaymentStatus != null ? this.PaymentStatus.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.HasPaymentInformation != null ? this.HasPaymentInformation.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Channels != null 
            			? this.Channels.Aggregate(0, (previous, current) => 
            				{ 
            				    unchecked
            				    {
            				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
            				    }
            				})
            			: 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(Subscriber other)
            {
                if (!object.Equals(this.Username, other.Username))
                {
                    return false;
                }
            
                if (!object.Equals(this.UserId, other.UserId))
                {
                    return false;
                }
            
                if (!object.Equals(this.ProfileImage, other.ProfileImage))
                {
                    return false;
                }
            
                if (!object.Equals(this.FreeAccessEmail, other.FreeAccessEmail))
                {
                    return false;
                }
            
                if (!object.Equals(this.PaymentStatus, other.PaymentStatus))
                {
                    return false;
                }
            
                if (!object.Equals(this.HasPaymentInformation, other.HasPaymentInformation))
                {
                    return false;
                }
            
                if (this.Channels != null && other.Channels != null)
                {
                    if (!this.Channels.SequenceEqual(other.Channels))
                    {
                        return false;    
                    }
                }
                else if (this.Channels != null || other.Channels != null)
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation 
    {
        public override string ToString()
        {
            return string.Format("BlogSubscriberInformation({0}, {1}, {2}, {3})", this.UnreleasedRevenue == null ? "null" : this.UnreleasedRevenue.ToString(), this.ReleasedRevenue == null ? "null" : this.ReleasedRevenue.ToString(), this.ReleasableRevenue == null ? "null" : this.ReleasableRevenue.ToString(), this.Subscribers == null ? "null" : this.Subscribers.ToString());
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
        
            return this.Equals((BlogSubscriberInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UnreleasedRevenue != null ? this.UnreleasedRevenue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ReleasedRevenue != null ? this.ReleasedRevenue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ReleasableRevenue != null ? this.ReleasableRevenue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscribers != null 
        			? this.Subscribers.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlogSubscriberInformation other)
        {
            if (!object.Equals(this.UnreleasedRevenue, other.UnreleasedRevenue))
            {
                return false;
            }
        
            if (!object.Equals(this.ReleasedRevenue, other.ReleasedRevenue))
            {
                return false;
            }
        
            if (!object.Equals(this.ReleasableRevenue, other.ReleasableRevenue))
            {
                return false;
            }
        
            if (this.Subscribers != null && other.Subscribers != null)
            {
                if (!this.Subscribers.SequenceEqual(other.Subscribers))
                {
                    return false;    
                }
            }
            else if (this.Subscribers != null || other.Subscribers != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriberInformation
    {
        public partial class SubscriberChannel 
        {
            public override string ToString()
            {
                return string.Format("SubscriberChannel({0}, {1}, {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString());
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
            
                return this.Equals((SubscriberChannel)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(SubscriberChannel other)
            {
                if (!object.Equals(this.ChannelId, other.ChannelId))
                {
                    return false;
                }
            
                if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
                {
                    return false;
                }
            
                if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogSubscriptionStatus 
    {
        public override string ToString()
        {
            return string.Format("BlogSubscriptionStatus({0}, \"{1}\", {2}, {3}, {4}, {5}, {6})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ProfileImage == null ? "null" : this.ProfileImage.ToString(), this.FreeAccess == null ? "null" : this.FreeAccess.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
        
            return this.Equals((BlogSubscriptionStatus)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImage != null ? this.ProfileImage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FreeAccess != null ? this.FreeAccess.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Channels != null 
        			? this.Channels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlogSubscriptionStatus other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImage, other.ProfileImage))
            {
                return false;
            }
        
            if (!object.Equals(this.FreeAccess, other.FreeAccess))
            {
                return false;
            }
        
            if (this.Channels != null && other.Channels != null)
            {
                if (!this.Channels.SequenceEqual(other.Channels))
                {
                    return false;    
                }
            }
            else if (this.Channels != null || other.Channels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class BlogWithFileInformation 
    {
        public override string ToString()
        {
            return string.Format("BlogWithFileInformation({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString(), this.HeaderImage == null ? "null" : this.HeaderImage.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.Channels == null ? "null" : this.Channels.ToString(), this.Queues == null ? "null" : this.Queues.ToString());
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
        
            return this.Equals((BlogWithFileInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImage != null ? this.HeaderImage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Channels != null 
        			? this.Channels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.Queues != null 
        			? this.Queues.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlogWithFileInformation other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Introduction, other.Introduction))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            if (!object.Equals(this.HeaderImage, other.HeaderImage))
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
        
            if (this.Channels != null && other.Channels != null)
            {
                if (!this.Channels.SequenceEqual(other.Channels))
                {
                    return false;    
                }
            }
            else if (this.Channels != null || other.Channels != null)
            {
                return false;
            }
        
            if (this.Queues != null && other.Queues != null)
            {
                if (!this.Queues.SequenceEqual(other.Queues))
                {
                    return false;    
                }
            }
            else if (this.Queues != null || other.Queues != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class ChannelSubscriptionStatus 
    {
        public override string ToString()
        {
            return string.Format("ChannelSubscriptionStatus({0}, \"{1}\", {2}, {3}, {4}, {5}, {6}, {7})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.Price == null ? "null" : this.Price.ToString(), this.IsDefault == null ? "null" : this.IsDefault.ToString(), this.PriceLastSetDate == null ? "null" : this.PriceLastSetDate.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString());
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
        
            return this.Equals((ChannelSubscriptionStatus)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsDefault != null ? this.IsDefault.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceLastSetDate != null ? this.PriceLastSetDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ChannelSubscriptionStatus other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            if (!object.Equals(this.IsDefault, other.IsDefault))
            {
                return false;
            }
        
            if (!object.Equals(this.PriceLastSetDate, other.PriceLastSetDate))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetAllCreatorRevenuesQuery 
    {
        public override string ToString()
        {
            return string.Format("GetAllCreatorRevenuesQuery({0})", this.Requester == null ? "null" : this.Requester.ToString());
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
        
            return this.Equals((GetAllCreatorRevenuesQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetAllCreatorRevenuesQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogChannelsAndQueuesQuery 
    {
        public override string ToString()
        {
            return string.Format("GetBlogChannelsAndQueuesQuery({0})", this.UserId == null ? "null" : this.UserId.ToString());
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
        
            return this.Equals((GetBlogChannelsAndQueuesQuery)obj);
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
        
        protected bool Equals(GetBlogChannelsAndQueuesQuery other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogChannelsAndQueuesResult 
    {
        public override string ToString()
        {
            return string.Format("GetBlogChannelsAndQueuesResult({0})", this.Blog == null ? "null" : this.Blog.ToString());
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
        
            return this.Equals((GetBlogChannelsAndQueuesResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetBlogChannelsAndQueuesResult other)
        {
            if (!object.Equals(this.Blog, other.Blog))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetBlogSubscriberInformationQuery 
    {
        public override string ToString()
        {
            return string.Format("GetBlogSubscriberInformationQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString());
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
        
            return this.Equals((GetBlogSubscriberInformationQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetBlogSubscriberInformationQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersQuery 
    {
        public override string ToString()
        {
            return string.Format("GetFreeAccessUsersQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString());
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
        
            return this.Equals((GetFreeAccessUsersQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetFreeAccessUsersQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersResult
    {
        public partial class FreeAccessUser 
        {
            public override string ToString()
            {
                return string.Format("FreeAccessUser({0}, {1}, {2}, {3})", this.Email == null ? "null" : this.Email.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ChannelIds == null ? "null" : this.ChannelIds.ToString());
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
            
                return this.Equals((FreeAccessUser)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ChannelIds != null 
            			? this.ChannelIds.Aggregate(0, (previous, current) => 
            				{ 
            				    unchecked
            				    {
            				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
            				    }
            				})
            			: 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(FreeAccessUser other)
            {
                if (!object.Equals(this.Email, other.Email))
                {
                    return false;
                }
            
                if (!object.Equals(this.UserId, other.UserId))
                {
                    return false;
                }
            
                if (!object.Equals(this.Username, other.Username))
                {
                    return false;
                }
            
                if (this.ChannelIds != null && other.ChannelIds != null)
                {
                    if (!this.ChannelIds.SequenceEqual(other.ChannelIds))
                    {
                        return false;    
                    }
                }
                else if (this.ChannelIds != null || other.ChannelIds != null)
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetFreeAccessUsersResult 
    {
        public override string ToString()
        {
            return string.Format("GetFreeAccessUsersResult({0})", this.FreeAccessUsers == null ? "null" : this.FreeAccessUsers.ToString());
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
        
            return this.Equals((GetFreeAccessUsersResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FreeAccessUsers != null 
        			? this.FreeAccessUsers.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetFreeAccessUsersResult other)
        {
            if (this.FreeAccessUsers != null && other.FreeAccessUsers != null)
            {
                if (!this.FreeAccessUsers.SequenceEqual(other.FreeAccessUsers))
                {
                    return false;    
                }
            }
            else if (this.FreeAccessUsers != null || other.FreeAccessUsers != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetLandingPageQuery 
    {
        public override string ToString()
        {
            return string.Format("GetLandingPageQuery({0})", this.CreatorUsername == null ? "null" : this.CreatorUsername.ToString());
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
        
            return this.Equals((GetLandingPageQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorUsername != null ? this.CreatorUsername.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetLandingPageQuery other)
        {
            if (!object.Equals(this.CreatorUsername, other.CreatorUsername))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetLandingPageResult 
    {
        public override string ToString()
        {
            return string.Format("GetLandingPageResult({0}, {1}, {2})", this.UserId == null ? "null" : this.UserId.ToString(), this.ProfileImage == null ? "null" : this.ProfileImage.ToString(), this.Blog == null ? "null" : this.Blog.ToString());
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
        
            return this.Equals((GetLandingPageResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImage != null ? this.ProfileImage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetLandingPageResult other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImage, other.ProfileImage))
            {
                return false;
            }
        
            if (!object.Equals(this.Blog, other.Blog))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetUserSubscriptionsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserSubscriptionsQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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
        
            return this.Equals((GetUserSubscriptionsQuery)obj);
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
        
        protected bool Equals(GetUserSubscriptionsQuery other)
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Collections.Shared;

    public partial class GetUserSubscriptionsResult 
    {
        public override string ToString()
        {
            return string.Format("GetUserSubscriptionsResult({0})", this.Blogs == null ? "null" : this.Blogs.ToString());
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
        
            return this.Equals((GetUserSubscriptionsResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Blogs != null 
        			? this.Blogs.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetUserSubscriptionsResult other)
        {
            if (this.Blogs != null && other.Blogs != null)
            {
                if (!this.Blogs.SequenceEqual(other.Blogs))
                {
                    return false;    
                }
            }
            else if (this.Blogs != null || other.Blogs != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewBlogResult 
    {
        public override string ToString()
        {
            return string.Format("NewBlogResult({0}, {1})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString());
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
        
            return this.Equals((NewBlogResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewBlogResult other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class ChannelSubscriptionDataWithChannelId 
    {
        public override string ToString()
        {
            return string.Format("ChannelSubscriptionDataWithChannelId(\"{0}\", {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString());
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
        
            return this.Equals((ChannelSubscriptionDataWithChannelId)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ChannelSubscriptionDataWithChannelId other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class ChannelSubscriptionDataWithoutChannelId 
    {
        public override string ToString()
        {
            return string.Format("ChannelSubscriptionDataWithoutChannelId({0})", this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString());
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
        
            return this.Equals((ChannelSubscriptionDataWithoutChannelId)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ChannelSubscriptionDataWithoutChannelId other)
        {
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class FreeAccessUsersData 
    {
        public override string ToString()
        {
            return string.Format("FreeAccessUsersData({0})", this.Emails == null ? "null" : this.Emails.ToString());
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
        
            return this.Equals((FreeAccessUsersData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Emails != null 
        			? this.Emails.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FreeAccessUsersData other)
        {
            if (this.Emails != null && other.Emails != null)
            {
                if (!this.Emails.SequenceEqual(other.Emails))
                {
                    return false;    
                }
            }
            else if (this.Emails != null || other.Emails != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewBlogData 
    {
        public override string ToString()
        {
            return string.Format("NewBlogData(\"{0}\", {1})", this.Name == null ? "null" : this.Name.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
        
            return this.Equals((NewBlogData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewBlogData other)
        {
            if (!object.Equals(this.Name, other.Name))
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdatedBlogData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedBlogData(\"{0}\", \"{1}\", {2}, \"{3}\", \"{4}\")", this.Name == null ? "null" : this.Name.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString());
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
        
            return this.Equals((UpdatedBlogData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatedBlogData other)
        {
            if (!object.Equals(this.Name, other.Name))
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdatedBlogSubscriptionData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedBlogSubscriptionData({0})", this.Subscriptions == null ? "null" : this.Subscriptions.ToString());
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
        
            return this.Equals((UpdatedBlogSubscriptionData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Subscriptions != null 
        			? this.Subscriptions.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatedBlogSubscriptionData other)
        {
            if (this.Subscriptions != null && other.Subscriptions != null)
            {
                if (!this.Subscriptions.SequenceEqual(other.Subscriptions))
                {
                    return false;    
                }
            }
            else if (this.Subscriptions != null || other.Subscriptions != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class ChannelSubscriptionDataWithChannelId 
    {
        public class Parsed
        {
            public Parsed(
                System.String channelId,
                ValidAcceptedChannelPrice acceptedPrice)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (acceptedPrice == null)
                {
                    throw new ArgumentNullException("acceptedPrice");
                }

                this.ChannelId = channelId;
                this.AcceptedPrice = acceptedPrice;
            }
        
            public System.String ChannelId { get; private set; }
        
            public ValidAcceptedChannelPrice AcceptedPrice { get; private set; }
        }
    }

    public static partial class ChannelSubscriptionDataWithChannelIdExtensions
    {
        public static ChannelSubscriptionDataWithChannelId.Parsed Parse(this ChannelSubscriptionDataWithChannelId target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidAcceptedChannelPrice parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!ValidAcceptedChannelPrice.TryParse(target.AcceptedPrice, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("AcceptedPrice", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new ChannelSubscriptionDataWithChannelId.Parsed(
                target.ChannelId,
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class ChannelSubscriptionDataWithoutChannelId 
    {
        public class Parsed
        {
            public Parsed(
                ValidAcceptedChannelPrice acceptedPrice)
            {
                if (acceptedPrice == null)
                {
                    throw new ArgumentNullException("acceptedPrice");
                }

                this.AcceptedPrice = acceptedPrice;
            }
        
            public ValidAcceptedChannelPrice AcceptedPrice { get; private set; }
        }
    }

    public static partial class ChannelSubscriptionDataWithoutChannelIdExtensions
    {
        public static ChannelSubscriptionDataWithoutChannelId.Parsed Parse(this ChannelSubscriptionDataWithoutChannelId target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidAcceptedChannelPrice parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!ValidAcceptedChannelPrice.TryParse(target.AcceptedPrice, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("AcceptedPrice", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new ChannelSubscriptionDataWithoutChannelId.Parsed(
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewBlogData 
    {
        public class Parsed
        {
            public Parsed(
                ValidBlogName name,
                ValidChannelPrice basePrice)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (basePrice == null)
                {
                    throw new ArgumentNullException("basePrice");
                }

                this.Name = name;
                this.BasePrice = basePrice;
            }
        
            public ValidBlogName Name { get; private set; }
        
            public ValidChannelPrice BasePrice { get; private set; }
        }
    }

    public static partial class NewBlogDataExtensions
    {
        public static NewBlogData.Parsed Parse(this NewBlogData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidBlogName parsed0 = null;
            if (!ValidBlogName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidBlogName.TryParse(target.Name, out parsed0, out parsed0Errors))
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

            ValidChannelPrice parsed1 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
            if (!ValidChannelPrice.TryParse(target.BasePrice, out parsed1, out parsed1Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed1Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("BasePrice", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewBlogData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Net;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdatedBlogData 
    {
        public class Parsed
        {
            public Parsed(
                ValidBlogName name,
                ValidIntroduction introduction,
                Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
                ValidExternalVideoUrl video,
                ValidBlogDescription description)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                this.Name = name;
                this.Introduction = introduction;
                this.HeaderImageFileId = headerImageFileId;
                this.Video = video;
                this.Description = description;
            }
        
            public ValidBlogName Name { get; private set; }
        
            public ValidIntroduction Introduction { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId HeaderImageFileId { get; private set; }
        
            public ValidExternalVideoUrl Video { get; private set; }
        
            public ValidBlogDescription Description { get; private set; }
        }
    }

    public static partial class UpdatedBlogDataExtensions
    {
        public static UpdatedBlogData.Parsed Parse(this UpdatedBlogData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidBlogName parsed0 = null;
            if (!ValidBlogName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidBlogName.TryParse(target.Name, out parsed0, out parsed0Errors))
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

            ValidIntroduction parsed1 = null;
            if (!ValidIntroduction.IsEmpty(target.Introduction))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidIntroduction.TryParse(target.Introduction, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Introduction", modelState);
                }
            }

            ValidExternalVideoUrl parsed2 = null;
            if (!ValidExternalVideoUrl.IsEmpty(target.Video))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidExternalVideoUrl.TryParse(target.Video, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Video", modelState);
                }
            }

            ValidBlogDescription parsed3 = null;
            if (!ValidBlogDescription.IsEmpty(target.Description))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed3Errors;
                if (!ValidBlogDescription.TryParse(target.Description, out parsed3, out parsed3Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed3Errors)
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
        
            return new UpdatedBlogData.Parsed(
                parsed0,
                parsed1,
                target.HeaderImageFileId,
                parsed2,
                parsed3);
        }    
    }
}


