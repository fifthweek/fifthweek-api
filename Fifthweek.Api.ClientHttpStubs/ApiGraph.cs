namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ApiGraph
    {
        public IReadOnlyList<ControllerElement> Controllers { get; private set; }
    }
}