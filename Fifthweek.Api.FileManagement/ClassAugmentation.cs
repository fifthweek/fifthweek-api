

























using System;


namespace Fifthweek.Api.FileManagement
{
    public partial class FileId
    {
        public FileId(

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

namespace Fifthweek.Api.FileManagement
{
    public partial class FileVariantId
    {
        public FileVariantId(

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








namespace Fifthweek.Api.FileManagement
{
    public partial class FileId
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

            return this.Equals((FileId)obj);
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

        protected bool Equals(FileId other)
        {

            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }
}

namespace Fifthweek.Api.FileManagement
{
    public partial class FileVariantId
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

            return this.Equals((FileVariantId)obj);
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

        protected bool Equals(FileVariantId other)
        {

            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }
}

