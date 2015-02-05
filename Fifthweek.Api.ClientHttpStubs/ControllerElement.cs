namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ControllerElement
    {
        public string Name { get; private set; }

        public IReadOnlyList<MethodElement> Methods { get; private set; }
    }
}
