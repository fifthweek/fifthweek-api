using System;
using System.Linq;

//// Generated on 06/08/2015 09:39:40 (UTC)
//// Mapped solution in 11.98s


namespace Fifthweek.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class FilePurpose 
    {
        public FilePurpose(
            System.String name,
            System.Boolean isPublic)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (isPublic == null)
            {
                throw new ArgumentNullException("isPublic");
            }

            this.Name = name;
            this.IsPublic = isPublic;
        }
    }
}

namespace Fifthweek.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class FilePurpose 
    {
        public override string ToString()
        {
            return string.Format("FilePurpose(\"{0}\", {1})", this.Name == null ? "null" : this.Name.ToString(), this.IsPublic == null ? "null" : this.IsPublic.ToString());
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
        
            return this.Equals((FilePurpose)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsPublic != null ? this.IsPublic.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FilePurpose other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.IsPublic, other.IsPublic))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class PositiveInt 
    {
        public override string ToString()
        {
            return string.Format("PositiveInt({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((PositiveInt)obj);
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
        
        protected bool Equals(PositiveInt other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Shared
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;

    public partial class NonNegativeInt 
    {
        public override string ToString()
        {
            return string.Format("NonNegativeInt({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((NonNegativeInt)obj);
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
        
        protected bool Equals(NonNegativeInt other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}


