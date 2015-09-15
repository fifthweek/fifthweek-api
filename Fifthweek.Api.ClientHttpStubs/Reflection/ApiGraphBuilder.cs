namespace Fifthweek.Api.ClientHttpStubs.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Shared;

    using Humanizer;

    using HttpMethod = Fifthweek.Api.ClientHttpStubs.HttpMethod;

    public class ApiGraphBuilder
    {
        private const string ControllerSuffix = "Controller";

        public ApiGraph Build(IEnumerable<Type> controllerTypes)
        {
            var controllers = new List<ControllerElement>();
            foreach (var controllerType in controllerTypes)
            {
                var methods = new List<MethodElement>();
                var methodInfos = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var methodInfo in methodInfos)
                {
                    ParameterElement bodyParameter = null;
                    var urlParameters = new List<ParameterElement>();
                    var fullRoute = GetFullRoute(controllerType, methodInfo);
                    var methodName = methodInfo.Name.EndsWith("Async")
                        ? methodInfo.Name.Substring(0, methodInfo.Name.Length - 5)
                        : methodInfo.Name;

                    foreach (var parameter in methodInfo.GetParameters())
                    {
                        if (parameter.GetCustomAttribute<FromUriAttribute>() != null || RouteUtility.GetPlaceholders(parameter.Name).Any(fullRoute.Contains))
                        {
                            urlParameters.Add(new ParameterElement(parameter.Name, parameter.ParameterType, !parameter.HasDefaultValue));
                        }
                        else if (bodyParameter == null)
                        {
                            bodyParameter = new ParameterElement(parameter.Name, parameter.ParameterType, true);
                        }
                        else
                        {
                            throw new Exception("Multiple body parameters detected for " + methodInfo.Name);
                        }
                    }

                    methods.Add(new MethodElement(
                        GetHttpMethod(methodInfo),
                        methodName,
                        fullRoute,
                        urlParameters,
                        bodyParameter,
                        TryGetReturnType(controllerType, methodInfo)));
                }

                var controllerName = controllerType.Name.Substring(0, controllerType.Name.Length - ControllerSuffix.Length);
                controllers.Add(new ControllerElement(controllerName, methods));
            }

            return new ApiGraph(controllers);
        }

        private static HttpMethod GetHttpMethod(MethodInfo methodInfo)
        {
            if (methodInfo.GetCustomAttribute<HttpGetAttribute>() != null)
            {
                return HttpMethod.Get;
            }

            if (methodInfo.GetCustomAttribute<HttpPostAttribute>() != null)
            {
                return HttpMethod.Post;
            }

            if (methodInfo.GetCustomAttribute<HttpDeleteAttribute>() != null)
            {
                return HttpMethod.Delete;
            }

            if (methodInfo.GetCustomAttribute<HttpPutAttribute>() != null)
            {
                return HttpMethod.Put;
            }

            var name = methodInfo.Name.ToLower();
            
            if (name.StartsWith("get"))
            {
                return HttpMethod.Get;
            }

            if (name.StartsWith("post"))
            {
                return HttpMethod.Post;
            }

            if (name.StartsWith("delete"))
            {
                return HttpMethod.Delete;
            }

            if (name.StartsWith("put"))
            {
                return HttpMethod.Put;
            }

            return HttpMethod.Get;
        }

        private static string GetFullRoute(Type controllerType, MethodInfo methodInfo)
        {
            var routePrefix = controllerType.GetCustomAttribute<RoutePrefixAttribute>();
            var routes = methodInfo.GetCustomAttributes<RouteAttribute>().ToArray();

            if (routes.Length > 1)
            {
                throw new Exception("Multiple routes not supported. Please use separate methods.");
            }

            var methodRoute = routes.Length == 1 && !string.IsNullOrEmpty(routes[0].Template)
                ? routes[0].Template
                : null;

            if (methodRoute != null && routePrefix != null)
            {
                return routePrefix.Prefix + "/" + methodRoute;
            }

            if (methodRoute != null)
            {
                return methodRoute;
            }

            if (routePrefix != null)
            {
                return routePrefix.Prefix;
            }

            throw new Exception("No route information found.");
        }

        private static Type TryGetReturnType(Type classType, MethodInfo methodInfo)
        {
            var responseType = methodInfo.GetCustomAttribute<ResponseTypeAttribute>();
            if (responseType != null)
            {
                return responseType.ResponseType;
            }

            var type = methodInfo.ReturnType;
            if (type == typeof(void) || type == typeof(Task) || type == typeof(Task<IHttpActionResult>) || type == typeof(Task<HttpResponseMessage>))
            {
                return null;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return type.GetGenericArguments().First();
            }

            throw new Exception(string.Format("Unable to determine return type for '{0}' from method '{1}.{2}'", type.Name, classType.Name, methodInfo.Name));
        }
    }
}