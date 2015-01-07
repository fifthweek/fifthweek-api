
using System;






namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
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
            Fifthweek.Api.Core.Tests.ClassAugmentationDummy.ComplexType someComplexType, 
            Fifthweek.Api.Core.Tests.ClassAugmentationDummy.ComplexType optionalComplexType)
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
        }
	}
}
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class UnconditionalString
		{
        public UnconditionalString(
            System.String value)
        {
            this.Value = value;
        }
		}
	}
}
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class UnconditionalInt
		{
        public UnconditionalInt(
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
}

namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationDummy
	{
		public partial class ComplexType
		{
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationDummy
	{
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class UnconditionalString
		{
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

            return this.Equals((UnconditionalString)obj);
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

        protected bool Equals(UnconditionalString other)
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
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
                hashCode = (hashCode * 397) ^ (this.SomeUnconditionalStringObject != null ? this.SomeUnconditionalStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalUnconditionalStringObject != null ? this.OptionalUnconditionalStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConditionalStringObject != null ? this.SomeConditionalStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConditionalStringObject != null ? this.OptionalConditionalStringObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeUnconditionalIntObject != null ? this.SomeUnconditionalIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConditionalIntObject != null ? this.SomeConditionalIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConditionalIntObject != null ? this.OptionalConditionalIntObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NotStrongTyped != null ? this.NotStrongTyped.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeUnconditionalString != null ? this.SomeUnconditionalString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalUnconditionalString != null ? this.OptionalUnconditionalString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConditionalString != null ? this.SomeConditionalString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConditionalString != null ? this.OptionalConditionalString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeUnconditionalInt != null ? this.SomeUnconditionalInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SomeConditionalInt != null ? this.SomeConditionalInt.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OptionalConditionalInt != null ? this.OptionalConditionalInt.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ClassAugmentationParsingDummy other)
        {
            if (!object.Equals(this.SomeUnconditionalStringObject, other.SomeUnconditionalStringObject))
            {
                return false;
            }
            if (!object.Equals(this.OptionalUnconditionalStringObject, other.OptionalUnconditionalStringObject))
            {
                return false;
            }
            if (!object.Equals(this.SomeConditionalStringObject, other.SomeConditionalStringObject))
            {
                return false;
            }
            if (!object.Equals(this.OptionalConditionalStringObject, other.OptionalConditionalStringObject))
            {
                return false;
            }
            if (!object.Equals(this.SomeUnconditionalIntObject, other.SomeUnconditionalIntObject))
            {
                return false;
            }
            if (!object.Equals(this.SomeConditionalIntObject, other.SomeConditionalIntObject))
            {
                return false;
            }
            if (!object.Equals(this.OptionalConditionalIntObject, other.OptionalConditionalIntObject))
            {
                return false;
            }
            if (!object.Equals(this.NotStrongTyped, other.NotStrongTyped))
            {
                return false;
            }
            if (!object.Equals(this.SomeUnconditionalString, other.SomeUnconditionalString))
            {
                return false;
            }
            if (!object.Equals(this.OptionalUnconditionalString, other.OptionalUnconditionalString))
            {
                return false;
            }
            if (!object.Equals(this.SomeConditionalString, other.SomeConditionalString))
            {
                return false;
            }
            if (!object.Equals(this.OptionalConditionalString, other.OptionalConditionalString))
            {
                return false;
            }
            if (!object.Equals(this.SomeUnconditionalInt, other.SomeUnconditionalInt))
            {
                return false;
            }
            if (!object.Equals(this.SomeConditionalInt, other.SomeConditionalInt))
            {
                return false;
            }
            if (!object.Equals(this.OptionalConditionalInt, other.OptionalConditionalInt))
            {
                return false;
            }
            return true;
        }
	}
}
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class UnconditionalInt
		{
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

            return this.Equals((UnconditionalInt)obj);
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

        protected bool Equals(UnconditionalInt other)
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class ConditionalString
		{
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

            return this.Equals((ConditionalString)obj);
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

        protected bool Equals(ConditionalString other)
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
namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public partial class ConditionalInt
		{
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

            return this.Equals((ConditionalInt)obj);
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

        protected bool Equals(ConditionalInt other)
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

namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
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
            public Fifthweek.Api.Core.Tests.ClassAugmentationDummy.ComplexType SomeComplexType { get; set; }
            public Fifthweek.Api.Core.Tests.ClassAugmentationDummy.ComplexType OptionalComplexType { get; set; }

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
					this.OptionalComplexType);
			}
        }
	}
}

namespace Fifthweek.Api.Core.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public partial class ClassAugmentationParsingDummy
	{
		public UnconditionalString SomeUnconditionalStringObject { get; private set; }
		public UnconditionalString OptionalUnconditionalStringObject { get; private set; }
		public ConditionalString SomeConditionalStringObject { get; private set; }
		public ConditionalString OptionalConditionalStringObject { get; private set; }
		public UnconditionalInt SomeUnconditionalIntObject { get; private set; }
		public ConditionalInt SomeConditionalIntObject { get; private set; }
		public ConditionalInt OptionalConditionalIntObject { get; private set; }

		public void Parse()
		{
			var modelState = new System.Web.Http.ModelBinding.ModelStateDictionary();


			if (!modelState.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelState);
			}
		}	
	}
}

