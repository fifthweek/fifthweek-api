namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Humanizer;

    using Microsoft.VisualStudio.TextTemplating;

    using Newtonsoft.Json.Linq;

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

        private static bool IsCustomPrimitive(Type type, PropertyInfo[] properties)
        {
            return properties.Length == 1 && (type.GetCustomAttribute<AutoJsonAttribute>() != null || type.GetCustomAttribute<AutoPrimitiveAttribute>() != null);
        }

        private void RenderController(ControllerElement controller)
        {
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
            this.DocumentMethod(method);
            
            this.output.Write("service.");
            this.output.Write(method.Name.Camelize());
            this.output.Write(" = function(");

            this.output.Write(string.Join(", ", method.GetAllParameters().Select(_ => _.Name)));

            this.output.WriteLine(") {");

            this.output.PushIndent(Tab);

            this.output.Write("return $http.");
            this.output.Write(method.HttpMethod.ToString().ToLower());
            this.output.Write("(utilities.fixUri(apiBaseUri + ");

            this.output.Write(AngularUtility.GetRouteBuilder(method));

            this.output.Write(")");
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

            this.output.WriteLine(").catch(function(response) {");

            this.output.PushIndent(Tab);
            this.output.WriteLine("return $q.reject(utilities.getHttpError(response));");
            this.output.PopIndent();
            this.output.WriteLine("});");

            this.output.PopIndent();
            this.output.WriteLine("};");

            this.output.WriteLine(string.Empty);
        }

        private void DocumentMethod(MethodElement method)
        {
            var allParameters = method.GetAllParameters();

            this.output.PushIndent("// ");

            foreach (var parameter in allParameters)
            {
                this.output.Write(parameter.Name + " = ");
                this.DocumentType(parameter.Name, parameter.Type, parameter.IsRequired);
            }

            if (method.ReturnType != null)
            {
                this.output.Write("result = ");
                this.DocumentType(string.Empty, method.ReturnType, true);
            }

            this.output.PopIndent();
        }

        private void DocumentType(string name, Type type, bool isRequired, bool delimit = false)
        {
            var integers = new HashSet<Type>(new[]
            {
                typeof(byte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(ushort),
                typeof(uint),
                typeof(ulong)
            });

            var floats = new HashSet<Type>(new[]
            {
                typeof(float),
                typeof(double),
                typeof(decimal)
            });

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Optionality is defined with parameter/property attributes. Everything is assumed to be required otherwise.
                type = type.GetGenericArguments().First();
            }

            if (integers.Contains(type))
            {
                this.DocumentPrimitiveParameter("0", isRequired, delimit);
            }
            else if (floats.Contains(type))
            {
                this.DocumentPrimitiveParameter("0.0", isRequired, delimit);
            }
            else if (type == typeof(string))
            {
                if (name.EndsWith("Id"))
                {
                    // Conventional assumption.
                    this.DocumentPrimitiveParameter("'Base64Guid'", isRequired, delimit);    
                }
                else
                {
                    this.DocumentPrimitiveParameter("''", isRequired, delimit);
                }
            }
            else if (type == typeof(bool))
            {
                this.DocumentPrimitiveParameter("false", isRequired, delimit);
            }
            else if (type == typeof(DateTime))
            {
                this.DocumentPrimitiveParameter("'2015-12-25T14:45:05Z'", isRequired, delimit);
            }
            else if (type == typeof(Guid))
            {
                this.DocumentPrimitiveParameter("'Base64Guid'", isRequired, delimit);
            }
            else if (type == typeof(JToken))
            {
                this.DocumentPrimitiveParameter("{ arbitrary: 'json' }", isRequired, delimit);
            }
            else if (type.IsPrimitiveEx())
            {
                this.DocumentPrimitiveParameter("'" + type.Name.ToLower() + "'", isRequired, delimit);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType && type.GetGenericArguments().Length == 1)
            {
                var elementType = type.GetGenericArguments().First();
                
                this.output.Write("[");
                this.DocumentParameterRequirement(isRequired);
                this.output.PushIndent(Tab);
                this.DocumentType(name, elementType, true);
                this.output.PopIndent();
                this.output.WriteLine(delimit ? "]," : "]");
            }
            else
            {
                var properties = type.GetProperties().Where(_ => _.CanWrite).ToArray();

                if (IsCustomPrimitive(type, properties))
                {
                    var property = properties.First();
                    this.DocumentType(property.Name, property.PropertyType, isRequired, delimit);
                }
                else
                {
                    this.output.Write("{");
                    this.DocumentParameterRequirement(isRequired);
                    this.output.PushIndent(Tab);

                    for (var i = 0; i < properties.Length; i++)
                    {
                        var propertyName = properties[i].Name.Camelize();
                        this.output.Write(propertyName);
                        this.output.Write(": ");
                        this.DocumentType(propertyName, properties[i].PropertyType, properties[i].GetCustomAttribute<OptionalAttribute>() == null, i < properties.Length - 1);
                    }

                    this.output.PopIndent();
                    this.output.WriteLine(delimit ? "}," : "}");
                }
            }
        }

        private void DocumentPrimitiveParameter(string exampleValue, bool isRequired, bool delimit)
        {
            this.output.Write(exampleValue);

            if (delimit)
            {
                this.output.Write(",");
            }

            this.DocumentParameterRequirement(isRequired);
        }

        private void DocumentParameterRequirement(bool isRequired)
        {
            this.output.WriteLine(isRequired ? string.Empty : " /* optional */");
        }
    }
}