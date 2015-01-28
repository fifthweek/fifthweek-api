using System;
using System.Linq;



namespace Fifthweek.CodeGeneration.Tests
{
    using System;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class PrimitiveGuidDummy 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (PrimitiveGuidDummy)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(PrimitiveGuidDummy))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(PrimitiveGuidDummy).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Guid>(reader);
                return new PrimitiveGuidDummy(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PrimitiveGuidDummy);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<PrimitiveGuidDummy>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<PrimitiveGuidDummy>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, PrimitiveGuidDummy value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }
        
            public override PrimitiveGuidDummy Parse(object value)
            {
                return new PrimitiveGuidDummy((System.Guid)value);
            }
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class PrimitiveIntDummy 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (PrimitiveIntDummy)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(PrimitiveIntDummy))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(PrimitiveIntDummy).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Int32>(reader);
                return new PrimitiveIntDummy(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PrimitiveIntDummy);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<PrimitiveIntDummy>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<PrimitiveIntDummy>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, PrimitiveIntDummy value)
            {
                parameter.DbType = System.Data.DbType.Int32;
                parameter.Value = value.Value;
            }
        
            public override PrimitiveIntDummy Parse(object value)
            {
                return new PrimitiveIntDummy((System.Int32)value);
            }
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class PrimitiveStringDummy 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (PrimitiveStringDummy)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(PrimitiveStringDummy))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(PrimitiveStringDummy).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.String>(reader);
                return new PrimitiveStringDummy(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PrimitiveStringDummy);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<PrimitiveStringDummy>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<PrimitiveStringDummy>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, PrimitiveStringDummy value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }
        
            public override PrimitiveStringDummy Parse(object value)
            {
                return new PrimitiveStringDummy((System.String)value);
            }
        }
    }
}

namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ClassAugmentationDummy
    {
        public partial class ComplexType 
        {
            public ComplexType(
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
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ClassAugmentationDummy 
    {
        public ClassAugmentationDummy(
            System.Guid someGuid,
            System.Int32 someInt,
            System.Nullable<System.Guid> optionalGuid,
            System.Nullable<System.Int32> optionalInt,
            System.String someString,
            System.String optionalString,
            System.Collections.Generic.IEnumerable<System.String> someCollection,
            System.Collections.Generic.IEnumerable<System.String> optionalCollection,
            Fifthweek.CodeGeneration.Tests.ClassAugmentationDummy.ComplexType someComplexType,
            Fifthweek.CodeGeneration.Tests.ClassAugmentationDummy.ComplexType optionalComplexType,
            System.String someStringField,
            System.String optionalStringField)
        {
            if (someGuid == null)
            {
                throw new ArgumentNullException("someGuid");
            }

            if (someInt == null)
            {
                throw new ArgumentNullException("someInt");
            }

            if (someString == null)
            {
                throw new ArgumentNullException("someString");
            }

            if (someCollection == null)
            {
                throw new ArgumentNullException("someCollection");
            }

            if (someComplexType == null)
            {
                throw new ArgumentNullException("someComplexType");
            }

            if (someStringField == null)
            {
                throw new ArgumentNullException("someStringField");
            }

            this.SomeGuid = someGuid;
            this.SomeInt = someInt;
            this.OptionalGuid = optionalGuid;
            this.OptionalInt = optionalInt;
            this.SomeString = someString;
            this.OptionalString = optionalString;
            this.SomeCollection = someCollection;
            this.OptionalCollection = optionalCollection;
            this.SomeComplexType = someComplexType;
            this.OptionalComplexType = optionalComplexType;
            this.someStringField = someStringField;
            this.optionalStringField = optionalStringField;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedInt 
    {
        public ConstructedInt(
            System.Int32 value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedNonNullableString 
    {
        public ConstructedNonNullableString(
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
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedNullableString 
    {
        public ConstructedNullableString(
            System.String value)
        {
            this.Value = value;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;

    public partial class PrimitiveGuidDummy 
    {
        public PrimitiveGuidDummy(
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
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class PrimitiveIntDummy 
    {
        public PrimitiveIntDummy(
            System.Int32 value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class PrimitiveStringDummy 
    {
        public PrimitiveStringDummy(
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

namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ClassAugmentationDummy
    {
        public partial class ComplexType 
        {
            public override string ToString()
            {
                return string.Format("ComplexType(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
            
                return this.Equals((ComplexType)obj);
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
            
            protected bool Equals(ComplexType other)
            {
                if (!object.Equals(this.Value, other.Value))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ClassAugmentationDummy 
    {
        public override string ToString()
        {
            return string.Format("ClassAugmentationDummy({0}, {1}, {2}, {3}, \"{4}\", \"{5}\", {6}, {7}, {8}, {9})", this.SomeGuid == null ? "null" : this.SomeGuid.ToString(), this.SomeInt == null ? "null" : this.SomeInt.ToString(), this.OptionalGuid == null ? "null" : this.OptionalGuid.ToString(), this.OptionalInt == null ? "null" : this.OptionalInt.ToString(), this.SomeString == null ? "null" : this.SomeString.ToString(), this.OptionalString == null ? "null" : this.OptionalString.ToString(), this.SomeCollection == null ? "null" : this.SomeCollection.ToString(), this.OptionalCollection == null ? "null" : this.OptionalCollection.ToString(), this.SomeComplexType == null ? "null" : this.SomeComplexType.ToString(), this.OptionalComplexType == null ? "null" : this.OptionalComplexType.ToString());
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
        
            return this.Equals((ClassAugmentationDummy)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SomeGuid != null ? this.SomeGuid.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeInt != null ? this.SomeInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalGuid != null ? this.OptionalGuid.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalInt != null ? this.OptionalInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeString != null ? this.SomeString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalString != null ? this.OptionalString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeCollection != null 
        			? this.SomeCollection.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.OptionalCollection != null 
        			? this.OptionalCollection.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.SomeComplexType != null ? this.SomeComplexType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalComplexType != null ? this.OptionalComplexType.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ClassAugmentationDummy other)
        {
            if (!object.Equals(this.SomeGuid, other.SomeGuid))
            {
                return false;
            }
        
            if (!object.Equals(this.SomeInt, other.SomeInt))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalGuid, other.OptionalGuid))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalInt, other.OptionalInt))
            {
                return false;
            }
        
            if (!object.Equals(this.SomeString, other.SomeString))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalString, other.OptionalString))
            {
                return false;
            }
        
            if (this.SomeCollection != null && other.SomeCollection != null)
            {
                if (!this.SomeCollection.SequenceEqual(other.SomeCollection))
                {
                    return false;    
                }
            }
            else if (this.SomeCollection != null || other.SomeCollection != null)
            {
                return false;
            }
        
            if (this.OptionalCollection != null && other.OptionalCollection != null)
            {
                if (!this.OptionalCollection.SequenceEqual(other.OptionalCollection))
                {
                    return false;    
                }
            }
            else if (this.OptionalCollection != null || other.OptionalCollection != null)
            {
                return false;
            }
        
            if (!object.Equals(this.SomeComplexType, other.SomeComplexType))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalComplexType, other.OptionalComplexType))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ClassAugmentationParsingDummy 
    {
        public override string ToString()
        {
            return string.Format("ClassAugmentationParsingDummy({0}, \"{1}\", \"{2}\", {3}, {4})", this.NotStrongTyped == null ? "null" : this.NotStrongTyped.ToString(), this.SomeParsedString == null ? "null" : this.SomeParsedString.ToString(), this.OptionalParsedString == null ? "null" : this.OptionalParsedString.ToString(), this.SomeParsedInt == null ? "null" : this.SomeParsedInt.ToString(), this.OptionalParsedInt == null ? "null" : this.OptionalParsedInt.ToString());
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
        
            return this.Equals((ClassAugmentationParsingDummy)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.NotStrongTyped != null ? this.NotStrongTyped.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedString != null ? this.SomeParsedString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedString != null ? this.OptionalParsedString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedInt != null ? this.SomeParsedInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedInt != null ? this.OptionalParsedInt.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ClassAugmentationParsingDummy other)
        {
            if (!object.Equals(this.NotStrongTyped, other.NotStrongTyped))
            {
                return false;
            }
        
            if (!object.Equals(this.SomeParsedString, other.SomeParsedString))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalParsedString, other.OptionalParsedString))
            {
                return false;
            }
        
            if (!object.Equals(this.SomeParsedInt, other.SomeParsedInt))
            {
                return false;
            }
        
            if (!object.Equals(this.OptionalParsedInt, other.OptionalParsedInt))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedInt 
    {
        public override string ToString()
        {
            return string.Format("ConstructedInt({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ConstructedInt)obj);
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
        
        protected bool Equals(ConstructedInt other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedNonNullableString 
    {
        public override string ToString()
        {
            return string.Format("ConstructedNonNullableString(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ConstructedNonNullableString)obj);
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
        
        protected bool Equals(ConstructedNonNullableString other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ConstructedNullableString 
    {
        public override string ToString()
        {
            return string.Format("ConstructedNullableString(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ConstructedNullableString)obj);
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
        
        protected bool Equals(ConstructedNullableString other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ParsedInt 
    {
        public override string ToString()
        {
            return string.Format("ParsedInt({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ParsedInt)obj);
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
        
        protected bool Equals(ParsedInt other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ParsedString 
    {
        public override string ToString()
        {
            return string.Format("ParsedString(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ParsedString)obj);
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
        
        protected bool Equals(ParsedString other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;

    public partial class PrimitiveGuidDummy 
    {
        public override string ToString()
        {
            return string.Format("PrimitiveGuidDummy({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((PrimitiveGuidDummy)obj);
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
        
        protected bool Equals(PrimitiveGuidDummy other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class PrimitiveIntDummy 
    {
        public override string ToString()
        {
            return string.Format("PrimitiveIntDummy({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((PrimitiveIntDummy)obj);
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
        
        protected bool Equals(PrimitiveIntDummy other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class PrimitiveStringDummy 
    {
        public override string ToString()
        {
            return string.Format("PrimitiveStringDummy(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((PrimitiveStringDummy)obj);
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
        
        protected bool Equals(PrimitiveStringDummy other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;

    public partial class ClassAugmentationDummy 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.SomeGuid = this.SomeGuid;
            builder.SomeInt = this.SomeInt;
            builder.OptionalGuid = this.OptionalGuid;
            builder.OptionalInt = this.OptionalInt;
            builder.SomeString = this.SomeString;
            builder.OptionalString = this.OptionalString;
            builder.SomeCollection = this.SomeCollection;
            builder.OptionalCollection = this.OptionalCollection;
            builder.SomeComplexType = this.SomeComplexType;
            builder.OptionalComplexType = this.OptionalComplexType;
            builder.someStringField = this.someStringField;
            builder.optionalStringField = this.optionalStringField;
            return builder;
        }
        
        public ClassAugmentationDummy Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }
        
        public partial class Builder
        {
            public System.Guid SomeGuid { get; set; }
            public System.Int32 SomeInt { get; set; }
            public System.Nullable<System.Guid> OptionalGuid { get; set; }
            public System.Nullable<System.Int32> OptionalInt { get; set; }
            public System.String SomeString { get; set; }
            public System.String OptionalString { get; set; }
            public System.Collections.Generic.IEnumerable<System.String> SomeCollection { get; set; }
            public System.Collections.Generic.IEnumerable<System.String> OptionalCollection { get; set; }
            public Fifthweek.CodeGeneration.Tests.ClassAugmentationDummy.ComplexType SomeComplexType { get; set; }
            public Fifthweek.CodeGeneration.Tests.ClassAugmentationDummy.ComplexType OptionalComplexType { get; set; }
            public System.String someStringField { get; set; }
            public System.String optionalStringField { get; set; }
        
            public ClassAugmentationDummy Build()
            {
                return new ClassAugmentationDummy(
                    this.SomeGuid, 
                    this.SomeInt, 
                    this.OptionalGuid, 
                    this.OptionalInt, 
                    this.SomeString, 
                    this.OptionalString, 
                    this.SomeCollection, 
                    this.OptionalCollection, 
                    this.SomeComplexType, 
                    this.OptionalComplexType, 
                    this.someStringField, 
                    this.optionalStringField);
            }
        }
    }
}
namespace Fifthweek.CodeGeneration.Tests
{

    public partial class ClassAugmentationParsingDummy 
    {
        public class Parsed
        {
            public Parsed(
                System.Int32 notStrongTyped,
                ParsedString someParsedString,
                ParsedString optionalParsedString,
                ParsedInt someParsedInt,
                ParsedInt optionalParsedInt)
            {
                if (notStrongTyped == null)
                {
                    throw new ArgumentNullException("notStrongTyped");
                }

                if (someParsedString == null)
                {
                    throw new ArgumentNullException("someParsedString");
                }

                if (someParsedInt == null)
                {
                    throw new ArgumentNullException("someParsedInt");
                }

                this.NotStrongTyped = notStrongTyped;
                this.SomeParsedString = someParsedString;
                this.OptionalParsedString = optionalParsedString;
                this.SomeParsedInt = someParsedInt;
                this.OptionalParsedInt = optionalParsedInt;
            }
        
        	public System.Int32 NotStrongTyped { get; private set; }
        
        	public ParsedString SomeParsedString { get; private set; }
        
        	public ParsedString OptionalParsedString { get; private set; }
        
        	public ParsedInt SomeParsedInt { get; private set; }
        
        	public ParsedInt OptionalParsedInt { get; private set; }
        }
    }

    public static partial class ClassAugmentationParsingDummyExtensions
    {
        public static ClassAugmentationParsingDummy.Parsed Parse(this ClassAugmentationParsingDummy target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
        	ParsedString parsed0 = null;
            if (true || !ParsedString.IsEmpty(target.SomeParsedString))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ParsedString.TryParse(target.SomeParsedString, out parsed0, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("SomeParsedString", modelState);
                }
            }
        
        	ParsedString parsed1 = null;
            if (false || !ParsedString.IsEmpty(target.OptionalParsedString))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ParsedString.TryParse(target.OptionalParsedString, out parsed1, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("OptionalParsedString", modelState);
                }
            }
        
        	ParsedInt parsed2 = null;
            if (true || !ParsedInt.IsEmpty(target.SomeParsedInt))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ParsedInt.TryParse(target.SomeParsedInt, out parsed2, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("SomeParsedInt", modelState);
                }
            }
        
        	ParsedInt parsed3 = null;
            if (false || !ParsedInt.IsEmpty(target.OptionalParsedInt))
            {
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (!ParsedInt.TryParse(target.OptionalParsedInt, out parsed3, out errorMessages))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }
        
                    modelStateDictionary.Add("OptionalParsedInt", modelState);
                }
            }
        
            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
        	return new ClassAugmentationParsingDummy.Parsed(
                target.NotStrongTyped,
                parsed0,
                parsed1,
                parsed2,
                parsed3);
        }    
    }
}

