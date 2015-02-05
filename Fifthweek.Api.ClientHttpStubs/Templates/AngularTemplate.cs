namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Humanizer;

    using Microsoft.VisualStudio.TextTemplating;

    public partial class AngularTemplate : ITemplate
    {
        public void Render(ApiGraph api, TextTransformation output)
        {
            api.AssertNotNull("api");
            output.AssertNotNull("output");

            new RenderingSession(api, output).Run();
        }

        [AutoConstructor]
        public partial class RenderingSession
        {
            private const string Tab = "  ";

            private readonly ApiGraph api;
            private readonly TextTransformation output;

            public void Run()
            {
                this.output.WriteLine("var moduleName = 'webApp';");

                foreach (var controller in api.Controllers)
                {
                    this.RenderController(controller);
                }
            }

            private void RenderController(ControllerElement controller)
            {
                this.output.WriteLine("//");
                this.output.WriteLine(string.Format("// {0} API", controller.Name.Humanize()));
                this.output.WriteLine("//");
                this.output.WriteLine(string.Format("angular.module(moduleName).factory('{0}Service',", controller.Name.Camelize()));
                this.output.PushIndent(Tab);
                
                this.output.WriteLine("function($http, $q, fifthweekConstants, utilities) {");
                this.output.PushIndent(Tab);

                this.output.WriteLine("'use strict';");
                this.output.WriteLine(string.Empty);
                this.output.WriteLine("var apiBaseUri = fifthweekConstants.apiBaseUri;");
                this.output.WriteLine("var service = {};");
                this.output.WriteLine(string.Empty);

                foreach (var method in controller.Methods)
                {
                    this.RenderMethod(method);
                }

                this.output.WriteLine("return service;");
                
                this.output.PopIndent();
                this.output.WriteLine("});");

                this.output.PopIndent();
                this.output.WriteLine(string.Empty);
            }

            private void RenderMethod(MethodElement method)
            {
                this.output.Write("service.");
                this.output.Write(method.Name.Camelize());
                this.output.Write(" = function(");

                this.output.Write(string.Join(", ", method.GetAllParameters().Select(_ => _.Name)));

                this.output.WriteLine(") {");

                this.output.PushIndent(Tab);

                this.output.Write("return $http.");
                this.output.Write(method.HttpMethod.ToString().ToLower());
                this.output.Write("(apiBaseUri + ");

                this.output.Write(this.GetRouteBuilder(method));

                if (method.BodyParameter != null)
                {
                    this.output.Write(", ");
                    this.output.Write(method.BodyParameter.Name);
                }

                this.output.WriteLine(").catch(function(response) {");

                this.output.PushIndent(Tab);
                this.output.WriteLine("return $q.reject(utilities.getHttpError(response));");
                this.output.PopIndent();
                this.output.WriteLine("});");

                this.output.PopIndent();
                this.output.WriteLine("};");

                this.output.WriteLine(string.Empty);
            }

            private string GetRouteBuilder(MethodElement method)
            {
                var route = "'" + method.Route + "'";
                var queryParameters = method.UrlParameters.ToList();
                foreach (var urlParameter in method.UrlParameters)
                {
                    var routeParameterToken = "{" + urlParameter.Name + "}";
                    if (route.Contains(routeParameterToken))
                    {
                        queryParameters.Remove(urlParameter);
                        route = route.Replace(routeParameterToken, string.Format("' + {0} + '", urlParameter.Name));
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

                    var querystring = queryParameters.Select(_ => string.Format("{0}=' + {0} + '", _.Name));
                    route += string.Join("&", querystring);

                    // Trim trailing " + '"
                    route = route.Substring(0, route.Length - 4);
                }

                return route;
            }
        }
    }
}