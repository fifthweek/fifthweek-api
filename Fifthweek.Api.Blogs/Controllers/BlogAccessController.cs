namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("blogAccess"), AutoConstructor]
    public partial class BlogAccessController : ApiController
    {
        private readonly IRequesterContext requesterContext;
        private readonly ICommandHandler<UpdateFreeAccessUsersCommand> updateFreeAccessUsers;
        private readonly IQueryHandler<GetFreeAccessUsersQuery, GetFreeAccessUsersResult> getFreeAccessUsers;

        [Route("freeAccessList/{blogId}")]
        public async Task<PutFreeAccessUsersResult> PutFreeAccessList(string blogId, [FromBody]FreeAccessUsersData data)
        {
            blogId.AssertUrlParameterProvided("blogId");
            data.AssertBodyProvided("blogData");

            if (data.Emails == null)
            {
                data.Emails = new List<string>();
            }

            var validEmails = new List<ValidEmail>();
            var invalidEmails = new List<Email>();
            foreach (var email in data.Emails)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    continue;
                }

                ValidEmail validEmail;
                if (ValidEmail.TryParse(email, out validEmail))
                {
                    validEmails.Add(validEmail);
                }
                else
                {
                    invalidEmails.Add(new Email(email));
                }
            }

            var requester = this.requesterContext.GetRequester();
            var blogIdObject = new BlogId(blogId.DecodeGuid());

            await this.updateFreeAccessUsers.HandleAsync(new UpdateFreeAccessUsersCommand(
                requester,
                blogIdObject,
                validEmails));

            return new PutFreeAccessUsersResult(invalidEmails);
        }

        [Route("freeAccessList/{blogId}")]
        public async Task<GetFreeAccessUsersResult> GetFreeAccessList(string blogId)
        {
            blogId.AssertUrlParameterProvided("blogId");

            var requester = this.requesterContext.GetRequester();
            var blogIdObject = new BlogId(blogId.DecodeGuid());
            return await this.getFreeAccessUsers.HandleAsync(new GetFreeAccessUsersQuery(requester, blogIdObject));
        }
    }
}