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
        }

        public Predicate<T> AreEqual { get; set; } 

        public bool IdentityEquals(object other)
        {
            return this.entity.IdentityEquals(other);
        }

        public bool WildcardEquals(object other)
        {
            if (this.AreEqual == null || !(other is T))
            {
                return false;
            }

            return this.AreEqual((T)other);
        }
    }
}