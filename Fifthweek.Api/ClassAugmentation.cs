using System;
using System.Linq;

//// Generated on 13/02/2015 17:26:18 (UTC)
//// Mapped solution in 9.56s


namespace Fifthweek.Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Microsoft.Owin.Security.OAuth;

    public partial class ExceptionHandler 
    {
        public ExceptionHandler(
            Fifthweek.Api.Core.IRequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            this.requestContext = requestContext;
        }
    }
}



