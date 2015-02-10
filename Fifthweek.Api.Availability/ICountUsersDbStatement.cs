namespace Fifthweek.Api.Availability
{
    using System.Threading.Tasks;

    public interface ICountUsersDbStatement
    {
        Task<int> ExecuteAsync(); 
    }
}