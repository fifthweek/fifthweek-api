namespace Fifthweek.Api.Identity.Tests
{
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// This is required as the constructor for IdentityResult that indicates success is protected.
    /// </summary>
    public class MockIdentityResult : IdentityResult
    {
        public MockIdentityResult()
            : base(true)
        {
        }

        public MockIdentityResult(params string[] errors)
            : base(errors)
        {
        }
    }
}