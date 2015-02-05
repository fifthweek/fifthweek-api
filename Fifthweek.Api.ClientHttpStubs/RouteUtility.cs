namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;

    public static class RouteUtility
    {
        public static IEnumerable<string> GetPlaceholders(string parameterName)
        {
            yield return "{" + parameterName + "}";
            yield return "{*" + parameterName + "}";
        }
    }
}