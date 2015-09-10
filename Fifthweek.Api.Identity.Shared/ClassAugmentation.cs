using System;
using System.Linq;

//// Generated on 10/09/2015 17:19:23 (UTC)
//// Mapped solution in 10.58s

namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class UserId 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (UserId)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(UserId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(UserId).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new UserId(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(UserId);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<UserId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<UserId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, UserId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override UserId Parse(object value)
            {
                return new UserId((System.Guid)value);
            }
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Password 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Password)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Password))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Password).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new Password(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Password);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Password>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Password>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Password value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override Password Parse(object value)
            {
                return new Password((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Email 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Email)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Email))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Email).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new Email(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Email);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Email>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Email>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Email value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override Email Parse(object value)
            {
                return new Email((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Username 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Username)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Username))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Username).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new Username(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Username);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Username>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Username>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Username value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override Username Parse(object value)
            {
                return new Username((System.String)value);
            }
        }
    }
}

namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class UserId 
    {
        public UserId(
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
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Password 
    {
        public Password(
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
namespace Fifthweek.Api.Identity.Shared.Membership.Events
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class UserRegisteredEvent 
    {
        public UserRegisteredEvent(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.UserId = userId;
        }
    }
}

namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class UserId 
    {
        public override string ToString()
        {
            return string.Format("UserId({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((UserId)obj);
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
        
        protected bool Equals(UserId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Password 
    {
        public override string ToString()
        {
            return string.Format("Password(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((Password)obj);
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
        
        protected bool Equals(Password other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Email 
    {
        public override string ToString()
        {
            return string.Format("Email(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((Email)obj);
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
        
        protected bool Equals(Email other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class Username 
    {
        public override string ToString()
        {
            return string.Format("Username(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((Username)obj);
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
        
        protected bool Equals(Username other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Identity.Shared.Membership.Events
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class UserRegisteredEvent 
    {
        public override string ToString()
        {
            return string.Format("UserRegisteredEvent({0})", this.UserId == null ? "null" : this.UserId.ToString());
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
        
            return this.Equals((UserRegisteredEvent)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserRegisteredEvent other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            return true;
        }
    }
}


