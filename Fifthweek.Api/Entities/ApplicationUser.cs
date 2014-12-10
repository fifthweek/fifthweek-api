using Microsoft.AspNet.Identity.EntityFramework;

namespace Fifthweek.Api.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ExampleWork { get; set; }
    }
}