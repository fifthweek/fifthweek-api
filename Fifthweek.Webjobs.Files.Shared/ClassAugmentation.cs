﻿using System;
using System.Linq;



namespace Fifthweek.Webjobs.Files.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.Shared;
    using Fifthweek.CodeGeneration;
    public partial class ProcessFileQueueItem 
    {
        public ProcessFileQueueItem(
            System.String containerName, 
            System.String blobName, 
            System.String purpose, 
            System.Boolean overwrite)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (blobName == null)
            {
                throw new ArgumentNullException("blobName");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            if (overwrite == null)
            {
                throw new ArgumentNullException("overwrite");
            }

            this.ContainerName = containerName;
            this.BlobName = blobName;
            this.Purpose = purpose;
            this.Overwrite = overwrite;
        }
    }

}

namespace Fifthweek.Webjobs.Files.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.Shared;
    using Fifthweek.CodeGeneration;
    public partial class ProcessFileQueueItem 
    {
        public override string ToString()
        {
            return string.Format("ProcessFileQueueItem(\"{0}\", \"{1}\", \"{2}\", {3})", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.BlobName == null ? "null" : this.BlobName.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString(), this.Overwrite == null ? "null" : this.Overwrite.ToString());
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

            return this.Equals((ProcessFileQueueItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobName != null ? this.BlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Overwrite != null ? this.Overwrite.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ProcessFileQueueItem other)
        {
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }

            if (!object.Equals(this.BlobName, other.BlobName))
            {
                return false;
            }

            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }

            if (!object.Equals(this.Overwrite, other.Overwrite))
            {
                return false;
            }

            return true;
        }
    }

}


