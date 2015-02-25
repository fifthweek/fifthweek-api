using System;
using System.Linq;

//// Generated on 25/02/2015 10:11:50 (UTC)
//// Mapped solution in 8.49s

namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class ExternalVideoUrl 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (ExternalVideoUrl)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(ExternalVideoUrl))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(ExternalVideoUrl).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new ExternalVideoUrl(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ExternalVideoUrl);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<ExternalVideoUrl>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<ExternalVideoUrl>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, ExternalVideoUrl value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override ExternalVideoUrl Parse(object value)
            {
                return new ExternalVideoUrl((System.String)value);
            }
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

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Introduction 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Introduction)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Introduction))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Introduction).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new Introduction(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Introduction);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Introduction>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Introduction>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Introduction value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override Introduction Parse(object value)
            {
                return new Introduction((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class SubscriptionDescription 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (SubscriptionDescription)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(SubscriptionDescription))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(SubscriptionDescription).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new SubscriptionDescription(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SubscriptionDescription);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<SubscriptionDescription>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<SubscriptionDescription>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, SubscriptionDescription value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override SubscriptionDescription Parse(object value)
            {
                return new SubscriptionDescription((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class SubscriptionName 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (SubscriptionName)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(SubscriptionName))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(SubscriptionName).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new SubscriptionName(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SubscriptionName);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<SubscriptionName>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<SubscriptionName>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, SubscriptionName value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override SubscriptionName Parse(object value)
            {
                return new SubscriptionName((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Tagline 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Tagline)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Tagline))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Tagline).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new Tagline(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Tagline);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Tagline>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Tagline>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Tagline value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override Tagline Parse(object value)
            {
                return new Tagline((System.String)value);
            }
        }
    }
}

namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class ExternalVideoUrl 
    {
        public ExternalVideoUrl(
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
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Introduction 
    {
        public Introduction(
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
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class SubscriptionDescription 
    {
        public SubscriptionDescription(
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
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class SubscriptionName 
    {
        public SubscriptionName(
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
namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Tagline 
    {
        public Tagline(
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

namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class ExternalVideoUrl 
    {
        public override string ToString()
        {
            return string.Format("ExternalVideoUrl(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ExternalVideoUrl)obj);
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
        
        protected bool Equals(ExternalVideoUrl other)
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

    public partial class Introduction 
    {
        public override string ToString()
        {
            return string.Format("Introduction(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((Introduction)obj);
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
        
        protected bool Equals(Introduction other)
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

    public partial class SubscriptionDescription 
    {
        public override string ToString()
        {
            return string.Format("SubscriptionDescription(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((SubscriptionDescription)obj);
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
        
        protected bool Equals(SubscriptionDescription other)
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

    public partial class SubscriptionName 
    {
        public override string ToString()
        {
            return string.Format("SubscriptionName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((SubscriptionName)obj);
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
        
        protected bool Equals(SubscriptionName other)
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

    public partial class Tagline 
    {
        public override string ToString()
        {
            return string.Format("Tagline(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((Tagline)obj);
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
        
        protected bool Equals(Tagline other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}


