namespace Fifthweek.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class ValidatedCollectionTests<TParsed, TRaw> : PrimitiveTests<TParsed>
    {
        protected abstract IReadOnlyList<TRaw> ValueA { get; }

        protected abstract IReadOnlyList<TRaw> ValueB { get; }

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

        protected abstract TParsed Parse(IReadOnlyList<TRaw> value);

        protected abstract bool TryParse(IReadOnlyList<TRaw> value, out TParsed parsedObject);

        protected abstract bool TryParse(IReadOnlyList<TRaw> value, out TParsed parsedObject, out IReadOnlyCollection<string> errorMessages);

        protected abstract IReadOnlyList<TRaw> GetValue(TParsed parsedObject);

        protected void GoodValue(IReadOnlyList<TRaw> value)
        {
            this.GoodValue(value, value);
        }

        protected void GoodNonExactValue(IReadOnlyList<TRaw> value, IReadOnlyList<TRaw> exactValue)
        {
            this.GoodValue(value, exactValue);
        }

        protected void BadValue(IReadOnlyList<TRaw> value)
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

        private void GoodValue(IReadOnlyList<TRaw> actualValue, IReadOnlyList<TRaw> expectedValue)
        {
            TParsed parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(actualValue, out parsedObject);
            Assert.IsTrue(valid, string.Format("Value expected to be valid: '{0}'", actualValue));
            CollectionAssert.AreEqual(expectedValue.ToArray(), this.GetValue(parsedObject).ToArray());

            valid = this.TryParse(actualValue, out parsedObject, out errorMessages);
            Assert.IsTrue(valid);
            CollectionAssert.AreEqual(expectedValue.ToArray(), this.GetValue(parsedObject).ToArray());
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(actualValue);
            CollectionAssert.AreEqual(expectedValue.ToArray(), this.GetValue(parsedObject).ToArray());
        }
    }
}