using System;
using System.Linq;



namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Threading.Tasks;
	using System.Transactions;
	using Fifthweek.Api.Core;
	public partial class PersistenceTestsBase
	{
		public partial class TestDatabaseContext 
		{
        public TestDatabaseContext(
            Fifthweek.Api.Persistence.Tests.Shared.TestDatabase testDatabase, 
            Fifthweek.Api.Persistence.Tests.Shared.TestDatabaseSnapshot testDatabaseSnapshot)
        {
            if (testDatabase == null)
            {
                throw new ArgumentNullException("testDatabase");
            }

            if (testDatabaseSnapshot == null)
            {
                throw new ArgumentNullException("testDatabaseSnapshot");
            }

            this.testDatabase = testDatabase;
            this.testDatabaseSnapshot = testDatabaseSnapshot;
        }
		}

		}
}
namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.Migrations;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using System.Transactions;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Migrations;
	public partial class TestDatabase 
	{
        public TestDatabase(
            System.String connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            this.connectionString = connectionString;
        }
	}

}



