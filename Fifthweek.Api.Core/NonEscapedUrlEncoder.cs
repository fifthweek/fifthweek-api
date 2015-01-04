using System.Web;

namespace Fifthweek.Api.Core
{
    public static class NonEscapedUrlEncoder
    {
        public static string Encode(string value)
        {
            var encoded = HttpUtility.UrlEncode(value);
            return encoded == null ? null : encoded.Replace("%", "!");
        }

        public static string Decode(string value)
        {
            return HttpUtility.UrlDecode(value.Replace("!", "%"));
        }
    }
}