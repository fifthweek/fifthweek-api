using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Moq;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public static class TestDbAsync
    {
        public static void Populate<T>(IEnumerable<T> entities, Mock<IDbSet<T>> dbSet) where T : class
        {
            var queryable = entities.AsQueryable();
            dbSet.As<IQueryable<T>>().Setup(_ => _.Provider).Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            dbSet.As<IQueryable<T>>().Setup(_ => _.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(_ => _.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(_ => _.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.As<IDbAsyncEnumerable<T>>().Setup(_ => _.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));
        } 
    }
}