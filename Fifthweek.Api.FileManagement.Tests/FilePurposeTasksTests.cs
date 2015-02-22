namespace Fifthweek.Api.FileManagement.Tests
{
    using System.Linq;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FilePurposeTasksTests
    {
        [TestMethod]
        public void WhenCalledWithValidPurpose_ItShouldReturnTasks()
        {
            var filePurposeTasks = new FilePurposeTasks();

            var tasks = filePurposeTasks.GetTasks(FilePurposes.ProfileHeaderImage);

            Assert.IsTrue(tasks.Any());
        }

        [TestMethod]
        public void WhenCalledWithInvalidPurpose_ItShouldReturnEmptyListOfTasks()
        {
            var filePurposeTasks = new FilePurposeTasks();

            var tasks = filePurposeTasks.GetTasks("!");

            Assert.IsFalse(tasks.Any());
        }
    }
}