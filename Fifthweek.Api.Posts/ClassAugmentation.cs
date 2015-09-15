using System;
using System.Linq;

//// Generated on 11/09/2015 13:59:32 (UTC)
//// Mapped solution in 20.83s


namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeletePostCommand 
    {
        public DeletePostCommand(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Identity.Shared.Membership.Requester requester)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.PostId = postId;
            this.Requester = requester;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeletePostCommandHandler 
    {
        public DeletePostCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.IDeletePostDbStatement deletePost,
            Fifthweek.Api.Posts.IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired,
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (deletePost == null)
            {
                throw new ArgumentNullException("deletePost");
            }

            if (defragmentQueueIfRequired == null)
            {
                throw new ArgumentNullException("defragmentQueueIfRequired");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.deletePost = deletePost;
            this.defragmentQueueIfRequired = defragmentQueueIfRequired;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class PostToChannelCommand 
    {
        public PostToChannelCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            System.DateTime timestamp)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.ChannelId = channelId;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.QueueId = queueId;
            this.Timestamp = timestamp;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class PostToChannelCommandHandler 
    {
        public PostToChannelCommandHandler(
            Fifthweek.Api.Collections.Shared.IQueueSecurity queueSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IPostFileTypeChecks postFileTypeChecks,
            Fifthweek.Api.Posts.IPostToChannelDbStatement postToChannel)
        {
            if (queueSecurity == null)
            {
                throw new ArgumentNullException("queueSecurity");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postFileTypeChecks == null)
            {
                throw new ArgumentNullException("postFileTypeChecks");
            }

            if (postToChannel == null)
            {
                throw new ArgumentNullException("postToChannel");
            }

            this.queueSecurity = queueSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.postFileTypeChecks = postFileTypeChecks;
            this.postToChannel = postToChannel;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class ReorderQueueCommand 
    {
        public ReorderQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Shared.PostId> newPartialQueueOrder)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            if (newPartialQueueOrder == null)
            {
                throw new ArgumentNullException("newPartialQueueOrder");
            }

            this.Requester = requester;
            this.QueueId = queueId;
            this.NewPartialQueueOrder = newPartialQueueOrder;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class ReorderQueueCommandHandler 
    {
        public ReorderQueueCommandHandler(
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PostController 
    {
        public PostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeletePostCommand> deletePost,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReorderQueueCommand> reorderQueue,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.RescheduleForNowCommand> rescheduleForNow,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.RescheduleForTimeCommand> rescheduleForTime,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.RescheduleWithQueueCommand> rescheduleWithQueue,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorBacklogQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.GetCreatorBacklogQueryResult>> getCreatorBacklog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetNewsfeedQuery,Fifthweek.Api.Posts.Queries.GetNewsfeedQueryResult> getNewsfeed,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.CommentOnPostCommand> postComment,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCommentsQuery,Fifthweek.Api.Posts.Controllers.CommentsResult> getComments,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.LikePostCommand> postLike,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeleteLikeCommand> deleteLike,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostToChannelCommand> postPost,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.RevisePostCommand> revisePost,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Shared.IGuidCreator guidCreator)
        {
            if (deletePost == null)
            {
                throw new ArgumentNullException("deletePost");
            }

            if (reorderQueue == null)
            {
                throw new ArgumentNullException("reorderQueue");
            }

            if (rescheduleForNow == null)
            {
                throw new ArgumentNullException("rescheduleForNow");
            }

            if (rescheduleForTime == null)
            {
                throw new ArgumentNullException("rescheduleForTime");
            }

            if (rescheduleWithQueue == null)
            {
                throw new ArgumentNullException("rescheduleWithQueue");
            }

            if (getCreatorBacklog == null)
            {
                throw new ArgumentNullException("getCreatorBacklog");
            }

            if (getNewsfeed == null)
            {
                throw new ArgumentNullException("getNewsfeed");
            }

            if (postComment == null)
            {
                throw new ArgumentNullException("postComment");
            }

            if (getComments == null)
            {
                throw new ArgumentNullException("getComments");
            }

            if (postLike == null)
            {
                throw new ArgumentNullException("postLike");
            }

            if (deleteLike == null)
            {
                throw new ArgumentNullException("deleteLike");
            }

            if (postPost == null)
            {
                throw new ArgumentNullException("postPost");
            }

            if (revisePost == null)
            {
                throw new ArgumentNullException("revisePost");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.deletePost = deletePost;
            this.reorderQueue = reorderQueue;
            this.rescheduleForNow = rescheduleForNow;
            this.rescheduleForTime = rescheduleForTime;
            this.rescheduleWithQueue = rescheduleWithQueue;
            this.getCreatorBacklog = getCreatorBacklog;
            this.getNewsfeed = getNewsfeed;
            this.postComment = postComment;
            this.getComments = getComments;
            this.postLike = postLike;
            this.deleteLike = deleteLike;
            this.postPost = postPost;
            this.revisePost = revisePost;
            this.requesterContext = requesterContext;
            this.timestampCreator = timestampCreator;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class DeletePostDbStatement 
    {
        public DeletePostDbStatement(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class PostFileTypeChecks 
    {
        public PostFileTypeChecks(
            Fifthweek.Api.FileManagement.Shared.IGetFileExtensionDbStatement getFileExtension,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap)
        {
            if (getFileExtension == null)
            {
                throw new ArgumentNullException("getFileExtension");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            this.getFileExtension = getFileExtension;
            this.mimeTypeMap = mimeTypeMap;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class IsPostOwnerDbStatement 
    {
        public IsPostOwnerDbStatement(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class PostSecurity 
    {
        public PostSecurity(
            Fifthweek.Api.Posts.IIsPostOwnerDbStatement isPostOwner,
            Fifthweek.Api.Posts.IIsPostSubscriberDbStatement isPostSubscriber)
        {
            if (isPostOwner == null)
            {
                throw new ArgumentNullException("isPostOwner");
            }

            if (isPostSubscriber == null)
            {
                throw new ArgumentNullException("isPostSubscriber");
            }

            this.isPostOwner = isPostOwner;
            this.isPostSubscriber = isPostSubscriber;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class PostToChannelDbStatement 
    {
        public PostToChannelDbStatement(
            Fifthweek.Api.Posts.IPostToChannelDbSubStatements subStatements)
        {
            if (subStatements == null)
            {
                throw new ArgumentNullException("subStatements");
            }

            this.subStatements = subStatements;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class PostToChannelDbSubStatements 
    {
        public PostToChannelDbSubStatements(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Api.Collections.Shared.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost,
            Fifthweek.Api.Posts.IScheduledDateClippingFunction scheduledDateClipping)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            if (scheduledDateClipping == null)
            {
                throw new ArgumentNullException("scheduledDateClipping");
            }

            this.connectionFactory = connectionFactory;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
            this.scheduledDateClipping = scheduledDateClipping;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.DateTime liveDate,
            System.String fileName,
            System.String fileExtension,
            System.Nullable<System.Int64> fileSize,
            System.String imageName,
            System.String imageExtension,
            System.Nullable<System.Int64> imageSize,
            System.Nullable<System.Int32> imageRenderWidth,
            System.Nullable<System.Int32> imageRenderHeight,
            System.DateTime creationDate)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.PostId = postId;
            this.ChannelId = channelId;
            this.QueueId = queueId;
            this.Comment = comment;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.LiveDate = liveDate;
            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.FileSize = fileSize;
            this.ImageName = imageName;
            this.ImageExtension = imageExtension;
            this.ImageSize = imageSize;
            this.ImageRenderWidth = imageRenderWidth;
            this.ImageRenderHeight = imageRenderHeight;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogQuery 
    {
        public GetCreatorBacklogQuery(
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogQueryHandler 
    {
        public GetCreatorBacklogQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IGetCreatorBacklogDbStatement getCreatorBacklogDbStatement,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getCreatorBacklogDbStatement == null)
            {
                throw new ArgumentNullException("getCreatorBacklogDbStatement");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            this.requesterSecurity = requesterSecurity;
            this.getCreatorBacklogDbStatement = getCreatorBacklogDbStatement;
            this.fileInformationAggregator = fileInformationAggregator;
            this.mimeTypeMap = mimeTypeMap;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class NewsfeedPost 
    {
        public NewsfeedPost(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.DateTime liveDate,
            System.String fileName,
            System.String fileExtension,
            System.Nullable<System.Int64> fileSize,
            System.String imageName,
            System.String imageExtension,
            System.Nullable<System.Int64> imageSize,
            System.Nullable<System.Int32> imageRenderWidth,
            System.Nullable<System.Int32> imageRenderHeight,
            System.Int32 likesCount,
            System.Int32 commentsCount,
            System.Boolean hasLikedPost,
            System.DateTime creationDate)
        {
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            if (likesCount == null)
            {
                throw new ArgumentNullException("likesCount");
            }

            if (commentsCount == null)
            {
                throw new ArgumentNullException("commentsCount");
            }

            if (hasLikedPost == null)
            {
                throw new ArgumentNullException("hasLikedPost");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.CreatorId = creatorId;
            this.PostId = postId;
            this.BlogId = blogId;
            this.ChannelId = channelId;
            this.Comment = comment;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.LiveDate = liveDate;
            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.FileSize = fileSize;
            this.ImageName = imageName;
            this.ImageExtension = imageExtension;
            this.ImageSize = imageSize;
            this.ImageRenderWidth = imageRenderWidth;
            this.ImageRenderHeight = imageRenderHeight;
            this.LikesCount = likesCount;
            this.CommentsCount = commentsCount;
            this.HasLikedPost = hasLikedPost;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewPostData 
    {
        public NewPostData(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            Fifthweek.Api.Collections.Shared.QueueId queueId)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            this.ChannelId = channelId;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.QueueId = queueId;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RevisePostCommand 
    {
        public RevisePostCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            Fifthweek.Api.Posts.Shared.ValidComment comment)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.Comment = comment;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedPostData 
    {
        public RevisedPostData(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.String comment)
        {
            this.FileId = fileId;
            this.ImageId = imageId;
            this.Comment = comment;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RevisePostCommandHandler 
    {
        public RevisePostCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.connectionFactory = connectionFactory;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class TryGetPostQueueIdDbStatement 
    {
        public TryGetPostQueueIdDbStatement(
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
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForNowCommand 
    {
        public RescheduleForNowCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            this.Requester = requester;
            this.PostId = postId;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForNowCommandHandler 
    {
        public RescheduleForNowCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.ISetPostLiveDateDbStatement setPostLiveDate,
            Fifthweek.Api.Posts.IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (setPostLiveDate == null)
            {
                throw new ArgumentNullException("setPostLiveDate");
            }

            if (defragmentQueueIfRequired == null)
            {
                throw new ArgumentNullException("defragmentQueueIfRequired");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.setPostLiveDate = setPostLiveDate;
            this.defragmentQueueIfRequired = defragmentQueueIfRequired;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class SetPostLiveDateDbStatement 
    {
        public SetPostLiveDateDbStatement(
            Fifthweek.Api.Posts.IScheduledDateClippingFunction scheduledDateClipping,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (scheduledDateClipping == null)
            {
                throw new ArgumentNullException("scheduledDateClipping");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.scheduledDateClipping = scheduledDateClipping;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class DefragmentQueueIfRequiredDbStatement 
    {
        public DefragmentQueueIfRequiredDbStatement(
            Fifthweek.Api.Posts.ITryGetPostQueueIdStatement tryGetPostQueueId,
            Fifthweek.Api.Collections.Shared.IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule,
            Fifthweek.Api.Collections.Shared.IDefragmentQueueDbStatement defragmentQueue)
        {
            if (tryGetPostQueueId == null)
            {
                throw new ArgumentNullException("tryGetPostQueueId");
            }

            if (getWeeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("getWeeklyReleaseSchedule");
            }

            if (defragmentQueue == null)
            {
                throw new ArgumentNullException("defragmentQueue");
            }

            this.tryGetPostQueueId = tryGetPostQueueId;
            this.getWeeklyReleaseSchedule = getWeeklyReleaseSchedule;
            this.defragmentQueue = defragmentQueue;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForTimeCommand 
    {
        public RescheduleForTimeCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            System.DateTime scheduledPostTime)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (scheduledPostTime == null)
            {
                throw new ArgumentNullException("scheduledPostTime");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.ScheduledPostTime = scheduledPostTime;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleWithQueueCommand 
    {
        public RescheduleWithQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Collections.Shared.QueueId queueId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.QueueId = queueId;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForTimeCommandHandler 
    {
        public RescheduleForTimeCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.ISetPostLiveDateDbStatement setPostLiveDate,
            Fifthweek.Api.Posts.IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (setPostLiveDate == null)
            {
                throw new ArgumentNullException("setPostLiveDate");
            }

            if (defragmentQueueIfRequired == null)
            {
                throw new ArgumentNullException("defragmentQueueIfRequired");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.setPostLiveDate = setPostLiveDate;
            this.defragmentQueueIfRequired = defragmentQueueIfRequired;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleWithQueueCommandHandler 
    {
        public RescheduleWithQueueCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.IMovePostToQueueDbStatement movePostToQueue,
            Fifthweek.Api.Posts.IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (movePostToQueue == null)
            {
                throw new ArgumentNullException("movePostToQueue");
            }

            if (defragmentQueueIfRequired == null)
            {
                throw new ArgumentNullException("defragmentQueueIfRequired");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.movePostToQueue = movePostToQueue;
            this.defragmentQueueIfRequired = defragmentQueueIfRequired;
            this.timestampCreator = timestampCreator;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class MovePostToQueueDbStatement 
    {
        public MovePostToQueueDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory,
            Fifthweek.Api.Collections.Shared.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            this.connectionFactory = connectionFactory;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogDbStatement 
    {
        public GetCreatorBacklogDbStatement(
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogQueryResult 
    {
        public GetCreatorBacklogQueryResult(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileInformation file,
            Fifthweek.Api.Posts.Queries.FileSourceInformation fileSource,
            Fifthweek.Api.FileManagement.Shared.FileInformation image,
            Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
            System.DateTime liveDate)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.PostId = postId;
            this.ChannelId = channelId;
            this.QueueId = queueId;
            this.Comment = comment;
            this.File = file;
            this.FileSource = fileSource;
            this.Image = image;
            this.ImageSource = imageSource;
            this.LiveDate = liveDate;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class FileSourceInformation 
    {
        public FileSourceInformation(
            System.String fileName,
            System.String fileExtension,
            System.String contentType,
            System.Int64 size,
            Fifthweek.Api.Posts.Queries.RenderSize renderSize)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException("fileExtension");
            }

            if (contentType == null)
            {
                throw new ArgumentNullException("contentType");
            }

            if (size == null)
            {
                throw new ArgumentNullException("size");
            }

            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.ContentType = contentType;
            this.Size = size;
            this.RenderSize = renderSize;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class RenderSize 
    {
        public RenderSize(
            System.Int32 width,
            System.Int32 height)
        {
            if (width == null)
            {
                throw new ArgumentNullException("width");
            }

            if (height == null)
            {
                throw new ArgumentNullException("height");
            }

            this.Width = width;
            this.Height = height;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQuery 
    {
        public GetNewsfeedQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Channels.Shared.ChannelId> channelIds,
            System.Nullable<System.DateTime> origin,
            System.Boolean searchForwards,
            Fifthweek.Shared.NonNegativeInt startIndex,
            Fifthweek.Shared.PositiveInt count)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (searchForwards == null)
            {
                throw new ArgumentNullException("searchForwards");
            }

            if (startIndex == null)
            {
                throw new ArgumentNullException("startIndex");
            }

            if (count == null)
            {
                throw new ArgumentNullException("count");
            }

            this.Requester = requester;
            this.CreatorId = creatorId;
            this.ChannelIds = channelIds;
            this.Origin = origin;
            this.SearchForwards = searchForwards;
            this.StartIndex = startIndex;
            this.Count = count;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQueryHandler 
    {
        public GetNewsfeedQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IGetNewsfeedDbStatement getNewsfeedDbStatement,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getNewsfeedDbStatement == null)
            {
                throw new ArgumentNullException("getNewsfeedDbStatement");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            this.requesterSecurity = requesterSecurity;
            this.getNewsfeedDbStatement = getNewsfeedDbStatement;
            this.fileInformationAggregator = fileInformationAggregator;
            this.mimeTypeMap = mimeTypeMap;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQueryResult 
    {
        public GetNewsfeedQueryResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.GetNewsfeedQueryResult.Post> posts,
            System.Int32 accountBalance)
        {
            if (posts == null)
            {
                throw new ArgumentNullException("posts");
            }

            if (accountBalance == null)
            {
                throw new ArgumentNullException("accountBalance");
            }

            this.Posts = posts;
            this.AccountBalance = accountBalance;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public Post(
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Posts.Shared.Comment comment,
                Fifthweek.Api.FileManagement.Shared.FileInformation file,
                Fifthweek.Api.Posts.Queries.FileSourceInformation fileSource,
                Fifthweek.Api.FileManagement.Shared.FileInformation image,
                Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
                System.DateTime liveDate,
                System.Int32 likesCount,
                System.Int32 commentsCount,
                System.Boolean hasLiked)
            {
                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                if (postId == null)
                {
                    throw new ArgumentNullException("postId");
                }

                if (blogId == null)
                {
                    throw new ArgumentNullException("blogId");
                }

                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (liveDate == null)
                {
                    throw new ArgumentNullException("liveDate");
                }

                if (likesCount == null)
                {
                    throw new ArgumentNullException("likesCount");
                }

                if (commentsCount == null)
                {
                    throw new ArgumentNullException("commentsCount");
                }

                if (hasLiked == null)
                {
                    throw new ArgumentNullException("hasLiked");
                }

                this.CreatorId = creatorId;
                this.PostId = postId;
                this.BlogId = blogId;
                this.ChannelId = channelId;
                this.Comment = comment;
                this.File = file;
                this.FileSource = fileSource;
                this.Image = image;
                this.ImageSource = imageSource;
                this.LiveDate = liveDate;
                this.LikesCount = likesCount;
                this.CommentsCount = commentsCount;
                this.HasLiked = hasLiked;
            }
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedDbStatement 
    {
        public GetNewsfeedDbStatement(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedDbResult 
    {
        public GetNewsfeedDbResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.NewsfeedPost> posts,
            System.Int32 accountBalance)
        {
            if (posts == null)
            {
                throw new ArgumentNullException("posts");
            }

            if (accountBalance == null)
            {
                throw new ArgumentNullException("accountBalance");
            }

            this.Posts = posts;
            this.AccountBalance = accountBalance;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeleteLikeCommand 
    {
        public DeleteLikeCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            this.Requester = requester;
            this.PostId = postId;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class CommentOnPostCommand 
    {
        public CommentOnPostCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Posts.Shared.CommentId commentId,
            Fifthweek.Api.Posts.Shared.ValidComment content,
            System.DateTime timestamp)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (commentId == null)
            {
                throw new ArgumentNullException("commentId");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.CommentId = commentId;
            this.Content = content;
            this.Timestamp = timestamp;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class LikePostCommand 
    {
        public LikePostCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            System.DateTime timestamp)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.Timestamp = timestamp;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCommentsQuery 
    {
        public GetCommentsQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            this.Requester = requester;
            this.PostId = postId;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class CommentOnPostCommandHandler 
    {
        public CommentOnPostCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.ICommentOnPostDbStatement commentOnPost)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (commentOnPost == null)
            {
                throw new ArgumentNullException("commentOnPost");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.commentOnPost = commentOnPost;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeleteLikeCommandHandler 
    {
        public DeleteLikeCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.IUnlikePostDbStatement unlikePost)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (unlikePost == null)
            {
                throw new ArgumentNullException("unlikePost");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.unlikePost = unlikePost;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class LikePostCommandHandler 
    {
        public LikePostCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.ILikePostDbStatement likePost)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (likePost == null)
            {
                throw new ArgumentNullException("likePost");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.likePost = likePost;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class CommentOnPostDbStatement 
    {
        public CommentOnPostDbStatement(
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentsResult
    {
        public partial class Item 
        {
            public Item(
                Fifthweek.Api.Posts.Shared.CommentId commentId,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                Fifthweek.Api.Identity.Shared.Membership.Username username,
                Fifthweek.Api.Posts.Shared.Comment content,
                System.DateTime creationDate)
            {
                if (commentId == null)
                {
                    throw new ArgumentNullException("commentId");
                }

                if (postId == null)
                {
                    throw new ArgumentNullException("postId");
                }

                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (username == null)
                {
                    throw new ArgumentNullException("username");
                }

                if (content == null)
                {
                    throw new ArgumentNullException("content");
                }

                if (creationDate == null)
                {
                    throw new ArgumentNullException("creationDate");
                }

                this.CommentId = commentId;
                this.PostId = postId;
                this.UserId = userId;
                this.Username = username;
                this.Content = content;
                this.CreationDate = creationDate;
            }
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentsResult 
    {
        public CommentsResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Controllers.CommentsResult.Item> comments)
        {
            if (comments == null)
            {
                throw new ArgumentNullException("comments");
            }

            this.Comments = comments;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCommentsDbStatement 
    {
        public GetCommentsDbStatement(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class IsPostSubscriberDbStatement 
    {
        public IsPostSubscriberDbStatement(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class LikePostDbStatement 
    {
        public LikePostDbStatement(
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCommentsQueryHandler 
    {
        public GetCommentsQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.IGetCommentsDbStatement getComments)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (getComments == null)
            {
                throw new ArgumentNullException("getComments");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.getComments = getComments;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class UnlikePostDbStatement 
    {
        public UnlikePostDbStatement(
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentData 
    {
        public CommentData(
            System.String content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            this.Content = content;
        }
    }
}

namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeletePostCommand 
    {
        public override string ToString()
        {
            return string.Format("DeletePostCommand({0}, {1})", this.PostId == null ? "null" : this.PostId.ToString(), this.Requester == null ? "null" : this.Requester.ToString());
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
        
            return this.Equals((DeletePostCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeletePostCommand other)
        {
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class PostToChannelCommand 
    {
        public override string ToString()
        {
            return string.Format("PostToChannelCommand({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
        
            return this.Equals((PostToChannelCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PostToChannelCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewPostId, other.NewPostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.ScheduledPostTime, other.ScheduledPostTime))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class ReorderQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("ReorderQueueCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.NewPartialQueueOrder == null ? "null" : this.NewPartialQueueOrder.ToString());
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
        
            return this.Equals((ReorderQueueCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPartialQueueOrder != null 
        			? this.NewPartialQueueOrder.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(ReorderQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (this.NewPartialQueueOrder != null && other.NewPartialQueueOrder != null)
            {
                if (!this.NewPartialQueueOrder.SequenceEqual(other.NewPartialQueueOrder))
                {
                    return false;    
                }
            }
            else if (this.NewPartialQueueOrder != null || other.NewPartialQueueOrder != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class BacklogPost 
    {
        public override string ToString()
        {
            return string.Format("BacklogPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, \"{7}\", \"{8}\", {9}, \"{10}\", \"{11}\", {12}, {13}, {14}, {15})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.FileSize == null ? "null" : this.FileSize.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
        
            return this.Equals((BacklogPost)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileName != null ? this.FileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileSize != null ? this.FileSize.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageName != null ? this.ImageName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageExtension != null ? this.ImageExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSize != null ? this.ImageSize.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageRenderWidth != null ? this.ImageRenderWidth.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageRenderHeight != null ? this.ImageRenderHeight.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BacklogPost other)
        {
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            if (!object.Equals(this.FileName, other.FileName))
            {
                return false;
            }
        
            if (!object.Equals(this.FileExtension, other.FileExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.FileSize, other.FileSize))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageName, other.ImageName))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageExtension, other.ImageExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageSize, other.ImageSize))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageRenderWidth, other.ImageRenderWidth))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageRenderHeight, other.ImageRenderHeight))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorBacklogQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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
        
            return this.Equals((GetCreatorBacklogQuery)obj);
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
        
        protected bool Equals(GetCreatorBacklogQuery other)
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class NewsfeedPost 
    {
        public override string ToString()
        {
            return string.Format("NewsfeedPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, \"{8}\", \"{9}\", {10}, \"{11}\", \"{12}\", {13}, {14}, {15}, {16}, {17}, {18}, {19})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.FileSize == null ? "null" : this.FileSize.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLikedPost == null ? "null" : this.HasLikedPost.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
        
            return this.Equals((NewsfeedPost)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileName != null ? this.FileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileSize != null ? this.FileSize.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageName != null ? this.ImageName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageExtension != null ? this.ImageExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSize != null ? this.ImageSize.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageRenderWidth != null ? this.ImageRenderWidth.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageRenderHeight != null ? this.ImageRenderHeight.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LikesCount != null ? this.LikesCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommentsCount != null ? this.CommentsCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HasLikedPost != null ? this.HasLikedPost.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewsfeedPost other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            if (!object.Equals(this.FileName, other.FileName))
            {
                return false;
            }
        
            if (!object.Equals(this.FileExtension, other.FileExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.FileSize, other.FileSize))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageName, other.ImageName))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageExtension, other.ImageExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageSize, other.ImageSize))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageRenderWidth, other.ImageRenderWidth))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageRenderHeight, other.ImageRenderHeight))
            {
                return false;
            }
        
            if (!object.Equals(this.LikesCount, other.LikesCount))
            {
                return false;
            }
        
            if (!object.Equals(this.CommentsCount, other.CommentsCount))
            {
                return false;
            }
        
            if (!object.Equals(this.HasLikedPost, other.HasLikedPost))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewPostData 
    {
        public override string ToString()
        {
            return string.Format("NewPostData({0}, {1}, {2}, \"{3}\", {4}, {5})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString());
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
        
            return this.Equals((NewPostData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewPostData other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.ScheduledPostTime, other.ScheduledPostTime))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RevisePostCommand 
    {
        public override string ToString()
        {
            return string.Format("RevisePostCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((RevisePostCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RevisePostCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedPostData 
    {
        public override string ToString()
        {
            return string.Format("RevisedPostData({0}, {1}, \"{2}\")", this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((RevisedPostData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RevisedPostData other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForNowCommand 
    {
        public override string ToString()
        {
            return string.Format("RescheduleForNowCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString());
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
        
            return this.Equals((RescheduleForNowCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RescheduleForNowCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleForTimeCommand 
    {
        public override string ToString()
        {
            return string.Format("RescheduleForTimeCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString());
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
        
            return this.Equals((RescheduleForTimeCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RescheduleForTimeCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ScheduledPostTime, other.ScheduledPostTime))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class RescheduleWithQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("RescheduleWithQueueCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString());
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
        
            return this.Equals((RescheduleWithQueueCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RescheduleWithQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCreatorBacklogQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorBacklogQueryResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.File == null ? "null" : this.File.ToString(), this.FileSource == null ? "null" : this.FileSource.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
        
            return this.Equals((GetCreatorBacklogQueryResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.File != null ? this.File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileSource != null ? this.FileSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetCreatorBacklogQueryResult other)
        {
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.File, other.File))
            {
                return false;
            }
        
            if (!object.Equals(this.FileSource, other.FileSource))
            {
                return false;
            }
        
            if (!object.Equals(this.Image, other.Image))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageSource, other.ImageSource))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class FileSourceInformation 
    {
        public override string ToString()
        {
            return string.Format("FileSourceInformation(\"{0}\", \"{1}\", \"{2}\", {3}, {4})", this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.ContentType == null ? "null" : this.ContentType.ToString(), this.Size == null ? "null" : this.Size.ToString(), this.RenderSize == null ? "null" : this.RenderSize.ToString());
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
        
            return this.Equals((FileSourceInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileName != null ? this.FileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ContentType != null ? this.ContentType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Size != null ? this.Size.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RenderSize != null ? this.RenderSize.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FileSourceInformation other)
        {
            if (!object.Equals(this.FileName, other.FileName))
            {
                return false;
            }
        
            if (!object.Equals(this.FileExtension, other.FileExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.ContentType, other.ContentType))
            {
                return false;
            }
        
            if (!object.Equals(this.Size, other.Size))
            {
                return false;
            }
        
            if (!object.Equals(this.RenderSize, other.RenderSize))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class RenderSize 
    {
        public override string ToString()
        {
            return string.Format("RenderSize({0}, {1})", this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString());
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
        
            return this.Equals((RenderSize)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Width != null ? this.Width.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Height != null ? this.Height.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RenderSize other)
        {
            if (!object.Equals(this.Width, other.Width))
            {
                return false;
            }
        
            if (!object.Equals(this.Height, other.Height))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQuery 
    {
        public override string ToString()
        {
            return string.Format("GetNewsfeedQuery({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.ChannelIds == null ? "null" : this.ChannelIds.ToString(), this.Origin == null ? "null" : this.Origin.ToString(), this.SearchForwards == null ? "null" : this.SearchForwards.ToString(), this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
        
            return this.Equals((GetNewsfeedQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelIds != null 
        			? this.ChannelIds.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.Origin != null ? this.Origin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SearchForwards != null ? this.SearchForwards.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StartIndex != null ? this.StartIndex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Count != null ? this.Count.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetNewsfeedQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
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
        
            if (!object.Equals(this.Origin, other.Origin))
            {
                return false;
            }
        
            if (!object.Equals(this.SearchForwards, other.SearchForwards))
            {
                return false;
            }
        
            if (!object.Equals(this.StartIndex, other.StartIndex))
            {
                return false;
            }
        
            if (!object.Equals(this.Count, other.Count))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetNewsfeedQueryResult({0}, {1})", this.Posts == null ? "null" : this.Posts.ToString(), this.AccountBalance == null ? "null" : this.AccountBalance.ToString());
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
        
            return this.Equals((GetNewsfeedQueryResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Posts != null 
        			? this.Posts.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.AccountBalance != null ? this.AccountBalance.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetNewsfeedQueryResult other)
        {
            if (this.Posts != null && other.Posts != null)
            {
                if (!this.Posts.SequenceEqual(other.Posts))
                {
                    return false;    
                }
            }
            else if (this.Posts != null || other.Posts != null)
            {
                return false;
            }
        
            if (!object.Equals(this.AccountBalance, other.AccountBalance))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public override string ToString()
            {
                return string.Format("Post({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.File == null ? "null" : this.File.ToString(), this.FileSource == null ? "null" : this.FileSource.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLiked == null ? "null" : this.HasLiked.ToString());
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
            
                return this.Equals((Post)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.File != null ? this.File.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileSource != null ? this.FileSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LikesCount != null ? this.LikesCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CommentsCount != null ? this.CommentsCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.HasLiked != null ? this.HasLiked.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(Post other)
            {
                if (!object.Equals(this.CreatorId, other.CreatorId))
                {
                    return false;
                }
            
                if (!object.Equals(this.PostId, other.PostId))
                {
                    return false;
                }
            
                if (!object.Equals(this.BlogId, other.BlogId))
                {
                    return false;
                }
            
                if (!object.Equals(this.ChannelId, other.ChannelId))
                {
                    return false;
                }
            
                if (!object.Equals(this.Comment, other.Comment))
                {
                    return false;
                }
            
                if (!object.Equals(this.File, other.File))
                {
                    return false;
                }
            
                if (!object.Equals(this.FileSource, other.FileSource))
                {
                    return false;
                }
            
                if (!object.Equals(this.Image, other.Image))
                {
                    return false;
                }
            
                if (!object.Equals(this.ImageSource, other.ImageSource))
                {
                    return false;
                }
            
                if (!object.Equals(this.LiveDate, other.LiveDate))
                {
                    return false;
                }
            
                if (!object.Equals(this.LikesCount, other.LikesCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.CommentsCount, other.CommentsCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.HasLiked, other.HasLiked))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels.Shared;
    using System.Transactions;
    using System.Collections.Generic;
    using Fifthweek.Api.Posts.Queries;
    using System.Text;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetNewsfeedDbResult 
    {
        public override string ToString()
        {
            return string.Format("GetNewsfeedDbResult({0}, {1})", this.Posts == null ? "null" : this.Posts.ToString(), this.AccountBalance == null ? "null" : this.AccountBalance.ToString());
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
        
            return this.Equals((GetNewsfeedDbResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Posts != null 
        			? this.Posts.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.AccountBalance != null ? this.AccountBalance.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetNewsfeedDbResult other)
        {
            if (this.Posts != null && other.Posts != null)
            {
                if (!this.Posts.SequenceEqual(other.Posts))
                {
                    return false;    
                }
            }
            else if (this.Posts != null || other.Posts != null)
            {
                return false;
            }
        
            if (!object.Equals(this.AccountBalance, other.AccountBalance))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeleteLikeCommand 
    {
        public override string ToString()
        {
            return string.Format("DeleteLikeCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString());
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
        
            return this.Equals((DeleteLikeCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeleteLikeCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class CommentOnPostCommand 
    {
        public override string ToString()
        {
            return string.Format("CommentOnPostCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.CommentId == null ? "null" : this.CommentId.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
        
            return this.Equals((CommentOnPostCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommentId != null ? this.CommentId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CommentOnPostCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.CommentId, other.CommentId))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class LikePostCommand 
    {
        public override string ToString()
        {
            return string.Format("LikePostCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
        
            return this.Equals((LikePostCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(LikePostCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class GetCommentsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCommentsQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString());
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
        
            return this.Equals((GetCommentsQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetCommentsQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentsResult
    {
        public partial class Item 
        {
            public override string ToString()
            {
                return string.Format("Item({0}, {1}, {2}, {3}, {4}, {5})", this.CommentId == null ? "null" : this.CommentId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
            
                return this.Equals((Item)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.CommentId != null ? this.CommentId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(Item other)
            {
                if (!object.Equals(this.CommentId, other.CommentId))
                {
                    return false;
                }
            
                if (!object.Equals(this.PostId, other.PostId))
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
            
                if (!object.Equals(this.Content, other.Content))
                {
                    return false;
                }
            
                if (!object.Equals(this.CreationDate, other.CreationDate))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentsResult 
    {
        public override string ToString()
        {
            return string.Format("CommentsResult({0})", this.Comments == null ? "null" : this.Comments.ToString());
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
        
            return this.Equals((CommentsResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Comments != null 
        			? this.Comments.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(CommentsResult other)
        {
            if (this.Comments != null && other.Comments != null)
            {
                if (!this.Comments.SequenceEqual(other.Comments))
                {
                    return false;    
                }
            }
            else if (this.Comments != null || other.Comments != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentData 
    {
        public override string ToString()
        {
            return string.Format("CommentData(\"{0}\")", this.Content == null ? "null" : this.Content.ToString());
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
        
            return this.Equals((CommentData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CommentData other)
        {
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreatorNewsfeedPaginationData 
    {
        public override string ToString()
        {
            return string.Format("CreatorNewsfeedPaginationData({0}, {1})", this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
        
            return this.Equals((CreatorNewsfeedPaginationData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartIndex != null ? this.StartIndex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Count != null ? this.Count.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorNewsfeedPaginationData other)
        {
            if (!object.Equals(this.StartIndex, other.StartIndex))
            {
                return false;
            }
        
            if (!object.Equals(this.Count, other.Count))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewsfeedFilter 
    {
        public override string ToString()
        {
            return string.Format("NewsfeedFilter(\"{0}\", \"{1}\", {2}, {3}, {4}, {5})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Origin == null ? "null" : this.Origin.ToString(), this.SearchForwards == null ? "null" : this.SearchForwards.ToString(), this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
        
            return this.Equals((NewsfeedFilter)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Origin != null ? this.Origin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SearchForwards != null ? this.SearchForwards.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StartIndex != null ? this.StartIndex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Count != null ? this.Count.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewsfeedFilter other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Origin, other.Origin))
            {
                return false;
            }
        
            if (!object.Equals(this.SearchForwards, other.SearchForwards))
            {
                return false;
            }
        
            if (!object.Equals(this.StartIndex, other.StartIndex))
            {
                return false;
            }
        
            if (!object.Equals(this.Count, other.Count))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class BacklogPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.QueueId = this.QueueId;
            builder.Comment = this.Comment;
            builder.FileId = this.FileId;
            builder.ImageId = this.ImageId;
            builder.LiveDate = this.LiveDate;
            builder.FileName = this.FileName;
            builder.FileExtension = this.FileExtension;
            builder.FileSize = this.FileSize;
            builder.ImageName = this.ImageName;
            builder.ImageExtension = this.ImageExtension;
            builder.ImageSize = this.ImageSize;
            builder.ImageRenderWidth = this.ImageRenderWidth;
            builder.ImageRenderHeight = this.ImageRenderHeight;
            builder.CreationDate = this.CreationDate;
            return builder;
        }
        
        public BacklogPost Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }
        
        public partial class Builder
        {
            public Fifthweek.Api.Posts.Shared.PostId PostId { get; set; }
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public Fifthweek.Api.Collections.Shared.QueueId QueueId { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Comment { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.DateTime LiveDate { get; set; }
            public System.String FileName { get; set; }
            public System.String FileExtension { get; set; }
            public System.Nullable<System.Int64> FileSize { get; set; }
            public System.String ImageName { get; set; }
            public System.String ImageExtension { get; set; }
            public System.Nullable<System.Int64> ImageSize { get; set; }
            public System.Nullable<System.Int32> ImageRenderWidth { get; set; }
            public System.Nullable<System.Int32> ImageRenderHeight { get; set; }
            public System.DateTime CreationDate { get; set; }
        
            public BacklogPost Build()
            {
                return new BacklogPost(
                    this.PostId, 
                    this.ChannelId, 
                    this.QueueId, 
                    this.Comment, 
                    this.FileId, 
                    this.ImageId, 
                    this.LiveDate, 
                    this.FileName, 
                    this.FileExtension, 
                    this.FileSize, 
                    this.ImageName, 
                    this.ImageExtension, 
                    this.ImageSize, 
                    this.ImageRenderWidth, 
                    this.ImageRenderHeight, 
                    this.CreationDate);
            }
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Posts.Controllers;

    public partial class NewsfeedPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.CreatorId = this.CreatorId;
            builder.PostId = this.PostId;
            builder.BlogId = this.BlogId;
            builder.ChannelId = this.ChannelId;
            builder.Comment = this.Comment;
            builder.FileId = this.FileId;
            builder.ImageId = this.ImageId;
            builder.LiveDate = this.LiveDate;
            builder.FileName = this.FileName;
            builder.FileExtension = this.FileExtension;
            builder.FileSize = this.FileSize;
            builder.ImageName = this.ImageName;
            builder.ImageExtension = this.ImageExtension;
            builder.ImageSize = this.ImageSize;
            builder.ImageRenderWidth = this.ImageRenderWidth;
            builder.ImageRenderHeight = this.ImageRenderHeight;
            builder.LikesCount = this.LikesCount;
            builder.CommentsCount = this.CommentsCount;
            builder.HasLikedPost = this.HasLikedPost;
            builder.CreationDate = this.CreationDate;
            return builder;
        }
        
        public NewsfeedPost Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }
        
        public partial class Builder
        {
            public Fifthweek.Api.Identity.Shared.Membership.UserId CreatorId { get; set; }
            public Fifthweek.Api.Posts.Shared.PostId PostId { get; set; }
            public Fifthweek.Api.Blogs.Shared.BlogId BlogId { get; set; }
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Comment { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.DateTime LiveDate { get; set; }
            public System.String FileName { get; set; }
            public System.String FileExtension { get; set; }
            public System.Nullable<System.Int64> FileSize { get; set; }
            public System.String ImageName { get; set; }
            public System.String ImageExtension { get; set; }
            public System.Nullable<System.Int64> ImageSize { get; set; }
            public System.Nullable<System.Int32> ImageRenderWidth { get; set; }
            public System.Nullable<System.Int32> ImageRenderHeight { get; set; }
            public System.Int32 LikesCount { get; set; }
            public System.Int32 CommentsCount { get; set; }
            public System.Boolean HasLikedPost { get; set; }
            public System.DateTime CreationDate { get; set; }
        
            public NewsfeedPost Build()
            {
                return new NewsfeedPost(
                    this.CreatorId, 
                    this.PostId, 
                    this.BlogId, 
                    this.ChannelId, 
                    this.Comment, 
                    this.FileId, 
                    this.ImageId, 
                    this.LiveDate, 
                    this.FileName, 
                    this.FileExtension, 
                    this.FileSize, 
                    this.ImageName, 
                    this.ImageExtension, 
                    this.ImageSize, 
                    this.ImageRenderWidth, 
                    this.ImageRenderHeight, 
                    this.LikesCount, 
                    this.CommentsCount, 
                    this.HasLikedPost, 
                    this.CreationDate);
            }
        }
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewPostData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                Fifthweek.Api.FileManagement.Shared.FileId imageId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostTime,
                Fifthweek.Api.Collections.Shared.QueueId queueId)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                this.ChannelId = channelId;
                this.FileId = fileId;
                this.ImageId = imageId;
                this.Comment = comment;
                this.ScheduledPostTime = scheduledPostTime;
                this.QueueId = queueId;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostTime { get; private set; }
        
            public Fifthweek.Api.Collections.Shared.QueueId QueueId { get; private set; }
        }
    }

    public static partial class NewPostDataExtensions
    {
        public static NewPostData.Parsed Parse(this NewPostData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Comment", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewPostData.Parsed(
                target.ChannelId,
                target.FileId,
                target.ImageId,
                parsed0,
                target.ScheduledPostTime,
                target.QueueId);
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedPostData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                Fifthweek.Api.FileManagement.Shared.FileId imageId,
                ValidComment comment)
            {
                this.FileId = fileId;
                this.ImageId = imageId;
                this.Comment = comment;
            }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        }
    }

    public static partial class RevisedPostDataExtensions
    {
        public static RevisedPostData.Parsed Parse(this RevisedPostData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Comment", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RevisedPostData.Parsed(
                target.FileId,
                target.ImageId,
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CommentData 
    {
        public class Parsed
        {
            public Parsed(
                ValidComment content)
            {
                if (content == null)
                {
                    throw new ArgumentNullException("content");
                }

                this.Content = content;
            }
        
            public ValidComment Content { get; private set; }
        }
    }

    public static partial class CommentDataExtensions
    {
        public static CommentData.Parsed Parse(this CommentData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Content))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Content, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Content", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Content", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CommentData.Parsed(
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreatorNewsfeedPaginationData 
    {
        public class Parsed
        {
            public Parsed(
                NonNegativeInt startIndex,
                PositiveInt count)
            {
                if (startIndex == null)
                {
                    throw new ArgumentNullException("startIndex");
                }

                if (count == null)
                {
                    throw new ArgumentNullException("count");
                }

                this.StartIndex = startIndex;
                this.Count = count;
            }
        
            public NonNegativeInt StartIndex { get; private set; }
        
            public PositiveInt Count { get; private set; }
        }
    }

    public static partial class CreatorNewsfeedPaginationDataExtensions
    {
        public static CreatorNewsfeedPaginationData.Parsed Parse(this CreatorNewsfeedPaginationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            NonNegativeInt parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!NonNegativeInt.TryParse(target.StartIndex, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("StartIndex", modelState);
            }

            PositiveInt parsed1 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
            if (!PositiveInt.TryParse(target.Count, out parsed1, out parsed1Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed1Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Count", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CreatorNewsfeedPaginationData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewsfeedFilter 
    {
        public class Parsed
        {
            public Parsed(
                System.String creatorId,
                System.String channelId,
                System.Nullable<System.DateTime> origin,
                System.Boolean searchForwards,
                NonNegativeInt startIndex,
                PositiveInt count)
            {
                if (searchForwards == null)
                {
                    throw new ArgumentNullException("searchForwards");
                }

                if (startIndex == null)
                {
                    throw new ArgumentNullException("startIndex");
                }

                if (count == null)
                {
                    throw new ArgumentNullException("count");
                }

                this.CreatorId = creatorId;
                this.ChannelId = channelId;
                this.Origin = origin;
                this.SearchForwards = searchForwards;
                this.StartIndex = startIndex;
                this.Count = count;
            }
        
            public System.String CreatorId { get; private set; }
        
            public System.String ChannelId { get; private set; }
        
            public System.Nullable<System.DateTime> Origin { get; private set; }
        
            public System.Boolean SearchForwards { get; private set; }
        
            public NonNegativeInt StartIndex { get; private set; }
        
            public PositiveInt Count { get; private set; }
        }
    }

    public static partial class NewsfeedFilterExtensions
    {
        public static NewsfeedFilter.Parsed Parse(this NewsfeedFilter target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            NonNegativeInt parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!NonNegativeInt.TryParse(target.StartIndex, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("StartIndex", modelState);
            }

            PositiveInt parsed1 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
            if (!PositiveInt.TryParse(target.Count, out parsed1, out parsed1Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed1Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Count", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewsfeedFilter.Parsed(
                target.CreatorId,
                target.ChannelId,
                target.Origin,
                target.SearchForwards,
                parsed0,
                parsed1);
        }    
    }
}


