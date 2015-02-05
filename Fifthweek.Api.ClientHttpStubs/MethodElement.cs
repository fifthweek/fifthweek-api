namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class MethodElement
    {
        public HttpMethod HttpMethod { get; private set; }

        public string Name { get; private set; }

        public string Route { get; private set; }

        public IReadOnlyList<ParameterElement> UrlParameters { get; private set; }

        [Optional]
        public ParameterElement BodyParameter { get; private set; }

        public IReadOnlyList<ParameterElement> GetAllParameters()
        {
            return this.BodyParameter == null 
                ? this.UrlParameters 
                : this.UrlParameters.Concat(new[] { this.BodyParameter }).ToArray();
        }
    }
}