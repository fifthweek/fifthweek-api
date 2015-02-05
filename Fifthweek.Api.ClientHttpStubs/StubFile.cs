namespace Fifthweek.Api.ClientHttpStubs
{
    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.ClientHttpStubs.Reflection;
    using Fifthweek.Api.ClientHttpStubs.Templates;
    using Fifthweek.CodeGeneration;

    using Microsoft.VisualStudio.TextTemplating;

    [AutoConstructor]
    public partial class StubFile
    {
        private readonly TextTransformation output;
        private readonly ITemplate template;

        public void Render()
        {
            var controllerTypes = new ControllerSource(FifthweekAssembliesResolver.Assemblies).GetControllerTypes();
            var api = new ApiGraphBuilder().Build(controllerTypes);
            this.template.Render(api, this.output);
        }
    }
}