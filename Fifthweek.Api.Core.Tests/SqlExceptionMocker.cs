namespace Fifthweek.Api.Core.Tests
{
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// https://gist.github.com/timabell/672719c63364c497377f
    /// Workaround completely test-unfriendly sql error classes.
    /// Copy-paste from http://stackoverflow.com/a/1387030/10245
    /// Adjusted with updates in comments
    /// </summary>
    class SqlExceptionMocker
    {
        private static T Construct<T>(params object[] p)
        {
            return (from ctor in typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                    where ctor.GetParameters().Count() == p.Count()
                    select (T)ctor.Invoke(p)).Single();
        }

        public static SqlException MakeSqlException(int errorNumber)
        {

            var collection = Construct<SqlErrorCollection>();
            var error = Construct<SqlError>(errorNumber, (byte)2, (byte)3, "server name", "This is a Mock-SqlException", "proc", 100);

            typeof(SqlErrorCollection)
                .GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(collection, new object[] { error });


            var e = typeof(SqlException)
                .GetMethod("CreateException", BindingFlags.NonPublic | BindingFlags.Static, null, CallingConventions.ExplicitThis, new[] { typeof(SqlErrorCollection), typeof(string) }, new ParameterModifier[] { })
                .Invoke(null, new object[] { collection, "7.0.0" }) as SqlException;

            return e;
        }
    }
}