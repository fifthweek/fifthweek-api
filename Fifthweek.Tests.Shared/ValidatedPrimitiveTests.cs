namespace Fifthweek.Tests.Shared
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class ValidatedPrimitiveTests<TParsed, TRaw> : PrimitiveTests<TParsed>
    {
        protected abstract TRaw ValueA { get; }

        protected abstract TRaw ValueB { get; }

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

        protected abstract TParsed Parse(TRaw value);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, out IReadOnlyCollection<string> errorMessages);

        protected abstract TRaw GetValue(TParsed parsedObject);

        protected void GoodValue(TRaw value)
        {
            this.GoodValue(value, value);
        }

        protected void GoodNonExactValue(TRaw value, TRaw exactValue)
        {
            this.GoodValue(value, exactValue);
        }

        protected void BadValue(TRaw value)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject);
            Assert.IsFalse(valid, string.Format("Value not expected to be valid: '{0}'", value));

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

        private void GoodValue(TRaw actualValue, TRaw expectedValue)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(actualValue, out parsedObject);
            Assert.IsTrue(valid, string.Format("Value expected to be valid: '{0}'", actualValue));
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));

            valid = this.TryParse(actualValue, out parsedObject, out errorMessages);
            Assert.IsTrue(valid);
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(actualValue);
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));
        }
    }
}