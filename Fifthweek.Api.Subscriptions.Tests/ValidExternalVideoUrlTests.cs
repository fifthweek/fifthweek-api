namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidExternalVideoUrlTests : ValidatedStringTests<ValidExternalVideoUrl>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "https://www.youtube.com/watch?v=vEQrP3bGX8k"; }
        }

        protected override string ValueB
        {
            get { return "http://vimeo.com/114229222"; }
        }

        protected override bool AppendPadding
        {
            get { return true; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidExternalVideoUrl.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidExternalVideoUrl.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsEmpty()
        {
            var result = ValidExternalVideoUrl.IsEmpty(" ");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidExternalVideoUrl.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicExternalVideoUrls()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldOnlyAllowUrlsFromVideoSitesInTheAllowedList()
        {
            this.BadValue("http://www.naughty.com/1241515");
            this.BadValue("http://www.google.com/wggasdf");

            foreach (var allowedDomain in ValidExternalVideoUrl.AllowedDomains)
            {
                this.GoodValue(string.Format("http://{0}/1241515", allowedDomain));
            }
        }

        [TestMethod]
        public void ItShouldOnlyAllowHttpAndHttpsUrls()
        {
            this.GoodValue("http://www.youtube.com/watch?v=vEQrP3bGX8k");
            this.GoodValue("https://www.youtube.com/watch?v=vEQrP3bGX8k");
            this.BadValue("ftp://www.youtube.com/watch?v=vEQrP3bGX8k");
        }

        [TestMethod]
        public void ItShouldAllowUppercaseOnUrlRightPart()
        {
            this.GoodValue("http://www.youtube.com/Watch?v=vEQrP3bGX8k");
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldNotAllowEmptyExternalVideoUrls()
        {
            this.BadValue(string.Empty);
        }

        [TestMethod]
        public void ItShouldNotAllowExternalVideoUrlsOver100Characters()
        {
            this.AssertMaxLength(100, whitespaceSensitive: false);
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.AssertTabsNotAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.AssertNewLinesNotAllowed();
        }

        [TestMethod]
        public void ItShouldNormalizeToHaveLowercaseLeftPart()
        {
            this.GoodNonExactValue("HtTp://VIMeo.coM/CapitalsAreAllowed?In=Here", "http://vimeo.com/CapitalsAreAllowed?In=Here");
        }

        [TestMethod]
        public void ItShouldNormalizeToHaveNoLeadingOrTrailingWhitespace()
        {
            this.GoodNonExactValue(" http://vimeo.com/114229222", "http://vimeo.com/114229222");
            this.GoodNonExactValue("http://vimeo.com/114229222 ", "http://vimeo.com/114229222");
        }

        protected override ValidExternalVideoUrl Parse(string value)
        {
            return ValidExternalVideoUrl.Parse(value);
        }

        protected override bool TryParse(string value, out ValidExternalVideoUrl parsedObject)
        {
            return ValidExternalVideoUrl.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidExternalVideoUrl parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidExternalVideoUrl.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidExternalVideoUrl parsedObject)
        {
            return parsedObject.Value;
        }
    }
}