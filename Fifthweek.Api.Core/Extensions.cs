namespace Fifthweek.Api.Core
{
    using System;
    using System.Web;
    using System.Web.Http.Dependencies;

    public static class Extensions
    {
        public static TResult GetService<TResult>(this IDependencyResolver resolver)
        {
            return (TResult)resolver.GetService(typeof(TResult));
        }

        public static string EncodeGuid(this Guid value)
        {
            return HttpServerUtility.UrlTokenEncode(value.ToByteArray());
        }

        public static Guid DecodeGuid(this string value)
        {
            var result = HttpServerUtility.UrlTokenDecode(value);

            if (result == null || result.Length != 16)
            {
                throw new BadRequestException("Value does not represent an ID: " + value);
            }

            return new Guid(result);
        }
    }
}