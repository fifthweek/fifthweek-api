namespace Fifthweek.Api
{
    using System;
    using System.Collections;
    using System.EnterpriseServices;
    using System.Security.Cryptography;
    using System.Web;

    using Autofac.Core.Lifetime;

    using Microsoft.Owin;

    public static class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        public static void SetAccessControlAllowOrigin(IOwinContext owinContext)
        {
            SetAccessControlAllowOrigin(owinContext, null);
        }

        public static void SetAccessControlAllowOrigin(IOwinContext owinContext, string allowedOrigin)
        {
            owinContext.Response.Headers[Constants.AllowedOriginHeaderKey] 
                = allowedOrigin ?? Constants.DefaultAllowedOrigin;
        }

        public static LifetimeScope GetOwinRequestLifetimeScope()
        {
            const string OwinLifetimeScopeKey = "autofac:OwinLifetimeScope";
            var owinContext = HttpContext.Current.Request.GetOwinContext();

            if (owinContext.Environment.ContainsKey(OwinLifetimeScopeKey) == false)
            {
                throw new Exception("Request lifetime scope not found.");
            }

            object requestScopeObject = owinContext.Environment[OwinLifetimeScopeKey];

            if (requestScopeObject == null)
            {
                throw new Exception("Request lifetime scope cannot be null.");
            }

            return (LifetimeScope)requestScopeObject;
        }
    }
}