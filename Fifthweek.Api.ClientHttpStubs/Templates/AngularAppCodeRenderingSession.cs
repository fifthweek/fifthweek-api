namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    using Humanizer;

    using Microsoft.VisualStudio.TextTemplating;

    [AutoConstructor]
    public partial class AngularAppCodeRenderingSession
    {
        private const string Tab = "  ";

        private readonly ApiGraph api;
        private readonly TextTransformation output;

        public void Run()
        {
            foreach (var controller in this.api.Controllers)
            {
                this.RenderController(controller);
            }
        }

        private void RenderController(ControllerElement controller)
        {
            this.output.WriteLine("//");
            this.output.WriteLine(string.Format("// {0} API stub.", controller.Name.Humanize()));
            this.output.WriteLine("//");
            this.output.WriteLine(string.Format("angular.module('webApp').factory('{0}Stub',", controller.Name.Camelize()));
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

            this.output.Write(AngularUtility.GetRouteBuilder(method));

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
    }
}