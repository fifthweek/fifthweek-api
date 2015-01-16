using System;
using System.Linq;



namespace Fifthweek.Webjobs.Files.Shared
{
	using Fifthweek.Shared;
	public partial class ProcessFileQueueItem 
	{
        public ProcessFileQueueItem(
            System.String purpose, 
            System.String blobLocation, 
            System.Boolean overwrite)
        {
            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            if (blobLocation == null)
            {
                throw new ArgumentNullException("blobLocation");
            }

            if (overwrite == null)
            {
                throw new ArgumentNullException("overwrite");
            }

            this.Purpose = purpose;
            this.BlobLocation = blobLocation;
            this.Overwrite = overwrite;
        }
	}

}

namespace Fifthweek.Webjobs.Files.Shared
{
	using Fifthweek.Shared;
	public partial class ProcessFileQueueItem 
	{
		public override string ToString()
        {
			return string.Format("ProcessFileQueueItem(\"{0}\", \"{1}\", {2})", this.Purpose == null ? "null" : this.Purpose.ToString(), this.BlobLocation == null ? "null" : this.BlobLocation.ToString(), this.Overwrite == null ? "null" : this.Overwrite.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobLocation != null ? this.BlobLocation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Overwrite != null ? this.Overwrite.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ProcessFileQueueItem other)
        {
            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }

            if (!object.Equals(this.BlobLocation, other.BlobLocation))
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


