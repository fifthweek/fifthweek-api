namespace Fifthweek.Api.Core
{
    using System;

    public interface IGuidCreator
    {
        Guid Create();

        Guid CreateSqlSequential();

        Guid CreateClrSequential();
    }
}