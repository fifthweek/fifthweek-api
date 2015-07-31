using System;
using System.Linq;

//// Generated on 31/07/2015 14:33:12 (UTC)
//// Mapped solution in 16.45s


namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateCreatorChannelsSnapshotDbStatement 
    {
        public CreateCreatorChannelsSnapshotDbStatement(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.SnapshotCreation.ISnapshotTimestampCreator timestampCreator,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.guidCreator = guidCreator;
            this.timestampCreator = timestampCreator;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateCreatorFreeAccessUsersSnapshotDbStatement 
    {
        public CreateCreatorFreeAccessUsersSnapshotDbStatement(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.SnapshotCreation.ISnapshotTimestampCreator timestampCreator,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.guidCreator = guidCreator;
            this.timestampCreator = timestampCreator;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateSnapshotMessage 
    {
        public CreateSnapshotMessage(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Payments.SnapshotCreation.SnapshotType snapshotType)
        {
            if (snapshotType == null)
            {
                throw new ArgumentNullException("snapshotType");
            }

            this.UserId = userId;
            this.SnapshotType = snapshotType;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateSubscriberChannelsSnapshotDbStatement 
    {
        public CreateSubscriberChannelsSnapshotDbStatement(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.SnapshotCreation.ISnapshotTimestampCreator timestampCreator,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.guidCreator = guidCreator;
            this.timestampCreator = timestampCreator;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateSubscriberSnapshotDbStatement 
    {
        public CreateSubscriberSnapshotDbStatement(
            Fifthweek.Payments.SnapshotCreation.ISnapshotTimestampCreator timestampCreator,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.timestampCreator = timestampCreator;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class RequestSnapshotService 
    {
        public RequestSnapshotService(
            Fifthweek.Azure.IQueueService queueService)
        {
            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            this.queueService = queueService;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;

    public partial class CreateAllSnapshotsProcessor 
    {
        public CreateAllSnapshotsProcessor(
            Fifthweek.Payments.SnapshotCreation.IGetAllStandardUsersDbStatement getAllStandardUsers,
            Fifthweek.Payments.SnapshotCreation.ICreateSnapshotMultiplexer createSnapshotMultiplexer)
        {
            if (getAllStandardUsers == null)
            {
                throw new ArgumentNullException("getAllStandardUsers");
            }

            if (createSnapshotMultiplexer == null)
            {
                throw new ArgumentNullException("createSnapshotMultiplexer");
            }

            this.getAllStandardUsers = getAllStandardUsers;
            this.createSnapshotMultiplexer = createSnapshotMultiplexer;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    public partial class CreateSnapshotMultiplexer 
    {
        public CreateSnapshotMultiplexer(
            Fifthweek.Payments.SnapshotCreation.ICreateSubscriberSnapshotDbStatement createSubscriberSnapshot,
            Fifthweek.Payments.SnapshotCreation.ICreateSubscriberChannelsSnapshotDbStatement createSubscriberChannelsSnapshot,
            Fifthweek.Payments.SnapshotCreation.ICreateCreatorChannelsSnapshotDbStatement createCreatorChannelsSnapshot,
            Fifthweek.Payments.SnapshotCreation.ICreateCreatorFreeAccessUsersSnapshotDbStatement createCreatorFreeAccessUsersSnapshot)
        {
            if (createSubscriberSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberSnapshot");
            }

            if (createSubscriberChannelsSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberChannelsSnapshot");
            }

            if (createCreatorChannelsSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorChannelsSnapshot");
            }

            if (createCreatorFreeAccessUsersSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorFreeAccessUsersSnapshot");
            }

            this.createSubscriberSnapshot = createSubscriberSnapshot;
            this.createSubscriberChannelsSnapshot = createSubscriberChannelsSnapshot;
            this.createCreatorChannelsSnapshot = createCreatorChannelsSnapshot;
            this.createCreatorFreeAccessUsersSnapshot = createCreatorFreeAccessUsersSnapshot;
        }
    }
}
namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    public partial class GetAllStandardUsersDbStatement 
    {
        public GetAllStandardUsersDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}

namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Azure;

    public partial class CreateSnapshotMessage 
    {
        public override string ToString()
        {
            return string.Format("CreateSnapshotMessage({0}, {1})", this.UserId == null ? "null" : this.UserId.ToString(), this.SnapshotType == null ? "null" : this.SnapshotType.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreateSnapshotMessage)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SnapshotType != null ? this.SnapshotType.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateSnapshotMessage other)
        {
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.SnapshotType, other.SnapshotType))
            {
                return false;
            }
        
            return true;
        }
    }
}


