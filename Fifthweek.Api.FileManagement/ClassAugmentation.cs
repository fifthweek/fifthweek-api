using System;
using System.Linq;

//// Generated on 30/05/2015 10:59:38 (UTC)
//// Mapped solution in 8.48s


namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Linq;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class AddNewFileDbStatement 
    {
        public AddNewFileDbStatement(
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
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class CompleteFileUploadCommand 
    {
        public CompleteFileUploadCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
            this.FileId = fileId;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class CompleteFileUploadCommandHandler 
    {
        public CompleteFileUploadCommandHandler(
            Fifthweek.Api.FileManagement.IGetFileWaitingForUploadDbStatement getFileWaitingForUpload,
            Fifthweek.Api.FileManagement.ISetFileUploadCompleteDbStatement setFileUploadComplete,
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap,
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.FileManagement.IFileProcessor fileProcessor,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity)
        {
            if (getFileWaitingForUpload == null)
            {
                throw new ArgumentNullException("getFileWaitingForUpload");
            }

            if (setFileUploadComplete == null)
            {
                throw new ArgumentNullException("setFileUploadComplete");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (fileProcessor == null)
            {
                throw new ArgumentNullException("fileProcessor");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            this.getFileWaitingForUpload = getFileWaitingForUpload;
            this.setFileUploadComplete = setFileUploadComplete;
            this.mimeTypeMap = mimeTypeMap;
            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.requesterSecurity = requesterSecurity;
            this.fileProcessor = fileProcessor;
            this.channelSecurity = channelSecurity;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class InitiateFileUploadCommand 
    {
        public InitiateFileUploadCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String filePath,
            System.String purpose)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
            this.FileId = fileId;
            this.FilePath = filePath;
            this.Purpose = purpose;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class InitiateFileUploadCommandHandler 
    {
        public InitiateFileUploadCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator,
            Fifthweek.Api.FileManagement.IAddNewFileDbStatement addNewFile)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (addNewFile == null)
            {
                throw new ArgumentNullException("addNewFile");
            }

            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.addNewFile = addNewFile;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileUploadController 
    {
        public FileUploadController(
            IGuidCreator guidCreator,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.InitiateFileUploadCommand> initiateFileUpload,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.FileManagement.Queries.GenerateWritableBlobUriQuery,Fifthweek.Api.Azure.BlobSharedAccessInformation> generateWritableBlobUri,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.CompleteFileUploadCommand> completeFileUpload,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (initiateFileUpload == null)
            {
                throw new ArgumentNullException("initiateFileUpload");
            }

            if (generateWritableBlobUri == null)
            {
                throw new ArgumentNullException("generateWritableBlobUri");
            }

            if (completeFileUpload == null)
            {
                throw new ArgumentNullException("completeFileUpload");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.guidCreator = guidCreator;
            this.initiateFileUpload = initiateFileUpload;
            this.generateWritableBlobUri = generateWritableBlobUri;
            this.completeFileUpload = completeFileUpload;
            this.requesterContext = requesterContext;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GrantedUpload 
    {
        public GrantedUpload(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Azure.BlobSharedAccessInformation accessInformation)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (accessInformation == null)
            {
                throw new ArgumentNullException("accessInformation");
            }

            this.FileId = fileId;
            this.AccessInformation = accessInformation;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UploadRequest 
    {
        public UploadRequest(
            System.String channelId,
            System.String filePath,
            System.String purpose)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.ChannelId = channelId;
            this.FilePath = filePath;
            this.Purpose = purpose;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileOwnership 
    {
        public FileOwnership(
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileProcessor 
    {
        public FileProcessor(
            Fifthweek.Api.FileManagement.IFilePurposeTasks filePurposeTasks,
            IQueueService queueService)
        {
            if (filePurposeTasks == null)
            {
                throw new ArgumentNullException("filePurposeTasks");
            }

            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            this.filePurposeTasks = filePurposeTasks;
            this.queueService = queueService;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileSecurity 
    {
        public FileSecurity(
            Fifthweek.Api.FileManagement.IFileOwnership fileOwnership)
        {
            if (fileOwnership == null)
            {
                throw new ArgumentNullException("fileOwnership");
            }

            this.fileOwnership = fileOwnership;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileWaitingForUpload 
    {
        public FileWaitingForUpload(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.String fileNameWithoutExtension,
            System.String fileExtension,
            System.String purpose)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (fileNameWithoutExtension == null)
            {
                throw new ArgumentNullException("fileNameWithoutExtension");
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException("fileExtension");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.FileId = fileId;
            this.UserId = userId;
            this.FileNameWithoutExtension = fileNameWithoutExtension;
            this.FileExtension = fileExtension;
            this.Purpose = purpose;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetFileExtensionDbStatement 
    {
        public GetFileExtensionDbStatement(
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetFileWaitingForUploadDbStatement 
    {
        public GetFileWaitingForUploadDbStatement(
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
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GenerateWritableBlobUriQuery 
    {
        public GenerateWritableBlobUriQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String purpose)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.Requester = requester;
            this.ChannelId = channelId;
            this.FileId = fileId;
            this.Purpose = purpose;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GenerateWritableBlobUriQueryHandler 
    {
        public GenerateWritableBlobUriQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity)
        {
            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.channelSecurity = channelSecurity;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserAccessSignaturesQuery 
    {
        public GetUserAccessSignaturesQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Channels.Shared.ChannelId> creatorChannelIds,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Channels.Shared.ChannelId> subscribedChannelIds)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
            this.CreatorChannelIds = creatorChannelIds;
            this.SubscribedChannelIds = subscribedChannelIds;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserAccessSignaturesQueryHandler 
    {
        public GetUserAccessSignaturesQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService,
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity)
        {
            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.requesterSecurity = requesterSecurity;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UserAccessSignatures
    {
        public partial class PrivateAccessSignature 
        {
            public PrivateAccessSignature(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                Fifthweek.Api.Azure.BlobContainerSharedAccessInformation information)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (information == null)
                {
                    throw new ArgumentNullException("information");
                }

                this.ChannelId = channelId;
                this.Information = information;
            }
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UserAccessSignatures 
    {
        public UserAccessSignatures(
            System.Int32 timeToLiveSeconds,
            Fifthweek.Api.Azure.BlobContainerSharedAccessInformation publicSignature,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.FileManagement.Queries.UserAccessSignatures.PrivateAccessSignature> privateSignatures)
        {
            if (timeToLiveSeconds == null)
            {
                throw new ArgumentNullException("timeToLiveSeconds");
            }

            if (publicSignature == null)
            {
                throw new ArgumentNullException("publicSignature");
            }

            if (privateSignatures == null)
            {
                throw new ArgumentNullException("privateSignatures");
            }

            this.TimeToLiveSeconds = timeToLiveSeconds;
            this.PublicSignature = publicSignature;
            this.PrivateSignatures = privateSignatures;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class ScheduleGarbageCollectionStatement 
    {
        public ScheduleGarbageCollectionStatement(
            IQueueService queueService)
        {
            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            this.queueService = queueService;
        }
    }
}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class SetFileUploadCompleteDbStatement 
    {
        public SetFileUploadCompleteDbStatement(
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Dapper;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class FileInformationAggregator 
    {
        public FileInformationAggregator(
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator)
        {
            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            this.blobLocationGenerator = blobLocationGenerator;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UploadCompleteNotification 
    {
        public UploadCompleteNotification(
            System.String channelId,
            System.String fileId)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.ChannelId = channelId;
            this.FileId = fileId;
        }
    }
}

namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class CompleteFileUploadCommand 
    {
        public override string ToString()
        {
            return string.Format("CompleteFileUploadCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString());
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
        
            return this.Equals((CompleteFileUploadCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CompleteFileUploadCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class InitiateFileUploadCommand 
    {
        public override string ToString()
        {
            return string.Format("InitiateFileUploadCommand({0}, {1}, {2}, \"{3}\", \"{4}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.FilePath == null ? "null" : this.FilePath.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
        
            return this.Equals((InitiateFileUploadCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FilePath != null ? this.FilePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(InitiateFileUploadCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
        
            if (!object.Equals(this.FilePath, other.FilePath))
            {
                return false;
            }
        
            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GrantedUpload 
    {
        public override string ToString()
        {
            return string.Format("GrantedUpload({0}, {1})", this.FileId == null ? "null" : this.FileId.ToString(), this.AccessInformation == null ? "null" : this.AccessInformation.ToString());
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
        
            return this.Equals((GrantedUpload)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccessInformation != null ? this.AccessInformation.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GrantedUpload other)
        {
            if (!object.Equals(this.FileId, other.FileId))
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
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UploadRequest 
    {
        public override string ToString()
        {
            return string.Format("UploadRequest(\"{0}\", \"{1}\", \"{2}\")", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FilePath == null ? "null" : this.FilePath.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
        
            return this.Equals((UploadRequest)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FilePath != null ? this.FilePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UploadRequest other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.FilePath, other.FilePath))
            {
                return false;
            }
        
            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GenerateWritableBlobUriQuery 
    {
        public override string ToString()
        {
            return string.Format("GenerateWritableBlobUriQuery({0}, {1}, {2}, \"{3}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
        
            return this.Equals((GenerateWritableBlobUriQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GenerateWritableBlobUriQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
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
        
            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class GetUserAccessSignaturesQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserAccessSignaturesQuery({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.CreatorChannelIds == null ? "null" : this.CreatorChannelIds.ToString(), this.SubscribedChannelIds == null ? "null" : this.SubscribedChannelIds.ToString());
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
        
            return this.Equals((GetUserAccessSignaturesQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannelIds != null 
        			? this.CreatorChannelIds.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.SubscribedChannelIds != null 
        			? this.SubscribedChannelIds.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(GetUserAccessSignaturesQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }
        
            if (this.CreatorChannelIds != null && other.CreatorChannelIds != null)
            {
                if (!this.CreatorChannelIds.SequenceEqual(other.CreatorChannelIds))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannelIds != null || other.CreatorChannelIds != null)
            {
                return false;
            }
        
            if (this.SubscribedChannelIds != null && other.SubscribedChannelIds != null)
            {
                if (!this.SubscribedChannelIds.SequenceEqual(other.SubscribedChannelIds))
                {
                    return false;    
                }
            }
            else if (this.SubscribedChannelIds != null || other.SubscribedChannelIds != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Channels.Shared;

    public partial class UploadCompleteNotification 
    {
        public override string ToString()
        {
            return string.Format("UploadCompleteNotification(\"{0}\", \"{1}\")", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.FileId == null ? "null" : this.FileId.ToString());
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
        
            return this.Equals((UploadCompleteNotification)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UploadCompleteNotification other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateThumbnailsTask 
    {
        public override string ToString()
        {
            return string.Format("CreateThumbnailsTask({0})", this.Items == null ? "null" : this.Items.ToString());
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
        
            return this.Equals((CreateThumbnailsTask)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Items != null 
        			? this.Items.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(CreateThumbnailsTask other)
        {
            if (this.Items != null && other.Items != null)
            {
                if (!this.Items.SequenceEqual(other.Items))
                {
                    return false;    
                }
            }
            else if (this.Items != null || other.Items != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class Thumbnail 
    {
        public override string ToString()
        {
            return string.Format("Thumbnail({0}, {1}, \"{2}\", {3}, {4})", this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.Alias == null ? "null" : this.Alias.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString(), this.Children == null ? "null" : this.Children.ToString());
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
        
            return this.Equals((Thumbnail)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Width != null ? this.Width.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Height != null ? this.Height.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Alias != null ? this.Alias.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ResizeBehaviour != null ? this.ResizeBehaviour.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Children != null 
        			? this.Children.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(Thumbnail other)
        {
            if (!object.Equals(this.Width, other.Width))
            {
                return false;
            }
        
            if (!object.Equals(this.Height, other.Height))
            {
                return false;
            }
        
            if (!object.Equals(this.Alias, other.Alias))
            {
                return false;
            }
        
            if (!object.Equals(this.ResizeBehaviour, other.ResizeBehaviour))
            {
                return false;
            }
        
            if (this.Children != null && other.Children != null)
            {
                if (!this.Children.SequenceEqual(other.Children))
                {
                    return false;    
                }
            }
            else if (this.Children != null || other.Children != null)
            {
                return false;
            }
        
            return true;
        }
    }
}


