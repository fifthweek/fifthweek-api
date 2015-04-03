using System;
using System.Linq;

//// Generated on 03/04/2015 14:30:40 (UTC)
//// Mapped solution in 7.99s


namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateThumbnailsMessage 
    {
        public CreateThumbnailsMessage(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String containerName,
            System.String inputBlobName,
            System.Collections.Generic.List<Fifthweek.WebJobs.Thumbnails.Shared.ThumbnailDefinition> items,
            System.Boolean overwrite)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (inputBlobName == null)
            {
                throw new ArgumentNullException("inputBlobName");
            }

            if (overwrite == null)
            {
                throw new ArgumentNullException("overwrite");
            }

            this.FileId = fileId;
            this.ContainerName = containerName;
            this.InputBlobName = inputBlobName;
            this.Items = items;
            this.Overwrite = overwrite;
        }
    }
}
namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ThumbnailDefinition 
    {
        public ThumbnailDefinition(
            System.String outputBlobName,
            System.Int32 width,
            System.Int32 height,
            Fifthweek.WebJobs.Thumbnails.Shared.ResizeBehaviour resizeBehaviour,
            System.Collections.Generic.List<Fifthweek.WebJobs.Thumbnails.Shared.ThumbnailDefinition> children)
        {
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

            this.OutputBlobName = outputBlobName;
            this.Width = width;
            this.Height = height;
            this.ResizeBehaviour = resizeBehaviour;
            this.Children = children;
        }
    }
}

namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreateThumbnailsMessage 
    {
        public override string ToString()
        {
            return string.Format("CreateThumbnailsMessage({0}, \"{1}\", \"{2}\", {3}, {4})", this.FileId == null ? "null" : this.FileId.ToString(), this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.InputBlobName == null ? "null" : this.InputBlobName.ToString(), this.Items == null ? "null" : this.Items.ToString(), this.Overwrite == null ? "null" : this.Overwrite.ToString());
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
        
            return this.Equals((CreateThumbnailsMessage)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.InputBlobName != null ? this.InputBlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Items != null 
        			? this.Items.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.Overwrite != null ? this.Overwrite.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateThumbnailsMessage other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }
        
            if (!object.Equals(this.InputBlobName, other.InputBlobName))
            {
                return false;
            }
        
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
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ThumbnailDefinition 
    {
        public override string ToString()
        {
            return string.Format("ThumbnailDefinition(\"{0}\", {1}, {2}, {3}, {4})", this.OutputBlobName == null ? "null" : this.OutputBlobName.ToString(), this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString(), this.Children == null ? "null" : this.Children.ToString());
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
        
            return this.Equals((ThumbnailDefinition)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.OutputBlobName != null ? this.OutputBlobName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Width != null ? this.Width.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Height != null ? this.Height.GetHashCode() : 0);
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
        
        protected bool Equals(ThumbnailDefinition other)
        {
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


