namespace Fifthweek.CodeGeneration.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    [TestClass]
    public class AutoPrimitiveTests
    {
        private static readonly Func<PrimitiveIntDummy> PrimitiveIntA = () => new PrimitiveIntDummy(0);
        private static readonly Func<PrimitiveIntDummy> PrimitiveIntB = () => new PrimitiveIntDummy(1);
        private static readonly Func<PrimitiveStringDummy> PrimitiveStringA = () => new PrimitiveStringDummy("hello");
        private static readonly Func<PrimitiveStringDummy> PrimitiveStringB = () => new PrimitiveStringDummy("world");
        private static readonly Func<PrimitiveGuidDummy> PrimitiveGuidA = () => new PrimitiveGuidDummy(Guid.Parse("{2271BC88-2C7A-4F03-B213-C350059C8022}"));
        private static readonly Func<PrimitiveGuidDummy> PrimitiveGuidB = () => new PrimitiveGuidDummy(Guid.Parse("{FA9BC8F7-DBBD-40DB-8750-555B1DDDD472}"));
        private static readonly Func<PrimitiveDoubleWithIndividualAttributesDummy> PrimitiveDoubleA = () => new PrimitiveDoubleWithIndividualAttributesDummy(0.0);
        private static readonly Func<PrimitiveDoubleWithIndividualAttributesDummy> PrimitiveDoubleB = () => new PrimitiveDoubleWithIndividualAttributesDummy(0.2);

        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            Assert.AreEqual(PrimitiveIntA(), PrimitiveIntA());
            Assert.AreEqual(PrimitiveStringA(), PrimitiveStringA());
            Assert.AreEqual(PrimitiveGuidA(), PrimitiveGuidA());
            Assert.AreEqual(PrimitiveDoubleA(), PrimitiveDoubleA());
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            Assert.AreNotEqual(PrimitiveIntA(), PrimitiveIntB());
            Assert.AreNotEqual(PrimitiveStringA(), PrimitiveStringB());
            Assert.AreNotEqual(PrimitiveGuidA(), PrimitiveGuidB());
            Assert.AreNotEqual(PrimitiveDoubleA(), PrimitiveDoubleB());
        }

        [TestMethod]
        public void ItShouldRecogniseNullAsDifferent()
        {
            Assert.AreNotEqual(PrimitiveIntA(), null);
            Assert.AreNotEqual(PrimitiveStringA(), null);
            Assert.AreNotEqual(PrimitiveGuidA(), null);
            Assert.AreNotEqual(PrimitiveDoubleA(), null);
        }

        [TestMethod]
        public void ItShouldDeserializeJson()
        {
            string serialized;
            object deserialized;

            serialized = JsonConvert.SerializeObject(PrimitiveIntA());
            deserialized = JsonConvert.DeserializeObject<PrimitiveIntDummy>(serialized);
            Assert.AreEqual(PrimitiveIntA(), deserialized);

            serialized = JsonConvert.SerializeObject(PrimitiveStringA());
            deserialized = JsonConvert.DeserializeObject<PrimitiveStringDummy>(serialized);
            Assert.AreEqual(PrimitiveStringA(), deserialized);

            serialized = JsonConvert.SerializeObject(PrimitiveGuidA());
            deserialized = JsonConvert.DeserializeObject<PrimitiveGuidDummy>(serialized);
            Assert.AreEqual(PrimitiveGuidA(), deserialized);

            serialized = JsonConvert.SerializeObject(PrimitiveDoubleA());
            deserialized = JsonConvert.DeserializeObject<PrimitiveDoubleWithIndividualAttributesDummy>(serialized);
            Assert.AreEqual(PrimitiveDoubleA(), deserialized);
        }
    }
}