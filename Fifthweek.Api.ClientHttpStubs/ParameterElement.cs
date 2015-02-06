namespace Fifthweek.Api.ClientHttpStubs
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ParameterElement
    {
        public string Name { get; private set; }

        public Type Type { get; private set; }

        public bool IsRequired { get; private set; }
    }
}