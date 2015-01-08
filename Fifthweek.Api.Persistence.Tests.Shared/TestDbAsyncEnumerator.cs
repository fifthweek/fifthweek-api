using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    // See: http://msdn.microsoft.com/en-gb/data/dn314429.aspx
    public class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        public void Dispose()
        {
            this.inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.inner.MoveNext());
        }

        public T Current
        {
            get { return this.inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}