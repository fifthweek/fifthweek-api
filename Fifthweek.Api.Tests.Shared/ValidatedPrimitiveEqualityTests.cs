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

        protected abstract TRaw ValueA { get; }

        protected abstract TRaw ValueB { get; }

        protected abstract TParsed Parse(TRaw value, bool exact);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, bool exact);

        protected abstract bool TryParse(TRaw value, out TParsed parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact);

        protected abstract TRaw GetValue(TParsed parsedObject);

        protected void GoodValue(TRaw value)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject, true);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, this.GetValue(parsedObject));

            valid = this.TryParse(value, out parsedObject, out errorMessages, true);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, this.GetValue(parsedObject));
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(value, true);
            Assert.AreEqual(value, this.GetValue(parsedObject));
        }

        protected void GoodNonExactValue(TRaw value, TRaw exactValue)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject, false);
            Assert.IsTrue(valid);
            Assert.AreEqual(exactValue, this.GetValue(parsedObject));

            valid = this.TryParse(value, out parsedObject, out errorMessages, false);
            Assert.IsTrue(valid);
            Assert.AreEqual(exactValue, this.GetValue(parsedObject));
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(value, false);
            Assert.AreEqual(exactValue, this.GetValue(parsedObject));

            // Good non-exact values are bad exact values.
            this.BadValue(value);
        }

        protected void BadValue(TRaw value)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject, true);
            Assert.IsFalse(valid);

            valid = this.TryParse(value, out parsedObject, out errorMessages, true);
            Assert.IsFalse(valid);
            Assert.IsTrue(errorMessages.Count > 0);

            try
            {
                this.Parse(value, true);
                Assert.Fail("Expected argument exception");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}