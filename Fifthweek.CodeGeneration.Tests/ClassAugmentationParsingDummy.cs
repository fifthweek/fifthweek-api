namespace Fifthweek.CodeGeneration.Tests
{
    [AutoEqualityMembers]
    public partial class ClassAugmentationParsingDummy
    {
        public int NotStrongTyped { get; set; }

        #region Parsed Strings

        [Parsed(typeof(ParsedString))]
        public string SomeParsedString { get; set; }

        [Optional]
        [Parsed(typeof(ParsedString))]
        public string OptionalParsedString { get; set; }

        #endregion

        #region Parsed Integers

        [Parsed(typeof(ParsedInt))]
        public int SomeParsedInt { get; set; }

        [Optional]
        [Parsed(typeof(ParsedInt))]
        public int OptionalParsedInt { get; set; }

        #endregion
    }
}
