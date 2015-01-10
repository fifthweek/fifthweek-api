
using System;





namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.Migrations;
	using System.Diagnostics;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	using Fifthweek.Api.Persistence.Migrations;
	using System.Threading.Tasks;
	public partial class TemporaryDatabase 
	{
        public TemporaryDatabase(
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
namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.Migrations;
	using System.Diagnostics;
	using System.Linq;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	using Fifthweek.Api.Persistence.Migrations;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Data.Entity;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	public partial class DatabaseState 
	{
        public DatabaseState(
            Fifthweek.Api.Persistence.Tests.Shared.TemporaryDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.database = database;
        }
	}

}
namespace Fifthweek.Api.Persistence.Tests.Shared
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Fifthweek.Api.Core;
	public partial class DatabaseSeed 
	{
        public DatabaseSeed(
            Fifthweek.Api.Persistence.Tests.Shared.TemporaryDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.database = database;
        }
	}

}


