namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;

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
        /// Prevents checking of specific entities so they may be tested in isolation.
        /// </summary>
        /// <remarks>
        /// Any range excluded by this predicate must be included in another test running the exact same Arrange & Act logic.
        /// This ensures the universal set of all entities are tested.
        /// </remarks>
        public Predicate<object> ExcludedFromTest { get; set; }
    }
}