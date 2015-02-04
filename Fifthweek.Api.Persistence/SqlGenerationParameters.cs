namespace Fifthweek.Api.Persistence
{
    using System.Collections.Generic;

    public class SqlGenerationParameters<T, TFields> where TFields : struct 
    {
        public SqlGenerationParameters(T entity)
        {
            this.Entity = entity;
            this.IdempotentInsert = true;
        }

        public T Entity { get; private set; }

        public TFields? ExcludedFromInput { get; set; }

        public TFields? UpdateMask { get; set; }

        public object AdditionalParameters { get; set; }

        public IReadOnlyList<string> Conditions { get; set; }

        public string Declarations { get; set; }

        public bool IdempotentInsert { get; set; }
    }
}