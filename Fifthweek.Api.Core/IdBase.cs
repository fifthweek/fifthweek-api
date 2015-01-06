namespace Fifthweek.Api.Core
{
    using System;

    public abstract class IdBase<T>
    {
        protected IdBase()
        {
        }

        protected IdBase(T value)
        {
            this.Value = value;
        }

        public T Value { get; protected set; }

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

            return object.Equals(this.Value, ((IdBase<T>)obj).Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}