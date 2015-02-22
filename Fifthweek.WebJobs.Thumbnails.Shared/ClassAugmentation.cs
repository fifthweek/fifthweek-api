using System;
using System.Linq;

//// Generated on 20/02/2015 16:40:32 (UTC)
//// Mapped solution in 7.71s


namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class CreateThumbnailSetMessage 
    {
        public CreateThumbnailSetMessage(
            System.String containerName,
            System.String inputBlobName,
            System.Collections.Generic.List<Fifthweek.WebJobs.Thumbnails.Shared.ThumbnailSetItemMessage> items,
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

            if (overwrite == null)
            {
                throw new ArgumentNullException("overwrite");
            }

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
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class ThumbnailSetItemMessage 
    {
        public ThumbnailSetItemMessage(
            System.String outputBlobName,
            System.Int32 width,
            System.Int32 height,
            Fifthweek.WebJobs.Thumbnails.Shared.ResizeBehaviour resizeBehaviour,
            System.Collections.Generic.List<Fifthweek.WebJobs.Thumbnails.Shared.ThumbnailSetItemMessage> children)
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
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class CreateThumbnailSetMessage 
    {
        public override string ToString()
        {
            return string.Format("CreateThumbnailSetMessage(\"{0}\", \"{1}\", {2}, {3})", this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.InputBlobName == null ? "null" : this.InputBlobName.ToString(), this.Items == null ? "null" : this.Items.ToString(), this.Overwrite == null ? "null" : this.Overwrite.ToString());
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
        
            return this.Equals((CreateThumbnailSetMessage)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
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
        
        protected bool Equals(CreateThumbnailSetMessage other)
        {
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
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class ThumbnailSetItemMessage 
    {
        public override string ToString()
        {
            return string.Format("ThumbnailSetItem(\"{0}\", {1}, {2}, {3}, {4})", this.OutputBlobName == null ? "null" : this.OutputBlobName.ToString(), this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString(), this.Children == null ? "null" : this.Children.ToString());
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
        
            return this.Equals((ThumbnailSetItemMessage)obj);
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
        
        protected bool Equals(ThumbnailSetItemMessage other)
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
namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class ThumbnailSetFileTask 
    {
        public override string ToString()
        {
            return string.Format("ThumbnailSetFileTask({0})", this.Items == null ? "null" : this.Items.ToString());
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
        
            return this.Equals((ThumbnailSetFileTask)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Items != null 
        			? this.Items.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(ThumbnailSetFileTask other)
        {
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
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Fifthweek.WebJobs.Files.Shared;

    public partial class Thumbnail 
    {
        public override string ToString()
        {
            return string.Format("Thumbnail({0}, {1}, {2}, {3})", this.Width == null ? "null" : this.Width.ToString(), this.Height == null ? "null" : this.Height.ToString(), this.ResizeBehaviour == null ? "null" : this.ResizeBehaviour.ToString(), this.Children == null ? "null" : this.Children.ToString());
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
        
            return this.Equals((Thumbnail)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
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
        
        protected bool Equals(Thumbnail other)
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


