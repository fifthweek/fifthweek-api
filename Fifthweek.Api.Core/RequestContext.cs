namespace Fifthweek.Api.Core
{
    using System;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Controllers;

    using Fifthweek.Shared;

    public class RequestContext : IRequestContext
    {
        private bool initialized = false;

        private HttpRequestMessage request;
        private HttpRequestContext context;

        public HttpRequestMessage Request
        {
            get
            {
                if (!this.initialized)
                {
                    throw new InvalidOperationException("The request context has not been initialized");
                }

                return this.request;
            }
        }

        public HttpRequestContext Context
        {
            get
            {
                if (!this.initialized)
                {
                    throw new InvalidOperationException("The request context has not been initialized");
                }

                return this.context;
            }
        }

        public void Initialize(HttpRequestMessage request, HttpRequestContext context)
        {
            if (this.initialized)
            {
                throw new InvalidOperationException("The request context is already initialized.");
            }

            request.AssertNotNull("request");
            context.AssertNotNull("context");

            this.initialized = true;
            this.request = request;
            this.context = context;
        }
    }
}