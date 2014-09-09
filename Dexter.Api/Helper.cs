namespace Dexter.Api
{
    using System;
    using System.Security.Cryptography;

    using Microsoft.Owin;

    public class Helper
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
    }
}