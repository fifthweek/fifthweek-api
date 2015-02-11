namespace Fifthweek.Api.Persistence
{
    using System.Data.Common;

    public interface IFifthweekDbConnectionFactory
    {
        DbConnection CreateConnection();

        FifthweekDbContext CreateContext();
    }
}