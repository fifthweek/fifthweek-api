using System;
using System.Linq;

//// Generated on 08/06/2015 17:11:49 (UTC)
//// Mapped solution in 13.6s


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

    public partial class CreateSnapshotMessage 
    {
        public CreateSnapshotMessage(
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Payments.SnapshotCreation.SnapshotType snapshotType)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

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
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Azure;
    using Dapper;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Shared;

    public partial class RequestSnapshotService 
    {
        public RequestSnapshotService(
            IQueueService queueService)
        {
            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            this.queueService = queueService;
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


