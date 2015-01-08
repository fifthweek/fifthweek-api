
using System;





namespace Fifthweek.Api.FileManagement.Commands
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class InitiateFileUploadRequestCommand
	{
        public InitiateFileUploadRequestCommand(
            Fifthweek.Api.FileManagement.FileId fileId, 
            Fifthweek.Api.Identity.Membership.UserId userId)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.FileId = fileId;
            this.UserId = userId;
        }
	}

}
namespace Fifthweek.Api.FileManagement.Commands
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class FileUploadCompleteCommand
	{
        public FileUploadCompleteCommand(
            Fifthweek.Api.FileManagement.FileId fileId)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.FileId = fileId;
        }
	}

}
namespace Fifthweek.Api.FileManagement.Queries
{
	using System;
	using Fifthweek.Api.Core;
	public partial class GetSharedAccessSignatureUriQuery
	{
        public GetSharedAccessSignatureUriQuery(
            Fifthweek.Api.FileManagement.FileId fileId)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            this.FileId = fileId;
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
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.InitiateFileUploadRequestCommand> initiateFileUploadRequest, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.FileManagement.Queries.GetSharedAccessSignatureUriQuery,System.String> getSharedAccessSignatureUri, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.FileManagement.Commands.FileUploadCompleteCommand> fileUploadComplete, 
            IUserContext userContext)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (initiateFileUploadRequest == null)
            {
                throw new ArgumentNullException("initiateFileUploadRequest");
            }

            if (getSharedAccessSignatureUri == null)
            {
                throw new ArgumentNullException("getSharedAccessSignatureUri");
            }

            if (fileUploadComplete == null)
            {
                throw new ArgumentNullException("fileUploadComplete");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.guidCreator = guidCreator;
            this.initiateFileUploadRequest = initiateFileUploadRequest;
            this.getSharedAccessSignatureUri = getSharedAccessSignatureUri;
            this.fileUploadComplete = fileUploadComplete;
            this.userContext = userContext;
        }
	}

}

namespace Fifthweek.Api.FileManagement.Commands
{
	using System;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class InitiateFileUploadRequestCommand
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

            return this.Equals((InitiateFileUploadRequestCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(InitiateFileUploadRequestCommand other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
            if (!object.Equals(this.UserId, other.UserId))
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Identity.Membership;
	public partial class FileUploadCompleteCommand
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

            return this.Equals((FileUploadCompleteCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(FileUploadCompleteCommand other)
        {
            if (!object.Equals(this.FileId, other.FileId))
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
	using Fifthweek.Api.Core;
	public partial class GetSharedAccessSignatureUriQuery
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

            return this.Equals((GetSharedAccessSignatureUriQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetSharedAccessSignatureUriQuery other)
        {
            if (!object.Equals(this.FileId, other.FileId))
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
namespace Fifthweek.Api.FileManagement.Controllers
{
	using System;
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
                hashCode = (hashCode * 397) ^ (this.FileSizeBytes != null ? this.FileSizeBytes.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UploadRequest other)
        {
            if (!object.Equals(this.FilePath, other.FilePath))
            {
                return false;
            }
            if (!object.Equals(this.FileSizeBytes, other.FileSizeBytes))
            {
                return false;
            }
            return true;
        }
	}

}

