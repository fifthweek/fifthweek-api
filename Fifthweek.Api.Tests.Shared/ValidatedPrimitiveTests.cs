namespace Fifthweek.Api.Tests.Shared
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
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, true));
            Assert.IsTrue(this.TryParse(this.ValueA, out objectB, true));
            Assert.AreEqual(objectA, objectB);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, out errorMessages, true));
            Assert.IsTrue(this.TryParse(this.ValueA, out objectB, out errorMessages, true));
            Assert.AreEqual(objectA, objectB);
        }

        public override void ItShouldRecogniseDifferentObjects()
        {
            base.ItShouldRecogniseDifferentObjects();

            TParsed objectA;
            TParsed objectB;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, true));
            Assert.IsTrue(this.TryParse(this.ValueB, out objectB, true));
            Assert.AreNotEqual(objectA, objectB);

            IReadOnlyCollection<string> errorMessages;
            Assert.IsTrue(this.TryParse(this.ValueA, out objectA, out errorMessages, true));
            Assert.IsTrue(this.TryParse(this.ValueB, out objectB, out errorMessages, true));
            Assert.AreNotEqual(objectA, objectB);
        }

        protected override TParsed NewInstanceOfObjectA()
        {
            return this.Parse(this.ValueA, true);
        }

        protected override TParsed NewInstanceOfObjectB()
        {
            return this.Parse(this.ValueB, true);
        }

        protected abstract TParsed Parse(TRaw value, bool exact);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, bool exact);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact);

        protected abstract TRaw GetValue(TParsed parsedObject);

        protected void GoodValue(TRaw value)
        {
            this.GoodValue(value, value, true);
            this.GoodValue(value, value, false);
        }

        protected void GoodNonExactValue(TRaw value, TRaw exactValue)
        {
            // Good non-exact values are bad exact values.
            this.GoodValue(value, exactValue, false);
            this.BadValue(value, true);
        }

        protected void BadValue(TRaw value)
        {
            this.BadValue(value, true);
            this.BadValue(value, false);
        }

        private void BadValue(TRaw value, bool exact)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject, exact);
            Assert.IsFalse(valid, string.Format("Value not expected to be valid: '{0}' (exact = {1})", value, exact));

            valid = this.TryParse(value, out parsedObject, out errorMessages, exact);
            Assert.IsFalse(valid);
            Assert.IsTrue(errorMessages.Count > 0);

            try
            {
                this.Parse(value, exact);
                Assert.Fail("Expected argument exception");
            }
            catch (ArgumentException)
            {
            }
        }

        private void GoodValue(TRaw actualValue, TRaw expectedValue, bool exact)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(actualValue, out parsedObject, exact);
            Assert.IsTrue(valid, string.Format("Value expected to be valid: '{0}' (exact = {1})", actualValue, exact));
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));

            valid = this.TryParse(actualValue, out parsedObject, out errorMessages, exact);
            Assert.IsTrue(valid);
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(actualValue, exact);
            Assert.AreEqual(expectedValue, this.GetValue(parsedObject));
        }
    }
}