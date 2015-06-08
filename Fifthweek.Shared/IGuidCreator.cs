namespace Fifthweek.Shared
{
    using System;

    public interface IGuidCreator
    {
        Guid Create();

        Guid CreateSqlSequential();

        Guid CreateClrSequential();
    }
}