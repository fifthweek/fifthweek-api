namespace Fifthweek.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    using Fifthweek.Api.Identity;
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ControllerConfigurationTests
    {
        private List<Type> controllers;

        [TestInitialize]
        public void TestInitialize()
        {
            this.controllers = (from t in typeof(MembershipController).Assembly.GetTypes()
                           where typeof(ApiController).IsAssignableFrom(t)
                           select t).ToList();
        }

        [TestMethod]
        public void AllControllerMethodsShouldNotManuallyRequireHttps()
        {
            foreach (var controller in this.controllers)
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.IsFalse(method.GetCustomAttributes<RequireHttpsAttribute>().Any());
                }
            }
        }

        [TestMethod]
        public void AllControllerMethodsShouldNotManuallyConvertExceptionsToResponses()
        {
            foreach (var controller in this.controllers)
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.IsFalse(method.GetCustomAttributes<ConvertExceptionsToResponsesAttribute>().Any());
                }
            }
        }

        [TestMethod]
        public void AllControllerMethodsShouldNotManuallyValidateTheModel()
        {
            foreach (var controller in this.controllers)
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.IsFalse(method.GetCustomAttributes<ValidateModelAttribute>().Any());
                }
            }
        }
    }
}