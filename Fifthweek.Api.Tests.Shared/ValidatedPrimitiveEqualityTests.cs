using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Shared
{
    public abstract class ValidatedPrimitiveEqualityTests<TParsed, TRaw> : PrimitiveEqualityTests<TParsed>
    {
        public override void ItShouldRecogniseEqualObjects()
        {
            base.ItShouldRecogniseEqualObjects();

            TParsed objectA;
            TParsed objectB;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA));
            Assert.IsTrue(this.TryParse(this.ValueA, out objectB));
            Assert.AreEqual(objectA, objectB);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, out errorMessages));
            Assert.IsTrue(this.TryParse(this.ValueA, out objectB, out errorMessages));
            Assert.AreEqual(objectA, objectB);
        }

        public override void ItShouldRecogniseDifferentObjects()
        {
            base.ItShouldRecogniseDifferentObjects();

            TParsed objectA;
            TParsed objectB;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA));
            Assert.IsTrue(this.TryParse(this.ValueB, out objectB));
            Assert.AreNotEqual(objectA, objectB);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, out errorMessages));
            Assert.IsTrue(this.TryParse(this.ValueB, out objectB, out errorMessages));
            Assert.AreNotEqual(objectA, objectB);
        }

        protected override TParsed NewInstanceOfObjectA()
        {
            return this.Parse(this.ValueA);
        }

        protected override TParsed NewInstanceOfObjectB()
        {
            return this.Parse(this.ValueB);
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