using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Api.Logging
{
    public static class Extensions
    {
        public static string GetExceptionIdentifier(this Exception t)
        {
            var hashCode = t.ToString().GetHashCode() + "." + DateTime.UtcNow.ToString("yyMMddHHmmss");
            return hashCode;
        }
    }
}
