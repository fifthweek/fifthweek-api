namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IReservedUsernameService
    {
        bool IsReserved(ValidUsername username);

        void AssertNotReserved(ValidUsername username);
    }
}