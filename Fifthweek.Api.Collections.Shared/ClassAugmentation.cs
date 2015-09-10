using System;
using System.Linq;

//// Generated on 11/03/2015 18:39:30 (UTC)
//// Mapped solution in 3.94s

namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class QueueId 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (QueueId)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(QueueId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(QueueId).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new QueueId(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(QueueId);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<QueueId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<QueueId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, QueueId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override QueueId Parse(object value)
            {
                return new QueueId((System.Guid)value);
            }
        }
    }
}
namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class HourOfWeek 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (HourOfWeek)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(HourOfWeek))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(HourOfWeek).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Int32>(reader);
                return new HourOfWeek(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(HourOfWeek);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<HourOfWeek>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<HourOfWeek>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, HourOfWeek value)
            {
                parameter.DbType = System.Data.DbType.Int32;
                parameter.Value = value.Value;
            }
        
            public override HourOfWeek Parse(object value)
            {
                return new HourOfWeek((System.Int32)value);
            }
        }
    }
}

namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class QueueId 
    {
        public QueueId(
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
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class QueueId 
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
        
            return this.Equals((QueueId)obj);
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
        
        protected bool Equals(QueueId other)
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
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

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
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class ValidQueueName 
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
        
            return this.Equals((ValidQueueName)obj);
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
        
        protected bool Equals(ValidQueueName other)
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
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class WeeklyReleaseSchedule 
    {
        public override string ToString()
        {
            return string.Format("WeeklyReleaseSchedule({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((WeeklyReleaseSchedule)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null 
        			? this.Value.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(WeeklyReleaseSchedule other)
        {
            if (this.Value != null && other.Value != null)
            {
                if (!this.Value.SequenceEqual(other.Value))
                {
                    return false;    
                }
            }
            else if (this.Value != null || other.Value != null)
            {
                return false;
            }
        
            return true;
        }
    }
}


