using System;
using System.Linq;

//// Generated on 13/03/2015 19:28:46 (UTC)
//// Mapped solution in 34.59s

namespace Fifthweek.Api.FileManagement.Shared
{
    using System;
    using System.Linq;
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
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<FileId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<FileId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, FileId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override FileId Parse(object value)
            {
                return new FileId((System.Guid)value);
            }
        }
    }
}

namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

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
namespace Fifthweek.Api.FileManagement.Shared
{
    using System;
    using System.Linq;
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
namespace Fifthweek.Api.FileManagement.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class FileInformation 
    {
        public FileInformation(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String containerName,
            System.String uri)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            this.FileId = fileId;
            this.ContainerName = containerName;
            this.Uri = uri;
        }
    }
}

namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

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
namespace Fifthweek.Api.FileManagement.Shared
{
    using System;
    using System.Linq;
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
namespace Fifthweek.Api.FileManagement.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class FileInformation 
    {
        public override string ToString()
        {
            return string.Format("FileInformation({0}, \"{1}\", \"{2}\")", this.FileId == null ? "null" : this.FileId.ToString(), this.ContainerName == null ? "null" : this.ContainerName.ToString(), this.Uri == null ? "null" : this.Uri.ToString());
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
        
            return this.Equals((FileInformation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ContainerName != null ? this.ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Uri != null ? this.Uri.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FileInformation other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ContainerName, other.ContainerName))
            {
                return false;
            }
        
            if (!object.Equals(this.Uri, other.Uri))
            {
                return false;
            }
        
            return true;
        }
    }
}


