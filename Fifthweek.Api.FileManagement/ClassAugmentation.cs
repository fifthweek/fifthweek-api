using System;
using System.Linq;



namespace Fifthweek.Api.FileManagement.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class CompleteFileUploadCommand 
	{
        public CompleteFileUploadCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
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
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Azure;
	using Fifthweek.Api.Core;
	public partial class CompleteFileUploadCommandHandler 
	{
        public CompleteFileUploadCommandHandler(
            Fifthweek.Api.FileManagement.IFileRepository fileRepository, 
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobNameCreator blobNameCreator)
        {
            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobNameCreator == null)
            {
                throw new ArgumentNullException("blobNameCreator");
            }

            this.fileRepository = fileRepository;
            this.blobService = blobService;
            this.blobNameCreator = blobNameCreator;
        }
	}

}
namespace Fifthweek.Api.FileManagement.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class InitiateFileUploadCommand 
	{
        public InitiateFileUploadCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
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
	using System;
	using System.Threading.Tasks;
	using Fifthweek.Api.Azure;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence;
	public partial class InitiateFileUploadCommandHandler 
	{
        public InitiateFileUploadCommandHandler(
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IFileRepository fileRepository)
        {
            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            this.blobService = blobService;
            this.fileRepository = fileRepository;
        }
	}

}
namespace Fifthweek.Api.FileManagement.Controllers
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Http.Description;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.FileManagement.Commands;
	using Fifthweek.Api.FileManagement.Queries;
	using Fifthweek.Api.Identity;
	using Fifthweek.Api.Identity.OAuth;
	using Microsoft.WindowsAzure;
	using Microsoft.WindowsAzure.Storage;
	using Microsoft.WindowsAzure.Storage.Blob;
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
	public partial class GrantedUpload 
	{
        public GrantedUpload(
            System.Guid fileId, 
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
	using Fifthweek.Api.Core;
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
	using System;
	using System.Security;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
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
	using System;
	using Fifthweek.Api.Core;
	public partial class FileVariantId 
	{
        public FileVariantId(
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
namespace Fifthweek.Api.FileManagement.Queries
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class GenerateWritableBlobUriQuery 
	{
        public GenerateWritableBlobUriQuery(
            Fifthweek.Api.Identity.Membership.UserId requester, 
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
namespace Fifthweek.Api.FileManagement.Queries
{
	using System.Security;
	using System.Threading.Tasks;
	using Fifthweek.Api.Azure;
	using Fifthweek.Api.Core;
	public partial class GenerateWritableBlobUriQueryHandler 
	{
        public GenerateWritableBlobUriQueryHandler(
            Fifthweek.Api.Azure.IBlobService blobService, 
            Fifthweek.Api.FileManagement.IBlobNameCreator blobNameCreator, 
            Fifthweek.Api.FileManagement.IFileRepository fileRepository)
        {
            if (blobService == null)
            {
                throw new ArgumentNullException("blobService");
            }

            if (blobNameCreator == null)
            {
                throw new ArgumentNullException("blobNameCreator");
            }

            if (fileRepository == null)
            {
                throw new ArgumentNullException("fileRepository");
            }

            this.blobService = blobService;
            this.blobNameCreator = blobNameCreator;
            this.fileRepository = fileRepository;
        }
	}

}

namespace Fifthweek.Api.FileManagement.Commands
{
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class CompleteFileUploadCommand 
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class InitiateFileUploadCommand 
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
	public partial class GrantedUpload 
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
	public partial class UploadRequest 
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
	using Fifthweek.Api.Core;
	public partial class FileId 
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
namespace Fifthweek.Api.FileManagement
{
	using System;
	using Fifthweek.Api.Core;
	public partial class FileVariantId 
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

            return this.Equals((FileVariantId)obj);
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

        protected bool Equals(FileVariantId other)
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
	public partial class GenerateWritableBlobUriQuery 
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

            return this.Equals((GenerateWritableBlobUriQuery)obj);
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

            return true;
        }
	}

}


