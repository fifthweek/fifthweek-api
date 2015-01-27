using System;
using System.Linq;




namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    public partial class AddNewFileDbStatement 
    {
        public AddNewFileDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}
namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class BlobLocation 
    {
        public BlobLocation(
            System.String containerName, 
            System.String blobName)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (blobName == null)
            {
                throw new ArgumentNullException("blobName");
            }

            this.ContainerName = containerName;
            this.BlobName = blobName;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class CompleteFileUploadCommand 
    {
        public CompleteFileUploadCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
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
            this.FileId = fileId;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Files.Shared;
    public partial class CompleteFileUploadCommandHandler 
    {
        public CompleteFileUploadCommandHandler(
            Fifthweek.Api.FileManagement.IGetFileWaitingForUploadDbStatement getFileWaitingForUpload, 
            Fifthweek.Api.FileManagement.ISetFileUploadCompleteDbStatement setFileUploadComplete, 
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap, 
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.Azure.IQueueService queueService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity)
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

            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            this.getFileWaitingForUpload = getFileWaitingForUpload;
            this.setFileUploadComplete = setFileUploadComplete;
            this.mimeTypeMap = mimeTypeMap;
            this.blobService = blobService;
            this.queueService = queueService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.requesterSecurity = requesterSecurity;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class InitiateFileUploadCommand 
    {
        public InitiateFileUploadCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
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
            this.FileId = fileId;
            this.FilePath = filePath;
            this.Purpose = purpose;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class InitiateFileUploadCommandHandler 
    {
        public InitiateFileUploadCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.FileManagement.IAddNewFileDbStatement addNewFile)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
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
            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.addNewFile = addNewFile;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Controllers
{
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
    public partial class FileUploadController 
    {
        public FileUploadController(
            Fifthweek.Api.Core.IGuidCreator guidCreator, 
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
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UploadRequest 
    {
        public UploadRequest(
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

            this.FilePath = filePath;
            this.Purpose = purpose;
        }
    }

}
namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    public partial class FileOwnership 
    {
        public FileOwnership(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}
namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    public partial class GetFileExtensionDbStatement 
    {
        public GetFileExtensionDbStatement(
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    public partial class GetFileWaitingForUploadDbStatement 
    {
        public GetFileWaitingForUploadDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GenerateWritableBlobUriQuery 
    {
        public GenerateWritableBlobUriQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
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
            this.FileId = fileId;
            this.Purpose = purpose;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GenerateWritableBlobUriQueryHandler 
    {
        public GenerateWritableBlobUriQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity, 
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

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GetUserAccessSignaturesQuery 
    {
        public GetUserAccessSignaturesQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester, 
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GetUserAccessSignaturesQueryHandler 
    {
        public GetUserAccessSignaturesQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
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
    using System.Collections.Generic;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class UserAccessSignatures
    {
        public partial class PrivateAccessSignature 
        {
        public PrivateAccessSignature(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId, 
            Fifthweek.Api.Azure.BlobContainerSharedAccessInformation information)
        {
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (information == null)
            {
                throw new ArgumentNullException("information");
            }

            this.CreatorId = creatorId;
            this.Information = information;
        }
        }

        }
}
namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Collections.Generic;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class UserAccessSignatures 
    {
        public UserAccessSignatures(
            Fifthweek.Api.Azure.BlobContainerSharedAccessInformation publicSignature, 
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.FileManagement.Queries.UserAccessSignatures.PrivateAccessSignature> privateSignatures)
        {
            if (publicSignature == null)
            {
                throw new ArgumentNullException("publicSignature");
            }

            if (privateSignatures == null)
            {
                throw new ArgumentNullException("privateSignatures");
            }

            this.PublicSignature = publicSignature;
            this.PrivateSignatures = privateSignatures;
        }
    }

}
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    public partial class ScheduleGarbageCollectionStatement 
    {
        public ScheduleGarbageCollectionStatement(
            Fifthweek.Api.Azure.IQueueService queueService)
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    public partial class SetFileUploadCompleteDbStatement 
    {
        public SetFileUploadCompleteDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}

namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class BlobLocation 
    {
        public override string ToString()
        {
            return string.Format("BlobLocation(\"{0}\", \"{1}\")", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.BlobName == null ? "null" : this.BlobName.ToString());
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

            return this.Equals((BlobLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobName != null ? this.BlobName.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(BlobLocation other)
        {
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }

            if (!object.Equals(this.BlobName, other.BlobName))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class CompleteFileUploadCommand 
    {
        public override string ToString()
        {
            return string.Format("CompleteFileUploadCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.FileId == null ? "null" : this.FileId.ToString());
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
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class InitiateFileUploadCommand 
    {
        public override string ToString()
        {
            return string.Format("InitiateFileUploadCommand({0}, {1}, \"{2}\", \"{3}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.FilePath == null ? "null" : this.FilePath.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class UploadRequest 
    {
        public override string ToString()
        {
            return string.Format("UploadRequest(\"{0}\", \"{1}\")", this.FilePath == null ? "null" : this.FilePath.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
                hashCode = (hashCode * 397) ^ (this.FilePath != null ? this.FilePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UploadRequest other)
        {
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
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GenerateWritableBlobUriQuery 
    {
        public override string ToString()
        {
            return string.Format("GenerateWritableBlobUriQuery({0}, {1}, \"{2}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GetUserAccessSignaturesQuery 
    {
        public override string ToString()
        {
            return string.Format("GetUserAccessSignaturesQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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

            return true;
        }
    }

}

