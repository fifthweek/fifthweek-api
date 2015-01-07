using System;

namespace Fifthweek.Api.Core.Tests
{
    using System.Collections.Generic;

    [AutoEqualityMembers, AutoConstructor, AutoBuilder]
    public partial class ClassAugmentationDummy
    {
        public Guid SomeGuid { get; private set; }

        public int SomeInt { get; private set; }

        [Optional]
        public Guid? OptionalGuid { get; private set; }

        [Optional]
        public int? OptionalInt { get; private set; }

        public string SomeString { get; private set; }

        [Optional]
        public string OptionalString { get; private set; }

        public IEnumerable<string> SomeCollection { get; private set; }

        [Optional]
        public IEnumerable<string> OptionalCollection { get; private set; }

        public ComplexType SomeComplexType { get; private set; }

        [Optional]
        public ComplexType OptionalComplexType { get; private set; }

        [AutoEqualityMembers, AutoConstructor]
        public partial class ComplexType
        {
            // It's not so complex...
            public string Value { get; private set; }
        }
    }
}
