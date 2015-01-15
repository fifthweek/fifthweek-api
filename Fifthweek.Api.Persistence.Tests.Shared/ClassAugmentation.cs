using System;
using System.Linq;



namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	public partial class TableBeforeAndAfter 
	{
        public TableBeforeAndAfter(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Persistence.IIdentityEquatable> snapshot, 
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Persistence.IIdentityEquatable> database)
        {
            if (snapshot == null)
            {
                throw new ArgumentNullException("snapshot");
            }

            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.Snapshot = snapshot;
            this.Database = database;
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
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	using Fifthweek.Api.Persistence.Migrations;
	public partial class TemporaryDatabase 
	{
        public TemporaryDatabase(
            System.String connectionString, 
            System.Threading.Tasks.Task databaseInitialization)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (databaseInitialization == null)
            {
                throw new ArgumentNullException("databaseInitialization");
            }

            this.connectionString = connectionString;
            this.databaseInitialization = databaseInitialization;
        }
	}

}
namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Data.Common;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class TemporaryDatabaseSeed 
	{
        public TemporaryDatabaseSeed(
            System.Func<Fifthweek.Api.Persistence.IFifthweekDbContext> databaseContextFactory)
        {
            if (databaseContextFactory == null)
            {
                throw new ArgumentNullException("databaseContextFactory");
            }

            this.databaseContextFactory = databaseContextFactory;
        }
	}

}
namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	public partial class TemporaryDatabaseState 
	{
        public TemporaryDatabaseState(
            Fifthweek.Api.Persistence.Tests.Shared.TemporaryDatabase temporaryDatabase)
        {
            if (temporaryDatabase == null)
            {
                throw new ArgumentNullException("temporaryDatabase");
            }

            this.temporaryDatabase = temporaryDatabase;
        }
	}

}



