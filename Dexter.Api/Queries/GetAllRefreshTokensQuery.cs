namespace Dexter.Api.Queries
{
    using System.Collections.Generic;

    using Dexter.Api.Entities;

    public class GetAllRefreshTokensQuery : IQuery<List<RefreshToken>>
    {
        public static readonly GetAllRefreshTokensQuery Default = new GetAllRefreshTokensQuery();
    }
}