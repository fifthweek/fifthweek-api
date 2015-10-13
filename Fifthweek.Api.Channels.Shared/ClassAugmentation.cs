using System;
using System.Linq;

//// Generated on 12/10/2015 13:48:00 (UTC)
//// Mapped solution in 46.95s

namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class ChannelId 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (ChannelId)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(ChannelId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(ChannelId).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new ChannelId(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ChannelId);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<ChannelId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<ChannelId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, ChannelId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override ChannelId Parse(object value)
            {
                return new ChannelId((System.Guid)value);
            }
        }
    }
}
namespace Fifthweek.Api.Channels.Shared
{
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class ChannelName 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (ChannelName)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(ChannelName))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(ChannelName).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new ChannelName(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ChannelName);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<ChannelName>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<ChannelName>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, ChannelName value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override ChannelName Parse(object value)
            {
                return new ChannelName((System.String)value);
            }
        }
    }
}

namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public partial class ChannelId 
    {
        public ChannelId(
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
namespace Fifthweek.Api.Channels.Shared
{
    using Fifthweek.CodeGeneration;

    public partial class ChannelName 
    {
        public ChannelName(
            System.String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}

namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public partial class ChannelId 
    {
        public override string ToString()
        {
            return string.Format("ChannelId({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ChannelId)obj);
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
        
        protected bool Equals(ChannelId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Shared
{
    using Fifthweek.CodeGeneration;

    public partial class ChannelName 
    {
        public override string ToString()
        {
            return string.Format("ChannelName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ChannelName)obj);
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
        
        protected bool Equals(ChannelName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public partial class ValidAcceptedChannelPrice 
    {
        public override string ToString()
        {
            return string.Format("ValidAcceptedChannelPrice({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidAcceptedChannelPrice)obj);
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
        
        protected bool Equals(ValidAcceptedChannelPrice other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public partial class ValidChannelPrice 
    {
        public override string ToString()
        {
            return string.Format("ValidChannelPrice({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidChannelPrice)obj);
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
        
        protected bool Equals(ValidChannelPrice other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}


