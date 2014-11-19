namespace Fifthweek.Api.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Entities;

    public class GetAllRefreshTokensQuery : IQuery<List<RefreshToken>>
    {
        public static readonly GetAllRefreshTokensQuery Default = new GetAllRefreshTokensQuery();
    }
}