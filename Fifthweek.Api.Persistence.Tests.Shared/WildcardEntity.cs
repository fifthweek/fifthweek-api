namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public class WildcardEntity<T> : IWildcardEntity where T : IIdentityEquatable
    {
        private readonly T entity;

        public WildcardEntity(T entity)
        {
            if (object.Equals(entity, default(T)))
            {
                throw new ArgumentNullException("entity");
            }

            this.entity = entity;
            this.EntityType = typeof(T);
        }

        public Func<T, T> Expected { get; set; } 

        public bool IdentityEquals(object other)
        {
            return this.entity.IdentityEquals(other);
        }

        public Type EntityType { get; private set; }

        public IIdentityEquatable GetExpectedValue(IIdentityEquatable other)
        {
            if (this.Expected == null)
            {
                throw new Exception("Expected property must be set on wildcard");
            }

            if (!(other is T))
            {
                throw new Exception("Cannot get expected value when actual value is a different type");
            }

            return this.Expected((T)other);
        }
    }
}