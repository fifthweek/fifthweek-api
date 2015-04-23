namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System.Linq;

    using Fifthweek.Shared;

    using Humanizer;

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
                    route += "?'";
                }
                else
                {
                    route += " + '?'";
                }

                foreach (var parameter in queryParameters)
                {
                    if (parameter.Type.IsPrimitiveEx())
                    {
                        route += string.Format(
                            " + ({0} === undefined ? '' : '{0}=' + encodeURIComponent({0}) + '&')",
                            parameter.Name);
                    }
                    else
                    {
                        // Flatten complex types used as URL parameters.
                        var flattenedComplexType = parameter.Type.GetProperties().Where(_ => _.PropertyType.IsPrimitiveEx() && _.CanWrite);
                        foreach (var property in flattenedComplexType)
                        {
                            route += string.Format(
                                " + ({0} === undefined ? '' : '{1}=' + encodeURIComponent({0}) + '&')",
                                parameter.Name + "." + property.Name.Camelize(),
                                property.Name.Camelize());
                        }
                    }
                }
            }

            return route;
        }
    }
}