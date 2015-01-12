using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public class ExpectedSideEffects
    {
        public static readonly ExpectedSideEffects None = new ExpectedSideEffects();

        public IReadOnlyList<IIdentityEquatable> Inserts { get; set; }
        public IReadOnlyList<IIdentityEquatable> Updates { get; set; }
        public IReadOnlyList<IIdentityEquatable> Deletes { get; set; }

        public IIdentityEquatable Insert
        {
            set { this.Inserts = new[] {value}; }
        }

        public IIdentityEquatable Update
        {
            set { this.Updates = new[] { value }; }
        }

        public IIdentityEquatable Delete
        {
            set { this.Deletes = new[] { value }; }
        }

        /// <summary>
        /// Prevents checking of specific entities so they may be tested in separate tests.
        /// </summary>
        /// <remarks>
        /// Any range excluded by this predicate must be included in another test running the exact same logic.
        /// This ensures the universal set of all entities are tested.
        /// </remarks>
        public Predicate<object> ExcludedFromTest { get; set; }
    }

    public class WildcardEntity<T> : IWildcardEntity where T : IIdentityEquatable
    {
        private readonly T entity;

        public WildcardEntity(T entity)
        {
            if (Equals(entity, default(T)))
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

    public interface IWildcardEntity : IIdentityEquatable
    {
        bool WildcardEquals(object other);
    }
}