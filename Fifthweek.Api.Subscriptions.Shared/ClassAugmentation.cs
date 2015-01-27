using System;
using System.Linq;



namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class SubscriptionId 
    {
		public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (SubscriptionId)value;
                serializer.Serialize(writer, valueType.Value);
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(SubscriptionId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(SubscriptionId).Name, "objectType");
                }

                var value = serializer.Deserialize<System.Guid>(reader);
                return new SubscriptionId(value);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SubscriptionId);
            }
        }

		public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<SubscriptionId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<SubscriptionId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, SubscriptionId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }

            public override SubscriptionId Parse(object value)
            {
                return new SubscriptionId((System.Guid)value);
            }
        }
    }

}

namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class SubscriptionId 
    {
        public SubscriptionId(
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

namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class SubscriptionId 
    {
        public override string ToString()
        {
            return string.Format("SubscriptionId({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((SubscriptionId)obj);
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

        protected bool Equals(SubscriptionId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    public partial class ValidDescription 
    {
        public override string ToString()
        {
            return string.Format("ValidDescription(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidDescription)obj);
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

        protected bool Equals(ValidDescription other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    public partial class ValidExternalVideoUrl 
    {
        public override string ToString()
        {
            return string.Format("ValidExternalVideoUrl(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidExternalVideoUrl)obj);
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

        protected bool Equals(ValidExternalVideoUrl other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    public partial class ValidIntroduction 
    {
        public override string ToString()
        {
            return string.Format("ValidIntroduction(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidIntroduction)obj);
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

        protected bool Equals(ValidIntroduction other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    public partial class ValidSubscriptionName 
    {
        public override string ToString()
        {
            return string.Format("ValidSubscriptionName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidSubscriptionName)obj);
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

        protected bool Equals(ValidSubscriptionName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    public partial class ValidTagline 
    {
        public override string ToString()
        {
            return string.Format("ValidTagline(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidTagline)obj);
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

        protected bool Equals(ValidTagline other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}

