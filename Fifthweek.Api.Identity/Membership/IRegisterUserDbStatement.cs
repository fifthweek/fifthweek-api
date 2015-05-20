namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IRegisterUserDbStatement
    {
        Task ExecuteAsync(
            UserId userId, 
            Username username,
            Email email,
            string exampleWork,
            CreatorName name,
            Password password,
            DateTime timeStamp);
    }
}