using System;
using System.Collections.Generic;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public class ExpectedSideEffects
    {
        public static readonly ExpectedSideEffects None = new ExpectedSideEffects();

        public IReadOnlyList<object> Inserts { get; set; }
        public IReadOnlyList<object> Updates { get; set; }
        public IReadOnlyList<object> Deletes { get; set; }

        public object Insert
        {
            set { this.Inserts = new[] {value}; }
        }

        public object Update
        {
            set { this.Updates = new[] { value }; }
        }

        public object Delete
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
}