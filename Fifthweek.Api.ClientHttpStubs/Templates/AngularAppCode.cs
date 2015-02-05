namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TextTemplating;

    public class AngularAppCode : ITemplate
    {
        public void Render(ApiGraph api, TextTransformation output)
        {
            api.AssertNotNull("api");
            output.AssertNotNull("output");

            new AngularAppCodeRenderingSession(api, output).Run();
        }
    }
}