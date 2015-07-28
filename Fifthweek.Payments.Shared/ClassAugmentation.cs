using System;
using System.Linq;

//// Generated on 28/07/2015 11:12:05 (UTC)
//// Mapped solution in 19.18s

namespace Fifthweek.Payments.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Newtonsoft.Json;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class TransactionReference 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (TransactionReference)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(TransactionReference))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(TransactionReference).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new TransactionReference(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(TransactionReference);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<TransactionReference>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<TransactionReference>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, TransactionReference value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override TransactionReference Parse(object value)
            {
                return new TransactionReference((System.Guid)value);
            }
        }
    }
}

namespace Fifthweek.Payments.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Newtonsoft.Json;

    public partial class TransactionReference 
    {
        public TransactionReference(
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

namespace Fifthweek.Payments.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Newtonsoft.Json;

    public partial class TransactionReference 
    {
        public override string ToString()
        {
            return string.Format("TransactionReference({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((TransactionReference)obj);
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
        
        protected bool Equals(TransactionReference other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}


