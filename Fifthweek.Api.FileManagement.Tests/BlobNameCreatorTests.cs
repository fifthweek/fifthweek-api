namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlobNameCreatorTests
    {
        [TestMethod]
        public void ItShouldGenerateBase32Characters()
        {
            var blobNameCreator = new BlobNameCreator();

            var chars = new HashSet<char>();
            for (int i = 0; i < 32; i++)
            {
                var c = blobNameCreator.GenerateRandomCharacter(i);
                Assert.IsTrue((c >= 'a' && c <= 'z') || (c >= '2' && c <= '7'));
                Assert.IsFalse(chars.Contains(c));
                chars.Add(c);
            }

            try
            {
                blobNameCreator.GenerateRandomCharacter(-1);
                Assert.Fail("An exception should be thrown.");
            }
            catch (Exception t)
            {
                Assert.IsInstanceOfType(t, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                blobNameCreator.GenerateRandomCharacter(32);
                Assert.Fail("An exception should be thrown.");
            }
            catch (Exception t)
            {
                Assert.IsInstanceOfType(t, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void ItShouldGenerateStringsOfTheCorrectLength()
        {
            var blobNameCreator = new BlobNameCreator();

            for (int i = 0; i < 30; i++)
            {
                var sb = new StringBuilder();
                blobNameCreator.AppendRandomString(sb, i);
                Assert.AreEqual(i, sb.Length);
            }
        }

        [TestMethod]
        public void ItShouldGenerateUniqueNames()
        {
            var blobNameCreator = new BlobNameCreator();

            var names = new HashSet<string>();
            for (int i = 0; i < 100000; i++)
            {
                var name = blobNameCreator.CreateFileName();
                Assert.IsFalse(string.IsNullOrWhiteSpace(name));
                Assert.IsTrue(name == name.Trim());
                Assert.IsFalse(names.Contains(name));
                names.Add(name);
            }
        }
    }
}