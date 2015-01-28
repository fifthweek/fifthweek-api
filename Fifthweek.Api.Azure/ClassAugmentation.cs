using System;
using System.Linq;




namespace Fifthweek.Api.Azure
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class BlobContainerSharedAccessInformation 
    {
        public BlobContainerSharedAccessInformation(
            System.String containerName,
            System.String uri,
            System.String signature,
            System.DateTime expiry)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }

            if (expiry == null)
            {
                throw new ArgumentNullException("expiry");
            }

            this.ContainerName = containerName;
            this.Uri = uri;
            this.Signature = signature;
            this.Expiry = expiry;
        }
    }
}
namespace Fifthweek.Api.Azure
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class BlobSharedAccessInformation 
    {
        public BlobSharedAccessInformation(
            System.String containerName,
            System.String blobName,
            System.String uri,
            System.String signature,
            System.DateTime expiry)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (blobName == null)
            {
                throw new ArgumentNullException("blobName");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }

            if (expiry == null)
            {
                throw new ArgumentNullException("expiry");
            }

            this.ContainerName = containerName;
            this.BlobName = blobName;
            this.Uri = uri;
            this.Signature = signature;
            this.Expiry = expiry;
        }
    }
}

namespace Fifthweek.Api.Azure
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class BlobContainerSharedAccessInformation 
    {
        public override string ToString()
        {
            return string.Format("BlobContainerSharedAccessInformation(\"{0}\", \"{1}\", \"{2}\", {3})", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.Uri == null ? "null" : this.Uri.ToString(), this.Signature == null ? "null" : this.Signature.ToString(), this.Expiry == null ? "null" : this.Expiry.ToString());
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
        
            return this.Equals((BlobContainerSharedAccessInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Uri != null ? this.Uri.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Signature != null ? this.Signature.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Expiry != null ? this.Expiry.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlobContainerSharedAccessInformation other)
        {
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }
        
            if (!object.Equals(this.Uri, other.Uri))
            {
                return false;
            }
        
            if (!object.Equals(this.Signature, other.Signature))
            {
                return false;
            }
        
            if (!object.Equals(this.Expiry, other.Expiry))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Azure
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class BlobSharedAccessInformation 
    {
        public override string ToString()
        {
            return string.Format("BlobSharedAccessInformation(\"{0}\", \"{1}\", \"{2}\", \"{3}\", {4})", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.BlobName == null ? "null" : this.BlobName.ToString(), this.Uri == null ? "null" : this.Uri.ToString(), this.Signature == null ? "null" : this.Signature.ToString(), this.Expiry == null ? "null" : this.Expiry.ToString());
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
        
            return this.Equals((BlobSharedAccessInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobName != null ? this.BlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Uri != null ? this.Uri.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Signature != null ? this.Signature.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Expiry != null ? this.Expiry.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(BlobSharedAccessInformation other)
        {
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }
        
            if (!object.Equals(this.BlobName, other.BlobName))
            {
                return false;
            }
        
            if (!object.Equals(this.Uri, other.Uri))
            {
                return false;
            }
        
            if (!object.Equals(this.Signature, other.Signature))
            {
                return false;
            }
        
            if (!object.Equals(this.Expiry, other.Expiry))
            {
                return false;
            }
        
            return true;
        }
    }
}

