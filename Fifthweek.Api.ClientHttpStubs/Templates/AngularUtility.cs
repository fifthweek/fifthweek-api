namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System.Linq;

    public static class AngularUtility
    {
        public static string GetRouteBuilder(MethodElement method)
        {
            var route = "'" + method.Route + "'";
            var queryParameters = method.UrlParameters.ToList();
            foreach (var urlParameter in method.UrlParameters)
            {
                foreach (var placeholder in RouteUtility.GetPlaceholders(urlParameter.Name))
                {
                    if (!route.Contains(placeholder))
                    {
                        continue;
                    }

                    queryParameters.Remove(urlParameter);
                    route = route.Replace(placeholder, string.Format("' + encodeURIComponent({0}) + '", urlParameter.Name));
                    break;
                }
            }

            if (route.StartsWith("'' + "))
            {
                route = route.Substring(5);
            }

            if (route.EndsWith(" + ''"))
            {
                route = route.Substring(0, route.Length - 5);
            }

            if (queryParameters.Count > 0)
            {
                if (route.EndsWith("'"))
                {
                    route = route.Substring(0, route.Length - 1);
                    route += "?";
                }
                else
                {
                    route += " + '?";
                }

                var querystring = queryParameters.Select(_ => string.Format("{0}=' + encodeURIComponent({0}) + '", _.Name));
                route += string.Join("&", querystring);

                // Trim trailing " + '"
                route = route.Substring(0, route.Length - 4);
            }

            return route;
        }
    }
}