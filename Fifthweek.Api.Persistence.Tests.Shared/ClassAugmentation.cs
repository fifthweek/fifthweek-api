using System;
using System.Linq;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using Fifthweek.Api.Persistence.Migrations;
    using Fifthweek.Shared;
    using System.Data.Entity;
    using Fifthweek.CodeGeneration;
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
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using Fifthweek.Api.Persistence.Migrations;
    using Fifthweek.Shared;
    using System.Data.Entity;
    using Fifthweek.CodeGeneration;
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


