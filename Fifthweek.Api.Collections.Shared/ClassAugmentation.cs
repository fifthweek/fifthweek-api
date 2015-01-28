using System;
using System.Linq;



namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class CollectionId 
    {
		public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (CollectionId)value;
                serializer.Serialize(writer, valueType.Value);
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(CollectionId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(CollectionId).Name, "objectType");
                }

                var value = serializer.Deserialize<System.Guid>(reader);
                return new CollectionId(value);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(CollectionId);
            }
        }

		public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<CollectionId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<CollectionId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, CollectionId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }

            public override CollectionId Parse(object value)
            {
                return new CollectionId((System.Guid)value);
            }
        }
    }

}

namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class CollectionId 
    {
        public CollectionId(
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

namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class CollectionId 
    {
        public override string ToString()
        {
            return string.Format("CollectionId({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((CollectionId)obj);
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

        protected bool Equals(CollectionId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    public partial class HourOfWeek 
    {
        public override string ToString()
        {
            return string.Format("HourOfWeek({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((HourOfWeek)obj);
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

        protected bool Equals(HourOfWeek other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    public partial class ValidCollectionName 
    {
        public override string ToString()
        {
            return string.Format("ValidCollectionName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidCollectionName)obj);
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

        protected bool Equals(ValidCollectionName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}

