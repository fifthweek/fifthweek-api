using System;
using System.Linq;

//// Generated on 13/04/2015 18:23:09 (UTC)
//// Mapped solution in 8.93s


namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class CreateBlogCommand 
    {
        public CreateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId newBlogId,
            Fifthweek.Api.Blogs.Shared.ValidBlogName blogName,
            Fifthweek.Api.Blogs.Shared.ValidTagline tagline,
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class CreateBlogCommandHandler 
    {
        public CreateBlogCommandHandler(
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class PromoteNewUserToCreatorCommandInitiator 
    {
        public PromoteNewUserToCreatorCommandInitiator(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.PromoteNewUserToCreatorCommand> promoteNewUserToCreator)
        {
            if (promoteNewUserToCreator == null)
            {
                throw new ArgumentNullException("promoteNewUserToCreator");
            }

            this.promoteNewUserToCreator = promoteNewUserToCreator;
        }
    }
}
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class UpdateBlogCommand 
    {
        public UpdateBlogCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Blogs.Shared.ValidBlogName blogName,
            Fifthweek.Api.Blogs.Shared.ValidTagline tagline,
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class BlogController 
    {
        public BlogController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.CreateBlogCommand> createBlog,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Blogs.Commands.UpdateBlogCommand> updateBlog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Blogs.Queries.GetBlogQuery,Fifthweek.Api.Blogs.Queries.GetBlogResult> getBlog,
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogDbStatement
    {
        public partial class GetBlogDataDbResult 
        {
            public GetBlogDataDbResult(
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Blogs.Shared.BlogName blogName,
                Fifthweek.Api.Blogs.Shared.Tagline tagline,
                Fifthweek.Api.Blogs.Shared.Introduction introduction,
                System.DateTime creationDate,
                Fifthweek.Api.FileManagement.Shared.FileId headerImageFileId,
                Fifthweek.Api.Blogs.Shared.ExternalVideoUrl video,
                Fifthweek.Api.Blogs.Shared.BlogDescription description)
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogQuery 
    {
        public GetBlogQuery(
            Fifthweek.Api.Blogs.Shared.BlogId newBlogId)
        {
            if (newBlogId == null)
            {
                throw new ArgumentNullException("newBlogId");
            }

            this.NewBlogId = newBlogId;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogQueryHandler 
    {
        public GetBlogQueryHandler(
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Blogs.IGetBlogDbStatement getBlog)
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogResult 
    {
        public GetBlogResult(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Blogs.Shared.BlogName blogName,
            Fifthweek.Api.Blogs.Shared.Tagline tagline,
            Fifthweek.Api.Blogs.Shared.Introduction introduction,
            System.DateTime creationDate,
            Fifthweek.Api.FileManagement.Shared.FileInformation headerImage,
            Fifthweek.Api.Blogs.Shared.ExternalVideoUrl video,
            Fifthweek.Api.Blogs.Shared.BlogDescription description)
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserSubscriptionsQuery 
    {
        public GetUserSubscriptionsQuery(
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

    public partial class UpdateFreeAccessUsersDbStatement 
    {
        public UpdateFreeAccessUsersDbStatement(
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserSubscriptionsQueryHandler 
    {
        public GetUserSubscriptionsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.IGetUserSubscriptionsDbStatement getUserSubscriptions)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getUserSubscriptions == null)
            {
                throw new ArgumentNullException("getUserSubscriptions");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserSubscriptions = getUserSubscriptions;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetFreeAccessUsersResult
    {
        public partial class FreeAccessUser 
        {
            public FreeAccessUser(
                Fifthweek.Api.Identity.Shared.Membership.Email email,
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                Fifthweek.Api.Identity.Shared.Membership.Username username)
            {
                if (email == null)
                {
                    throw new ArgumentNullException("email");
                }

                this.Email = email;
                this.UserId = userId;
                this.Username = username;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class BlogSubscriptionStatus 
    {
        public BlogSubscriptionStatus(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String blogName,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Identity.Shared.Membership.Username creatorUsername,
            System.Boolean guestList,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Blogs.Queries.ChannelSubscriptionStatus> channels)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (blogName == null)
            {
                throw new ArgumentNullException("blogName");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (creatorUsername == null)
            {
                throw new ArgumentNullException("creatorUsername");
            }

            if (guestList == null)
            {
                throw new ArgumentNullException("guestList");
            }

            if (channels == null)
            {
                throw new ArgumentNullException("channels");
            }

            this.BlogId = blogId;
            this.BlogName = blogName;
            this.CreatorId = creatorId;
            this.CreatorUsername = creatorUsername;
            this.GuestList = guestList;
            this.Channels = channels;
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class ChannelSubscriptionStatus 
    {
        public ChannelSubscriptionStatus(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String channelName,
            System.Int32 acceptedPrice,
            System.Int32 currentPrice,
            System.DateTime priceLastSetDate,
            System.DateTime subscriptionStartDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (channelName == null)
            {
                throw new ArgumentNullException("channelName");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            if (currentPrice == null)
            {
                throw new ArgumentNullException("currentPrice");
            }

            if (priceLastSetDate == null)
            {
                throw new ArgumentNullException("priceLastSetDate");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.ChannelId = channelId;
            this.ChannelName = channelName;
            this.AcceptedPrice = acceptedPrice;
            this.CurrentPrice = currentPrice;
            this.PriceLastSetDate = priceLastSetDate;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}

namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class CreateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateBlogCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewBlogId == null ? "null" : this.NewBlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class UpdateBlogCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateBlogCommand({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", this.Requester == null ? "null" : this.Requester.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString());
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
namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;
    using System.Transactions;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogQuery 
    {
        public override string ToString()
        {
            return string.Format("GetBlogQuery({0})", this.NewBlogId == null ? "null" : this.NewBlogId.ToString());
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetBlogResult 
    {
        public override string ToString()
        {
            return string.Format("GetBlogResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString(), this.HeaderImage == null ? "null" : this.HeaderImage.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString());
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserSubscriptionsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetBlogSubscriptionsQuery({0})", this.Requester == null ? "null" : this.Requester.ToString());
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
                return hashCode;
            }
        }
        
        protected bool Equals(GetUserSubscriptionsQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetFreeAccessUsersResult
    {
        public partial class FreeAccessUser 
        {
            public override string ToString()
            {
                return string.Format("FreeAccessUser({0}, {1}, {2})", this.Email == null ? "null" : this.Email.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Username == null ? "null" : this.Username.ToString());
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
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserSubscriptionsResult 
    {
        public override string ToString()
        {
            return string.Format("GetBlogSubscriptionsResult({0})", this.Blogs == null ? "null" : this.Blogs.ToString());
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
namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class BlogSubscriptionStatus 
    {
        public override string ToString()
        {
            return string.Format("BlogSubscriptionStatus({0}, \"{1}\", {2}, {3}, {4}, {5})", this.BlogId == null ? "null" : this.BlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.CreatorUsername == null ? "null" : this.CreatorUsername.ToString(), this.GuestList == null ? "null" : this.GuestList.ToString(), this.Channels == null ? "null" : this.Channels.ToString());
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
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorUsername != null ? this.CreatorUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.GuestList != null ? this.GuestList.GetHashCode() : 0);
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
        
            if (!object.Equals(this.BlogName, other.BlogName))
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
        
            if (!object.Equals(this.GuestList, other.GuestList))
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
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class ChannelSubscriptionStatus 
    {
        public override string ToString()
        {
            return string.Format("ChannelSubscriptionStatus({0}, \"{1}\", {2}, {3}, {4}, {5})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.ChannelName == null ? "null" : this.ChannelName.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.CurrentPrice == null ? "null" : this.CurrentPrice.ToString(), this.PriceLastSetDate == null ? "null" : this.PriceLastSetDate.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ChannelName != null ? this.ChannelName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CurrentPrice != null ? this.CurrentPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceLastSetDate != null ? this.PriceLastSetDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ChannelSubscriptionStatus other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelName, other.ChannelName))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            if (!object.Equals(this.CurrentPrice, other.CurrentPrice))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class NewBlogData 
    {
        public override string ToString()
        {
            return string.Format("NewBlogData(\"{0}\", \"{1}\", {2})", this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.BasePrice == null ? "null" : this.BasePrice.ToString());
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

    public partial class UpdatedBlogData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedBlogData(\"{0}\", \"{1}\", \"{2}\", {3}, \"{4}\", \"{5}\")", this.BlogName == null ? "null" : this.BlogName.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.Video == null ? "null" : this.Video.ToString(), this.Description == null ? "null" : this.Description.ToString());
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
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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

    public static partial class NewBlogDataExtensions
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

                    modelStateDictionary.Add("BlogName", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("BlogName", modelState);
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
namespace Fifthweek.Api.Blogs.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using System.Collections.Generic;

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

    public static partial class UpdatedBlogDataExtensions
    {
        public static UpdatedBlogData.Parsed Parse(this UpdatedBlogData target)
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

                    modelStateDictionary.Add("BlogName", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("BlogName", modelState);
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


