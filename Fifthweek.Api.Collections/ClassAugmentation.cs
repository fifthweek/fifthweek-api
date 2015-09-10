using System;
using System.Linq;

//// Generated on 10/09/2015 17:40:41 (UTC)
//// Mapped solution in 21.99s


namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class QueueOwnership 
    {
        public QueueOwnership(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class QueueSecurity 
    {
        public QueueSecurity(
            Fifthweek.Api.Collections.IQueueOwnership queueOwnership)
        {
            if (queueOwnership == null)
            {
                throw new ArgumentNullException("queueOwnership");
            }

            this.queueOwnership = queueOwnership;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class CreateQueueCommand 
    {
        public CreateQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.QueueId newQueueId,
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            Fifthweek.Api.Collections.Shared.ValidQueueName name,
            Fifthweek.Api.Collections.Shared.HourOfWeek initialWeeklyReleaseTime)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newQueueId == null)
            {
                throw new ArgumentNullException("newQueueId");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (initialWeeklyReleaseTime == null)
            {
                throw new ArgumentNullException("initialWeeklyReleaseTime");
            }

            this.Requester = requester;
            this.NewQueueId = newQueueId;
            this.BlogId = blogId;
            this.Name = name;
            this.InitialWeeklyReleaseTime = initialWeeklyReleaseTime;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class CreateQueueCommandHandler 
    {
        public CreateQueueCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Blogs.Shared.IBlogSecurity blogSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (blogSecurity == null)
            {
                throw new ArgumentNullException("blogSecurity");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.requesterSecurity = requesterSecurity;
            this.blogSecurity = blogSecurity;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateQueueCommand 
    {
        public UpdateQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Collections.Shared.ValidQueueName name,
            Fifthweek.Api.Collections.Shared.WeeklyReleaseSchedule weeklyReleaseSchedule)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (weeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("weeklyReleaseSchedule");
            }

            this.Requester = requester;
            this.QueueId = queueId;
            this.Name = name;
            this.WeeklyReleaseSchedule = weeklyReleaseSchedule;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class QueueController 
    {
        public QueueController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Collections.Commands.CreateQueueCommand> createQueue,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Collections.Commands.UpdateQueueCommand> updateQueue,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Collections.Commands.DeleteQueueCommand> deleteQueue,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Collections.Queries.GetLiveDateOfNewQueuedPostQuery,System.DateTime> getLiveDateOfNewQueuedPost,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Shared.IRandom random)
        {
            if (createQueue == null)
            {
                throw new ArgumentNullException("createQueue");
            }

            if (updateQueue == null)
            {
                throw new ArgumentNullException("updateQueue");
            }

            if (deleteQueue == null)
            {
                throw new ArgumentNullException("deleteQueue");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (random == null)
            {
                throw new ArgumentNullException("random");
            }

            this.createQueue = createQueue;
            this.updateQueue = updateQueue;
            this.deleteQueue = deleteQueue;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
            this.random = random;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewQueueData 
    {
        public NewQueueData(
            Fifthweek.Api.Blogs.Shared.BlogId blogId,
            System.String name)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.BlogId = blogId;
            this.Name = name;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedQueueData 
    {
        public UpdatedQueueData(
            System.String name,
            System.Collections.Generic.List<System.Int32> weeklyReleaseSchedule)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (weeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("weeklyReleaseSchedule");
            }

            this.Name = name;
            this.WeeklyReleaseSchedule = weeklyReleaseSchedule;
        }
    }
}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class GetWeeklyReleaseScheduleDbStatement 
    {
        public GetWeeklyReleaseScheduleDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class GetLiveDateOfNewQueuedPostDbStatement 
    {
        public GetLiveDateOfNewQueuedPostDbStatement(
            Fifthweek.Api.Collections.IGetNewQueuedPostLiveDateLowerBoundDbStatement getNewQueuedPostLiveDateLowerBound,
            Fifthweek.Api.Collections.Shared.IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule,
            Fifthweek.Api.Collections.IQueuedPostLiveDateCalculator queuedPostLiveDateCalculator)
        {
            if (getNewQueuedPostLiveDateLowerBound == null)
            {
                throw new ArgumentNullException("getNewQueuedPostLiveDateLowerBound");
            }

            if (getWeeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("getWeeklyReleaseSchedule");
            }

            if (queuedPostLiveDateCalculator == null)
            {
                throw new ArgumentNullException("queuedPostLiveDateCalculator");
            }

            this.getNewQueuedPostLiveDateLowerBound = getNewQueuedPostLiveDateLowerBound;
            this.getWeeklyReleaseSchedule = getWeeklyReleaseSchedule;
            this.queuedPostLiveDateCalculator = queuedPostLiveDateCalculator;
        }
    }
}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class GetNewQueuedPostLiveDateLowerBoundDbStatement 
    {
        public GetNewQueuedPostLiveDateLowerBoundDbStatement(
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
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Fifthweek.Shared;

    public partial class GetLiveDateOfNewQueuedPostQuery 
    {
        public GetLiveDateOfNewQueuedPostQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.QueueId queueId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            this.Requester = requester;
            this.QueueId = queueId;
        }
    }
}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Fifthweek.Shared;

    public partial class GetLiveDateOfNewQueuedPostQueryHandler 
    {
        public GetLiveDateOfNewQueuedPostQueryHandler(
            Fifthweek.Api.Collections.Shared.IQueueSecurity queueSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Collections.Shared.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost)
        {
            if (queueSecurity == null)
            {
                throw new ArgumentNullException("queueSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            this.queueSecurity = queueSecurity;
            this.requesterSecurity = requesterSecurity;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateQueueCommandHandler 
    {
        public UpdateQueueCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Collections.Shared.IQueueSecurity queueSecurity,
            Fifthweek.Api.Collections.IUpdateQueueFieldsDbStatement updateQueueFields,
            Fifthweek.Api.Collections.IUpdateWeeklyReleaseScheduleDbStatement updateWeeklyReleaseSchedule)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (queueSecurity == null)
            {
                throw new ArgumentNullException("queueSecurity");
            }

            if (updateQueueFields == null)
            {
                throw new ArgumentNullException("updateQueueFields");
            }

            if (updateWeeklyReleaseSchedule == null)
            {
                throw new ArgumentNullException("updateWeeklyReleaseSchedule");
            }

            this.requesterSecurity = requesterSecurity;
            this.queueSecurity = queueSecurity;
            this.updateQueueFields = updateQueueFields;
            this.updateWeeklyReleaseSchedule = updateWeeklyReleaseSchedule;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class DeleteQueueCommand 
    {
        public DeleteQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.QueueId queueId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            this.Requester = requester;
            this.QueueId = queueId;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class DeleteQueueCommandHandler 
    {
        public DeleteQueueCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Collections.Shared.IQueueSecurity queueSecurity,
            Fifthweek.Api.Collections.IDeleteQueueDbStatement deleteQueue,
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (queueSecurity == null)
            {
                throw new ArgumentNullException("queueSecurity");
            }

            if (deleteQueue == null)
            {
                throw new ArgumentNullException("deleteQueue");
            }

            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            this.requesterSecurity = requesterSecurity;
            this.queueSecurity = queueSecurity;
            this.deleteQueue = deleteQueue;
            this.scheduleGarbageCollection = scheduleGarbageCollection;
        }
    }
}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DeleteQueueDbStatement 
    {
        public DeleteQueueDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class DefragmentQueueDbStatement 
    {
        public DefragmentQueueDbStatement(
            Fifthweek.Api.Collections.IGetQueueSizeDbStatement getQueueSize,
            Fifthweek.Api.Collections.IQueuedPostLiveDateCalculator liveDateCalculator,
            Fifthweek.Api.Collections.IUpdateAllLiveDatesInQueueDbStatement updateAllLiveDatesInQueue)
        {
            if (getQueueSize == null)
            {
                throw new ArgumentNullException("getQueueSize");
            }

            if (liveDateCalculator == null)
            {
                throw new ArgumentNullException("liveDateCalculator");
            }

            if (updateAllLiveDatesInQueue == null)
            {
                throw new ArgumentNullException("updateAllLiveDatesInQueue");
            }

            this.getQueueSize = getQueueSize;
            this.liveDateCalculator = liveDateCalculator;
            this.updateAllLiveDatesInQueue = updateAllLiveDatesInQueue;
        }
    }
}
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class GetQueueSizeDbStatement 
    {
        public GetQueueSizeDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class UpdateAllLiveDatesInQueueDbStatement 
    {
        public UpdateAllLiveDatesInQueueDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class ReplaceWeeklyReleaseTimesDbStatement 
    {
        public ReplaceWeeklyReleaseTimesDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class UpdateQueueFieldsDbStatement 
    {
        public UpdateQueueFieldsDbStatement(
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
namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections.Shared;
    using System.Collections.Generic;
    using System.Text;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using System.Transactions;
    using Fifthweek.Shared;

    public partial class UpdateWeeklyReleaseScheduleDbStatement 
    {
        public UpdateWeeklyReleaseScheduleDbStatement(
            Fifthweek.Api.Collections.IReplaceWeeklyReleaseTimesDbStatement replaceWeeklyReleaseTimes,
            Fifthweek.Api.Collections.Shared.IDefragmentQueueDbStatement defragmentQueue)
        {
            if (replaceWeeklyReleaseTimes == null)
            {
                throw new ArgumentNullException("replaceWeeklyReleaseTimes");
            }

            if (defragmentQueue == null)
            {
                throw new ArgumentNullException("defragmentQueue");
            }

            this.replaceWeeklyReleaseTimes = replaceWeeklyReleaseTimes;
            this.defragmentQueue = defragmentQueue;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class QueueCreation 
    {
        public QueueCreation(
            Fifthweek.Api.Collections.Shared.QueueId queueId,
            Fifthweek.Api.Collections.Shared.HourOfWeek defaultWeeklyReleaseTime)
        {
            if (queueId == null)
            {
                throw new ArgumentNullException("queueId");
            }

            if (defaultWeeklyReleaseTime == null)
            {
                throw new ArgumentNullException("defaultWeeklyReleaseTime");
            }

            this.QueueId = queueId;
            this.DefaultWeeklyReleaseTime = defaultWeeklyReleaseTime;
        }
    }
}

namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class CreateQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateQueueCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewQueueId == null ? "null" : this.NewQueueId.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.InitialWeeklyReleaseTime == null ? "null" : this.InitialWeeklyReleaseTime.ToString());
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
        
            return this.Equals((CreateQueueCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewQueueId != null ? this.NewQueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.InitialWeeklyReleaseTime != null ? this.InitialWeeklyReleaseTime.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.NewQueueId, other.NewQueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.InitialWeeklyReleaseTime, other.InitialWeeklyReleaseTime))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class UpdateQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdateQueueCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.WeeklyReleaseSchedule == null ? "null" : this.WeeklyReleaseSchedule.ToString());
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
        
            return this.Equals((UpdateQueueCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WeeklyReleaseSchedule != null ? this.WeeklyReleaseSchedule.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdateQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.WeeklyReleaseSchedule, other.WeeklyReleaseSchedule))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewQueueData 
    {
        public override string ToString()
        {
            return string.Format("NewQueueData({0}, \"{1}\")", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString());
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
        
            return this.Equals((NewQueueData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(NewQueueData other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedQueueData 
    {
        public override string ToString()
        {
            return string.Format("UpdatedQueueData(\"{0}\", {1})", this.Name == null ? "null" : this.Name.ToString(), this.WeeklyReleaseSchedule == null ? "null" : this.WeeklyReleaseSchedule.ToString());
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
        
            return this.Equals((UpdatedQueueData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.WeeklyReleaseSchedule != null 
        			? this.WeeklyReleaseSchedule.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatedQueueData other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (this.WeeklyReleaseSchedule != null && other.WeeklyReleaseSchedule != null)
            {
                if (!this.WeeklyReleaseSchedule.SequenceEqual(other.WeeklyReleaseSchedule))
                {
                    return false;    
                }
            }
            else if (this.WeeklyReleaseSchedule != null || other.WeeklyReleaseSchedule != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Fifthweek.Shared;

    public partial class GetLiveDateOfNewQueuedPostQuery 
    {
        public override string ToString()
        {
            return string.Format("GetLiveDateOfNewQueuedPostQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString());
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
        
            return this.Equals((GetLiveDateOfNewQueuedPostQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetLiveDateOfNewQueuedPostQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using System.Transactions;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class DeleteQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("DeleteQueueCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.QueueId == null ? "null" : this.QueueId.ToString());
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
        
            return this.Equals((DeleteQueueCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeleteQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class QueueCreation 
    {
        public override string ToString()
        {
            return string.Format("QueueCreation({0}, {1})", this.QueueId == null ? "null" : this.QueueId.ToString(), this.DefaultWeeklyReleaseTime == null ? "null" : this.DefaultWeeklyReleaseTime.ToString());
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
        
            return this.Equals((QueueCreation)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.QueueId != null ? this.QueueId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.DefaultWeeklyReleaseTime != null ? this.DefaultWeeklyReleaseTime.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(QueueCreation other)
        {
            if (!object.Equals(this.QueueId, other.QueueId))
            {
                return false;
            }
        
            if (!object.Equals(this.DefaultWeeklyReleaseTime, other.DefaultWeeklyReleaseTime))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class NewQueueData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Blogs.Shared.BlogId blogId,
                ValidQueueName name)
            {
                if (blogId == null)
                {
                    throw new ArgumentNullException("blogId");
                }

                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                this.BlogId = blogId;
                this.Name = name;
            }
        
            public Fifthweek.Api.Blogs.Shared.BlogId BlogId { get; private set; }
        
            public ValidQueueName Name { get; private set; }
        }
    }

    public static partial class NewQueueDataExtensions
    {
        public static NewQueueData.Parsed Parse(this NewQueueData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidQueueName parsed0 = null;
            if (!ValidQueueName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidQueueName.TryParse(target.Name, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Name", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Name", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewQueueData.Parsed(
                target.BlogId,
                parsed0);
        }    
    }
}
namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using Fifthweek.Shared;
    using Fifthweek.Api.Blogs.Shared;

    public partial class UpdatedQueueData 
    {
        public class Parsed
        {
            public Parsed(
                ValidQueueName name,
                WeeklyReleaseSchedule weeklyReleaseSchedule)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (weeklyReleaseSchedule == null)
                {
                    throw new ArgumentNullException("weeklyReleaseSchedule");
                }

                this.Name = name;
                this.WeeklyReleaseSchedule = weeklyReleaseSchedule;
            }
        
            public ValidQueueName Name { get; private set; }
        
            public WeeklyReleaseSchedule WeeklyReleaseSchedule { get; private set; }
        }
    }

    public static partial class UpdatedQueueDataExtensions
    {
        public static UpdatedQueueData.Parsed Parse(this UpdatedQueueData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidQueueName parsed0 = null;
            if (!ValidQueueName.IsEmpty(target.Name))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidQueueName.TryParse(target.Name, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Name", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Name", modelState);
            }

            System.Collections.Generic.List<Fifthweek.Api.Collections.Shared.HourOfWeek> parsed1Buffer = null;
            if (target.WeeklyReleaseSchedule != null)
            {
                parsed1Buffer = new System.Collections.Generic.List<Fifthweek.Api.Collections.Shared.HourOfWeek>();
                for (var i = 0; i < target.WeeklyReleaseSchedule.Count; i++)
                {
                    Fifthweek.Api.Collections.Shared.HourOfWeek parsedElement = null;
                    System.Collections.Generic.IReadOnlyCollection<string> parsedElementErrors;
                    if (!Fifthweek.Api.Collections.Shared.HourOfWeek.TryParse(target.WeeklyReleaseSchedule[i], out parsedElement, out parsedElementErrors))
                    {
                        var modelState = new System.Web.Http.ModelBinding.ModelState();
                        foreach (var errorMessage in parsedElementErrors)
                        {
                            modelState.Errors.Add(errorMessage);
                        }

                        modelStateDictionary.Add("WeeklyReleaseSchedule[" + i + "]", modelState);
                    }

                    if (parsedElement != null)
                    {
                        parsed1Buffer.Add(parsedElement);
                    }
                }
            }

            WeeklyReleaseSchedule parsed1 = null;
            if (!WeeklyReleaseSchedule.IsEmpty(parsed1Buffer))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!WeeklyReleaseSchedule.TryParse(parsed1Buffer, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("WeeklyReleaseSchedule", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("WeeklyReleaseSchedule", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new UpdatedQueueData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}


