using System;
using System.Linq;

//// Generated on 11/02/2015 19:09:08 (UTC)
//// Mapped solution in 4.99s


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
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence.Migrations;
    using Fifthweek.CodeGeneration;

    public partial class TestDatabase 
    {
        public TestDatabase(
            Fifthweek.Api.Persistence.FifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}



