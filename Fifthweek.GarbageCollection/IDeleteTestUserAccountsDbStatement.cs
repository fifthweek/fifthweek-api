namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Threading.Tasks;

    public interface IDeleteTestUserAccountsDbStatement
    {
        Task ExecuteAsync(DateTime endTimeExclusive);
    }
}