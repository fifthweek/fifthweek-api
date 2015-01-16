using System;
using System.Linq;



namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
            Fifthweek.Api.Core.Tests.ClassAugmentation.ClassAugmentationDummy.ComplexType someComplexType, 
            Fifthweek.Api.Core.Tests.ClassAugmentation.ClassAugmentationDummy.ComplexType optionalComplexType, 
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	public partial class ConstructedNullableString 
	{
        public ConstructedNullableString(
            System.String value)
        {
            this.Value = value;
        }
	}

}

namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
                hashCode = (hashCode * 397) ^ (this.SomeCollection != null ? this.SomeCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalCollection != null ? this.OptionalCollection.GetHashCode() : 0);
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

            if (!object.Equals(this.SomeCollection, other.SomeCollection))
            {
                return false;
            }

            if (!object.Equals(this.OptionalCollection, other.OptionalCollection))
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	public partial class ClassAugmentationParsingDummy 
	{
		public override string ToString()
        {
			return string.Format("ClassAugmentationParsingDummy({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20})", this.NotStrongTyped == null ? "null" : this.NotStrongTyped.ToString(), this.SomeConstructedNullableString == null ? "null" : this.SomeConstructedNullableString.ToString(), this.OptionalConstructedNullableString == null ? "null" : this.OptionalConstructedNullableString.ToString(), this.SomeConstructedNonNullableString == null ? "null" : this.SomeConstructedNonNullableString.ToString(), this.OptionalConstructedNonNullableString == null ? "null" : this.OptionalConstructedNonNullableString.ToString(), this.SomeParsedString == null ? "null" : this.SomeParsedString.ToString(), this.OptionalParsedString == null ? "null" : this.OptionalParsedString.ToString(), this.SomeConstructedInt == null ? "null" : this.SomeConstructedInt.ToString(), this.OptionalConstructedInt == null ? "null" : this.OptionalConstructedInt.ToString(), this.SomeParsedInt == null ? "null" : this.SomeParsedInt.ToString(), this.OptionalParsedInt == null ? "null" : this.OptionalParsedInt.ToString(), this.SomeConstructedNullableStringObject == null ? "null" : this.SomeConstructedNullableStringObject.ToString(), this.OptionalConstructedNullableStringObject == null ? "null" : this.OptionalConstructedNullableStringObject.ToString(), this.SomeConstructedNonNullableStringObject == null ? "null" : this.SomeConstructedNonNullableStringObject.ToString(), this.OptionalConstructedNonNullableStringObject == null ? "null" : this.OptionalConstructedNonNullableStringObject.ToString(), this.SomeParsedStringObject == null ? "null" : this.SomeParsedStringObject.ToString(), this.OptionalParsedStringObject == null ? "null" : this.OptionalParsedStringObject.ToString(), this.SomeConstructedIntObject == null ? "null" : this.SomeConstructedIntObject.ToString(), this.OptionalConstructedIntObject == null ? "null" : this.OptionalConstructedIntObject.ToString(), this.SomeParsedIntObject == null ? "null" : this.SomeParsedIntObject.ToString(), this.OptionalParsedIntObject == null ? "null" : this.OptionalParsedIntObject.ToString());
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
                hashCode = (hashCode * 397) ^ (this.SomeConstructedNullableString != null ? this.SomeConstructedNullableString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedNullableString != null ? this.OptionalConstructedNullableString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConstructedNonNullableString != null ? this.SomeConstructedNonNullableString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedNonNullableString != null ? this.OptionalConstructedNonNullableString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedString != null ? this.SomeParsedString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedString != null ? this.OptionalParsedString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConstructedInt != null ? this.SomeConstructedInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedInt != null ? this.OptionalConstructedInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedInt != null ? this.SomeParsedInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedInt != null ? this.OptionalParsedInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConstructedNullableStringObject != null ? this.SomeConstructedNullableStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedNullableStringObject != null ? this.OptionalConstructedNullableStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConstructedNonNullableStringObject != null ? this.SomeConstructedNonNullableStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedNonNullableStringObject != null ? this.OptionalConstructedNonNullableStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedStringObject != null ? this.SomeParsedStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedStringObject != null ? this.OptionalParsedStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConstructedIntObject != null ? this.SomeConstructedIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConstructedIntObject != null ? this.OptionalConstructedIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeParsedIntObject != null ? this.SomeParsedIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalParsedIntObject != null ? this.OptionalParsedIntObject.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ClassAugmentationParsingDummy other)
        {
            if (!object.Equals(this.NotStrongTyped, other.NotStrongTyped))
            {
                return false;
            }

            if (!object.Equals(this.SomeConstructedNullableString, other.SomeConstructedNullableString))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedNullableString, other.OptionalConstructedNullableString))
            {
                return false;
            }

            if (!object.Equals(this.SomeConstructedNonNullableString, other.SomeConstructedNonNullableString))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedNonNullableString, other.OptionalConstructedNonNullableString))
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

            if (!object.Equals(this.SomeConstructedInt, other.SomeConstructedInt))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedInt, other.OptionalConstructedInt))
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

            if (!object.Equals(this.SomeConstructedNullableStringObject, other.SomeConstructedNullableStringObject))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedNullableStringObject, other.OptionalConstructedNullableStringObject))
            {
                return false;
            }

            if (!object.Equals(this.SomeConstructedNonNullableStringObject, other.SomeConstructedNonNullableStringObject))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedNonNullableStringObject, other.OptionalConstructedNonNullableStringObject))
            {
                return false;
            }

            if (!object.Equals(this.SomeParsedStringObject, other.SomeParsedStringObject))
            {
                return false;
            }

            if (!object.Equals(this.OptionalParsedStringObject, other.OptionalParsedStringObject))
            {
                return false;
            }

            if (!object.Equals(this.SomeConstructedIntObject, other.SomeConstructedIntObject))
            {
                return false;
            }

            if (!object.Equals(this.OptionalConstructedIntObject, other.OptionalConstructedIntObject))
            {
                return false;
            }

            if (!object.Equals(this.SomeParsedIntObject, other.SomeParsedIntObject))
            {
                return false;
            }

            if (!object.Equals(this.OptionalParsedIntObject, other.OptionalParsedIntObject))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Shared;
	using System.Linq;
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
            public Fifthweek.Api.Core.Tests.ClassAugmentation.ClassAugmentationDummy.ComplexType SomeComplexType { get; set; }
            public Fifthweek.Api.Core.Tests.ClassAugmentation.ClassAugmentationDummy.ComplexType OptionalComplexType { get; set; }
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

namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
	using Fifthweek.Shared;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	public partial class ClassAugmentationParsingDummy 
	{
		public ConstructedNullableString SomeConstructedNullableStringObject { get; set; }
		public ConstructedNullableString OptionalConstructedNullableStringObject { get; set; }
		public ConstructedNonNullableString SomeConstructedNonNullableStringObject { get; set; }
		public ConstructedNonNullableString OptionalConstructedNonNullableStringObject { get; set; }
		public ParsedString SomeParsedStringObject { get; set; }
		public ParsedString OptionalParsedStringObject { get; set; }
		public ConstructedInt SomeConstructedIntObject { get; set; }
		public ConstructedInt OptionalConstructedIntObject { get; set; }
		public ParsedInt SomeParsedIntObject { get; set; }
		public ParsedInt OptionalParsedIntObject { get; set; }
	}

	public static partial class ClassAugmentationParsingDummyExtensions
	{
		public static void Parse(this ClassAugmentationParsingDummy target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			target.SomeConstructedNullableStringObject = new ConstructedNullableString(target.SomeConstructedNullableString);
			target.OptionalConstructedNullableStringObject = new ConstructedNullableString(target.OptionalConstructedNullableString);
		    if (target.SomeConstructedNonNullableString != null)
		    {
                target.SomeConstructedNonNullableStringObject = new ConstructedNonNullableString(target.SomeConstructedNonNullableString);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("SomeConstructedNonNullableString", modelState);
            }

		    if (target.OptionalConstructedNonNullableString != null)
		    {
                target.OptionalConstructedNonNullableStringObject = new ConstructedNonNullableString(target.OptionalConstructedNonNullableString);
		    }
		    else if (false)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("OptionalConstructedNonNullableString", modelState);
            }

			if (true || !ParsedString.IsEmpty(target.SomeParsedString))
			{
				ParsedString @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ParsedString.TryParse(target.SomeParsedString, out @object, out errorMessages))
				{
					target.SomeParsedStringObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("SomeParsedString", modelState);
				}
			}

			if (false || !ParsedString.IsEmpty(target.OptionalParsedString))
			{
				ParsedString @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ParsedString.TryParse(target.OptionalParsedString, out @object, out errorMessages))
				{
					target.OptionalParsedStringObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("OptionalParsedString", modelState);
				}
			}

		    if (target.SomeConstructedInt != null)
		    {
                target.SomeConstructedIntObject = new ConstructedInt(target.SomeConstructedInt);
		    }
		    else if (true)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("SomeConstructedInt", modelState);
            }

		    if (target.OptionalConstructedInt != null)
		    {
                target.OptionalConstructedIntObject = new ConstructedInt(target.OptionalConstructedInt.Value);
		    }
		    else if (false)
		    {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("OptionalConstructedInt", modelState);
            }

			if (true || !ParsedInt.IsEmpty(target.SomeParsedInt))
			{
				ParsedInt @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ParsedInt.TryParse(target.SomeParsedInt, out @object, out errorMessages))
				{
					target.SomeParsedIntObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("SomeParsedInt", modelState);
				}
			}

			if (false || !ParsedInt.IsEmpty(target.OptionalParsedInt))
			{
				ParsedInt @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ParsedInt.TryParse(target.OptionalParsedInt, out @object, out errorMessages))
				{
					target.OptionalParsedIntObject = @object;
				}
				else
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
		}	
	}
}

