using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    [TestClass]
    public abstract class ValidatedCustomPrimitiveTypeTests<TParsed, TRaw>
    {
        public void TestEquality()
        {
            this.ItShouldRecogniseEqualObjects();
            this.ItShouldRecogniseDifferentObjects();
        }

        public void ItShouldRecogniseEqualObjects()
        {
            var value1 = this.Parse(this.ValueA);
            var value2 = this.Parse(this.ValueA);
            
            Assert.AreEqual(value1, value2);

            Assert.IsTrue(this.TryParse(this.ValueA, out value1));
            Assert.IsTrue(this.TryParse(this.ValueA, out value2));
            
            Assert.AreEqual(value1, value2);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out value1, out errorMessages));
            Assert.IsTrue(this.TryParse(this.ValueA, out value2, out errorMessages));

            Assert.AreEqual(value1, value2);
        }

        public void ItShouldRecogniseDifferentObjects()
        {
            var value1 = this.Parse(this.ValueA);
            var value2 = this.Parse(this.ValueB);

            Assert.AreNotEqual(value1, value2);

            Assert.IsTrue(this.TryParse(this.ValueA, out value1));
            Assert.IsTrue(this.TryParse(this.ValueB, out value2));

            Assert.AreNotEqual(value1, value2);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out value1, out errorMessages));
            Assert.IsTrue(this.TryParse(this.ValueB, out value2, out errorMessages));

            Assert.AreNotEqual(value1, value2);
        }

        protected abstract TRaw ValueA { get; }

        protected abstract TRaw ValueB { get; }

        protected abstract TParsed Parse(TRaw value);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, out IReadOnlyCollection<string> errorMessages);

        protected abstract TRaw GetValue(TParsed parsedObject);

        protected void GoodValue(TRaw value)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, this.GetValue(parsedObject));

            valid = this.TryParse(value, out parsedObject, out errorMessages);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, this.GetValue(parsedObject));
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(value);
            Assert.AreEqual(value, this.GetValue(parsedObject));
        }

        protected void BadValue(TRaw value)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject);
            Assert.IsFalse(valid);

            valid = this.TryParse(value, out parsedObject, out errorMessages);
            Assert.IsFalse(valid);
            Assert.IsTrue(errorMessages.Count > 0);

            try
            {
                this.Parse(value);
                Assert.Fail("Expected argument exception");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}