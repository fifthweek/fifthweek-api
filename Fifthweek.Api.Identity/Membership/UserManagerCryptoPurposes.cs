namespace Fifthweek.Api.Identity.Membership
{
    public static class UserManagerDataProtectorPurposes
    {
        // This is internal detail from ASP.NET UserManager.cs, available on GitHub:
        // https://github.com/aspnet/Identity/src/Microsoft.AspNet.Identity/UserManager.cs
        public const string ResetPassword = "ResetPassword";
    }
}