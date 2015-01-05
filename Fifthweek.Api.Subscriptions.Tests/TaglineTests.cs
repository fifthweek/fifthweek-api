using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class TaglineTests : ValidatedCustomPrimitiveTypeTests<Tagline, string>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        protected override string ValueA
        {
            get { return "Tagline A"; }
        }

        protected override string ValueB
        {
            get { return "Tagline B"; }
        }

        protected override Tagline Parse(string value)
        {
            return Tagline.Parse(value);
        }

        protected override bool TryParse(string value, out Tagline parsedObject)
        {
            return Tagline.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out Tagline parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return Tagline.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(Tagline parsedObject)
        {
            return parsedObject.Value;
        }
    }
}