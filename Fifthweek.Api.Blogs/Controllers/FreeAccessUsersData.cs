namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class FreeAccessUsersData
    {
        public List<string> Emails { get; set; }
    }
}