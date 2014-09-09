namespace Dexter.Api
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Register()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
    }
}