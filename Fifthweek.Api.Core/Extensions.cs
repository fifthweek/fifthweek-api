namespace Fifthweek.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public static void AssertBodyProvided(this object value, string argumentName)
        {
            if (value == null)
            {
                throw new BadRequestException("The value '" + argumentName + "' must be provided in the request body.");
            }
        }

        public static void AssertUrlParameterProvided(this object value, string argumentName)
        {
            if (value == null)
            {
                throw new BadRequestException("The value '" + argumentName + "' must be provided in the request URL.");
            }
        }

        public static void AssertUrlParameterProvided(this string value, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BadRequestException("The value '" + argumentName + "' must be provided in the request URL.");
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> value)
        {
            if (value == null)
            {
                return Enumerable.Empty<T>();
            }

            return value;
        }
    }
}