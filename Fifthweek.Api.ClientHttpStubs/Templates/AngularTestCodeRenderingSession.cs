namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    using Humanizer;

    using Microsoft.VisualStudio.TextTemplating;

    [AutoConstructor]
    public partial class AngularTestCodeRenderingSession
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
            this.output.WriteLine(string.Format("describe('{0} stub', function() {{", controller.Name.Humanize(LetterCasing.LowerCase)));
            this.output.PushIndent(Tab);

            this.output.WriteLine("'use strict';");
            this.output.WriteLine(string.Empty);
 
            this.output.WriteLine("var fifthweekConstants;");
            this.output.WriteLine("var $httpBackend;");
            this.output.WriteLine("var $rootScope;");
            this.output.WriteLine("var target;");
            this.output.WriteLine(string.Empty);

            this.output.WriteLine("beforeEach(module('webApp', 'stateMock'));");
            this.output.WriteLine(string.Empty);

            this.output.WriteLine("beforeEach(inject(function($injector) {");
            this.output.PushIndent(Tab);
            this.output.WriteLine("fifthweekConstants = $injector.get('fifthweekConstants');");
            this.output.WriteLine("$httpBackend = $injector.get('$httpBackend');");
            this.output.WriteLine("$rootScope = $injector.get('$rootScope');");
            this.output.WriteLine(string.Format("target = $injector.get('{0}Stub');", controller.Name.Camelize()));
            this.output.PopIndent();
            this.output.WriteLine("}));");
            this.output.WriteLine(string.Empty);

            this.output.WriteLine("afterEach(function() {");
            this.output.PushIndent(Tab);
            this.output.WriteLine("$httpBackend.verifyNoOutstandingExpectation();");
            this.output.WriteLine("$httpBackend.verifyNoOutstandingRequest();");
            this.output.PopIndent();
            this.output.WriteLine("});");

            foreach (var method in controller.Methods)
            {
                this.RenderMethod(method);
            }

            this.output.PopIndent();
            this.output.WriteLine("});");
            this.output.WriteLine(string.Empty);
        }

        private void RenderMethod(MethodElement method)
        {
            var parameterNames = method.GetAllParameters().Select(_ => _.Name).ToArray();

            this.output.WriteLine(string.Empty);

            this.output.WriteLine(string.Format("it('should {0}', function() {{", method.Name.Humanize(LetterCasing.LowerCase)));
            this.output.PushIndent(Tab);

            for (var i = 0; i < parameterNames.Length; i++)
            {
                this.output.WriteLine(string.Format("var {0} = 'value{1}';", parameterNames[i], i));
            }

            this.output.WriteLine(string.Empty);
            this.output.WriteLine("var responseData = 'response data';");
            this.output.Write("$httpBackend.expect");
            this.output.Write(method.HttpMethod.ToString().ToUpper());
            this.output.Write("(fifthweekConstants.apiBaseUri + ");

            this.output.Write(AngularUtility.GetRouteBuilder(method));

            if (method.BodyParameter != null)
            {
                this.output.Write(", ");

                if (method.BodyParameter.Type == typeof(string))
                {
                    this.output.Write("JSON.stringify(");
                    this.output.Write(method.BodyParameter.Name);
                    this.output.Write(")");
                }
                else
                {
                    this.output.Write(method.BodyParameter.Name);
                }
            }

            this.output.WriteLine(").respond(200, responseData);");
            this.output.WriteLine(string.Empty);

            var actualParameters = string.Join(", ", parameterNames);
            this.output.WriteLine("var result = null;");
            this.output.WriteLine(string.Format("target.{0}({1}).then(function(response) {{ result = response.data; }});", method.Name.Camelize(), actualParameters));
            this.output.WriteLine(string.Empty);
            this.output.WriteLine("$httpBackend.flush();");
            this.output.WriteLine("$rootScope.$apply();");
            this.output.WriteLine(string.Empty);
            this.output.WriteLine("expect(result).toBe(responseData);");
            
            this.output.PopIndent();
            this.output.WriteLine("});");
        }
    }
}