using System;
using System.Linq;



namespace Fifthweek.Webjobs.Thumbnails.Shared
{
	using Fifthweek.Shared;
	using Fifthweek.Webjobs.Files.Shared;
	public partial class ThumbnailFileTask 
	{
        public ThumbnailFileTask(
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

namespace Fifthweek.Webjobs.Thumbnails.Shared
{
	using Fifthweek.Shared;
	using Fifthweek.Webjobs.Files.Shared;
	public partial class ThumbnailFileTask 
	{
		public override string ToString()
        {
			return string.Format("ThumbnailFileTask({0}, {1})", this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString());
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

            return this.Equals((ThumbnailFileTask)obj);
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

        protected bool Equals(ThumbnailFileTask other)
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


