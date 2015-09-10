using System;
using System.Linq;

//// Generated on 10/09/2015 13:52:14 (UTC)
//// Mapped solution in 19.65s

namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class BlogDescription 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (BlogDescription)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(BlogDescription))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(BlogDescription).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new BlogDescription(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(BlogDescription);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<BlogDescription>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<BlogDescription>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, BlogDescription value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override BlogDescription Parse(object value)
            {
                return new BlogDescription((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class BlogId 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (BlogId)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(BlogId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(BlogId).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new BlogId(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(BlogId);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<BlogId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<BlogId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, BlogId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override BlogId Parse(object value)
            {
                return new BlogId((System.Guid)value);
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class BlogName 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (BlogName)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(BlogName))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(BlogName).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new BlogName(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(BlogName);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<BlogName>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<BlogName>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, BlogName value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override BlogName Parse(object value)
            {
                return new BlogName((System.String)value);
            }
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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

namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class BlogDescription 
    {
        public BlogDescription(
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
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class BlogId 
    {
        public BlogId(
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
namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class BlogName 
    {
        public BlogName(
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
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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

namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class BlogDescription 
    {
        public override string ToString()
        {
            return string.Format("BlogDescription(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((BlogDescription)obj);
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
        
        protected bool Equals(BlogDescription other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class BlogId 
    {
        public override string ToString()
        {
            return string.Format("BlogId({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((BlogId)obj);
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
        
        protected bool Equals(BlogId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class BlogName 
    {
        public override string ToString()
        {
            return string.Format("BlogName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((BlogName)obj);
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
        
        protected bool Equals(BlogName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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
namespace Fifthweek.Api.Blogs.Shared
{
    using System;
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


