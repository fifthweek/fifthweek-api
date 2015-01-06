using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SetMandatorySubscriptionFieldsCommandTests
    {
        /*
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var data = PasswordResetConfirmationDataTests.NewData();
            var command1 = NewCommand(data);
            var command2 = NewCommand(data);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentConfirmationData()
        {
            var data = PasswordResetConfirmationDataTests.NewData();
            var command1 = NewCommand(data);

            data.Token = "different";
            var command2 = NewCommand(data);

            Assert.AreNotEqual(command1, command2);
        }

        public static ConfirmPasswordResetCommand NewCommand(PasswordResetConfirmationData passwordResetConfirmation)
        {
            return new ConfirmPasswordResetCommand(
                UserId.Parse(passwordResetConfirmation.UserId),
                passwordResetConfirmation.Token,
                Password.Parse(passwordResetConfirmation.NewPassword));
        } 
        */
    }
}