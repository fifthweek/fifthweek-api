namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class ClassAugmentationParsingDummy
    {
        public int NotStrongTyped { get; set; }

        #region Constructed Nullable Strings

        [Constructed(typeof(ConstructedNullableString), TypeAcceptsNull = true)]
        public string SomeConstructedNullableString { get; set; }

        [Optional]
        [Constructed(typeof(ConstructedNullableString), TypeAcceptsNull = true)]
        public string OptionalConstructedNullableString { get; set; }

        #endregion

        #region Constructed Non Nullable Strings

        [Constructed(typeof(ConstructedNonNullableString))]
        public string SomeConstructedNonNullableString { get; set; }

        [Optional]
        [Constructed(typeof(ConstructedNonNullableString))]
        public string OptionalConstructedNonNullableString { get; set; }

        #endregion

        #region Parsed Strings

        [Parsed(typeof(ParsedString))]
        public string SomeParsedString { get; set; }

        [Optional]
        [Parsed(typeof(ParsedString))]
        public string OptionalParsedString { get; set; }

        #endregion


        #region Constructed Integers

        [Constructed(typeof(ConstructedInt))]
        public int SomeConstructedInt { get; set; }

        [Optional]
        [Constructed(typeof(ConstructedInt))]
        public int? OptionalConstructedInt { get; set; }

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
