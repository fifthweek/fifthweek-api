using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class TaglineTests : ValidatedPrimitiveEqualityTests<Tagline, string>
    {
        [TestMethod]
        public void ItShouldAllowBasicTaglines()
        {
            this.GoodValue("Web Comics and More");
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldAllowPunctuation()
        {
            this.GoodValue("A webcomic of romance! Sarcasm? Math, and language.");
        }

        [TestMethod]
        public void ItShouldNotAllowTaglinesUnder5Characters()
        {
            this.GoodValue("12345");
            this.BadValue("1234");
        }

        [TestMethod]
        public void ItShouldNotAllowTaglinesOver55Characters()
        {
            this.GoodValue(new string('x', 55));
            this.BadValue(new string('x', 56));

            // Test whitespace sensitivity.
            this.GoodValue(new string(' ', 55));
            this.BadValue(new string(' ', 56)); 
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.BadValue("abcdef\t");
            this.BadValue("\tabcdef");
            this.BadValue("abc\tdef");
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.BadValue("abcdef\n");
            this.BadValue("\nabcdef");
            this.BadValue("abc\ndef");

            this.BadValue("abcdef\r");
            this.BadValue("\rabcdef");
            this.BadValue("abc\rdef");
        }

        protected override string ValueA
        {
            get { return "Tagline A"; }
        }

        protected override string ValueB
        {
            get { return "Tagline B"; }
        }

        protected override Tagline Parse(string value, bool exact)
        {
            return Tagline.Parse(value);
        }

        protected override bool TryParse(string value, out Tagline parsedObject, bool exact)
        {
            return Tagline.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out Tagline parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Tagline.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(Tagline parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly string InvalidValue = "!";
    }
}