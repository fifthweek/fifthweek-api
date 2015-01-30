namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RemoveRefreshTokenDbStatement : IRemoveRefreshTokenDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task ExecuteAsync(HashedRefreshTokenId hashedTokenId)
        {
            hashedTokenId.AssertNotNull("hashedTokenId");

            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                string.Format(@"DELETE FROM {0} WHERE {1}=@{1}", RefreshToken.Table, RefreshToken.Fields.HashedId),
                new { HashedId = hashedTokenId.Value });
        }
    }
}