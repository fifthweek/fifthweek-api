using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests
{
    [TestClass]
    public class UserInputNormalizationTests
    {
        [TestMethod]
        public void ItShouldTrim_WhenNormalizingUsername()
        {
            const string validUsername = "lawrence";
            var normalization = new UserInputNormalization();

            Assert.AreEqual(
                normalization.NormalizeUsername(validUsername), 
                validUsername, "No effect when normalization not required.");

            Assert.AreEqual(
                normalization.NormalizeUsername(Whitespace + validUsername + Whitespace),
                validUsername);
        }

        [TestMethod]
        public void ItShouldConvertToLowercase_WhenNormalizingUsername()
        {
            const string mixedCaseUsername = "Lawrence";
            const string lowerCaseUsername = "lawrence";
            var normalization = new UserInputNormalization();

            Assert.AreEqual(
                normalization.NormalizeUsername(lowerCaseUsername),
                lowerCaseUsername, "No effect when normalization not required.");

            Assert.AreEqual(
                normalization.NormalizeUsername(mixedCaseUsername),
                lowerCaseUsername);
        }

        [TestMethod]
        public void ItShouldTrim_WhenNormalizingEmailAddress()
        {
            const string validEmail = "lawrence@fifthweek.com";
            var normalization = new UserInputNormalization();

            Assert.AreEqual(
                normalization.NormalizeEmailAddress(validEmail),
                validEmail, "No effect when normalization not required.");

            Assert.AreEqual(
                normalization.NormalizeEmailAddress(Whitespace + validEmail + Whitespace),
                validEmail);
        }

        [TestMethod]
        public void ItShouldConvertToLowercase_WhenNormalizingEmailAddress()
        {
            const string mixedCaseEmail = "Lawrence@Fifthweek.com";
            const string lowerCaseEmail = "lawrence@fifthweek.com";
            var normalization = new UserInputNormalization();

            Assert.AreEqual(
                normalization.NormalizeUsername(lowerCaseEmail),
                lowerCaseEmail, "No effect when normalization not required.");

            Assert.AreEqual(
                normalization.NormalizeUsername(mixedCaseEmail),
                lowerCaseEmail);
        }

        private const string Whitespace = " \t\n";
    }
}