namespace Fifthweek.Api.Core
{
    using System.Web;

    public interface IRequestContext
    {
        HttpContext HttpContext { get; }
    }
}