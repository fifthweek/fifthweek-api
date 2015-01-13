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

        /// <summary>
        /// RequireHttpsAttribute is applied automatically.
        /// </summary>
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

        /// <summary>
        /// ConvertExceptionsToResponsesAttribute is applied automatically.
        /// </summary>
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

        /// <summary>
        /// ValidateModelAttribute is applied automatically.
        /// </summary>
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

        /// <summary>
        /// Command handlers should perform authorization, not the controllers.
        /// </summary>
        [TestMethod]
        public void AllControllerMethodsShouldNotAuthorizeTheUser()
        {
            foreach (var controller in this.controllers)
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.IsFalse(method.GetCustomAttributes<AuthorizeAttribute>().Any());
                }
            }
        }
    }
}