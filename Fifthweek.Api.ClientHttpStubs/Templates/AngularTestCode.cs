namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TextTemplating;

    public class AngularTestCode : ITemplate
    {
        public void Render(ApiGraph api, TextTransformation output)
        {
            api.AssertNotNull("api");
            output.AssertNotNull("output");

            new AngularTestCodeRenderingSession(api, output).Run();
        }
    }
}