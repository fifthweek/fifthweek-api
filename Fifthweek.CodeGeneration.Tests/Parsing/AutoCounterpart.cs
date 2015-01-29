namespace Fifthweek.CodeGeneration.Tests.Parsing
{
    using System.Collections.Generic;

    [AutoEqualityMembers]
    public partial class AutoCounterpart
    {
        public string SomeWeaklyTypedString { get; set; }

        [Optional]
        public string OptionalWeaklyTypedString { get; set; }

        [Parsed(typeof(ParsedString))]
        public string SomeParsedString { get; set; }

        [Optional]
        [Parsed(typeof(ParsedString))]
        public string OptionalParsedString { get; set; }

        [Parsed(typeof(ParsedNormalizedString))]
        public string SomeParsedNormalizedString { get; set; }

        [Optional]
        [Parsed(typeof(ParsedNormalizedString))]
        public string OptionalParsedNormalizedString { get; set; }

        public int SomeWeaklyTypedInt { get; set; }

        [Optional]
        public int? OptionalWeaklyTypedInt { get; set; }

        [Parsed(typeof(ParsedInt))]
        public int SomeParsedInt { get; set; }

        [Optional]
        [Parsed(typeof(ParsedInt))]
        public int? OptionalParsedInt { get; set; }

        [ParsedElements(typeof(ParsedInt))]
        public List<int> SomeParsedIntList { get; set; }

        [Optional]
        [ParsedElements(typeof(ParsedInt))]
        public List<int> OptionalParsedIntList { get; set; }

        [Parsed(typeof(ParsedCollection))]
        [ParsedElements(typeof(ParsedNormalizedString))]
        public List<string> SomeParsedCollection { get; set; }

        [Optional]
        [Parsed(typeof(ParsedCollection))]
        [ParsedElements(typeof(ParsedNormalizedString))]
        public List<string> OptionalParsedCollection { get; set; }
    }
}
