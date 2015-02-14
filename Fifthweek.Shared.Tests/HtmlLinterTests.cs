namespace Fifthweek.Shared.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HtmlLinterTests
    {
        private HtmlLinter target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new HtmlLinter();
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItShouldTrim()
        {
            const string Input = " \t\n <html></html> \n\t ";
            const string Expected = "<html></html>";
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItRemoveWhitespaceBetweenElements()
        {
            const string Input = "<html> \n\t <p></p> \n\t </html>";
            const string Expected = "<html> <p></p> </html>"; // New lines get converted to spaces.
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItRemoveLineBreaksWithinElements()
        {
            const string Input = "<html<p>Hello World! Foo\nbar.\n</p></html>";
            const string Expected = "<html<p>Hello World! Foo bar. </p></html>";
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItPreserveSingleSpacesBetweenElements()
        {
            const string Input = "<html><strong>hello</strong> <em>world</em></html>";
            const string Expected = "<html><strong>hello</strong> <em>world</em></html>";
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItPreserveSingleSpacesBetweenElements2()
        {
            const string Input = "<html><strong>hello</strong>\n<em>world</em></html>";
            const string Expected = "<html><strong>hello</strong> <em>world</em></html>";
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void WhenRemovingWhitespaceForHtmlEmails_ItPreserveSingleSpacesBetweenElements3()
        {
            const string Input = "<html><strong>hello</strong>    <em>world</em></html>";
            const string Expected = "<html><strong>hello</strong> <em>world</em></html>";
            var actual = this.target.RemoveWhitespaceForHtmlEmail(Input);

            Assert.AreEqual(Expected, actual);
        }
    }
}