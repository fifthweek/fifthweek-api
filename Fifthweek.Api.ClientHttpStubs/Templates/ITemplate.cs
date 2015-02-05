namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using Microsoft.VisualStudio.TextTemplating;

    public interface ITemplate
    {
        void Render(ApiGraph api, TextTransformation output);
    }
}