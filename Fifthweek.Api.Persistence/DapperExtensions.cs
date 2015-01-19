namespace Fifthweek.Api.Persistence
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class DapperExtensions
    {
        public static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> self)
        {
            var list = self as List<T>;
            if (list != null)
            {
                return list;
            }

            Trace.TraceWarning("Enumerable needed to be copied into a new list - optimisation could not be performed.");

            return self.ToList();
        }
    }
}