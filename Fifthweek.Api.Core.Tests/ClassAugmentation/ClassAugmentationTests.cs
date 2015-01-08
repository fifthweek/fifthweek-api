using System;
using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    [TestClass]
    public class ClassAugmentationDummyTests : ImmutableComplexTypeTests<ClassAugmentation.ClassAugmentationDummy, ClassAugmentation.ClassAugmentationDummy.Builder>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseIndividualPropertyChanges()
        {
            this.AssertDifference(_ => _.SomeGuid = Guid.NewGuid());
            this.AssertDifference(_ => _.OptionalGuid = Guid.NewGuid());
            this.AssertDifference(_ => _.OptionalGuid = null);

            this.AssertDifference(_ => _.SomeInt = 123);
            this.AssertDifference(_ => _.OptionalInt = 123);
            this.AssertDifference(_ => _.OptionalInt = null);

            this.AssertDifference(_ => _.SomeString = "different");
            this.AssertDifference(_ => _.OptionalString = "different");
            this.AssertDifference(_ => _.OptionalString = null);

            this.AssertDifference(_ => _.SomeCollection = CollectionB);
            this.AssertDifference(_ => _.OptionalCollection = CollectionB);
            this.AssertDifference(_ => _.OptionalCollection = null);

            this.AssertDifference(_ => _.SomeComplexType = new ClassAugmentation.ClassAugmentationDummy.ComplexType("different"));
            this.AssertDifference(_ => _.OptionalComplexType = new ClassAugmentation.ClassAugmentationDummy.ComplexType("different"));
            this.AssertDifference(_ => _.OptionalComplexType = null);
        }

        [TestMethod]
        public void ItShouldRequirePropertiesByDefault()
        {
            this.AssertRequired(_ => _.SomeString = null);
            this.AssertRequired(_ => _.SomeCollection = null);
            this.AssertRequired(_ => _.SomeComplexType = null);
        }

        [TestMethod]
        public void ItShouldNotRequirePropertiesMarkedOptional()
        {
            this.AssertOptional(_ => _.OptionalGuid = null);
            this.AssertOptional(_ => _.OptionalInt = null);
            this.AssertOptional(_ => _.OptionalString = null);
            this.AssertOptional(_ => _.OptionalCollection = null);
            this.AssertOptional(_ => _.OptionalComplexType = null);
        }

        protected override ClassAugmentation.ClassAugmentationDummy.Builder NewInstanceOfBuilderForObjectA()
        {
            return new ClassAugmentation.ClassAugmentationDummy.Builder
            {
                SomeGuid = Guid.Parse("{34FAA659-9A26-4068-8EF9-3986F0E6ECBC}"),
                SomeInt = 75,
                OptionalGuid = Guid.Parse("{2088F70B-5D64-4A17-B8D2-381F8DE0C271}"),
                OptionalInt = 52,
                SomeString = "Hello",
                OptionalString = "World",
                SomeCollection = CollectionA,
                OptionalCollection = CollectionA,
                SomeComplexType = new ClassAugmentation.ClassAugmentationDummy.ComplexType("Foo"),
                OptionalComplexType = new ClassAugmentation.ClassAugmentationDummy.ComplexType("Bar"),
            };
        }

        protected override ClassAugmentation.ClassAugmentationDummy FromBuilder(ClassAugmentation.ClassAugmentationDummy.Builder builder)
        {
            return builder.Build();
        }

        // We do not currently have a way of checking equality on collections. Need to consider external classes mutating those collections too.
        // Ultimately it needs to be an implementation.
        private static readonly IEnumerable<string> CollectionA = new[] {"Yay", "This is fun!"};
        private static readonly IEnumerable<string> CollectionB = new[] { "yep", "Sure is :P" };
    }
}