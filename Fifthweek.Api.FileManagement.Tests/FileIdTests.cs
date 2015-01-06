namespace Fifthweek.Api.FileManagement.Tests
{
    using System;

    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileIdTests : PrimitiveEqualityTests<FileId>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        protected override FileId NewInstanceOfObjectA()
        {
            return new FileId(Guid.Parse("{EA7D04CC-803D-4338-BDC0-18D143F8FEC3}"));
        }

        protected override FileId NewInstanceOfObjectB()
        {
            return new FileId(Guid.Parse("{69BD9D77-D578-45A5-BC40-266653B43EEE}"));
        }
    }
}