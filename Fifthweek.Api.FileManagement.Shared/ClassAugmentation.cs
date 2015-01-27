using System;
using System.Linq;



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

