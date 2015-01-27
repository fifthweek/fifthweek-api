using System;
using System.Linq;

namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;
    public partial class CreateThumbnailMessage 
    {
        public CreateThumbnailMessage(
            System.String containerName, 
            System.String inputBlobName, 
            System.String outputBlobName, 
            System.Int32 width, 
            System.Int32 height, 
            Fifthweek.WebJobs.Thumbnails.Shared.ResizeBehaviour resizeBehaviour, 
            System.Boolean overwrite)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (inputBlobName == null)
            {
                throw new ArgumentNullException("inputBlobName");
            }

            if (outputBlobName == null)
            {
                throw new ArgumentNullException("outputBlobName");
            }

            if (width == null)
            {
                throw new ArgumentNullException("width");
            }

            if (height == null)
            {
                throw new ArgumentNullException("height");
            }

            if (resizeBehaviour == null)
            {
                throw new ArgumentNullException("resizeBehaviour");
            }

            if (overwrite == null)
            {
                throw new ArgumentNullException("overwrite");
            }

            this.ContainerName = containerName;
            this.InputBlobName = inputBlobName;
            this.OutputBlobName = outputBlobName;
            this.Width = width;
            this.Height = height;
            this.ResizeBehaviour = resizeBehaviour;
            this.Overwrite = overwrite;
        }
    }

}

namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;
    public partial class CreateThumbnailMessage 
    {
        public override string ToString()
        {
            return string.Format("CreateThumbnailMessage(\"{0}\", \"{1}\", \"{2}\", {3}, {4}, {5}, {6})", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.InputBlobName == null ? "null" : this.InputBlobName.ToString(), this.OutputBlobName == null ? "null" : this.OutputBlobName.ToString(), this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString(), this.Overwrite == null ? "null" : this.Overwrite.ToString());
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

            return this.Equals((CreateThumbnailMessage)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.InputBlobName != null ? this.InputBlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OutputBlobName != null ? this.OutputBlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Width != null ? this.Width.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Height != null ? this.Height.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ResizeBehaviour != null ? this.ResizeBehaviour.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Overwrite != null ? this.Overwrite.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(CreateThumbnailMessage other)
        {
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }

            if (!object.Equals(this.InputBlobName, other.InputBlobName))
            {
                return false;
            }

            if (!object.Equals(this.OutputBlobName, other.OutputBlobName))
            {
                return false;
            }

            if (!object.Equals(this.Width, other.Width))
            {
                return false;
            }

            if (!object.Equals(this.Height, other.Height))
            {
                return false;
            }

            if (!object.Equals(this.ResizeBehaviour, other.ResizeBehaviour))
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
namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;
    public partial class ThumbnailFileTask 
    {
        public override string ToString()
        {
            return string.Format("ThumbnailFileTask({0}, {1}, {2})", this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ResizeBehaviour != null ? this.ResizeBehaviour.GetHashCode() : 0);
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

            if (!object.Equals(this.ResizeBehaviour, other.ResizeBehaviour))
            {
                return false;
            }

            return true;
        }
    }

}

