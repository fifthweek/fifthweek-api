namespace Fifthweek.Api.Posts.Tests
{
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetPostPreviewContentTests
    {
        private GetPostPreviewContent target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetPostPreviewContent();
        }

        [TestMethod]
        public void WhenPostContentIsNull_ItShouldReturnEmptyString()
        {
            var result = this.target.Execute(null, new PreviewText("preview"));

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenPostContentIsWhitespace_ItShouldReturnEmptyString()
        {
            var result = this.target.Execute("  ", new PreviewText("preview"));

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenPostContainersTextBlocks_ItShouldReturnObfuscatedText()
        {
            var result = this.target.Execute(
                "[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'One two three, four!'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'5-6 Se7en.'}}]",
                null);

            Assert.AreEqual(
               ("[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'⁂⁂⁂ ⁂⁂⁂ ⁂⁂⁂⁂⁂⁂ ⁂⁂⁂⁂⁂'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'⁂⁂⁂ ⁂⁂⁂⁂⁂⁂'}}]").Replace("'", "\""),
                result);
        }

        [TestMethod]
        public void WhenPostContainersTextBlocks_AndLinks_ItShouldReturnObfuscatedText()
        {
            var result = this.target.Execute(
                "[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'One [two three](some url), four!'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'[5-6](url)[ Se7en.](url2)'}}]",
                null);

            Assert.AreEqual(
               ("[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'⁂⁂⁂ ⁂⁂⁂ ⁂⁂⁂⁂⁂⁂ ⁂⁂⁂⁂⁂'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'⁂⁂⁂ ⁂⁂⁂⁂⁂⁂'}}]").Replace("'", "\""),
                result);
        }

        [TestMethod]
        public void WhenPostContainersTextBlocks_AndPreviewText_ItShouldReturnObfuscatedTextWithPreview()
        {
            var result = this.target.Execute(
                "[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'One two three, four!'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'5-6 Se7en.'}}]",
                new PreviewText("One two thr"));

            Assert.AreEqual(
               ("[{'type':'image','data':{'fileId':'abc'}}," +
                "{'type':'text','data':{'text':'One two thr⁂⁂⁂ ⁂⁂⁂⁂⁂'}}," +
                "{'type':'image','data':{'fileId':'def'}}," +
                "{'type':'text','data':{'text':'⁂⁂⁂ ⁂⁂⁂⁂⁂⁂'}}]").Replace("'", "\""),
                result);
        }
    }
}