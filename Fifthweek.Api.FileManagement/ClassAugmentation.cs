using System;
using System.Linq;



namespace Fifthweek.Api.FileManagement
{
    using System;
    using Fifthweek.CodeGeneration;
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class FileId 
    {
		public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (FileId)value;
                serializer.Serialize(writer, valueType.Value);
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(FileId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(FileId).Name, "objectType");
                }

                var value = serializer.Deserialize<System.Guid>(reader);
                return new FileId(value);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(FileId);
            }
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class CompleteFileUploadCommand 
    {
        public CompleteFileUploadCommand(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.FileManagement.FileId fileId)
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Files.Shared;
    public partial class CompleteFileUploadCommandHandler 
    {
        public CompleteFileUploadCommandHandler(
            Fifthweek.Api.FileManagement.IFileRepository fileRepository, 
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap, 
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.Azure.IQueueService queueService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity)
        {
            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
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

            this.fileRepository = fileRepository;
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class InitiateFileUploadCommand 
    {
        public InitiateFileUploadCommand(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.FileManagement.FileId fileId, 
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class InitiateFileUploadCommandHandler 
    {
        public InitiateFileUploadCommandHandler(
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.FileManagement.IFileRepository fileRepository)
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

            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            this.requesterSecurity = requesterSecurity;
            this.blobService = blobService;
            this.blobLocationGenerator = blobLocationGenerator;
            this.fileRepository = fileRepository;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.CodeGeneration;
    public partial class FileUploadController 
    {
        public FileUploadController(
            Fifthweek.Api.Core.IGuidCreator guidCreator, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.InitiateFileUploadCommand> initiateFileUpload, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.FileManagement.Queries.GenerateWritableBlobUriQuery,System.String> generateWritableBlobUri, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.CompleteFileUploadCommand> completeFileUpload, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext)
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

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.guidCreator = guidCreator;
            this.initiateFileUpload = initiateFileUpload;
            this.generateWritableBlobUri = generateWritableBlobUri;
            this.completeFileUpload = completeFileUpload;
            this.userContext = userContext;
        }
    }

}
namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GrantedUpload 
    {
        public GrantedUpload(
            System.String fileId, 
            System.String uploadUri)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (uploadUri == null)
            {
                throw new ArgumentNullException("uploadUri");
            }

            this.FileId = fileId;
            this.UploadUri = uploadUri;
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
    using System;
    using Fifthweek.CodeGeneration;
    public partial class FileId 
    {
        public FileId(
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
namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
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
    using System;
    using System.Linq;
    using System.Security;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class FileRepository 
    {
        public FileRepository(
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
    using Fifthweek.Api.Identity.Membership;
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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class FileWaitingForUpload 
    {
        public FileWaitingForUpload(
            Fifthweek.Api.FileManagement.FileId fileId, 
            Fifthweek.Api.Identity.Membership.UserId userId, 
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
namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GenerateWritableBlobUriQuery 
    {
        public GenerateWritableBlobUriQuery(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.FileManagement.FileId fileId, 
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GenerateWritableBlobUriQueryHandler 
    {
        public GenerateWritableBlobUriQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobLocationGenerator blobLocationGenerator, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity)
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
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
    using Fifthweek.Api.Identity.Membership;
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
    using Fifthweek.Api.Identity.Membership;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    public partial class GrantedUpload 
    {
        public override string ToString()
        {
            return string.Format("GrantedUpload(\"{0}\", \"{1}\")", this.FileId == null ? "null" : this.FileId.ToString(), this.UploadUri == null ? "null" : this.UploadUri.ToString());
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
                hashCode = (hashCode * 397) ^ (this.UploadUri != null ? this.UploadUri.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GrantedUpload other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }

            if (!object.Equals(this.UploadUri, other.UploadUri))
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
namespace Fifthweek.Api.FileManagement
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class FileId 
    {
        public override string ToString()
        {
            return string.Format("FileId({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((FileId)obj);
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

        protected bool Equals(FileId other)
        {
            if (!object.Equals(this.Value, other.Value))
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
    using Fifthweek.Api.Identity.Membership;
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

