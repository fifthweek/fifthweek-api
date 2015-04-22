using System;
using System.Linq;

//// Generated on 22/04/2015 12:03:58 (UTC)
//// Mapped solution in 11.19s


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
            Fifthweek.Api.Posts.IRemoveFromQueueIfRequiredDbStatement removeFromQueueIfRequired,
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

            if (deletePost == null)
            {
                throw new ArgumentNullException("deletePost");
            }

            if (removeFromQueueIfRequired == null)
            {
                throw new ArgumentNullException("removeFromQueueIfRequired");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.deletePost = deletePost;
            this.removeFromQueueIfRequired = removeFromQueueIfRequired;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
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

    public partial class PostFileCommand 
    {
        public PostFileCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.IsQueued = isQueued;
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

    public partial class PostFileCommandHandler 
    {
        public PostFileCommandHandler(
            Fifthweek.Api.Collections.Shared.ICollectionSecurity collectionSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IPostFileTypeChecks postFileTypeChecks,
            Fifthweek.Api.Posts.IPostToCollectionDbStatement postToCollection)
        {
            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
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

            if (postToCollection == null)
            {
                throw new ArgumentNullException("postToCollection");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.postFileTypeChecks = postFileTypeChecks;
            this.postToCollection = postToCollection;
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

    public partial class PostImageCommand 
    {
        public PostImageCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (imageFileId == null)
            {
                throw new ArgumentNullException("imageFileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.CollectionId = collectionId;
            this.ImageFileId = imageFileId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.IsQueued = isQueued;
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

    public partial class PostImageCommandHandler 
    {
        public PostImageCommandHandler(
            Fifthweek.Api.Collections.Shared.ICollectionSecurity collectionSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.IPostFileTypeChecks postFileTypeChecks,
            Fifthweek.Api.Posts.IPostToCollectionDbStatement postToCollection)
        {
            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
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

            if (postToCollection == null)
            {
                throw new ArgumentNullException("postToCollection");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.postFileTypeChecks = postFileTypeChecks;
            this.postToCollection = postToCollection;
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

    public partial class PostNoteCommand 
    {
        public PostNoteCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.ValidNote note,
            System.Nullable<System.DateTime> scheduledPostDate)
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

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.ChannelId = channelId;
            this.Note = note;
            this.ScheduledPostDate = scheduledPostDate;
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

    public partial class PostNoteCommandHandler 
    {
        public PostNoteCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Posts.IPostNoteDbStatement postNote)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (postNote == null)
            {
                throw new ArgumentNullException("postNote");
            }

            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
            this.postNote = postNote;
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
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Shared.PostId> newPartialQueueOrder)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (newPartialQueueOrder == null)
            {
                throw new ArgumentNullException("newPartialQueueOrder");
            }

            this.Requester = requester;
            this.CollectionId = collectionId;
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

    public partial class ReviseNoteCommand 
    {
        public ReviseNoteCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.ValidNote note)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.ChannelId = channelId;
            this.Note = note;
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

    public partial class FilePostController 
    {
        public FilePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostFileCommand> postFile,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseFileCommand> reviseFile,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postFile == null)
            {
                throw new ArgumentNullException("postFile");
            }

            if (reviseFile == null)
            {
                throw new ArgumentNullException("reviseFile");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postFile = postFile;
            this.reviseFile = reviseFile;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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

    public partial class ImagePostController 
    {
        public ImagePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostImageCommand> postImage,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseImageCommand> reviseImage,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postImage == null)
            {
                throw new ArgumentNullException("postImage");
            }

            if (reviseImage == null)
            {
                throw new ArgumentNullException("reviseImage");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postImage = postImage;
            this.reviseImage = reviseImage;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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

    public partial class NotePostController 
    {
        public NotePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostNoteCommand> postNote,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseNoteCommand> reviseNote,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postNote == null)
            {
                throw new ArgumentNullException("postNote");
            }

            if (reviseNote == null)
            {
                throw new ArgumentNullException("reviseNote");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postNote = postNote;
            this.reviseNote = reviseNote;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
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

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.deletePost = deletePost;
            this.reorderQueue = reorderQueue;
            this.rescheduleForNow = rescheduleForNow;
            this.rescheduleForTime = rescheduleForTime;
            this.rescheduleWithQueue = rescheduleWithQueue;
            this.getCreatorBacklog = getCreatorBacklog;
            this.getNewsfeed = getNewsfeed;
            this.requesterContext = requesterContext;
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

    public partial class RevisedNoteData 
    {
        public RevisedNoteData(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String note)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.ChannelId = channelId;
            this.Note = note;
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

    public partial class PostOwnership 
    {
        public PostOwnership(
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

    public partial class PostSecurity 
    {
        public PostSecurity(
            Fifthweek.Api.Posts.IPostOwnership postOwnership)
        {
            if (postOwnership == null)
            {
                throw new ArgumentNullException("postOwnership");
            }

            this.postOwnership = postOwnership;
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

    public partial class PostToCollectionDbStatement 
    {
        public PostToCollectionDbStatement(
            Fifthweek.Api.Posts.IPostToCollectionDbSubStatements subStatements)
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

    public partial class PostToCollectionDbSubStatements 
    {
        public PostToCollectionDbSubStatements(
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

    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
            System.Boolean scheduledByQueue,
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

            if (scheduledByQueue == null)
            {
                throw new ArgumentNullException("scheduledByQueue");
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
            this.CollectionId = collectionId;
            this.Comment = comment;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.ScheduledByQueue = scheduledByQueue;
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

    public partial class NewsfeedPost 
    {
        public NewsfeedPost(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
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
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

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

            this.CreatorId = creatorId;
            this.PostId = postId;
            this.ChannelId = channelId;
            this.CollectionId = collectionId;
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

    public partial class NewFileData 
    {
        public NewFileData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            System.Boolean isQueued)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.IsQueued = isQueued;
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

    public partial class NewImageData 
    {
        public NewImageData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostTime,
            System.Boolean isQueued)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostTime = scheduledPostTime;
            this.IsQueued = isQueued;
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

    public partial class NewNoteData 
    {
        public NewNoteData(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String note,
            System.Nullable<System.DateTime> scheduledPostTime)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.ChannelId = channelId;
            this.Note = note;
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

    public partial class ReviseFileCommand 
    {
        public ReviseFileCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
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

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.FileId = fileId;
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

    public partial class ReviseImageCommand 
    {
        public ReviseImageCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
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

            if (imageFileId == null)
            {
                throw new ArgumentNullException("imageFileId");
            }

            this.Requester = requester;
            this.PostId = postId;
            this.ImageFileId = imageFileId;
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

    public partial class RevisedFileData 
    {
        public RevisedFileData(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String comment)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.FileId = fileId;
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

    public partial class RevisedImageData 
    {
        public RevisedImageData(
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            System.String comment)
        {
            if (imageFileId == null)
            {
                throw new ArgumentNullException("imageFileId");
            }

            this.ImageFileId = imageFileId;
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

    public partial class ReviseNoteCommandHandler 
    {
        public ReviseNoteCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
            this.postSecurity = postSecurity;
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

    public partial class PostNoteDbStatement 
    {
        public PostNoteDbStatement(
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

    public partial class ReviseImageCommandHandler 
    {
        public ReviseImageCommandHandler(
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

    public partial class ReviseFileCommandHandler 
    {
        public ReviseFileCommandHandler(
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

    public partial class TryGetQueuedPostCollectionDbStatement 
    {
        public TryGetQueuedPostCollectionDbStatement(
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
            Fifthweek.Api.Posts.IRemoveFromQueueIfRequiredDbStatement removeFromQueueIfRequired)
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

            if (removeFromQueueIfRequired == null)
            {
                throw new ArgumentNullException("removeFromQueueIfRequired");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.setPostLiveDate = setPostLiveDate;
            this.removeFromQueueIfRequired = removeFromQueueIfRequired;
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

    public partial class RemoveFromQueueIfRequiredDbStatement 
    {
        public RemoveFromQueueIfRequiredDbStatement(
            Fifthweek.Api.Posts.ITryGetQueuedPostCollectionDbStatement tryGetQueuedPostCollection,
            Fifthweek.Api.Collections.Shared.IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule,
            Fifthweek.Api.Collections.Shared.IDefragmentQueueDbStatement defragmentQueue)
        {
            if (tryGetQueuedPostCollection == null)
            {
                throw new ArgumentNullException("tryGetQueuedPostCollection");
            }

            if (getWeeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("getWeeklyReleaseSchedule");
            }

            if (defragmentQueue == null)
            {
                throw new ArgumentNullException("defragmentQueue");
            }

            this.tryGetQueuedPostCollection = tryGetQueuedPostCollection;
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

    public partial class RescheduleForTimeCommandHandler 
    {
        public RescheduleForTimeCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Posts.ISetPostLiveDateDbStatement setPostLiveDate,
            Fifthweek.Api.Posts.IRemoveFromQueueIfRequiredDbStatement removeFromQueueIfRequired)
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

            if (removeFromQueueIfRequired == null)
            {
                throw new ArgumentNullException("removeFromQueueIfRequired");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.setPostLiveDate = setPostLiveDate;
            this.removeFromQueueIfRequired = removeFromQueueIfRequired;
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
            Fifthweek.Api.Posts.ITryGetUnqueuedPostCollectionDbStatement tryGetUnqueuedPostCollection,
            Fifthweek.Api.Posts.IMovePostToQueueDbStatement movePostToQueue)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (tryGetUnqueuedPostCollection == null)
            {
                throw new ArgumentNullException("tryGetUnqueuedPostCollection");
            }

            if (movePostToQueue == null)
            {
                throw new ArgumentNullException("movePostToQueue");
            }

            this.requesterSecurity = requesterSecurity;
            this.postSecurity = postSecurity;
            this.tryGetUnqueuedPostCollection = tryGetUnqueuedPostCollection;
            this.movePostToQueue = movePostToQueue;
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

    public partial class TryGetUnqueuedPostCollectionDbStatement 
    {
        public TryGetUnqueuedPostCollectionDbStatement(
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

    public partial class PostsController 
    {
        public PostsController(
            Fifthweek.Api.Posts.Controllers.IPostController postController,
            Fifthweek.Api.Posts.Controllers.INotePostController notePostController,
            Fifthweek.Api.Posts.Controllers.IImagePostController imagePostController,
            Fifthweek.Api.Posts.Controllers.IFilePostController filePostController)
        {
            if (postController == null)
            {
                throw new ArgumentNullException("postController");
            }

            if (notePostController == null)
            {
                throw new ArgumentNullException("notePostController");
            }

            if (imagePostController == null)
            {
                throw new ArgumentNullException("imagePostController");
            }

            if (filePostController == null)
            {
                throw new ArgumentNullException("filePostController");
            }

            this.postController = postController;
            this.notePostController = notePostController;
            this.imagePostController = imagePostController;
            this.filePostController = filePostController;
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

    public partial class GetCreatorBacklogQueryResult 
    {
        public GetCreatorBacklogQueryResult(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileInformation file,
            Fifthweek.Api.Posts.Queries.FileSourceInformation fileSource,
            Fifthweek.Api.FileManagement.Shared.FileInformation image,
            Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
            System.Boolean scheduledByQueue,
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

            if (scheduledByQueue == null)
            {
                throw new ArgumentNullException("scheduledByQueue");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.PostId = postId;
            this.ChannelId = channelId;
            this.CollectionId = collectionId;
            this.Comment = comment;
            this.File = file;
            this.FileSource = fileSource;
            this.Image = image;
            this.ImageSource = imageSource;
            this.ScheduledByQueue = scheduledByQueue;
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

    public partial class GetCreatorNewsfeedQueryResult 
    {
        public GetCreatorNewsfeedQueryResult(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
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
            this.CollectionId = collectionId;
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

    public partial class GetNewsfeedQuery 
    {
        public GetNewsfeedQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Channels.Shared.ChannelId> channelIds,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Collections.Shared.CollectionId> collectionIds,
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
            this.CollectionIds = collectionIds;
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

    public partial class GetNewsfeedQueryResult 
    {
        public GetNewsfeedQueryResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.GetNewsfeedQueryResult.Post> posts)
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

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public Post(
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                Fifthweek.Api.Posts.Shared.PostId postId,
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.Posts.Shared.Comment comment,
                Fifthweek.Api.FileManagement.Shared.FileInformation file,
                Fifthweek.Api.Posts.Queries.FileSourceInformation fileSource,
                Fifthweek.Api.FileManagement.Shared.FileInformation image,
                Fifthweek.Api.Posts.Queries.FileSourceInformation imageSource,
                System.DateTime liveDate)
            {
                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

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

                this.CreatorId = creatorId;
                this.PostId = postId;
                this.ChannelId = channelId;
                this.CollectionId = collectionId;
                this.Comment = comment;
                this.File = file;
                this.FileSource = fileSource;
                this.Image = image;
                this.ImageSource = imageSource;
                this.LiveDate = liveDate;
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

    public partial class PostFileCommand 
    {
        public override string ToString()
        {
            return string.Format("PostFileCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((PostFileCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PostFileCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewPostId, other.NewPostId))
            {
                return false;
            }
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
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
        
            if (!object.Equals(this.IsQueued, other.IsQueued))
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

    public partial class PostImageCommand 
    {
        public override string ToString()
        {
            return string.Format("PostImageCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((PostImageCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PostImageCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewPostId, other.NewPostId))
            {
                return false;
            }
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageFileId, other.ImageFileId))
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
        
            if (!object.Equals(this.IsQueued, other.IsQueued))
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

    public partial class PostNoteCommand 
    {
        public override string ToString()
        {
            return string.Format("PostNoteCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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
        
            return this.Equals((PostNoteCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PostNoteCommand other)
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
        
            if (!object.Equals(this.Note, other.Note))
            {
                return false;
            }
        
            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
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
            return string.Format("ReorderQueueCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.NewPartialQueueOrder == null ? "null" : this.NewPartialQueueOrder.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
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
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
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

    public partial class ReviseNoteCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseNoteCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString());
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
        
            return this.Equals((ReviseNoteCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseNoteCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Note, other.Note))
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

    public partial class RevisedNoteData 
    {
        public override string ToString()
        {
            return string.Format("RevisedNoteData({0}, \"{1}\")", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString());
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
        
            return this.Equals((RevisedNoteData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RevisedNoteData other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Note, other.Note))
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

    public partial class BacklogPost 
    {
        public override string ToString()
        {
            return string.Format("BacklogPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, \"{8}\", \"{9}\", {10}, \"{11}\", \"{12}\", {13}, {14}, {15}, {16})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.ScheduledByQueue == null ? "null" : this.ScheduledByQueue.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.FileSize == null ? "null" : this.FileSize.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledByQueue != null ? this.ScheduledByQueue.GetHashCode() : 0);
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
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
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
        
            if (!object.Equals(this.ScheduledByQueue, other.ScheduledByQueue))
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

    public partial class NewsfeedPost 
    {
        public override string ToString()
        {
            return string.Format("NewsfeedPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, \"{8}\", \"{9}\", {10}, \"{11}\", \"{12}\", {13}, {14}, {15}, {16})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.FileName == null ? "null" : this.FileName.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.FileSize == null ? "null" : this.FileSize.ToString(), this.ImageName == null ? "null" : this.ImageName.ToString(), this.ImageExtension == null ? "null" : this.ImageExtension.ToString(), this.ImageSize == null ? "null" : this.ImageSize.ToString(), this.ImageRenderWidth == null ? "null" : this.ImageRenderWidth.ToString(), this.ImageRenderHeight == null ? "null" : this.ImageRenderHeight.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
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
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
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

    public partial class NewFileData 
    {
        public override string ToString()
        {
            return string.Format("NewFileData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((NewFileData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewFileData other)
        {
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
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
        
            if (!object.Equals(this.IsQueued, other.IsQueued))
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

    public partial class NewImageData 
    {
        public override string ToString()
        {
            return string.Format("NewImageData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((NewImageData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewImageData other)
        {
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
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
        
            if (!object.Equals(this.IsQueued, other.IsQueued))
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

    public partial class NewNoteData 
    {
        public override string ToString()
        {
            return string.Format("NewNoteData({0}, \"{1}\", {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostTime == null ? "null" : this.ScheduledPostTime.ToString());
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
        
            return this.Equals((NewNoteData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostTime != null ? this.ScheduledPostTime.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewNoteData other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Note, other.Note))
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

    public partial class ReviseFileCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseFileCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((ReviseFileCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseFileCommand other)
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

    public partial class ReviseImageCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseImageCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((ReviseImageCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseImageCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageFileId, other.ImageFileId))
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

    public partial class RevisedFileData 
    {
        public override string ToString()
        {
            return string.Format("RevisedFileData({0}, \"{1}\")", this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((RevisedFileData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RevisedFileData other)
        {
            if (!object.Equals(this.FileId, other.FileId))
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

    public partial class RevisedImageData 
    {
        public override string ToString()
        {
            return string.Format("RevisedImageData({0}, \"{1}\")", this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((RevisedImageData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RevisedImageData other)
        {
            if (!object.Equals(this.ImageFileId, other.ImageFileId))
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
            return string.Format("RescheduleWithQueueCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString());
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

    public partial class GetCreatorBacklogQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorBacklogQueryResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.File == null ? "null" : this.File.ToString(), this.FileSource == null ? "null" : this.FileSource.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.ScheduledByQueue == null ? "null" : this.ScheduledByQueue.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.File != null ? this.File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileSource != null ? this.FileSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledByQueue != null ? this.ScheduledByQueue.GetHashCode() : 0);
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
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
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
        
            if (!object.Equals(this.ScheduledByQueue, other.ScheduledByQueue))
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

    public partial class GetCreatorNewsfeedQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorNewsfeedQueryResult({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.File == null ? "null" : this.File.ToString(), this.FileSource == null ? "null" : this.FileSource.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
        
            return this.Equals((GetCreatorNewsfeedQueryResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.File != null ? this.File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileSource != null ? this.FileSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetCreatorNewsfeedQueryResult other)
        {
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
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

    public partial class GetNewsfeedQuery 
    {
        public override string ToString()
        {
            return string.Format("GetNewsfeedQuery({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", this.Requester == null ? "null" : this.Requester.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.ChannelIds == null ? "null" : this.ChannelIds.ToString(), this.CollectionIds == null ? "null" : this.CollectionIds.ToString(), this.Origin == null ? "null" : this.Origin.ToString(), this.SearchForwards == null ? "null" : this.SearchForwards.ToString(), this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CollectionIds != null 
        			? this.CollectionIds.Aggregate(0, (previous, current) => 
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
        
            if (this.CollectionIds != null && other.CollectionIds != null)
            {
                if (!this.CollectionIds.SequenceEqual(other.CollectionIds))
                {
                    return false;    
                }
            }
            else if (this.CollectionIds != null || other.CollectionIds != null)
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

    public partial class GetNewsfeedQueryResult 
    {
        public override string ToString()
        {
            return string.Format("GetNewsfeedQueryResult({0})", this.Posts == null ? "null" : this.Posts.ToString());
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

    public partial class GetNewsfeedQueryResult
    {
        public partial class Post 
        {
            public override string ToString()
            {
                return string.Format("Post({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.File == null ? "null" : this.File.ToString(), this.FileSource == null ? "null" : this.FileSource.ToString(), this.Image == null ? "null" : this.Image.ToString(), this.ImageSource == null ? "null" : this.ImageSource.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
                    hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.File != null ? this.File.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.FileSource != null ? this.FileSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Image != null ? this.Image.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.ImageSource != null ? this.ImageSource.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
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
            
                if (!object.Equals(this.ChannelId, other.ChannelId))
                {
                    return false;
                }
            
                if (!object.Equals(this.CollectionId, other.CollectionId))
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
            return string.Format("NewsfeedFilter(\"{0}\", {1}, {2}, {3}, {4}, {5}, {6})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.ChannelIds == null ? "null" : this.ChannelIds.ToString(), this.CollectionIds == null ? "null" : this.CollectionIds.ToString(), this.Origin == null ? "null" : this.Origin.ToString(), this.SearchForwards == null ? "null" : this.SearchForwards.ToString(), this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ChannelIds != null 
        			? this.ChannelIds.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CollectionIds != null 
        			? this.CollectionIds.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(NewsfeedFilter other)
        {
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
        
            if (this.CollectionIds != null && other.CollectionIds != null)
            {
                if (!this.CollectionIds.SequenceEqual(other.CollectionIds))
                {
                    return false;    
                }
            }
            else if (this.CollectionIds != null || other.CollectionIds != null)
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

    public partial class BacklogPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.CollectionId = this.CollectionId;
            builder.Comment = this.Comment;
            builder.FileId = this.FileId;
            builder.ImageId = this.ImageId;
            builder.ScheduledByQueue = this.ScheduledByQueue;
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
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Comment { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
            public System.Boolean ScheduledByQueue { get; set; }
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
                    this.CollectionId, 
                    this.Comment, 
                    this.FileId, 
                    this.ImageId, 
                    this.ScheduledByQueue, 
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

    public partial class NewsfeedPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.CreatorId = this.CreatorId;
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.CollectionId = this.CollectionId;
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
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; set; }
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
        
            public NewsfeedPost Build()
            {
                return new NewsfeedPost(
                    this.CreatorId, 
                    this.PostId, 
                    this.ChannelId, 
                    this.CollectionId, 
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

    public partial class RevisedNoteData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                ValidNote note)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (note == null)
                {
                    throw new ArgumentNullException("note");
                }

                this.ChannelId = channelId;
                this.Note = note;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public ValidNote Note { get; private set; }
        }
    }

    public static partial class RevisedNoteDataExtensions
    {
        public static RevisedNoteData.Parsed Parse(this RevisedNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidNote parsed0 = null;
            if (!ValidNote.IsEmpty(target.Note))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidNote.TryParse(target.Note, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Note", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Note", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RevisedNoteData.Parsed(
                target.ChannelId,
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

    public partial class NewFileData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostTime,
                System.Boolean isQueued)
            {
                if (collectionId == null)
                {
                    throw new ArgumentNullException("collectionId");
                }

                if (fileId == null)
                {
                    throw new ArgumentNullException("fileId");
                }

                if (isQueued == null)
                {
                    throw new ArgumentNullException("isQueued");
                }

                this.CollectionId = collectionId;
                this.FileId = fileId;
                this.Comment = comment;
                this.ScheduledPostTime = scheduledPostTime;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostTime { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class NewFileDataExtensions
    {
        public static NewFileData.Parsed Parse(this NewFileData target)
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
        
            return new NewFileData.Parsed(
                target.CollectionId,
                target.FileId,
                parsed0,
                target.ScheduledPostTime,
                target.IsQueued);
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

    public partial class NewImageData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostTime,
                System.Boolean isQueued)
            {
                if (collectionId == null)
                {
                    throw new ArgumentNullException("collectionId");
                }

                if (fileId == null)
                {
                    throw new ArgumentNullException("fileId");
                }

                if (isQueued == null)
                {
                    throw new ArgumentNullException("isQueued");
                }

                this.CollectionId = collectionId;
                this.FileId = fileId;
                this.Comment = comment;
                this.ScheduledPostTime = scheduledPostTime;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostTime { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class NewImageDataExtensions
    {
        public static NewImageData.Parsed Parse(this NewImageData target)
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
        
            return new NewImageData.Parsed(
                target.CollectionId,
                target.FileId,
                parsed0,
                target.ScheduledPostTime,
                target.IsQueued);
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

    public partial class NewNoteData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                ValidNote note,
                System.Nullable<System.DateTime> scheduledPostTime)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (note == null)
                {
                    throw new ArgumentNullException("note");
                }

                this.ChannelId = channelId;
                this.Note = note;
                this.ScheduledPostTime = scheduledPostTime;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public ValidNote Note { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostTime { get; private set; }
        }
    }

    public static partial class NewNoteDataExtensions
    {
        public static NewNoteData.Parsed Parse(this NewNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidNote parsed0 = null;
            if (!ValidNote.IsEmpty(target.Note))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidNote.TryParse(target.Note, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Note", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Note", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewNoteData.Parsed(
                target.ChannelId,
                parsed0,
                target.ScheduledPostTime);
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

    public partial class RevisedFileData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                ValidComment comment)
            {
                if (fileId == null)
                {
                    throw new ArgumentNullException("fileId");
                }

                this.FileId = fileId;
                this.Comment = comment;
            }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        }
    }

    public static partial class RevisedFileDataExtensions
    {
        public static RevisedFileData.Parsed Parse(this RevisedFileData target)
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
        
            return new RevisedFileData.Parsed(
                target.FileId,
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

    public partial class RevisedImageData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
                ValidComment comment)
            {
                if (imageFileId == null)
                {
                    throw new ArgumentNullException("imageFileId");
                }

                this.ImageFileId = imageFileId;
                this.Comment = comment;
            }
        
            public Fifthweek.Api.FileManagement.Shared.FileId ImageFileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        }
    }

    public static partial class RevisedImageDataExtensions
    {
        public static RevisedImageData.Parsed Parse(this RevisedImageData target)
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
        
            return new RevisedImageData.Parsed(
                target.ImageFileId,
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
                System.Collections.Generic.List<System.String> channelIds,
                System.Collections.Generic.List<System.String> collectionIds,
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
                this.ChannelIds = channelIds;
                this.CollectionIds = collectionIds;
                this.Origin = origin;
                this.SearchForwards = searchForwards;
                this.StartIndex = startIndex;
                this.Count = count;
            }
        
            public System.String CreatorId { get; private set; }
        
            public System.Collections.Generic.List<System.String> ChannelIds { get; private set; }
        
            public System.Collections.Generic.List<System.String> CollectionIds { get; private set; }
        
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
                target.ChannelIds,
                target.CollectionIds,
                target.Origin,
                target.SearchForwards,
                parsed0,
                parsed1);
        }    
    }
}


