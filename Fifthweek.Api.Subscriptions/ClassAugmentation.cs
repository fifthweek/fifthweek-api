using System;
using System.Linq;

//// Generated on 02/04/2015 16:38:50 (UTC)
//// Mapped solution in 20.83s


namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateBlogCommand 
    {
        public CreateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Subscriptions.Shared.BlogId newBlogId,
            Fifthweek.Api.Subscriptions.Shared.ValidBlogName blogName,
            Fifthweek.Api.Subscriptions.Shared.ValidTagline tagline,
            Fifthweek.Api.Channels.Shared.ValidChannelPriceInUsCentsPerWeek basePrice)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newBlogId == null)
            {
                throw new ArgumentNullException("newBlogId");
            }

            if (blogName == null)
            {
                throw new ArgumentNullException("blogName");
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
            this.NewBlogId = newBlogId;
            this.BlogName = blogName;
            this.Tagline = tagline;
            this.BasePrice = basePrice;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateBlogCommandHandler 
    {
        public CreateBlogCommandHandler(
            Fifthweek.Api.Subscriptions.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
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

            this.blogSecurity = blogSecurity;
            this.requesterSecurity = requesterSecurity;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PromoteNewUserToCreatorCommand 
    {
        public PromoteNewUserToCreatorCommand(
            Fifthweek.Api.Identity.Shared.Membership.UserId newUserId)
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
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateBlogCommand 
    {
        public UpdateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Subscriptions.Shared.BlogId blogId,
            Fifthweek.Api.Subscriptions.Shared.ValidBlogName blogName,
            Fifthweek.Api.Subscriptions.Shared.ValidTagline tagline,
            Fifthweek.Api.Subscriptions.Shared.ValidIntroduction introduction,
            Fifthweek.Api.Subscriptions.Shared.ValidBlogDescription description,
            Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
            Fifthweek.Api.Subscriptions.Shared.ValidExternalVideoUrl video)
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

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (introduction == null)
            {
                throw new ArgumentNullException("introduction");
            }

            this.Requester = requester;
            this.BlogId = blogId;
            this.BlogName = blogName;
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
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateBlogCommandHandler 
    {
        public UpdateBlogCommandHandler(
            Fifthweek.Api.Subscriptions.Shared.IBlogSecurity blogSecurity,
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
namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    public partial class BlogController 
    {
        public BlogController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Subscriptions.Commands.CreateBlogCommand> createBlog,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Subscriptions.Commands.UpdateBlogCommand> updateBlog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Subscriptions.Queries.GetBlogQuery,Fifthweek.Api.Subscriptions.Queries.GetBlogResult> getBlog,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (createBlog == null)
            {
                throw new ArgumentNullException("createBlog");
            }

            if (updateBlog == null)
            {
                throw new ArgumentNullException("updateBlog");
            }

            if (getBlog == null)
            {
                throw new ArgumentNullException("getBlog");
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
            this.getBlog = getBlog;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreatorStatus 
    {
        public CreatorStatus(
            Fifthweek.Api.Subscriptions.Shared.BlogId blogId,
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
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetCreatorStatusQuery 
    {
        public GetCreatorStatusQuery(
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
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetCreatorStatusQueryHandler 
    {
        public GetCreatorStatusQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.requesterSecurity = requesterSecurity;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

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
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class BlogSecurity 
    {
        public BlogSecurity(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Subscriptions.IBlogOwnership blogOwnership)
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
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class GetBlogDbStatement
    {
        public partial class GetSubscriptionDataDbResult 
        {
            public GetSubscriptionDataDbResult(
                Fifthweek.Api.Subscriptions.Shared.BlogId blogId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Subscriptions.Shared.BlogName blogName,
                Fifthweek.Api.Subscriptions.Shared.Tagline tagline,
                Fifthweek.Api.Subscriptions.Shared.Introduction introduction,
                System.DateTime creationDate,
                Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
                Fifthweek.Api.Subscriptions.Shared.ExternalVideoUrl video,
                Fifthweek.Api.Subscriptions.Shared.BlogDescription description)
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

                if (tagline == null)
                {
                    throw new ArgumentNullException("tagline");
                }

                if (introduction == null)
                {
                    throw new ArgumentNullException("introduction");
                }

                if (creationDate == null)
                {
                    throw new ArgumentNullException("creationDate");
                }

                this.BlogId = blogId;
                this.CreatorId = creatorId;
                this.BlogName = blogName;
                this.Tagline = tagline;
                this.Introduction = introduction;
                this.CreationDate = creationDate;
                this.HeaderImageFileId = headerImageFileId;
                this.Video = video;
                this.Description = description;
            }
        }
    }
}
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class GetBlogDbStatement 
    {
        public GetBlogDbStatement(
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
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetBlogQuery 
    {
        public GetBlogQuery(
            Fifthweek.Api.Subscriptions.Shared.BlogId newBlogId)
        {
            if (newBlogId == null)
            {
                throw new ArgumentNullException("newBlogId");
            }

            this.NewBlogId = newBlogId;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetBlogQueryHandler 
    {
        public GetBlogQueryHandler(
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Subscriptions.IGetBlogDbStatement getBlog)
        {
            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (getBlog == null)
            {
                throw new ArgumentNullException("getBlog");
            }

            this.fileInformationAggregator = fileInformationAggregator;
            this.getBlog = getBlog;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetBlogResult 
    {
        public GetBlogResult(
            Fifthweek.Api.Subscriptions.Shared.BlogId blogId,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Subscriptions.Shared.BlogName blogName,
            Fifthweek.Api.Subscriptions.Shared.Tagline tagline,
            Fifthweek.Api.Subscriptions.Shared.Introduction introduction,
            System.DateTime creationDate,
            Fifthweek.Api.FileManagement.Shared.FileInformation headerImage,
            Fifthweek.Api.Subscriptions.Shared.ExternalVideoUrl video,
            Fifthweek.Api.Subscriptions.Shared.BlogDescription description)
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

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (introduction == null)
            {
                throw new ArgumentNullException("introduction");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.BlogId = blogId;
            this.CreatorId = creatorId;
            this.BlogName = blogName;
            this.Tagline = tagline;
            this.Introduction = introduction;
            this.CreationDate = creationDate;
            this.HeaderImage = headerImage;
            this.Video = video;
            this.Description = description;
        }
    }
}

namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateSubscriptionCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewBlogId == null ? "null" : this.NewBlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
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
        
            if (!object.Equals(this.BlogName, other.BlogName))
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PromoteNewUserToCreatorCommand 
    {
        public override string ToString()
        {
            return string.Format("PromoteNewUserToCreatorCommand({0})", this.NewUserId == null ? "null" : this.NewUserId.ToString());
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateSubscriptionCommand({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
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
namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;

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
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetCreatorStatusQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorStatusQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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
        
            return this.Equals((GetCreatorStatusQuery)obj);
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
        
        protected bool Equals(GetCreatorStatusQuery other)
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
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetBlogQuery 
    {
        public override string ToString()
        {
            return string.Format("GetSubscriptionQuery({0})", this.NewBlogId == null ? "null" : this.NewBlogId.ToString());
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
        
            return this.Equals((GetBlogQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NewBlogId != null ? this.NewBlogId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetBlogQuery other)
        {
            if (!object.Equals(this.NewBlogId, other.NewBlogId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;

    public partial class GetBlogResult 
    {
        public override string ToString()
        {
            return string.Format("GetSubscriptionResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString(), this.HeaderImage == null ? "null" : this.HeaderImage.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString());
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
        
            return this.Equals((GetBlogResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImage != null ? this.HeaderImage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetBlogResult other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogName, other.BlogName))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    public partial class NewBlogData 
    {
        public override string ToString()
        {
            return string.Format("NewSubscriptionData(\"{0}\", \"{1}\", {2})", this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewBlogData other)
        {
            if (!object.Equals(this.BlogName, other.BlogName))
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
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    public partial class UpdatedBlogData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedSubscriptionData(\"{0}\", \"{1}\", \"{2}\", {3}, \"{4}\", \"{5}\")", this.SubscriptionName == null ? "null" : this.SubscriptionName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString());
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
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Video != null ? this.Video.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatedBlogData other)
        {
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
namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    public partial class NewBlogData 
    {
        public class Parsed
        {
            public Parsed(
                ValidBlogName blogName,
                ValidTagline tagline,
                ValidChannelPriceInUsCentsPerWeek basePrice)
            {
                if (blogName == null)
                {
                    throw new ArgumentNullException("blogName");
                }

                if (tagline == null)
                {
                    throw new ArgumentNullException("tagline");
                }

                if (basePrice == null)
                {
                    throw new ArgumentNullException("basePrice");
                }

                this.BlogName = blogName;
                this.Tagline = tagline;
                this.BasePrice = basePrice;
            }
        
            public ValidBlogName BlogName { get; private set; }
        
            public ValidTagline Tagline { get; private set; }
        
            public ValidChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
        }
    }

    public static partial class NewSubscriptionDataExtensions
    {
        public static NewBlogData.Parsed Parse(this NewBlogData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidBlogName parsed0 = null;
            if (!ValidBlogName.IsEmpty(target.BlogName))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidBlogName.TryParse(target.BlogName, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("SubscriptionName", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("SubscriptionName", modelState);
            }

            ValidTagline parsed1 = null;
            if (!ValidTagline.IsEmpty(target.Tagline))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidTagline.TryParse(target.Tagline, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Tagline", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Tagline", modelState);
            }

            ValidChannelPriceInUsCentsPerWeek parsed2 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
            if (!ValidChannelPriceInUsCentsPerWeek.TryParse(target.BasePrice, out parsed2, out parsed2Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed2Errors)
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
                parsed1,
                parsed2);
        }    
    }
}
namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    public partial class UpdatedBlogData 
    {
        public class Parsed
        {
            public Parsed(
                ValidBlogName blogName,
                ValidTagline tagline,
                ValidIntroduction introduction,
                Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
                ValidExternalVideoUrl video,
                ValidBlogDescription description)
            {
                if (blogName == null)
                {
                    throw new ArgumentNullException("blogName");
                }

                if (tagline == null)
                {
                    throw new ArgumentNullException("tagline");
                }

                if (introduction == null)
                {
                    throw new ArgumentNullException("introduction");
                }

                this.BlogName = blogName;
                this.Tagline = tagline;
                this.Introduction = introduction;
                this.HeaderImageFileId = headerImageFileId;
                this.Video = video;
                this.Description = description;
            }
        
            public ValidBlogName BlogName { get; private set; }
        
            public ValidTagline Tagline { get; private set; }
        
            public ValidIntroduction Introduction { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId HeaderImageFileId { get; private set; }
        
            public ValidExternalVideoUrl Video { get; private set; }
        
            public ValidBlogDescription Description { get; private set; }
        }
    }

    public static partial class UpdatedSubscriptionDataExtensions
    {
        public static UpdatedBlogData.Parsed Parse(this UpdatedBlogData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidBlogName parsed0 = null;
            if (!ValidBlogName.IsEmpty(target.SubscriptionName))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidBlogName.TryParse(target.SubscriptionName, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("SubscriptionName", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("SubscriptionName", modelState);
            }

            ValidTagline parsed1 = null;
            if (!ValidTagline.IsEmpty(target.Tagline))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidTagline.TryParse(target.Tagline, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Tagline", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Tagline", modelState);
            }

            ValidIntroduction parsed2 = null;
            if (!ValidIntroduction.IsEmpty(target.Introduction))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidIntroduction.TryParse(target.Introduction, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Introduction", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Introduction", modelState);
            }

            ValidExternalVideoUrl parsed3 = null;
            if (!ValidExternalVideoUrl.IsEmpty(target.Video))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed3Errors;
                if (!ValidExternalVideoUrl.TryParse(target.Video, out parsed3, out parsed3Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed3Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Video", modelState);
                }
            }

            ValidBlogDescription parsed4 = null;
            if (!ValidBlogDescription.IsEmpty(target.Description))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed4Errors;
                if (!ValidBlogDescription.TryParse(target.Description, out parsed4, out parsed4Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed4Errors)
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
                parsed2,
                target.HeaderImageFileId,
                parsed3,
                parsed4);
        }    
    }
}


