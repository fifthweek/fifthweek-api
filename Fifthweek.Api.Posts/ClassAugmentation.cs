using System;
using System.Linq;

//// Generated on 27/10/2015 12:48:51 (UTC)
//// Mapped solution in 17.99s


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
            Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
            Fifthweek.Api.Posts.Shared.ValidPreviewText previewText,
            Fifthweek.Api.Posts.Shared.ValidComment content,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.FileManagement.Shared.FileId> fileIds,
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

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
            }

            if (fileIds == null)
            {
                throw new ArgumentNullException("fileIds");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.ChannelId = channelId;
            this.PreviewImageId = previewImageId;
            this.PreviewText = previewText;
            this.Content = content;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.FileIds = fileIds;
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

            if (postToChannel == null)
            {
                throw new ArgumentNullException("postToChannel");
            }

            this.queueSecurity = queueSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
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
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetNewsfeedQuery,Fifthweek.Api.Posts.Queries.GetPreviewNewsfeedQueryResult> getPreviewNewsfeed,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.CommentOnPostCommand> postComment,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCommentsQuery,Fifthweek.Api.Posts.Controllers.CommentsResult> getComments,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.LikePostCommand> postLike,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeleteLikeCommand> deleteLike,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostToChannelCommand> postPost,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.RevisePostCommand> revisePost,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetPostQuery,Fifthweek.Api.Posts.Queries.GetPostQueryResult> getPost,
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

            if (getPreviewNewsfeed == null)
            {
                throw new ArgumentNullException("getPreviewNewsfeed");
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

            if (getPost == null)
            {
                throw new ArgumentNullException("getPost");
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
            this.getPreviewNewsfeed = getPreviewNewsfeed;
            this.postComment = postComment;
            this.getComments = getComments;
            this.postLike = postLike;
            this.deleteLike = deleteLike;
            this.postPost = postPost;
            this.revisePost = revisePost;
            this.getPost = getPost;
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
    using Fifthweek.Api.Azure;

    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Posts.Shared.PreviewText previewText,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.DateTime liveDate,
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

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
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
            this.PreviewText = previewText;
            this.ImageId = imageId;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.LiveDate = liveDate;
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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

    public partial class NewsfeedPost 
    {
        public NewsfeedPost(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.PreviewText previewText,
            Fifthweek.Api.Posts.Shared.Comment content,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.DateTime liveDate,
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

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
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
            this.PreviewText = previewText;
            this.Content = content;
            this.ImageId = imageId;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.LiveDate = liveDate;
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
            Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
            System.String previewText,
            System.String content,
            System.Nullable<System.DateTime> scheduledPostTime,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> fileIds)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
            }

            this.ChannelId = channelId;
            this.PreviewImageId = previewImageId;
            this.PreviewText = previewText;
            this.Content = content;
            this.ScheduledPostTime = scheduledPostTime;
            this.QueueId = queueId;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.FileIds = fileIds;
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
            Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
            Fifthweek.Api.Posts.Shared.ValidPreviewText previewText,
            Fifthweek.Api.Posts.Shared.ValidComment content,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.FileManagement.Shared.FileId> fileIds)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
            }

            if (fileIds == null)
            {
                throw new ArgumentNullException("fileIds");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.PreviewImageId = previewImageId;
            this.PreviewText = previewText;
            this.Content = content;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.FileIds = fileIds;
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
            Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
            System.String previewText,
            System.String content,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> fileIds)
        {
            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
            }

            this.PreviewImageId = previewImageId;
            this.PreviewText = previewText;
            this.Content = content;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.FileIds = fileIds;
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
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Posts.IRevisePostDbStatement revisePostDbStatement)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (revisePostDbStatement == null)
            {
                throw new ArgumentNullException("revisePostDbStatement");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
            this.fileSecurity = fileSecurity;
            this.revisePostDbStatement = revisePostDbStatement;
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
    using Fifthweek.Api.Azure;

    public partial class GetCreatorBacklogQueryResult 
    {
        public GetCreatorBacklogQueryResult(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Posts.Shared.PreviewText previewText,
            Fifthweek.Api.FileManagement.Shared.FileInformation image,
            Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
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

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.PostId = postId;
            this.ChannelId = channelId;
            this.QueueId = queueId;
            this.PreviewText = previewText;
            this.Image = image;
            this.ImageSource = imageSource;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

    public partial class GetNewsfeedQueryHandler 
    {
        public GetNewsfeedQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IGetNewsfeedDbStatement getNewsfeedDbStatement,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap,
            Fifthweek.Shared.ITimestampCreator timestampCreator)
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

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            this.requesterSecurity = requesterSecurity;
            this.getNewsfeedDbStatement = getNewsfeedDbStatement;
            this.fileInformationAggregator = fileInformationAggregator;
            this.mimeTypeMap = mimeTypeMap;
            this.timestampCreator = timestampCreator;
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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public Post(
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Posts.Shared.PreviewText previewText,
                Fifthweek.Api.FileManagement.Shared.FileInformation image,
                Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
                System.Int32 previewWordCount,
                System.Int32 wordCount,
                System.Int32 imageCount,
                System.Int32 fileCount,
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

                if (previewWordCount == null)
                {
                    throw new ArgumentNullException("previewWordCount");
                }

                if (wordCount == null)
                {
                    throw new ArgumentNullException("wordCount");
                }

                if (imageCount == null)
                {
                    throw new ArgumentNullException("imageCount");
                }

                if (fileCount == null)
                {
                    throw new ArgumentNullException("fileCount");
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
                this.PreviewText = previewText;
                this.Image = image;
                this.ImageSource = imageSource;
                this.PreviewWordCount = previewWordCount;
                this.WordCount = wordCount;
                this.ImageCount = imageCount;
                this.FileCount = fileCount;
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
    using Fifthweek.Api.Azure;

    public partial class GetCommentsQuery 
    {
        public GetCommentsQuery(
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
            Fifthweek.Api.Posts.IUnlikePostDbStatement unlikePost)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (unlikePost == null)
            {
                throw new ArgumentNullException("unlikePost");
            }

            this.requesterSecurity = requesterSecurity;
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
    using Fifthweek.Api.Azure;

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

    public partial class GetPreviewNewsfeedDbStatement 
    {
        public GetPreviewNewsfeedDbStatement(
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryHandler 
    {
        public GetPreviewNewsfeedQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IGetPreviewNewsfeedDbStatement getPreviewNewsfeedDbStatement,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap,
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Api.FileManagement.Shared.IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getPreviewNewsfeedDbStatement == null)
            {
                throw new ArgumentNullException("getPreviewNewsfeedDbStatement");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (getAccessSignatureExpiryInformation == null)
            {
                throw new ArgumentNullException("getAccessSignatureExpiryInformation");
            }

            this.requesterSecurity = requesterSecurity;
            this.getPreviewNewsfeedDbStatement = getPreviewNewsfeedDbStatement;
            this.fileInformationAggregator = fileInformationAggregator;
            this.mimeTypeMap = mimeTypeMap;
            this.blobService = blobService;
            this.timestampCreator = timestampCreator;
            this.getAccessSignatureExpiryInformation = getAccessSignatureExpiryInformation;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult 
    {
        public GetPreviewNewsfeedQueryResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.GetPreviewNewsfeedQueryResult.PreviewPost> posts)
        {
            if (posts == null)
            {
                throw new ArgumentNullException("posts");
            }

            this.Posts = posts;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPost 
        {
            public PreviewPost(
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Posts.Queries.GetPreviewNewsfeedQueryResult.PreviewPostCreator creator,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Posts.Queries.GetPreviewNewsfeedQueryResult.PreviewPostBlog blog,
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Posts.Queries.GetPreviewNewsfeedQueryResult.PreviewPostChannel channel,
                Fifthweek.Api.Posts.Shared.PreviewText previewText,
                Fifthweek.Api.FileManagement.Shared.FileInformation image,
                Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
                Fifthweek.Api.Azure.BlobSharedAccessInformation imageAccessInformation,
                System.Int32 previewWordCount,
                System.Int32 wordCount,
                System.Int32 imageCount,
                System.Int32 fileCount,
                System.DateTime liveDate,
                System.Int32 likesCount,
                System.Int32 commentsCount,
                System.Boolean hasLiked)
            {
                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                if (creator == null)
                {
                    throw new ArgumentNullException("creator");
                }

                if (postId == null)
                {
                    throw new ArgumentNullException("postId");
                }

                if (blogId == null)
                {
                    throw new ArgumentNullException("blogId");
                }

                if (blog == null)
                {
                    throw new ArgumentNullException("blog");
                }

                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (channel == null)
                {
                    throw new ArgumentNullException("channel");
                }

                if (previewWordCount == null)
                {
                    throw new ArgumentNullException("previewWordCount");
                }

                if (wordCount == null)
                {
                    throw new ArgumentNullException("wordCount");
                }

                if (imageCount == null)
                {
                    throw new ArgumentNullException("imageCount");
                }

                if (fileCount == null)
                {
                    throw new ArgumentNullException("fileCount");
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
                this.Creator = creator;
                this.PostId = postId;
                this.BlogId = blogId;
                this.Blog = blog;
                this.ChannelId = channelId;
                this.Channel = channel;
                this.PreviewText = previewText;
                this.Image = image;
                this.ImageSource = imageSource;
                this.ImageAccessInformation = imageAccessInformation;
                this.PreviewWordCount = previewWordCount;
                this.WordCount = wordCount;
                this.ImageCount = imageCount;
                this.FileCount = fileCount;
                this.LiveDate = liveDate;
                this.LikesCount = likesCount;
                this.CommentsCount = commentsCount;
                this.HasLiked = hasLiked;
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
    using Fifthweek.Api.Azure;

    public partial class PreviewNewsfeedPost 
    {
        public PreviewNewsfeedPost(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.String username,
            Fifthweek.Api.FileManagement.Shared.FileId profileImageFileId,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String blogName,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String channelName,
            Fifthweek.Api.Posts.Shared.PreviewText previewText,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.Int32 previewWordCount,
            System.Int32 wordCount,
            System.Int32 imageCount,
            System.Int32 fileCount,
            System.DateTime liveDate,
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

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (blogName == null)
            {
                throw new ArgumentNullException("blogName");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (channelName == null)
            {
                throw new ArgumentNullException("channelName");
            }

            if (previewWordCount == null)
            {
                throw new ArgumentNullException("previewWordCount");
            }

            if (wordCount == null)
            {
                throw new ArgumentNullException("wordCount");
            }

            if (imageCount == null)
            {
                throw new ArgumentNullException("imageCount");
            }

            if (fileCount == null)
            {
                throw new ArgumentNullException("fileCount");
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
            this.Username = username;
            this.ProfileImageFileId = profileImageFileId;
            this.PostId = postId;
            this.BlogId = blogId;
            this.BlogName = blogName;
            this.ChannelId = channelId;
            this.ChannelName = channelName;
            this.PreviewText = previewText;
            this.ImageId = imageId;
            this.PreviewWordCount = previewWordCount;
            this.WordCount = wordCount;
            this.ImageCount = imageCount;
            this.FileCount = fileCount;
            this.LiveDate = liveDate;
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

    public partial class GetPreviewNewsfeedDbResult 
    {
        public GetPreviewNewsfeedDbResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.PreviewNewsfeedPost> posts)
        {
            if (posts == null)
            {
                throw new ArgumentNullException("posts");
            }

            this.Posts = posts;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostCreator 
        {
            public PreviewPostCreator(
                Fifthweek.Api.Identity.Shared.Membership.Username username,
                Fifthweek.Api.FileManagement.Shared.FileInformation profileImage)
            {
                if (username == null)
                {
                    throw new ArgumentNullException("username");
                }

                this.Username = username;
                this.ProfileImage = profileImage;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostBlog 
        {
            public PreviewPostBlog(
                Fifthweek.Api.Blogs.Shared.BlogName name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                this.Name = name;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostChannel 
        {
            public PreviewPostChannel(
                Fifthweek.Api.Channels.Shared.ChannelName name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                this.Name = name;
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

    public partial class RevisePostDbStatement 
    {
        public RevisePostDbStatement(
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

    public partial class GetPostDbResult
    {
        public partial class PostFileDbResult 
        {
            public PostFileDbResult(
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                System.String fileName,
                System.String fileExtension,
                System.String purpose,
                System.Int64 fileSize,
                System.Nullable<System.Int32> renderWidth,
                System.Nullable<System.Int32> renderHeight)
            {
                if (fileId == null)
                {
                    throw new ArgumentNullException("fileId");
                }

                if (fileName == null)
                {
                    throw new ArgumentNullException("fileName");
                }

                if (fileExtension == null)
                {
                    throw new ArgumentNullException("fileExtension");
                }

                if (purpose == null)
                {
                    throw new ArgumentNullException("purpose");
                }

                if (fileSize == null)
                {
                    throw new ArgumentNullException("fileSize");
                }

                this.FileId = fileId;
                this.FileName = fileName;
                this.FileExtension = fileExtension;
                this.Purpose = purpose;
                this.FileSize = fileSize;
                this.RenderWidth = renderWidth;
                this.RenderHeight = renderHeight;
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

    public partial class GetPostDbResult 
    {
        public GetPostDbResult(
            Fifthweek.Api.Posts.Queries.NewsfeedPost post,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.GetPostDbResult.PostFileDbResult> files)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }

            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            this.Post = post;
            this.Files = files;
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryHandler 
    {
        public GetPostQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.IGetPostDbStatement getPostDbStatement,
            Fifthweek.Api.FileManagement.Shared.IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation,
            Fifthweek.Api.FileManagement.Shared.IFileInformationAggregator fileInformationAggregator,
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap,
            Fifthweek.Api.Posts.IRequestFreePostDbStatement requestFreePost)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (getPostDbStatement == null)
            {
                throw new ArgumentNullException("getPostDbStatement");
            }

            if (getAccessSignatureExpiryInformation == null)
            {
                throw new ArgumentNullException("getAccessSignatureExpiryInformation");
            }

            if (fileInformationAggregator == null)
            {
                throw new ArgumentNullException("fileInformationAggregator");
            }

            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            if (requestFreePost == null)
            {
                throw new ArgumentNullException("requestFreePost");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.getPostDbStatement = getPostDbStatement;
            this.getAccessSignatureExpiryInformation = getAccessSignatureExpiryInformation;
            this.fileInformationAggregator = fileInformationAggregator;
            this.blobService = blobService;
            this.mimeTypeMap = mimeTypeMap;
            this.requestFreePost = requestFreePost;
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

    public partial class GetPostDbStatement 
    {
        public GetPostDbStatement(
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult
    {
        public partial class FullPost 
        {
            public FullPost(
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Posts.Shared.Comment content,
                System.Int32 previewWordCount,
                System.Int32 wordCount,
                System.Int32 imageCount,
                System.Int32 fileCount,
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

                if (content == null)
                {
                    throw new ArgumentNullException("content");
                }

                if (previewWordCount == null)
                {
                    throw new ArgumentNullException("previewWordCount");
                }

                if (wordCount == null)
                {
                    throw new ArgumentNullException("wordCount");
                }

                if (imageCount == null)
                {
                    throw new ArgumentNullException("imageCount");
                }

                if (fileCount == null)
                {
                    throw new ArgumentNullException("fileCount");
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
                this.Content = content;
                this.PreviewWordCount = previewWordCount;
                this.WordCount = wordCount;
                this.ImageCount = imageCount;
                this.FileCount = fileCount;
                this.LiveDate = liveDate;
                this.LikesCount = likesCount;
                this.CommentsCount = commentsCount;
                this.HasLiked = hasLiked;
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult 
    {
        public GetPostQueryResult(
            Fifthweek.Api.Posts.Queries.GetPostQueryResult.FullPost post,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.GetPostQueryResult.File> files)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }

            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            this.Post = post;
            this.Files = files;
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult
    {
        public partial class File 
        {
            public File(
                Fifthweek.Api.FileManagement.Shared.FileInformation information,
                Fifthweek.Api.Posts.Queries.FileSourceInformation source,
                Fifthweek.Api.Azure.BlobSharedAccessInformation accessInformation)
            {
                if (information == null)
                {
                    throw new ArgumentNullException("information");
                }

                if (source == null)
                {
                    throw new ArgumentNullException("source");
                }

                this.Information = information;
                this.Source = source;
                this.AccessInformation = accessInformation;
            }
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    public partial class GetPostQuery 
    {
        public GetPostQuery(
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
            return string.Format("PostToChannelCommand({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.PreviewImageId == null ? "null" : this.PreviewImageId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.FileIds == null ? "null" : this.FileIds.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewImageId != null ? this.PreviewImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileIds != null 
        			? this.FileIds.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
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
        
            if (!object.Equals(this.PreviewImageId, other.PreviewImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (this.FileIds != null && other.FileIds != null)
            {
                if (!this.FileIds.SequenceEqual(other.FileIds))
                {
                    return false;    
                }
            }
            else if (this.FileIds != null || other.FileIds != null)
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
    using Fifthweek.Api.Azure;

    public partial class BacklogPost 
    {
        public override string ToString()
        {
            return string.Format("BacklogPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, \"{10}\", \"{11}\", {12}, {13}, {14}, {15})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
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
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

    public partial class NewsfeedPost 
    {
        public override string ToString()
        {
            return string.Format("NewsfeedPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, \"{12}\", \"{13}\", {14}, {15}, {16}, {17}, {18}, {19}, {20})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLikedPost == null ? "null" : this.HasLikedPost.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
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
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
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
            return string.Format("NewPostData({0}, {1}, \"{2}\", \"{3}\", {4}, {5}, {6}, {7}, {8}, {9}, {10})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.PreviewImageId == null ? "null" : this.PreviewImageId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.FileIds == null ? "null" : this.FileIds.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewImageId != null ? this.PreviewImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileIds != null 
        			? this.FileIds.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(NewPostData other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewImageId, other.PreviewImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
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
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (this.FileIds != null && other.FileIds != null)
            {
                if (!this.FileIds.SequenceEqual(other.FileIds))
                {
                    return false;    
                }
            }
            else if (this.FileIds != null || other.FileIds != null)
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
            return string.Format("RevisePostCommand({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.PreviewImageId == null ? "null" : this.PreviewImageId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.FileIds == null ? "null" : this.FileIds.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewImageId != null ? this.PreviewImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileIds != null 
        			? this.FileIds.Aggregate(0, (previous, current) => 
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
        
            if (!object.Equals(this.PreviewImageId, other.PreviewImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (this.FileIds != null && other.FileIds != null)
            {
                if (!this.FileIds.SequenceEqual(other.FileIds))
                {
                    return false;    
                }
            }
            else if (this.FileIds != null || other.FileIds != null)
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
            return string.Format("RevisedPostData({0}, \"{1}\", \"{2}\", {3}, {4}, {5}, {6}, {7})", this.PreviewImageId == null ? "null" : this.PreviewImageId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.FileIds == null ? "null" : this.FileIds.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewImageId != null ? this.PreviewImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileIds != null 
        			? this.FileIds.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(RevisedPostData other)
        {
            if (!object.Equals(this.PreviewImageId, other.PreviewImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.Content, other.Content))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (this.FileIds != null && other.FileIds != null)
            {
                if (!this.FileIds.SequenceEqual(other.FileIds))
                {
                    return false;    
                }
            }
            else if (this.FileIds != null || other.FileIds != null)
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
    using Fifthweek.Api.Azure;

    public partial class GetCreatorBacklogQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorBacklogQueryResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
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
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
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
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

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
    using Fifthweek.Api.Azure;

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public override string ToString()
            {
                return string.Format("Post({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLiked == null ? "null" : this.HasLiked.ToString());
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
                    hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
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
            
                if (!object.Equals(this.PreviewText, other.PreviewText))
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
            
                if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.WordCount, other.WordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.ImageCount, other.ImageCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.FileCount, other.FileCount))
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
    using Fifthweek.Api.Azure;

    public partial class GetCommentsQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCommentsQuery({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
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
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetPreviewNewsfeedQueryResult({0})", this.Posts == null ? "null" : this.Posts.ToString());
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
        
            return this.Equals((GetPreviewNewsfeedQueryResult)obj);
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
                return hashCode;
            }
        }
        
        protected bool Equals(GetPreviewNewsfeedQueryResult other)
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPost 
        {
            public override string ToString()
            {
                return string.Format("PreviewPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Creator == null ? "null" : this.Creator.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.Blog == null ? "null" : this.Blog.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Channel == null ? "null" : this.Channel.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.ImageAccessInformation == null ? "null" : this.ImageAccessInformation.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLiked == null ? "null" : this.HasLiked.ToString());
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
            
                return this.Equals((PreviewPost)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Creator != null ? this.Creator.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Blog != null ? this.Blog.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Channel != null ? this.Channel.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageAccessInformation != null ? this.ImageAccessInformation.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LikesCount != null ? this.LikesCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CommentsCount != null ? this.CommentsCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.HasLiked != null ? this.HasLiked.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PreviewPost other)
            {
                if (!object.Equals(this.CreatorId, other.CreatorId))
                {
                    return false;
                }
            
                if (!object.Equals(this.Creator, other.Creator))
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
            
                if (!object.Equals(this.Blog, other.Blog))
                {
                    return false;
                }
            
                if (!object.Equals(this.ChannelId, other.ChannelId))
                {
                    return false;
                }
            
                if (!object.Equals(this.Channel, other.Channel))
                {
                    return false;
                }
            
                if (!object.Equals(this.PreviewText, other.PreviewText))
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
            
                if (!object.Equals(this.ImageAccessInformation, other.ImageAccessInformation))
                {
                    return false;
                }
            
                if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.WordCount, other.WordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.ImageCount, other.ImageCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.FileCount, other.FileCount))
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
    using Fifthweek.Api.Azure;

    public partial class PreviewNewsfeedPost 
    {
        public override string ToString()
        {
            return string.Format("PreviewNewsfeedPost({0}, \"{1}\", {2}, {3}, {4}, \"{5}\", {6}, \"{7}\", {8}, {9}, {10}, {11}, {12}, {13}, {14}, \"{15}\", \"{16}\", {17}, {18}, {19}, {20}, {21}, {22}, {23})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.BlogName == null ? "null" : this.BlogName.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.ChannelName == null ? "null" : this.ChannelName.ToString(), this.PreviewText == null ? "null" : this.PreviewText.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLikedPost == null ? "null" : this.HasLikedPost.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
        
            return this.Equals((PreviewNewsfeedPost)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogName != null ? this.BlogName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelName != null ? this.ChannelName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewText != null ? this.PreviewText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
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
        
        protected bool Equals(PreviewNewsfeedPost other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
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
        
            if (!object.Equals(this.BlogName, other.BlogName))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelName, other.ChannelName))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewText, other.PreviewText))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.WordCount, other.WordCount))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageCount, other.ImageCount))
            {
                return false;
            }
        
            if (!object.Equals(this.FileCount, other.FileCount))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
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

    public partial class GetPreviewNewsfeedDbResult 
    {
        public override string ToString()
        {
            return string.Format("GetPreviewNewsfeedDbResult({0})", this.Posts == null ? "null" : this.Posts.ToString());
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
        
            return this.Equals((GetPreviewNewsfeedDbResult)obj);
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
                return hashCode;
            }
        }
        
        protected bool Equals(GetPreviewNewsfeedDbResult other)
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostCreator 
        {
            public override string ToString()
            {
                return string.Format("PreviewPostCreator({0}, {1})", this.Username == null ? "null" : this.Username.ToString(), this.ProfileImage == null ? "null" : this.ProfileImage.ToString());
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
            
                return this.Equals((PreviewPostCreator)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ProfileImage != null ? this.ProfileImage.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PreviewPostCreator other)
            {
                if (!object.Equals(this.Username, other.Username))
                {
                    return false;
                }
            
                if (!object.Equals(this.ProfileImage, other.ProfileImage))
                {
                    return false;
                }
            
                return true;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostBlog 
        {
            public override string ToString()
            {
                return string.Format("PreviewPostBlog({0})", this.Name == null ? "null" : this.Name.ToString());
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
            
                return this.Equals((PreviewPostBlog)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PreviewPostBlog other)
            {
                if (!object.Equals(this.Name, other.Name))
                {
                    return false;
                }
            
                return true;
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
    using Fifthweek.Api.Azure;

    public partial class GetPreviewNewsfeedQueryResult
    {
        public partial class PreviewPostChannel 
        {
            public override string ToString()
            {
                return string.Format("PreviewPostChannel({0})", this.Name == null ? "null" : this.Name.ToString());
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
            
                return this.Equals((PreviewPostChannel)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PreviewPostChannel other)
            {
                if (!object.Equals(this.Name, other.Name))
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

    public partial class GetPostDbResult
    {
        public partial class PostFileDbResult 
        {
            public override string ToString()
            {
                return string.Format("PostFileDbResult({0}, \"{1}\", \"{2}\", \"{3}\", {4}, {5}, {6})", this.FileId == null ? "null" : this.FileId.ToString(), this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString(), this.FileSize == null ? "null" : this.FileSize.ToString(), this.RenderWidth == null ? "null" : this.RenderWidth.ToString(), this.RenderHeight == null ? "null" : this.RenderHeight.ToString());
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
            
                return this.Equals((PostFileDbResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileName != null ? this.FileName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileSize != null ? this.FileSize.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.RenderWidth != null ? this.RenderWidth.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.RenderHeight != null ? this.RenderHeight.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PostFileDbResult other)
            {
                if (!object.Equals(this.FileId, other.FileId))
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
            
                if (!object.Equals(this.Purpose, other.Purpose))
                {
                    return false;
                }
            
                if (!object.Equals(this.FileSize, other.FileSize))
                {
                    return false;
                }
            
                if (!object.Equals(this.RenderWidth, other.RenderWidth))
                {
                    return false;
                }
            
                if (!object.Equals(this.RenderHeight, other.RenderHeight))
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

    public partial class GetPostDbResult 
    {
        public override string ToString()
        {
            return string.Format("GetPostDbResult({0}, {1})", this.Post == null ? "null" : this.Post.ToString(), this.Files == null ? "null" : this.Files.ToString());
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
        
            return this.Equals((GetPostDbResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Post != null ? this.Post.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Files != null 
        			? this.Files.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(GetPostDbResult other)
        {
            if (!object.Equals(this.Post, other.Post))
            {
                return false;
            }
        
            if (this.Files != null && other.Files != null)
            {
                if (!this.Files.SequenceEqual(other.Files))
                {
                    return false;    
                }
            }
            else if (this.Files != null || other.Files != null)
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult
    {
        public partial class FullPost 
        {
            public override string ToString()
            {
                return string.Format("FullPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Content == null ? "null" : this.Content.ToString(), this.PreviewWordCount == null ? "null" : this.PreviewWordCount.ToString(), this.WordCount == null ? "null" : this.WordCount.ToString(), this.ImageCount == null ? "null" : this.ImageCount.ToString(), this.FileCount == null ? "null" : this.FileCount.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.LikesCount == null ? "null" : this.LikesCount.ToString(), this.CommentsCount == null ? "null" : this.CommentsCount.ToString(), this.HasLiked == null ? "null" : this.HasLiked.ToString());
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
            
                return this.Equals((FullPost)obj);
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
                    hashCode = (hashCode * 397) ^ (this.Content != null ? this.Content.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.PreviewWordCount != null ? this.PreviewWordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.WordCount != null ? this.WordCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageCount != null ? this.ImageCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileCount != null ? this.FileCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LikesCount != null ? this.LikesCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CommentsCount != null ? this.CommentsCount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.HasLiked != null ? this.HasLiked.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(FullPost other)
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
            
                if (!object.Equals(this.Content, other.Content))
                {
                    return false;
                }
            
                if (!object.Equals(this.PreviewWordCount, other.PreviewWordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.WordCount, other.WordCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.ImageCount, other.ImageCount))
                {
                    return false;
                }
            
                if (!object.Equals(this.FileCount, other.FileCount))
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetPostQueryResult({0}, {1})", this.Post == null ? "null" : this.Post.ToString(), this.Files == null ? "null" : this.Files.ToString());
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
        
            return this.Equals((GetPostQueryResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Post != null ? this.Post.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Files != null 
        			? this.Files.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(GetPostQueryResult other)
        {
            if (!object.Equals(this.Post, other.Post))
            {
                return false;
            }
        
            if (this.Files != null && other.Files != null)
            {
                if (!this.Files.SequenceEqual(other.Files))
                {
                    return false;    
                }
            }
            else if (this.Files != null || other.Files != null)
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
    using Fifthweek.Api.Azure;

    public partial class GetPostQueryResult
    {
        public partial class File 
        {
            public override string ToString()
            {
                return string.Format("File({0}, {1}, {2})", this.Information == null ? "null" : this.Information.ToString(), this.Source == null ? "null" : this.Source.ToString(), this.AccessInformation == null ? "null" : this.AccessInformation.ToString());
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
            
                return this.Equals((File)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Information != null ? this.Information.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Source != null ? this.Source.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.AccessInformation != null ? this.AccessInformation.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(File other)
            {
                if (!object.Equals(this.Information, other.Information))
                {
                    return false;
                }
            
                if (!object.Equals(this.Source, other.Source))
                {
                    return false;
                }
            
                if (!object.Equals(this.AccessInformation, other.AccessInformation))
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    public partial class GetPostQuery 
    {
        public override string ToString()
        {
            return string.Format("GetPostQuery({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString());
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
        
            return this.Equals((GetPostQuery)obj);
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
        
        protected bool Equals(GetPostQuery other)
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
    using Fifthweek.Api.Azure;

    public partial class BacklogPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.QueueId = this.QueueId;
            builder.PreviewText = this.PreviewText;
            builder.ImageId = this.ImageId;
            builder.PreviewWordCount = this.PreviewWordCount;
            builder.WordCount = this.WordCount;
            builder.ImageCount = this.ImageCount;
            builder.FileCount = this.FileCount;
            builder.LiveDate = this.LiveDate;
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
            public Fifthweek.Api.Posts.Shared.PreviewText PreviewText { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.Int32 PreviewWordCount { get; set; }
            public System.Int32 WordCount { get; set; }
            public System.Int32 ImageCount { get; set; }
            public System.Int32 FileCount { get; set; }
            public System.DateTime LiveDate { get; set; }
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
                    this.PreviewText, 
                    this.ImageId, 
                    this.PreviewWordCount, 
                    this.WordCount, 
                    this.ImageCount, 
                    this.FileCount, 
                    this.LiveDate, 
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
    using Fifthweek.Api.Azure;

    public partial class NewsfeedPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.CreatorId = this.CreatorId;
            builder.PostId = this.PostId;
            builder.BlogId = this.BlogId;
            builder.ChannelId = this.ChannelId;
            builder.PreviewText = this.PreviewText;
            builder.Content = this.Content;
            builder.ImageId = this.ImageId;
            builder.PreviewWordCount = this.PreviewWordCount;
            builder.WordCount = this.WordCount;
            builder.ImageCount = this.ImageCount;
            builder.FileCount = this.FileCount;
            builder.LiveDate = this.LiveDate;
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
            public Fifthweek.Api.Posts.Shared.PreviewText PreviewText { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Content { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.Int32 PreviewWordCount { get; set; }
            public System.Int32 WordCount { get; set; }
            public System.Int32 ImageCount { get; set; }
            public System.Int32 FileCount { get; set; }
            public System.DateTime LiveDate { get; set; }
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
                    this.PreviewText, 
                    this.Content, 
                    this.ImageId, 
                    this.PreviewWordCount, 
                    this.WordCount, 
                    this.ImageCount, 
                    this.FileCount, 
                    this.LiveDate, 
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
    using Fifthweek.Api.Azure;

    public partial class PreviewNewsfeedPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.CreatorId = this.CreatorId;
            builder.Username = this.Username;
            builder.ProfileImageFileId = this.ProfileImageFileId;
            builder.PostId = this.PostId;
            builder.BlogId = this.BlogId;
            builder.BlogName = this.BlogName;
            builder.ChannelId = this.ChannelId;
            builder.ChannelName = this.ChannelName;
            builder.PreviewText = this.PreviewText;
            builder.ImageId = this.ImageId;
            builder.PreviewWordCount = this.PreviewWordCount;
            builder.WordCount = this.WordCount;
            builder.ImageCount = this.ImageCount;
            builder.FileCount = this.FileCount;
            builder.LiveDate = this.LiveDate;
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
        
        public PreviewNewsfeedPost Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }
        
        public partial class Builder
        {
            public Fifthweek.Api.Identity.Shared.Membership.UserId CreatorId { get; set; }
            public System.String Username { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ProfileImageFileId { get; set; }
            public Fifthweek.Api.Posts.Shared.PostId PostId { get; set; }
            public Fifthweek.Api.Blogs.Shared.BlogId BlogId { get; set; }
            public System.String BlogName { get; set; }
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public System.String ChannelName { get; set; }
            public Fifthweek.Api.Posts.Shared.PreviewText PreviewText { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.Int32 PreviewWordCount { get; set; }
            public System.Int32 WordCount { get; set; }
            public System.Int32 ImageCount { get; set; }
            public System.Int32 FileCount { get; set; }
            public System.DateTime LiveDate { get; set; }
            public System.String ImageName { get; set; }
            public System.String ImageExtension { get; set; }
            public System.Nullable<System.Int64> ImageSize { get; set; }
            public System.Nullable<System.Int32> ImageRenderWidth { get; set; }
            public System.Nullable<System.Int32> ImageRenderHeight { get; set; }
            public System.Int32 LikesCount { get; set; }
            public System.Int32 CommentsCount { get; set; }
            public System.Boolean HasLikedPost { get; set; }
            public System.DateTime CreationDate { get; set; }
        
            public PreviewNewsfeedPost Build()
            {
                return new PreviewNewsfeedPost(
                    this.CreatorId, 
                    this.Username, 
                    this.ProfileImageFileId, 
                    this.PostId, 
                    this.BlogId, 
                    this.BlogName, 
                    this.ChannelId, 
                    this.ChannelName, 
                    this.PreviewText, 
                    this.ImageId, 
                    this.PreviewWordCount, 
                    this.WordCount, 
                    this.ImageCount, 
                    this.FileCount, 
                    this.LiveDate, 
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

    public partial class GetPostDbResult
    {
        public partial class PostFileDbResult 
        {
            public Builder ToBuilder()
            {
                var builder = new Builder();
                builder.FileId = this.FileId;
                builder.FileName = this.FileName;
                builder.FileExtension = this.FileExtension;
                builder.Purpose = this.Purpose;
                builder.FileSize = this.FileSize;
                builder.RenderWidth = this.RenderWidth;
                builder.RenderHeight = this.RenderHeight;
                return builder;
            }
            
            public PostFileDbResult Copy(Action<Builder> applyDelta)
            {
                var builder = this.ToBuilder();
                applyDelta(builder);
                return builder.Build();
            }
            
            public partial class Builder
            {
                public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
                public System.String FileName { get; set; }
                public System.String FileExtension { get; set; }
                public System.String Purpose { get; set; }
                public System.Int64 FileSize { get; set; }
                public System.Nullable<System.Int32> RenderWidth { get; set; }
                public System.Nullable<System.Int32> RenderHeight { get; set; }
            
                public PostFileDbResult Build()
                {
                    return new PostFileDbResult(
                        this.FileId, 
                        this.FileName, 
                        this.FileExtension, 
                        this.Purpose, 
                        this.FileSize, 
                        this.RenderWidth, 
                        this.RenderHeight);
                }
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
                Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
                ValidPreviewText previewText,
                ValidComment content,
                System.Nullable<System.DateTime> scheduledPostTime,
                Fifthweek.Api.Collections.Shared.QueueId queueId,
                System.Int32 previewWordCount,
                System.Int32 wordCount,
                System.Int32 imageCount,
                System.Int32 fileCount,
                System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> fileIds)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (previewWordCount == null)
                {
                    throw new ArgumentNullException("previewWordCount");
                }

                if (wordCount == null)
                {
                    throw new ArgumentNullException("wordCount");
                }

                if (imageCount == null)
                {
                    throw new ArgumentNullException("imageCount");
                }

                if (fileCount == null)
                {
                    throw new ArgumentNullException("fileCount");
                }

                this.ChannelId = channelId;
                this.PreviewImageId = previewImageId;
                this.PreviewText = previewText;
                this.Content = content;
                this.ScheduledPostTime = scheduledPostTime;
                this.QueueId = queueId;
                this.PreviewWordCount = previewWordCount;
                this.WordCount = wordCount;
                this.ImageCount = imageCount;
                this.FileCount = fileCount;
                this.FileIds = fileIds;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId PreviewImageId { get; private set; }
        
            public ValidPreviewText PreviewText { get; private set; }
        
            public ValidComment Content { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostTime { get; private set; }
        
            public Fifthweek.Api.Collections.Shared.QueueId QueueId { get; private set; }
        
            public System.Int32 PreviewWordCount { get; private set; }
        
            public System.Int32 WordCount { get; private set; }
        
            public System.Int32 ImageCount { get; private set; }
        
            public System.Int32 FileCount { get; private set; }
        
            public System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> FileIds { get; private set; }
        }
    }

    public static partial class NewPostDataExtensions
    {
        public static NewPostData.Parsed Parse(this NewPostData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidPreviewText parsed0 = null;
            if (!ValidPreviewText.IsEmpty(target.PreviewText))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidPreviewText.TryParse(target.PreviewText, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("PreviewText", modelState);
                }
            }

            ValidComment parsed1 = null;
            if (!ValidComment.IsEmpty(target.Content))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidComment.TryParse(target.Content, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Content", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewPostData.Parsed(
                target.ChannelId,
                target.PreviewImageId,
                parsed0,
                parsed1,
                target.ScheduledPostTime,
                target.QueueId,
                target.PreviewWordCount,
                target.WordCount,
                target.ImageCount,
                target.FileCount,
                target.FileIds);
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
                Fifthweek.Api.FileManagement.Shared.FileId previewImageId,
                ValidPreviewText previewText,
                ValidComment content,
                System.Int32 previewWordCount,
                System.Int32 wordCount,
                System.Int32 imageCount,
                System.Int32 fileCount,
                System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> fileIds)
            {
                if (previewWordCount == null)
                {
                    throw new ArgumentNullException("previewWordCount");
                }

                if (wordCount == null)
                {
                    throw new ArgumentNullException("wordCount");
                }

                if (imageCount == null)
                {
                    throw new ArgumentNullException("imageCount");
                }

                if (fileCount == null)
                {
                    throw new ArgumentNullException("fileCount");
                }

                this.PreviewImageId = previewImageId;
                this.PreviewText = previewText;
                this.Content = content;
                this.PreviewWordCount = previewWordCount;
                this.WordCount = wordCount;
                this.ImageCount = imageCount;
                this.FileCount = fileCount;
                this.FileIds = fileIds;
            }
        
            public Fifthweek.Api.FileManagement.Shared.FileId PreviewImageId { get; private set; }
        
            public ValidPreviewText PreviewText { get; private set; }
        
            public ValidComment Content { get; private set; }
        
            public System.Int32 PreviewWordCount { get; private set; }
        
            public System.Int32 WordCount { get; private set; }
        
            public System.Int32 ImageCount { get; private set; }
        
            public System.Int32 FileCount { get; private set; }
        
            public System.Collections.Generic.List<Fifthweek.Api.FileManagement.Shared.FileId> FileIds { get; private set; }
        }
    }

    public static partial class RevisedPostDataExtensions
    {
        public static RevisedPostData.Parsed Parse(this RevisedPostData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidPreviewText parsed0 = null;
            if (!ValidPreviewText.IsEmpty(target.PreviewText))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidPreviewText.TryParse(target.PreviewText, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("PreviewText", modelState);
                }
            }

            ValidComment parsed1 = null;
            if (!ValidComment.IsEmpty(target.Content))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidComment.TryParse(target.Content, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Content", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RevisedPostData.Parsed(
                target.PreviewImageId,
                parsed0,
                parsed1,
                target.PreviewWordCount,
                target.WordCount,
                target.ImageCount,
                target.FileCount,
                target.FileIds);
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


