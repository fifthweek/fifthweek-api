using System;
using System.Linq;

//// Generated on 29/07/2015 18:52:44 (UTC)
//// Mapped solution in 14.09s

namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class AmountInMinorDenomination 
    {
        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (AmountInMinorDenomination)value;
                serializer.Serialize(writer, valueType.Value);
            }
        
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(AmountInMinorDenomination))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(AmountInMinorDenomination).Name, "objectType");
                }
        
                var value = serializer.Deserialize<System.Int32>(reader);
                return new AmountInMinorDenomination(value);
            }
        
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(AmountInMinorDenomination);
            }
        }
        
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<AmountInMinorDenomination>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<AmountInMinorDenomination>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, AmountInMinorDenomination value)
            {
                parameter.DbType = System.Data.DbType.Int32;
                parameter.Value = value.Value;
            }
        
            public override AmountInMinorDenomination Parse(object value)
            {
                return new AmountInMinorDenomination((System.Int32)value);
            }
        }
    }
}

namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class AggregateCostSummary 
    {
        public AggregateCostSummary(
            System.Decimal cost)
        {
            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class AmountInMinorDenomination 
    {
        public AmountInMinorDenomination(
            System.Int32 value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    public partial class CalculateCostPeriodsExecutor 
    {
        public CalculateCostPeriodsExecutor(
            Fifthweek.Payments.Pipeline.ICalculateSnapshotCostExecutor costCalculator)
        {
            if (costCalculator == null)
            {
                throw new ArgumentNullException("costCalculator");
            }

            this.costCalculator = costCalculator;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    public partial class CostPeriod 
    {
        public CostPeriod(
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            System.Int32 cost)
        {
            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CalculatedAccountBalanceResult 
    {
        public CalculatedAccountBalanceResult(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Persistence.Payments.LedgerAccountType accountType,
            System.Decimal amount)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (accountType == null)
            {
                throw new ArgumentNullException("accountType");
            }

            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            this.Timestamp = timestamp;
            this.UserId = userId;
            this.AccountType = accountType;
            this.Amount = amount;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorIdAndFirstSubscribedDate 
    {
        public CreatorIdAndFirstSubscribedDate(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.DateTime firstSubscribedDate)
        {
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (firstSubscribedDate == null)
            {
                throw new ArgumentNullException("firstSubscribedDate");
            }

            this.CreatorId = creatorId;
            this.FirstSubscribedDate = firstSubscribedDate;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorPercentageOverrideData 
    {
        public CreatorPercentageOverrideData(
            System.Decimal percentage,
            System.Nullable<System.DateTime> expiryDate)
        {
            if (percentage == null)
            {
                throw new ArgumentNullException("percentage");
            }

            this.Percentage = percentage;
            this.ExpiryDate = expiryDate;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorPost 
    {
        public CreatorPost(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.DateTime liveDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.ChannelId = channelId;
            this.LiveDate = liveDate;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class ApplyUserCredit 
    {
        public ApplyUserCredit(
            Fifthweek.Payments.Services.Credit.IInitializeCreditRequest initializeCreditRequest,
            Fifthweek.Payments.Services.Credit.IPerformCreditRequest performCreditRequest,
            Fifthweek.Payments.Services.Credit.ICommitCreditToDatabase commitCreditToDatabase,
            Fifthweek.Payments.Services.Credit.ICommitTestUserCreditToDatabase commitTestUserCreditToDatabase,
            Fifthweek.Shared.IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure,
            Fifthweek.Payments.Services.Credit.Taxamo.ICommitTaxamoTransaction commitTaxamoTransaction)
        {
            if (initializeCreditRequest == null)
            {
                throw new ArgumentNullException("initializeCreditRequest");
            }

            if (performCreditRequest == null)
            {
                throw new ArgumentNullException("performCreditRequest");
            }

            if (commitCreditToDatabase == null)
            {
                throw new ArgumentNullException("commitCreditToDatabase");
            }

            if (commitTestUserCreditToDatabase == null)
            {
                throw new ArgumentNullException("commitTestUserCreditToDatabase");
            }

            if (retryOnTransientFailure == null)
            {
                throw new ArgumentNullException("retryOnTransientFailure");
            }

            if (commitTaxamoTransaction == null)
            {
                throw new ArgumentNullException("commitTaxamoTransaction");
            }

            this.initializeCreditRequest = initializeCreditRequest;
            this.performCreditRequest = performCreditRequest;
            this.commitCreditToDatabase = commitCreditToDatabase;
            this.commitTestUserCreditToDatabase = commitTestUserCreditToDatabase;
            this.retryOnTransientFailure = retryOnTransientFailure;
            this.commitTaxamoTransaction = commitTaxamoTransaction;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class ClearPaymentStatusDbStatement 
    {
        public ClearPaymentStatusDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class CommitCreditToDatabase 
    {
        public CommitCreditToDatabase(
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances,
            Fifthweek.Payments.Services.Credit.ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement setUserPaymentOriginOriginalTaxamoTransactionKey,
            Fifthweek.Payments.Services.Credit.ISaveCustomerCreditToLedgerDbStatement saveCustomerCreditToLedger,
            Fifthweek.Payments.Services.Credit.IClearPaymentStatusDbStatement clearPaymentStatus)
        {
            if (updateAccountBalances == null)
            {
                throw new ArgumentNullException("updateAccountBalances");
            }

            if (setUserPaymentOriginOriginalTaxamoTransactionKey == null)
            {
                throw new ArgumentNullException("setUserPaymentOriginOriginalTaxamoTransactionKey");
            }

            if (saveCustomerCreditToLedger == null)
            {
                throw new ArgumentNullException("saveCustomerCreditToLedger");
            }

            if (clearPaymentStatus == null)
            {
                throw new ArgumentNullException("clearPaymentStatus");
            }

            this.updateAccountBalances = updateAccountBalances;
            this.setUserPaymentOriginOriginalTaxamoTransactionKey = setUserPaymentOriginOriginalTaxamoTransactionKey;
            this.saveCustomerCreditToLedger = saveCustomerCreditToLedger;
            this.clearPaymentStatus = clearPaymentStatus;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class CommitTestUserCreditToDatabase 
    {
        public CommitTestUserCreditToDatabase(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Payments.Services.Credit.ISetTestUserAccountBalanceDbStatement setTestUserAccountBalance)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (setTestUserAccountBalance == null)
            {
                throw new ArgumentNullException("setTestUserAccountBalance");
            }

            this.timestampCreator = timestampCreator;
            this.setTestUserAccountBalance = setTestUserAccountBalance;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class FailPaymentStatusDbStatement 
    {
        public FailPaymentStatusDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class GetUserPaymentOriginDbStatement 
    {
        public GetUserPaymentOriginDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class GetUsersRequiringPaymentRetryDbStatement 
    {
        public GetUsersRequiringPaymentRetryDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class GetUserWeeklySubscriptionsCost 
    {
        public GetUserWeeklySubscriptionsCost(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class IncrementPaymentStatusDbStatement 
    {
        public IncrementPaymentStatusDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class InitializeCreditRequest 
    {
        public InitializeCreditRequest(
            Fifthweek.Payments.Services.Credit.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Payments.Services.Credit.Taxamo.IDeleteTaxamoTransaction deleteTaxamoTransaction,
            Fifthweek.Payments.Services.Credit.Taxamo.ICreateTaxamoTransaction createTaxamoTransaction)
        {
            if (getUserPaymentOrigin == null)
            {
                throw new ArgumentNullException("getUserPaymentOrigin");
            }

            if (deleteTaxamoTransaction == null)
            {
                throw new ArgumentNullException("deleteTaxamoTransaction");
            }

            if (createTaxamoTransaction == null)
            {
                throw new ArgumentNullException("createTaxamoTransaction");
            }

            this.getUserPaymentOrigin = getUserPaymentOrigin;
            this.deleteTaxamoTransaction = deleteTaxamoTransaction;
            this.createTaxamoTransaction = createTaxamoTransaction;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class InitializeCreditRequestResult 
    {
        public InitializeCreditRequestResult(
            Fifthweek.Payments.Services.Credit.Taxamo.TaxamoTransactionResult taxamoTransaction,
            Fifthweek.Payments.Services.Credit.UserPaymentOriginResult origin)
        {
            if (taxamoTransaction == null)
            {
                throw new ArgumentNullException("taxamoTransaction");
            }

            if (origin == null)
            {
                throw new ArgumentNullException("origin");
            }

            this.TaxamoTransaction = taxamoTransaction;
            this.Origin = origin;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class PerformCreditRequest 
    {
        public PerformCreditRequest(
            Fifthweek.Payments.Services.Credit.Stripe.IPerformStripeCharge performStripeCharge)
        {
            if (performStripeCharge == null)
            {
                throw new ArgumentNullException("performStripeCharge");
            }

            this.performStripeCharge = performStripeCharge;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class SaveCustomerCreditToLedgerDbStatement 
    {
        public SaveCustomerCreditToLedgerDbStatement(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.guidCreator = guidCreator;
            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class SetTestUserAccountBalanceDbStatement 
    {
        public SetTestUserAccountBalanceDbStatement(
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
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class SetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement 
    {
        public SetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement(
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
namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;
    using global::Stripe;
    using System.Collections.Generic;
    using Fifthweek.Payments.Shared;

    public partial class CreateStripeCustomer 
    {
        public CreateStripeCustomer(
            Fifthweek.Payments.Stripe.IStripeApiKeyRepository apiKeyRepository,
            Fifthweek.Payments.Stripe.IStripeService stripeService)
        {
            if (apiKeyRepository == null)
            {
                throw new ArgumentNullException("apiKeyRepository");
            }

            if (stripeService == null)
            {
                throw new ArgumentNullException("stripeService");
            }

            this.apiKeyRepository = apiKeyRepository;
            this.stripeService = stripeService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;
    using global::Stripe;
    using System.Collections.Generic;
    using Fifthweek.Payments.Shared;

    public partial class PerformStripeCharge 
    {
        public PerformStripeCharge(
            Fifthweek.Payments.Stripe.IStripeApiKeyRepository apiKeyRepository,
            Fifthweek.Payments.Stripe.IStripeService stripeService)
        {
            if (apiKeyRepository == null)
            {
                throw new ArgumentNullException("apiKeyRepository");
            }

            if (stripeService == null)
            {
                throw new ArgumentNullException("stripeService");
            }

            this.apiKeyRepository = apiKeyRepository;
            this.stripeService = stripeService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;
    using global::Stripe;
    using System.Collections.Generic;
    using Fifthweek.Payments.Shared;

    public partial class UpdateStripeCustomerCreditCard 
    {
        public UpdateStripeCustomerCreditCard(
            Fifthweek.Payments.Stripe.IStripeApiKeyRepository apiKeyRepository,
            Fifthweek.Payments.Stripe.IStripeService stripeService)
        {
            if (apiKeyRepository == null)
            {
                throw new ArgumentNullException("apiKeyRepository");
            }

            if (stripeService == null)
            {
                throw new ArgumentNullException("stripeService");
            }

            this.apiKeyRepository = apiKeyRepository;
            this.stripeService = stripeService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class StripeTransactionResult 
    {
        public StripeTransactionResult(
            System.DateTime timestamp,
            Fifthweek.Payments.Shared.TransactionReference transactionReference,
            System.String stripeChargeId)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (transactionReference == null)
            {
                throw new ArgumentNullException("transactionReference");
            }

            if (stripeChargeId == null)
            {
                throw new ArgumentNullException("stripeChargeId");
            }

            this.Timestamp = timestamp;
            this.TransactionReference = transactionReference;
            this.StripeChargeId = stripeChargeId;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class CommitTaxamoTransaction 
    {
        public CommitTaxamoTransaction(
            Fifthweek.Payments.Taxamo.ITaxamoApiKeyRepository taxamoApiKeyRepository,
            Fifthweek.Payments.Taxamo.ITaxamoService taxamoService)
        {
            if (taxamoApiKeyRepository == null)
            {
                throw new ArgumentNullException("taxamoApiKeyRepository");
            }

            if (taxamoService == null)
            {
                throw new ArgumentNullException("taxamoService");
            }

            this.taxamoApiKeyRepository = taxamoApiKeyRepository;
            this.taxamoService = taxamoService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class CreateTaxamoTransaction 
    {
        public CreateTaxamoTransaction(
            Fifthweek.Payments.Taxamo.ITaxamoApiKeyRepository taxamoApiKeyRepository,
            Fifthweek.Payments.Taxamo.ITaxamoService taxamoService)
        {
            if (taxamoApiKeyRepository == null)
            {
                throw new ArgumentNullException("taxamoApiKeyRepository");
            }

            if (taxamoService == null)
            {
                throw new ArgumentNullException("taxamoService");
            }

            this.taxamoApiKeyRepository = taxamoApiKeyRepository;
            this.taxamoService = taxamoService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class DeleteTaxamoTransaction 
    {
        public DeleteTaxamoTransaction(
            Fifthweek.Payments.Taxamo.ITaxamoApiKeyRepository taxamoApiKeyRepository,
            Fifthweek.Payments.Taxamo.ITaxamoService taxamoService)
        {
            if (taxamoApiKeyRepository == null)
            {
                throw new ArgumentNullException("taxamoApiKeyRepository");
            }

            if (taxamoService == null)
            {
                throw new ArgumentNullException("taxamoService");
            }

            this.taxamoApiKeyRepository = taxamoApiKeyRepository;
            this.taxamoService = taxamoService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class GetTaxInformation 
    {
        public GetTaxInformation(
            Fifthweek.Payments.Taxamo.ITaxamoApiKeyRepository taxamoApiKeyRepository,
            Fifthweek.Payments.Taxamo.ITaxamoService taxamoService)
        {
            if (taxamoApiKeyRepository == null)
            {
                throw new ArgumentNullException("taxamoApiKeyRepository");
            }

            if (taxamoService == null)
            {
                throw new ArgumentNullException("taxamoService");
            }

            this.taxamoApiKeyRepository = taxamoApiKeyRepository;
            this.taxamoService = taxamoService;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoCalculationResult
    {
        public partial class PossibleCountry 
        {
            public PossibleCountry(
                System.String name,
                System.String countryCode)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                if (countryCode == null)
                {
                    throw new ArgumentNullException("countryCode");
                }

                this.Name = name;
                this.CountryCode = countryCode;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoCalculationResult 
    {
        public TaxamoCalculationResult(
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination amount,
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination totalAmount,
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination taxAmount,
            System.Nullable<System.Decimal> taxRate,
            System.String taxName,
            System.String taxEntityName,
            System.String countryName,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.Credit.Taxamo.TaxamoCalculationResult.PossibleCountry> possibleCountries)
        {
            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            if (totalAmount == null)
            {
                throw new ArgumentNullException("totalAmount");
            }

            if (taxAmount == null)
            {
                throw new ArgumentNullException("taxAmount");
            }

            this.Amount = amount;
            this.TotalAmount = totalAmount;
            this.TaxAmount = taxAmount;
            this.TaxRate = taxRate;
            this.TaxName = taxName;
            this.TaxEntityName = taxEntityName;
            this.CountryName = countryName;
            this.PossibleCountries = possibleCountries;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoTransactionResult 
    {
        public TaxamoTransactionResult(
            System.String key,
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination amount,
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination totalAmount,
            Fifthweek.Payments.Services.Credit.AmountInMinorDenomination taxAmount,
            System.Nullable<System.Decimal> taxRate,
            System.String taxName,
            System.String taxEntityName,
            System.String countryName)
        {
            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            if (totalAmount == null)
            {
                throw new ArgumentNullException("totalAmount");
            }

            if (taxAmount == null)
            {
                throw new ArgumentNullException("taxAmount");
            }

            if (countryName == null)
            {
                throw new ArgumentNullException("countryName");
            }

            this.Key = key;
            this.Amount = amount;
            this.TotalAmount = totalAmount;
            this.TaxAmount = taxAmount;
            this.TaxRate = taxRate;
            this.TaxName = taxName;
            this.TaxEntityName = taxEntityName;
            this.CountryName = countryName;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class TopUpUserAccountsWithCredit 
    {
        public TopUpUserAccountsWithCredit(
            Fifthweek.Payments.Services.Credit.IGetUsersRequiringPaymentRetryDbStatement getUsersRequiringPaymentRetry,
            Fifthweek.Payments.Services.Credit.IApplyUserCredit applyUserCredit,
            Fifthweek.Payments.Services.Credit.IGetUserWeeklySubscriptionsCost getUserWeeklySubscriptionsCost,
            Fifthweek.Payments.Services.Credit.IIncrementPaymentStatusDbStatement incrementPaymentStatus,
            Fifthweek.Payments.Services.Credit.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Shared.IGuidCreator guidCreator)
        {
            if (getUsersRequiringPaymentRetry == null)
            {
                throw new ArgumentNullException("getUsersRequiringPaymentRetry");
            }

            if (applyUserCredit == null)
            {
                throw new ArgumentNullException("applyUserCredit");
            }

            if (getUserWeeklySubscriptionsCost == null)
            {
                throw new ArgumentNullException("getUserWeeklySubscriptionsCost");
            }

            if (incrementPaymentStatus == null)
            {
                throw new ArgumentNullException("incrementPaymentStatus");
            }

            if (getUserPaymentOrigin == null)
            {
                throw new ArgumentNullException("getUserPaymentOrigin");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.getUsersRequiringPaymentRetry = getUsersRequiringPaymentRetry;
            this.applyUserCredit = applyUserCredit;
            this.getUserWeeklySubscriptionsCost = getUserWeeklySubscriptionsCost;
            this.incrementPaymentStatus = incrementPaymentStatus;
            this.getUserPaymentOrigin = getUserPaymentOrigin;
            this.timestampCreator = timestampCreator;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class UserPaymentOriginResult 
    {
        public UserPaymentOriginResult(
            System.String paymentOriginKey,
            Fifthweek.Api.Persistence.Payments.PaymentOriginKeyType paymentOriginKeyType,
            System.String countryCode,
            System.String creditCardPrefix,
            System.String ipAddress,
            System.String originalTaxamoTransactionKey,
            Fifthweek.Api.Persistence.Payments.PaymentStatus paymentStatus)
        {
            this.PaymentOriginKey = paymentOriginKey;
            this.PaymentOriginKeyType = paymentOriginKeyType;
            this.CountryCode = countryCode;
            this.CreditCardPrefix = creditCardPrefix;
            this.IpAddress = ipAddress;
            this.OriginalTaxamoTransactionKey = originalTaxamoTransactionKey;
            this.PaymentStatus = paymentStatus;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetAllSubscribersDbStatement 
    {
        public GetAllSubscribersDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCalculatedAccountBalancesDbStatement 
    {
        public GetCalculatedAccountBalancesDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCommittedAccountBalanceDbStatement 
    {
        public GetCommittedAccountBalanceDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCreatorChannelsSnapshotsDbStatement 
    {
        public GetCreatorChannelsSnapshotsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCreatorFreeAccessUsersSnapshotsDbStatement 
    {
        public GetCreatorFreeAccessUsersSnapshotsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCreatorPercentageOverrideDbStatement 
    {
        public GetCreatorPercentageOverrideDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCreatorPostsDbStatement 
    {
        public GetCreatorPostsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetCreatorsAndFirstSubscribedDatesDbStatement 
    {
        public GetCreatorsAndFirstSubscribedDatesDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetLatestCommittedLedgerDateDbStatement 
    {
        public GetLatestCommittedLedgerDateDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetPaymentProcessingData
    {
        public partial class CachedData 
        {
            public CachedData(
                Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                System.DateTime startTimeInclusive,
                System.DateTime endTimeExclusive,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot> subscriberChannelsSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberSnapshot> subscriberSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot> calculatedAccountBalanceSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot> creatorChannelsSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.CreatorPost> creatorPosts,
                Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride)
            {
                if (subscriberId == null)
                {
                    throw new ArgumentNullException("subscriberId");
                }

                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                if (startTimeInclusive == null)
                {
                    throw new ArgumentNullException("startTimeInclusive");
                }

                if (endTimeExclusive == null)
                {
                    throw new ArgumentNullException("endTimeExclusive");
                }

                if (subscriberChannelsSnapshots == null)
                {
                    throw new ArgumentNullException("subscriberChannelsSnapshots");
                }

                if (subscriberSnapshots == null)
                {
                    throw new ArgumentNullException("subscriberSnapshots");
                }

                if (calculatedAccountBalanceSnapshots == null)
                {
                    throw new ArgumentNullException("calculatedAccountBalanceSnapshots");
                }

                if (creatorChannelsSnapshots == null)
                {
                    throw new ArgumentNullException("creatorChannelsSnapshots");
                }

                if (creatorFreeAccessUsersSnapshots == null)
                {
                    throw new ArgumentNullException("creatorFreeAccessUsersSnapshots");
                }

                if (creatorPosts == null)
                {
                    throw new ArgumentNullException("creatorPosts");
                }

                this.SubscriberId = subscriberId;
                this.CreatorId = creatorId;
                this.StartTimeInclusive = startTimeInclusive;
                this.EndTimeExclusive = endTimeExclusive;
                this.SubscriberChannelsSnapshots = subscriberChannelsSnapshots;
                this.SubscriberSnapshots = subscriberSnapshots;
                this.CalculatedAccountBalanceSnapshots = calculatedAccountBalanceSnapshots;
                this.CreatorChannelsSnapshots = creatorChannelsSnapshots;
                this.CreatorFreeAccessUsersSnapshots = creatorFreeAccessUsersSnapshots;
                this.CreatorPosts = creatorPosts;
                this.CreatorPercentageOverride = creatorPercentageOverride;
            }
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetPaymentProcessingData 
    {
        public GetPaymentProcessingData(
            Fifthweek.Payments.Services.IGetCreatorChannelsSnapshotsDbStatement getCreatorChannelsSnapshots,
            Fifthweek.Payments.Services.IGetCreatorFreeAccessUsersSnapshotsDbStatement getCreatorFreeAccessUsersSnapshots,
            Fifthweek.Payments.Services.IGetCreatorPostsDbStatement getCreatorPosts,
            Fifthweek.Payments.Services.IGetSubscriberChannelsSnapshotsDbStatement getSubscriberChannelsSnapshots,
            Fifthweek.Payments.Services.IGetSubscriberSnapshotsDbStatement getSubscriberSnapshots,
            Fifthweek.Payments.Services.IGetCalculatedAccountBalancesDbStatement getCalculatedAccountBalances,
            Fifthweek.Payments.Services.IGetCreatorPercentageOverrideDbStatement getCreatorPercentageOverride)
        {
            if (getCreatorChannelsSnapshots == null)
            {
                throw new ArgumentNullException("getCreatorChannelsSnapshots");
            }

            if (getCreatorFreeAccessUsersSnapshots == null)
            {
                throw new ArgumentNullException("getCreatorFreeAccessUsersSnapshots");
            }

            if (getCreatorPosts == null)
            {
                throw new ArgumentNullException("getCreatorPosts");
            }

            if (getSubscriberChannelsSnapshots == null)
            {
                throw new ArgumentNullException("getSubscriberChannelsSnapshots");
            }

            if (getSubscriberSnapshots == null)
            {
                throw new ArgumentNullException("getSubscriberSnapshots");
            }

            if (getCalculatedAccountBalances == null)
            {
                throw new ArgumentNullException("getCalculatedAccountBalances");
            }

            if (getCreatorPercentageOverride == null)
            {
                throw new ArgumentNullException("getCreatorPercentageOverride");
            }

            this.getCreatorChannelsSnapshots = getCreatorChannelsSnapshots;
            this.getCreatorFreeAccessUsersSnapshots = getCreatorFreeAccessUsersSnapshots;
            this.getCreatorPosts = getCreatorPosts;
            this.getSubscriberChannelsSnapshots = getSubscriberChannelsSnapshots;
            this.getSubscriberSnapshots = getSubscriberSnapshots;
            this.getCalculatedAccountBalances = getCalculatedAccountBalances;
            this.getCreatorPercentageOverride = getCreatorPercentageOverride;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetSubscriberChannelsSnapshotsDbStatement 
    {
        public GetSubscriberChannelsSnapshotsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class GetSubscriberSnapshotsDbStatement 
    {
        public GetSubscriberSnapshotsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingData 
    {
        public PaymentProcessingData(
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            Fifthweek.Payments.Services.CommittedAccountBalance committedAccountBalance,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot> subscriberChannelsSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberSnapshot> subscriberSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot> calculatedAccountBalanceSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot> creatorChannelsSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.CreatorPost> creatorPosts,
            Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride)
        {
            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (committedAccountBalance == null)
            {
                throw new ArgumentNullException("committedAccountBalance");
            }

            if (subscriberChannelsSnapshots == null)
            {
                throw new ArgumentNullException("subscriberChannelsSnapshots");
            }

            if (subscriberSnapshots == null)
            {
                throw new ArgumentNullException("subscriberSnapshots");
            }

            if (calculatedAccountBalanceSnapshots == null)
            {
                throw new ArgumentNullException("calculatedAccountBalanceSnapshots");
            }

            if (creatorChannelsSnapshots == null)
            {
                throw new ArgumentNullException("creatorChannelsSnapshots");
            }

            if (creatorFreeAccessUsersSnapshots == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsersSnapshots");
            }

            if (creatorPosts == null)
            {
                throw new ArgumentNullException("creatorPosts");
            }

            this.SubscriberId = subscriberId;
            this.CreatorId = creatorId;
            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.CommittedAccountBalance = committedAccountBalance;
            this.SubscriberChannelsSnapshots = subscriberChannelsSnapshots;
            this.SubscriberSnapshots = subscriberSnapshots;
            this.CalculatedAccountBalanceSnapshots = calculatedAccountBalanceSnapshots;
            this.CreatorChannelsSnapshots = creatorChannelsSnapshots;
            this.CreatorFreeAccessUsersSnapshots = creatorFreeAccessUsersSnapshots;
            this.CreatorPosts = creatorPosts;
            this.CreatorPercentageOverride = creatorPercentageOverride;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingResult 
    {
        public PaymentProcessingResult(
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            Fifthweek.Payments.AggregateCostSummary subscriptionCost,
            Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride,
            System.Boolean isCommitted)
        {
            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (subscriptionCost == null)
            {
                throw new ArgumentNullException("subscriptionCost");
            }

            if (isCommitted == null)
            {
                throw new ArgumentNullException("isCommitted");
            }

            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.SubscriptionCost = subscriptionCost;
            this.CreatorPercentageOverride = creatorPercentageOverride;
            this.IsCommitted = isCommitted;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingResults 
    {
        public PaymentProcessingResults(
            Fifthweek.Payments.Services.CommittedAccountBalance committedAccountBalance,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.PaymentProcessingResult> items)
        {
            if (committedAccountBalance == null)
            {
                throw new ArgumentNullException("committedAccountBalance");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.CommittedAccountBalance = committedAccountBalance;
            this.Items = items;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PersistCommittedAndUncommittedRecordsDbStatement 
    {
        public PersistCommittedAndUncommittedRecordsDbStatement(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PersistedPaymentProcessingData 
    {
        public PersistedPaymentProcessingData(
            System.Guid id,
            Fifthweek.Payments.Services.PaymentProcessingData input,
            Fifthweek.Payments.Services.PaymentProcessingResults output)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            this.Id = id;
            this.Input = input;
            this.Output = output;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PersistPaymentProcessingDataStatement 
    {
        public PersistPaymentProcessingDataStatement(
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PersistPaymentProcessingResults 
    {
        public PersistPaymentProcessingResults(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.Services.IPersistPaymentProcessingDataStatement persistPaymentProcessingData,
            Fifthweek.Payments.Services.IPersistCommittedAndUncommittedRecordsDbStatement persistCommittedAndUncommittedRecords)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (persistPaymentProcessingData == null)
            {
                throw new ArgumentNullException("persistPaymentProcessingData");
            }

            if (persistCommittedAndUncommittedRecords == null)
            {
                throw new ArgumentNullException("persistCommittedAndUncommittedRecords");
            }

            this.guidCreator = guidCreator;
            this.persistPaymentProcessingData = persistPaymentProcessingData;
            this.persistCommittedAndUncommittedRecords = persistCommittedAndUncommittedRecords;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class ProcessAllPayments 
    {
        public ProcessAllPayments(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Payments.Services.IGetAllSubscribersDbStatement getAllSubscribers,
            Fifthweek.Payments.Services.IProcessPaymentsForSubscriber processPaymentsForSubscriber,
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances,
            Fifthweek.Payments.Services.Credit.ITopUpUserAccountsWithCredit topUpUserAccountsWithCredit)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (getAllSubscribers == null)
            {
                throw new ArgumentNullException("getAllSubscribers");
            }

            if (processPaymentsForSubscriber == null)
            {
                throw new ArgumentNullException("processPaymentsForSubscriber");
            }

            if (updateAccountBalances == null)
            {
                throw new ArgumentNullException("updateAccountBalances");
            }

            if (topUpUserAccountsWithCredit == null)
            {
                throw new ArgumentNullException("topUpUserAccountsWithCredit");
            }

            this.timestampCreator = timestampCreator;
            this.getAllSubscribers = getAllSubscribers;
            this.processPaymentsForSubscriber = processPaymentsForSubscriber;
            this.updateAccountBalances = updateAccountBalances;
            this.topUpUserAccountsWithCredit = topUpUserAccountsWithCredit;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class ProcessPaymentProcessingData 
    {
        public ProcessPaymentProcessingData(
            Fifthweek.Payments.Services.ISubscriberPaymentPipeline subscriberPaymentPipeline)
        {
            if (subscriberPaymentPipeline == null)
            {
                throw new ArgumentNullException("subscriberPaymentPipeline");
            }

            this.subscriberPaymentPipeline = subscriberPaymentPipeline;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class ProcessPaymentsBetweenSubscriberAndCreator 
    {
        public ProcessPaymentsBetweenSubscriberAndCreator(
            Fifthweek.Payments.Services.IGetPaymentProcessingData getPaymentProcessingData,
            Fifthweek.Payments.Services.IProcessPaymentProcessingData processPaymentProcessingData,
            Fifthweek.Payments.Services.IPersistPaymentProcessingResults persistPaymentProcessingResults)
        {
            if (getPaymentProcessingData == null)
            {
                throw new ArgumentNullException("getPaymentProcessingData");
            }

            if (processPaymentProcessingData == null)
            {
                throw new ArgumentNullException("processPaymentProcessingData");
            }

            if (persistPaymentProcessingResults == null)
            {
                throw new ArgumentNullException("persistPaymentProcessingResults");
            }

            this.getPaymentProcessingData = getPaymentProcessingData;
            this.processPaymentProcessingData = processPaymentProcessingData;
            this.persistPaymentProcessingResults = persistPaymentProcessingResults;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class ProcessPaymentsForSubscriber 
    {
        public ProcessPaymentsForSubscriber(
            Fifthweek.Payments.Services.IGetCreatorsAndFirstSubscribedDatesDbStatement getCreatorsAndFirstSubscribedDates,
            Fifthweek.Payments.Services.IGetCommittedAccountBalanceDbStatement getCommittedAccountBalanceDbStatement,
            Fifthweek.Payments.Services.IProcessPaymentsBetweenSubscriberAndCreator processPaymentsBetweenSubscriberAndCreator,
            Fifthweek.Payments.Services.IGetLatestCommittedLedgerDateDbStatement getLatestCommittedLedgerDate)
        {
            if (getCreatorsAndFirstSubscribedDates == null)
            {
                throw new ArgumentNullException("getCreatorsAndFirstSubscribedDates");
            }

            if (getCommittedAccountBalanceDbStatement == null)
            {
                throw new ArgumentNullException("getCommittedAccountBalanceDbStatement");
            }

            if (processPaymentsBetweenSubscriberAndCreator == null)
            {
                throw new ArgumentNullException("processPaymentsBetweenSubscriberAndCreator");
            }

            if (getLatestCommittedLedgerDate == null)
            {
                throw new ArgumentNullException("getLatestCommittedLedgerDate");
            }

            this.getCreatorsAndFirstSubscribedDates = getCreatorsAndFirstSubscribedDates;
            this.getCommittedAccountBalanceDbStatement = getCommittedAccountBalanceDbStatement;
            this.processPaymentsBetweenSubscriberAndCreator = processPaymentsBetweenSubscriberAndCreator;
            this.getLatestCommittedLedgerDate = getLatestCommittedLedgerDate;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class CreateCreditRefund 
    {
        public CreateCreditRefund(
            Fifthweek.Shared.IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure,
            Fifthweek.Payments.Services.Refunds.IGetCreditTransactionInformation getCreditTransaction,
            Fifthweek.Payments.Services.Refunds.Taxamo.ICreateTaxamoRefund createTaxamoRefund,
            Fifthweek.Payments.Services.Refunds.Stripe.ICreateStripeRefund createStripeRefund,
            Fifthweek.Payments.Services.Refunds.IPersistCreditRefund persistCreditRefund)
        {
            if (retryOnTransientFailure == null)
            {
                throw new ArgumentNullException("retryOnTransientFailure");
            }

            if (getCreditTransaction == null)
            {
                throw new ArgumentNullException("getCreditTransaction");
            }

            if (createTaxamoRefund == null)
            {
                throw new ArgumentNullException("createTaxamoRefund");
            }

            if (createStripeRefund == null)
            {
                throw new ArgumentNullException("createStripeRefund");
            }

            if (persistCreditRefund == null)
            {
                throw new ArgumentNullException("persistCreditRefund");
            }

            this.retryOnTransientFailure = retryOnTransientFailure;
            this.getCreditTransaction = getCreditTransaction;
            this.createTaxamoRefund = createTaxamoRefund;
            this.createStripeRefund = createStripeRefund;
            this.persistCreditRefund = persistCreditRefund;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class PersistCreditRefund 
    {
        public PersistCreditRefund(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.Services.Refunds.IPersistCommittedRecordsDbStatement persistCommittedRecords)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (persistCommittedRecords == null)
            {
                throw new ArgumentNullException("persistCommittedRecords");
            }

            this.guidCreator = guidCreator;
            this.persistCommittedRecords = persistCommittedRecords;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class CreateTransactionRefund
    {
        public partial class CreateTransactionRefundResult 
        {
            public CreateTransactionRefundResult(
                Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId)
            {
                if (subscriberId == null)
                {
                    throw new ArgumentNullException("subscriberId");
                }

                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                this.SubscriberId = subscriberId;
                this.CreatorId = creatorId;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class CreateTransactionRefund 
    {
        public CreateTransactionRefund(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.Services.Refunds.IGetRecordsForTransactionDbStatement getRecordsForTransaction,
            Fifthweek.Payments.Services.Refunds.IPersistCommittedRecordsDbStatement persistCommittedRecords)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (getRecordsForTransaction == null)
            {
                throw new ArgumentNullException("getRecordsForTransaction");
            }

            if (persistCommittedRecords == null)
            {
                throw new ArgumentNullException("persistCommittedRecords");
            }

            this.guidCreator = guidCreator;
            this.getRecordsForTransaction = getRecordsForTransaction;
            this.persistCommittedRecords = persistCommittedRecords;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class GetCreditTransactionInformation
    {
        public partial class GetCreditTransactionResult 
        {
            public GetCreditTransactionResult(
                Fifthweek.Api.Identity.Shared.Membership.UserId userId,
                System.String stripeChargeId,
                System.String taxamoTransactionKey,
                System.Decimal totalCreditAmount,
                System.Decimal creditAmountAvailableForRefund)
            {
                if (userId == null)
                {
                    throw new ArgumentNullException("userId");
                }

                if (stripeChargeId == null)
                {
                    throw new ArgumentNullException("stripeChargeId");
                }

                if (taxamoTransactionKey == null)
                {
                    throw new ArgumentNullException("taxamoTransactionKey");
                }

                if (totalCreditAmount == null)
                {
                    throw new ArgumentNullException("totalCreditAmount");
                }

                if (creditAmountAvailableForRefund == null)
                {
                    throw new ArgumentNullException("creditAmountAvailableForRefund");
                }

                this.UserId = userId;
                this.StripeChargeId = stripeChargeId;
                this.TaxamoTransactionKey = taxamoTransactionKey;
                this.TotalCreditAmount = totalCreditAmount;
                this.CreditAmountAvailableForRefund = creditAmountAvailableForRefund;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class GetCreditTransactionInformation 
    {
        public GetCreditTransactionInformation(
            Fifthweek.Payments.Services.Refunds.IGetRecordsForTransactionDbStatement getRecordsForTransaction)
        {
            if (getRecordsForTransaction == null)
            {
                throw new ArgumentNullException("getRecordsForTransaction");
            }

            this.getRecordsForTransaction = getRecordsForTransaction;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class GetRecordsForTransactionDbStatement 
    {
        public GetRecordsForTransactionDbStatement(
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
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class PersistCommittedRecordsDbStatement 
    {
        public PersistCommittedRecordsDbStatement(
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
namespace Fifthweek.Payments.Services.Refunds.Stripe
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;
    using global::Stripe;

    public partial class CreateStripeRefund 
    {
        public CreateStripeRefund(
            Fifthweek.Payments.Stripe.IStripeApiKeyRepository apiKeyRepository,
            Fifthweek.Payments.Stripe.IStripeService stripeService)
        {
            if (apiKeyRepository == null)
            {
                throw new ArgumentNullException("apiKeyRepository");
            }

            if (stripeService == null)
            {
                throw new ArgumentNullException("stripeService");
            }

            this.apiKeyRepository = apiKeyRepository;
            this.stripeService = stripeService;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds.Taxamo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;

    public partial class CreateTaxamoRefund
    {
        public partial class TaxamoRefundResult 
        {
            public TaxamoRefundResult(
                Fifthweek.Shared.PositiveInt totalRefundAmount,
                Fifthweek.Shared.NonNegativeInt taxRefundAmount)
            {
                if (totalRefundAmount == null)
                {
                    throw new ArgumentNullException("totalRefundAmount");
                }

                if (taxRefundAmount == null)
                {
                    throw new ArgumentNullException("taxRefundAmount");
                }

                this.TotalRefundAmount = totalRefundAmount;
                this.TaxRefundAmount = taxRefundAmount;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds.Taxamo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;

    public partial class CreateTaxamoRefund 
    {
        public CreateTaxamoRefund(
            Fifthweek.Payments.Taxamo.ITaxamoApiKeyRepository taxamoApiKeyRepository,
            Fifthweek.Payments.Taxamo.ITaxamoService taxamoService)
        {
            if (taxamoApiKeyRepository == null)
            {
                throw new ArgumentNullException("taxamoApiKeyRepository");
            }

            if (taxamoService == null)
            {
                throw new ArgumentNullException("taxamoService");
            }

            this.taxamoApiKeyRepository = taxamoApiKeyRepository;
            this.taxamoService = taxamoService;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class RequestProcessPaymentsService 
    {
        public RequestProcessPaymentsService(
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
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class SubscriberPaymentPipeline 
    {
        public SubscriberPaymentPipeline(
            Fifthweek.Payments.Pipeline.IVerifySnapshotsExecutor verifySnapshots,
            Fifthweek.Payments.Pipeline.IMergeSnapshotsExecutor mergeSnapshots,
            Fifthweek.Payments.Pipeline.IRollBackSubscriptionsExecutor rollBackSubscriptions,
            Fifthweek.Payments.Pipeline.IRollForwardSubscriptionsExecutor rollForwardSubscriptions,
            Fifthweek.Payments.Pipeline.ITrimSnapshotsExecutor trimSnapshots,
            Fifthweek.Payments.Pipeline.IAddSnapshotsForBillingEndDatesExecutor addSnapshotsForBillingEndDates,
            Fifthweek.Payments.Pipeline.ICalculateCostPeriodsExecutor calculateCostPeriods,
            Fifthweek.Payments.Pipeline.IAggregateCostPeriodsExecutor aggregateCostPeriods)
        {
            if (verifySnapshots == null)
            {
                throw new ArgumentNullException("verifySnapshots");
            }

            if (mergeSnapshots == null)
            {
                throw new ArgumentNullException("mergeSnapshots");
            }

            if (rollBackSubscriptions == null)
            {
                throw new ArgumentNullException("rollBackSubscriptions");
            }

            if (rollForwardSubscriptions == null)
            {
                throw new ArgumentNullException("rollForwardSubscriptions");
            }

            if (trimSnapshots == null)
            {
                throw new ArgumentNullException("trimSnapshots");
            }

            if (addSnapshotsForBillingEndDates == null)
            {
                throw new ArgumentNullException("addSnapshotsForBillingEndDates");
            }

            if (calculateCostPeriods == null)
            {
                throw new ArgumentNullException("calculateCostPeriods");
            }

            if (aggregateCostPeriods == null)
            {
                throw new ArgumentNullException("aggregateCostPeriods");
            }

            this.verifySnapshots = verifySnapshots;
            this.mergeSnapshots = mergeSnapshots;
            this.rollBackSubscriptions = rollBackSubscriptions;
            this.rollForwardSubscriptions = rollForwardSubscriptions;
            this.trimSnapshots = trimSnapshots;
            this.addSnapshotsForBillingEndDates = addSnapshotsForBillingEndDates;
            this.calculateCostPeriods = calculateCostPeriods;
            this.aggregateCostPeriods = aggregateCostPeriods;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class UpdateAccountBalancesDbStatement 
    {
        public UpdateAccountBalancesDbStatement(
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
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CalculatedAccountBalanceSnapshot 
    {
        public CalculatedAccountBalanceSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Persistence.Payments.LedgerAccountType accountType,
            System.Decimal amount)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (accountType == null)
            {
                throw new ArgumentNullException("accountType");
            }

            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            this.Timestamp = timestamp;
            this.UserId = userId;
            this.AccountType = accountType;
            this.Amount = amount;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorChannelsSnapshot 
    {
        public CreatorChannelsSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshotItem> creatorChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (creatorChannels == null)
            {
                throw new ArgumentNullException("creatorChannels");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.CreatorChannels = creatorChannels;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorChannelsSnapshotItem 
    {
        public CreatorChannelsSnapshotItem(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.Int32 price)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            this.ChannelId = channelId;
            this.Price = price;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public CreatorFreeAccessUsersSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<System.String> freeAccessUserEmails)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (freeAccessUserEmails == null)
            {
                throw new ArgumentNullException("freeAccessUserEmails");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.FreeAccessUserEmails = freeAccessUserEmails;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class MergedSnapshot 
    {
        public MergedSnapshot(
            System.DateTime timestamp,
            Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot creatorChannels,
            Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot creatorFreeAccessUsers,
            Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot subscriberChannels,
            Fifthweek.Payments.Snapshots.SubscriberSnapshot subscriber,
            Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot calculatedAccountBalance)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorChannels == null)
            {
                throw new ArgumentNullException("creatorChannels");
            }

            if (creatorFreeAccessUsers == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsers");
            }

            if (subscriberChannels == null)
            {
                throw new ArgumentNullException("subscriberChannels");
            }

            if (subscriber == null)
            {
                throw new ArgumentNullException("subscriber");
            }

            if (calculatedAccountBalance == null)
            {
                throw new ArgumentNullException("calculatedAccountBalance");
            }

            this.Timestamp = timestamp;
            this.CreatorChannels = creatorChannels;
            this.CreatorFreeAccessUsers = creatorFreeAccessUsers;
            this.SubscriberChannels = subscriberChannels;
            this.Subscriber = subscriber;
            this.CalculatedAccountBalance = calculatedAccountBalance;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberChannelsSnapshot 
    {
        public SubscriberChannelsSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshotItem> subscribedChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            if (subscribedChannels == null)
            {
                throw new ArgumentNullException("subscribedChannels");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.SubscribedChannels = subscribedChannels;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public SubscriberChannelsSnapshotItem(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.Int32 acceptedPrice,
            System.DateTime subscriptionStartDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.ChannelId = channelId;
            this.AcceptedPrice = acceptedPrice;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberSnapshot 
    {
        public SubscriberSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            System.String email)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public partial class GetTransactionsDbStatement 
    {
        public GetTransactionsDbStatement(
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
namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public partial class GetTransactionsResult 
    {
        public GetTransactionsResult(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.Administration.GetTransactionsResult.Item> records)
        {
            if (records == null)
            {
                throw new ArgumentNullException("records");
            }

            this.Records = records;
        }
    }
}
namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public partial class GetTransactionsResult
    {
        public partial class Item 
        {
            public Item(
                System.Guid id,
                Fifthweek.Api.Identity.Shared.Membership.UserId accountOwnerId,
                Fifthweek.Api.Identity.Shared.Membership.Username accountOwnerUsername,
                Fifthweek.Api.Identity.Shared.Membership.UserId counterpartyId,
                Fifthweek.Api.Identity.Shared.Membership.Username counterpartyUsername,
                System.DateTime timestamp,
                System.Decimal amount,
                Fifthweek.Api.Persistence.Payments.LedgerAccountType accountType,
                Fifthweek.Api.Persistence.Payments.LedgerTransactionType transactionType,
                Fifthweek.Payments.Shared.TransactionReference transactionReference,
                System.Nullable<System.Guid> inputDataReference,
                System.String comment,
                System.String stripeChargeId,
                System.String taxamoTransactionKey)
            {
                if (id == null)
                {
                    throw new ArgumentNullException("id");
                }

                if (accountOwnerId == null)
                {
                    throw new ArgumentNullException("accountOwnerId");
                }

                if (timestamp == null)
                {
                    throw new ArgumentNullException("timestamp");
                }

                if (amount == null)
                {
                    throw new ArgumentNullException("amount");
                }

                if (accountType == null)
                {
                    throw new ArgumentNullException("accountType");
                }

                if (transactionType == null)
                {
                    throw new ArgumentNullException("transactionType");
                }

                if (transactionReference == null)
                {
                    throw new ArgumentNullException("transactionReference");
                }

                this.Id = id;
                this.AccountOwnerId = accountOwnerId;
                this.AccountOwnerUsername = accountOwnerUsername;
                this.CounterpartyId = counterpartyId;
                this.CounterpartyUsername = counterpartyUsername;
                this.Timestamp = timestamp;
                this.Amount = amount;
                this.AccountType = accountType;
                this.TransactionType = transactionType;
                this.TransactionReference = transactionReference;
                this.InputDataReference = inputDataReference;
                this.Comment = comment;
                this.StripeChargeId = stripeChargeId;
                this.TaxamoTransactionKey = taxamoTransactionKey;
            }
        }
    }
}

namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class AggregateCostSummary 
    {
        public override string ToString()
        {
            return string.Format("AggregateCostSummary({0})", this.Cost == null ? "null" : this.Cost.ToString());
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
        
            return this.Equals((AggregateCostSummary)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AggregateCostSummary other)
        {
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class AmountInMinorDenomination 
    {
        public override string ToString()
        {
            return string.Format("AmountInMinorDenomination({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((AmountInMinorDenomination)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AmountInMinorDenomination other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    public partial class CostPeriod 
    {
        public override string ToString()
        {
            return string.Format("CostPeriod({0}, {1}, {2})", this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.Cost == null ? "null" : this.Cost.ToString());
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
        
            return this.Equals((CostPeriod)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CostPeriod other)
        {
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CalculatedAccountBalanceResult 
    {
        public override string ToString()
        {
            return string.Format("CalculatedAccountBalanceResult({0}, {1}, {2}, {3})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.AccountType == null ? "null" : this.AccountType.ToString(), this.Amount == null ? "null" : this.Amount.ToString());
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
        
            return this.Equals((CalculatedAccountBalanceResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccountType != null ? this.AccountType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CalculatedAccountBalanceResult other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.AccountType, other.AccountType))
            {
                return false;
            }
        
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorIdAndFirstSubscribedDate 
    {
        public override string ToString()
        {
            return string.Format("CreatorIdAndFirstSubscribedDate({0}, {1})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.FirstSubscribedDate == null ? "null" : this.FirstSubscribedDate.ToString());
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
        
            return this.Equals((CreatorIdAndFirstSubscribedDate)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FirstSubscribedDate != null ? this.FirstSubscribedDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorIdAndFirstSubscribedDate other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.FirstSubscribedDate, other.FirstSubscribedDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorPercentageOverrideData 
    {
        public override string ToString()
        {
            return string.Format("CreatorPercentageOverrideData({0}, {1})", this.Percentage == null ? "null" : this.Percentage.ToString(), this.ExpiryDate == null ? "null" : this.ExpiryDate.ToString());
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
        
            return this.Equals((CreatorPercentageOverrideData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Percentage != null ? this.Percentage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpiryDate != null ? this.ExpiryDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorPercentageOverrideData other)
        {
            if (!object.Equals(this.Percentage, other.Percentage))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpiryDate, other.ExpiryDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CreatorPost 
    {
        public override string ToString()
        {
            return string.Format("CreatorPost({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
        
            return this.Equals((CreatorPost)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorPost other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class InitializeCreditRequestResult 
    {
        public override string ToString()
        {
            return string.Format("InitializeCreditRequestResult({0}, {1})", this.TaxamoTransaction == null ? "null" : this.TaxamoTransaction.ToString(), this.Origin == null ? "null" : this.Origin.ToString());
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
        
            return this.Equals((InitializeCreditRequestResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.TaxamoTransaction != null ? this.TaxamoTransaction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Origin != null ? this.Origin.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(InitializeCreditRequestResult other)
        {
            if (!object.Equals(this.TaxamoTransaction, other.TaxamoTransaction))
            {
                return false;
            }
        
            if (!object.Equals(this.Origin, other.Origin))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class StripeTransactionResult 
    {
        public override string ToString()
        {
            return string.Format("StripeTransactionResult({0}, {1}, \"{2}\")", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.TransactionReference == null ? "null" : this.TransactionReference.ToString(), this.StripeChargeId == null ? "null" : this.StripeChargeId.ToString());
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
        
            return this.Equals((StripeTransactionResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TransactionReference != null ? this.TransactionReference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StripeChargeId != null ? this.StripeChargeId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(StripeTransactionResult other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.TransactionReference, other.TransactionReference))
            {
                return false;
            }
        
            if (!object.Equals(this.StripeChargeId, other.StripeChargeId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoCalculationResult
    {
        public partial class PossibleCountry 
        {
            public override string ToString()
            {
                return string.Format("PossibleCountry(\"{0}\", \"{1}\")", this.Name == null ? "null" : this.Name.ToString(), this.CountryCode == null ? "null" : this.CountryCode.ToString());
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
            
                return this.Equals((PossibleCountry)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(PossibleCountry other)
            {
                if (!object.Equals(this.Name, other.Name))
                {
                    return false;
                }
            
                if (!object.Equals(this.CountryCode, other.CountryCode))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoCalculationResult 
    {
        public override string ToString()
        {
            return string.Format("TaxamoCalculationResult({0}, {1}, {2}, {3}, \"{4}\", \"{5}\", \"{6}\", {7})", this.Amount == null ? "null" : this.Amount.ToString(), this.TotalAmount == null ? "null" : this.TotalAmount.ToString(), this.TaxAmount == null ? "null" : this.TaxAmount.ToString(), this.TaxRate == null ? "null" : this.TaxRate.ToString(), this.TaxName == null ? "null" : this.TaxName.ToString(), this.TaxEntityName == null ? "null" : this.TaxEntityName.ToString(), this.CountryName == null ? "null" : this.CountryName.ToString(), this.PossibleCountries == null ? "null" : this.PossibleCountries.ToString());
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
        
            return this.Equals((TaxamoCalculationResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TotalAmount != null ? this.TotalAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxAmount != null ? this.TaxAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxRate != null ? this.TaxRate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxName != null ? this.TaxName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxEntityName != null ? this.TaxEntityName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CountryName != null ? this.CountryName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PossibleCountries != null 
        			? this.PossibleCountries.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(TaxamoCalculationResult other)
        {
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            if (!object.Equals(this.TotalAmount, other.TotalAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxAmount, other.TaxAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxRate, other.TaxRate))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxName, other.TaxName))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxEntityName, other.TaxEntityName))
            {
                return false;
            }
        
            if (!object.Equals(this.CountryName, other.CountryName))
            {
                return false;
            }
        
            if (this.PossibleCountries != null && other.PossibleCountries != null)
            {
                if (!this.PossibleCountries.SequenceEqual(other.PossibleCountries))
                {
                    return false;    
                }
            }
            else if (this.PossibleCountries != null || other.PossibleCountries != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;
    using System.Collections.Generic;
    using global::Taxamo.Api;
    using global::Taxamo.Client;

    public partial class TaxamoTransactionResult 
    {
        public override string ToString()
        {
            return string.Format("TaxamoTransactionResult(\"{0}\", {1}, {2}, {3}, {4}, \"{5}\", \"{6}\", \"{7}\")", this.Key == null ? "null" : this.Key.ToString(), this.Amount == null ? "null" : this.Amount.ToString(), this.TotalAmount == null ? "null" : this.TotalAmount.ToString(), this.TaxAmount == null ? "null" : this.TaxAmount.ToString(), this.TaxRate == null ? "null" : this.TaxRate.ToString(), this.TaxName == null ? "null" : this.TaxName.ToString(), this.TaxEntityName == null ? "null" : this.TaxEntityName.ToString(), this.CountryName == null ? "null" : this.CountryName.ToString());
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
        
            return this.Equals((TaxamoTransactionResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Key != null ? this.Key.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TotalAmount != null ? this.TotalAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxAmount != null ? this.TaxAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxRate != null ? this.TaxRate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxName != null ? this.TaxName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaxEntityName != null ? this.TaxEntityName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CountryName != null ? this.CountryName.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(TaxamoTransactionResult other)
        {
            if (!object.Equals(this.Key, other.Key))
            {
                return false;
            }
        
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            if (!object.Equals(this.TotalAmount, other.TotalAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxAmount, other.TaxAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxRate, other.TaxRate))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxName, other.TaxName))
            {
                return false;
            }
        
            if (!object.Equals(this.TaxEntityName, other.TaxEntityName))
            {
                return false;
            }
        
            if (!object.Equals(this.CountryName, other.CountryName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Newtonsoft.Json;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using System.Collections.Generic;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit;

    public partial class UserPaymentOriginResult 
    {
        public override string ToString()
        {
            return string.Format("UserPaymentOriginResult(\"{0}\", {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", {6})", this.PaymentOriginKey == null ? "null" : this.PaymentOriginKey.ToString(), this.PaymentOriginKeyType == null ? "null" : this.PaymentOriginKeyType.ToString(), this.CountryCode == null ? "null" : this.CountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString(), this.OriginalTaxamoTransactionKey == null ? "null" : this.OriginalTaxamoTransactionKey.ToString(), this.PaymentStatus == null ? "null" : this.PaymentStatus.ToString());
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
        
            return this.Equals((UserPaymentOriginResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PaymentOriginKey != null ? this.PaymentOriginKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PaymentOriginKeyType != null ? this.PaymentOriginKeyType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OriginalTaxamoTransactionKey != null ? this.OriginalTaxamoTransactionKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PaymentStatus != null ? this.PaymentStatus.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserPaymentOriginResult other)
        {
            if (!object.Equals(this.PaymentOriginKey, other.PaymentOriginKey))
            {
                return false;
            }
        
            if (!object.Equals(this.PaymentOriginKeyType, other.PaymentOriginKeyType))
            {
                return false;
            }
        
            if (!object.Equals(this.CountryCode, other.CountryCode))
            {
                return false;
            }
        
            if (!object.Equals(this.CreditCardPrefix, other.CreditCardPrefix))
            {
                return false;
            }
        
            if (!object.Equals(this.IpAddress, other.IpAddress))
            {
                return false;
            }
        
            if (!object.Equals(this.OriginalTaxamoTransactionKey, other.OriginalTaxamoTransactionKey))
            {
                return false;
            }
        
            if (!object.Equals(this.PaymentStatus, other.PaymentStatus))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingData 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingData({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})", this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.CommittedAccountBalance == null ? "null" : this.CommittedAccountBalance.ToString(), this.SubscriberChannelsSnapshots == null ? "null" : this.SubscriberChannelsSnapshots.ToString(), this.SubscriberSnapshots == null ? "null" : this.SubscriberSnapshots.ToString(), this.CalculatedAccountBalanceSnapshots == null ? "null" : this.CalculatedAccountBalanceSnapshots.ToString(), this.CreatorChannelsSnapshots == null ? "null" : this.CreatorChannelsSnapshots.ToString(), this.CreatorFreeAccessUsersSnapshots == null ? "null" : this.CreatorFreeAccessUsersSnapshots.ToString(), this.CreatorPosts == null ? "null" : this.CreatorPosts.ToString(), this.CreatorPercentageOverride == null ? "null" : this.CreatorPercentageOverride.ToString());
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
        
            return this.Equals((PaymentProcessingData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommittedAccountBalance != null ? this.CommittedAccountBalance.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberChannelsSnapshots != null 
        			? this.SubscriberChannelsSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberSnapshots != null 
        			? this.SubscriberSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CalculatedAccountBalanceSnapshots != null 
        			? this.CalculatedAccountBalanceSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannelsSnapshots != null 
        			? this.CreatorChannelsSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorFreeAccessUsersSnapshots != null 
        			? this.CreatorFreeAccessUsersSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPosts != null 
        			? this.CreatorPosts.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPercentageOverride != null ? this.CreatorPercentageOverride.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentProcessingData other)
        {
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.CommittedAccountBalance, other.CommittedAccountBalance))
            {
                return false;
            }
        
            if (this.SubscriberChannelsSnapshots != null && other.SubscriberChannelsSnapshots != null)
            {
                if (!this.SubscriberChannelsSnapshots.SequenceEqual(other.SubscriberChannelsSnapshots))
                {
                    return false;    
                }
            }
            else if (this.SubscriberChannelsSnapshots != null || other.SubscriberChannelsSnapshots != null)
            {
                return false;
            }
        
            if (this.SubscriberSnapshots != null && other.SubscriberSnapshots != null)
            {
                if (!this.SubscriberSnapshots.SequenceEqual(other.SubscriberSnapshots))
                {
                    return false;    
                }
            }
            else if (this.SubscriberSnapshots != null || other.SubscriberSnapshots != null)
            {
                return false;
            }
        
            if (this.CalculatedAccountBalanceSnapshots != null && other.CalculatedAccountBalanceSnapshots != null)
            {
                if (!this.CalculatedAccountBalanceSnapshots.SequenceEqual(other.CalculatedAccountBalanceSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CalculatedAccountBalanceSnapshots != null || other.CalculatedAccountBalanceSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorChannelsSnapshots != null && other.CreatorChannelsSnapshots != null)
            {
                if (!this.CreatorChannelsSnapshots.SequenceEqual(other.CreatorChannelsSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannelsSnapshots != null || other.CreatorChannelsSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorFreeAccessUsersSnapshots != null && other.CreatorFreeAccessUsersSnapshots != null)
            {
                if (!this.CreatorFreeAccessUsersSnapshots.SequenceEqual(other.CreatorFreeAccessUsersSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CreatorFreeAccessUsersSnapshots != null || other.CreatorFreeAccessUsersSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorPosts != null && other.CreatorPosts != null)
            {
                if (!this.CreatorPosts.SequenceEqual(other.CreatorPosts))
                {
                    return false;    
                }
            }
            else if (this.CreatorPosts != null || other.CreatorPosts != null)
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorPercentageOverride, other.CreatorPercentageOverride))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingResult 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingResult({0}, {1}, {2}, {3}, {4})", this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.SubscriptionCost == null ? "null" : this.SubscriptionCost.ToString(), this.CreatorPercentageOverride == null ? "null" : this.CreatorPercentageOverride.ToString(), this.IsCommitted == null ? "null" : this.IsCommitted.ToString());
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
        
            return this.Equals((PaymentProcessingResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionCost != null ? this.SubscriptionCost.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPercentageOverride != null ? this.CreatorPercentageOverride.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsCommitted != null ? this.IsCommitted.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentProcessingResult other)
        {
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionCost, other.SubscriptionCost))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorPercentageOverride, other.CreatorPercentageOverride))
            {
                return false;
            }
        
            if (!object.Equals(this.IsCommitted, other.IsCommitted))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessingResults 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingResults({0}, {1})", this.CommittedAccountBalance == null ? "null" : this.CommittedAccountBalance.ToString(), this.Items == null ? "null" : this.Items.ToString());
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
        
            return this.Equals((PaymentProcessingResults)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CommittedAccountBalance != null ? this.CommittedAccountBalance.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Items != null 
        			? this.Items.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(PaymentProcessingResults other)
        {
            if (!object.Equals(this.CommittedAccountBalance, other.CommittedAccountBalance))
            {
                return false;
            }
        
            if (this.Items != null && other.Items != null)
            {
                if (!this.Items.SequenceEqual(other.Items))
                {
                    return false;    
                }
            }
            else if (this.Items != null || other.Items != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class PersistedPaymentProcessingData 
    {
        public override string ToString()
        {
            return string.Format("PersistedPaymentProcessingData({0}, {1}, {2})", this.Id == null ? "null" : this.Id.ToString(), this.Input == null ? "null" : this.Input.ToString(), this.Output == null ? "null" : this.Output.ToString());
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
        
            return this.Equals((PersistedPaymentProcessingData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Input != null ? this.Input.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Output != null ? this.Output.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PersistedPaymentProcessingData other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Input, other.Input))
            {
                return false;
            }
        
            if (!object.Equals(this.Output, other.Output))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class CreateTransactionRefund
    {
        public partial class CreateTransactionRefundResult 
        {
            public override string ToString()
            {
                return string.Format("CreateTransactionRefundResult({0}, {1})", this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString());
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
            
                return this.Equals((CreateTransactionRefundResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(CreateTransactionRefundResult other)
            {
                if (!object.Equals(this.SubscriberId, other.SubscriberId))
                {
                    return false;
                }
            
                if (!object.Equals(this.CreatorId, other.CreatorId))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Dapper;

    public partial class GetCreditTransactionInformation
    {
        public partial class GetCreditTransactionResult 
        {
            public override string ToString()
            {
                return string.Format("GetCreditTransactionResult({0}, \"{1}\", \"{2}\", {3}, {4})", this.UserId == null ? "null" : this.UserId.ToString(), this.StripeChargeId == null ? "null" : this.StripeChargeId.ToString(), this.TaxamoTransactionKey == null ? "null" : this.TaxamoTransactionKey.ToString(), this.TotalCreditAmount == null ? "null" : this.TotalCreditAmount.ToString(), this.CreditAmountAvailableForRefund == null ? "null" : this.CreditAmountAvailableForRefund.ToString());
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
            
                return this.Equals((GetCreditTransactionResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.StripeChargeId != null ? this.StripeChargeId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TaxamoTransactionKey != null ? this.TaxamoTransactionKey.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TotalCreditAmount != null ? this.TotalCreditAmount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CreditAmountAvailableForRefund != null ? this.CreditAmountAvailableForRefund.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(GetCreditTransactionResult other)
            {
                if (!object.Equals(this.UserId, other.UserId))
                {
                    return false;
                }
            
                if (!object.Equals(this.StripeChargeId, other.StripeChargeId))
                {
                    return false;
                }
            
                if (!object.Equals(this.TaxamoTransactionKey, other.TaxamoTransactionKey))
                {
                    return false;
                }
            
                if (!object.Equals(this.TotalCreditAmount, other.TotalCreditAmount))
                {
                    return false;
                }
            
                if (!object.Equals(this.CreditAmountAvailableForRefund, other.CreditAmountAvailableForRefund))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Payments.Services.Refunds.Taxamo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;
    using global::Taxamo.Model;

    public partial class CreateTaxamoRefund
    {
        public partial class TaxamoRefundResult 
        {
            public override string ToString()
            {
                return string.Format("TaxamoRefundResult({0}, {1})", this.TotalRefundAmount == null ? "null" : this.TotalRefundAmount.ToString(), this.TaxRefundAmount == null ? "null" : this.TaxRefundAmount.ToString());
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
            
                return this.Equals((TaxamoRefundResult)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.TotalRefundAmount != null ? this.TotalRefundAmount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TaxRefundAmount != null ? this.TaxRefundAmount.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(TaxamoRefundResult other)
            {
                if (!object.Equals(this.TotalRefundAmount, other.TotalRefundAmount))
                {
                    return false;
                }
            
                if (!object.Equals(this.TaxRefundAmount, other.TaxRefundAmount))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CalculatedAccountBalanceSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CalculatedAccountBalanceSnapshot({0}, {1}, {2}, {3})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.AccountType == null ? "null" : this.AccountType.ToString(), this.Amount == null ? "null" : this.Amount.ToString());
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
        
            return this.Equals((CalculatedAccountBalanceSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccountType != null ? this.AccountType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CalculatedAccountBalanceSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.AccountType, other.AccountType))
            {
                return false;
            }
        
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.CreatorChannels == null ? "null" : this.CreatorChannels.ToString());
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
        
            return this.Equals((CreatorChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannels != null 
        			? this.CreatorChannels.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(CreatorChannelsSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.CreatorChannels != null && other.CreatorChannels != null)
            {
                if (!this.CreatorChannels.SequenceEqual(other.CreatorChannels))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannels != null || other.CreatorChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshotItem({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Price == null ? "null" : this.Price.ToString());
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
        
            return this.Equals((CreatorChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelsSnapshotItem other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorFreeAccessUsersSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.FreeAccessUserEmails == null ? "null" : this.FreeAccessUserEmails.ToString());
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
        
            return this.Equals((CreatorFreeAccessUsersSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FreeAccessUserEmails != null 
        			? this.FreeAccessUserEmails.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(CreatorFreeAccessUsersSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.FreeAccessUserEmails != null && other.FreeAccessUserEmails != null)
            {
                if (!this.FreeAccessUserEmails.SequenceEqual(other.FreeAccessUserEmails))
                {
                    return false;    
                }
            }
            else if (this.FreeAccessUserEmails != null || other.FreeAccessUserEmails != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class MergedSnapshot 
    {
        public override string ToString()
        {
            return string.Format("MergedSnapshot({0}, {1}, {2}, {3}, {4}, {5})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorChannels == null ? "null" : this.CreatorChannels.ToString(), this.CreatorFreeAccessUsers == null ? "null" : this.CreatorFreeAccessUsers.ToString(), this.SubscriberChannels == null ? "null" : this.SubscriberChannels.ToString(), this.Subscriber == null ? "null" : this.Subscriber.ToString(), this.CalculatedAccountBalance == null ? "null" : this.CalculatedAccountBalance.ToString());
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
        
            return this.Equals((MergedSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannels != null ? this.CreatorChannels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorFreeAccessUsers != null ? this.CreatorFreeAccessUsers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberChannels != null ? this.SubscriberChannels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscriber != null ? this.Subscriber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CalculatedAccountBalance != null ? this.CalculatedAccountBalance.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(MergedSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorChannels, other.CreatorChannels))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorFreeAccessUsers, other.CreatorFreeAccessUsers))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberChannels, other.SubscriberChannels))
            {
                return false;
            }
        
            if (!object.Equals(this.Subscriber, other.Subscriber))
            {
                return false;
            }
        
            if (!object.Equals(this.CalculatedAccountBalance, other.CalculatedAccountBalance))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.SubscribedChannels == null ? "null" : this.SubscribedChannels.ToString());
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
        
            return this.Equals((SubscriberChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscribedChannels != null 
        			? this.SubscribedChannels.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(SubscriberChannelsSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (this.SubscribedChannels != null && other.SubscribedChannels != null)
            {
                if (!this.SubscribedChannels.SequenceEqual(other.SubscribedChannels))
                {
                    return false;    
                }
            }
            else if (this.SubscribedChannels != null || other.SubscribedChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshotItem({0}, {1}, {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
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
        
            return this.Equals((SubscriberChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelsSnapshotItem other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Channels.Shared;

    public partial class SubscriberSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberSnapshot({0}, {1}, \"{2}\")", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((SubscriberSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public partial class GetTransactionsResult 
    {
        public override string ToString()
        {
            return string.Format("GetTransactionsResult({0})", this.Records == null ? "null" : this.Records.ToString());
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
        
            return this.Equals((GetTransactionsResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Records != null 
        			? this.Records.Aggregate(0, (previous, current) => 
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
        
        protected bool Equals(GetTransactionsResult other)
        {
            if (this.Records != null && other.Records != null)
            {
                if (!this.Records.SequenceEqual(other.Records))
                {
                    return false;    
                }
            }
            else if (this.Records != null || other.Records != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public partial class GetTransactionsResult
    {
        public partial class Item 
        {
            public override string ToString()
            {
                return string.Format("Item({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, \"{11}\", \"{12}\", \"{13}\")", this.Id == null ? "null" : this.Id.ToString(), this.AccountOwnerId == null ? "null" : this.AccountOwnerId.ToString(), this.AccountOwnerUsername == null ? "null" : this.AccountOwnerUsername.ToString(), this.CounterpartyId == null ? "null" : this.CounterpartyId.ToString(), this.CounterpartyUsername == null ? "null" : this.CounterpartyUsername.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.Amount == null ? "null" : this.Amount.ToString(), this.AccountType == null ? "null" : this.AccountType.ToString(), this.TransactionType == null ? "null" : this.TransactionType.ToString(), this.TransactionReference == null ? "null" : this.TransactionReference.ToString(), this.InputDataReference == null ? "null" : this.InputDataReference.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.StripeChargeId == null ? "null" : this.StripeChargeId.ToString(), this.TaxamoTransactionKey == null ? "null" : this.TaxamoTransactionKey.ToString());
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
            
                return this.Equals((Item)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.AccountOwnerId != null ? this.AccountOwnerId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.AccountOwnerUsername != null ? this.AccountOwnerUsername.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CounterpartyId != null ? this.CounterpartyId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CounterpartyUsername != null ? this.CounterpartyUsername.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.AccountType != null ? this.AccountType.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TransactionType != null ? this.TransactionType.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TransactionReference != null ? this.TransactionReference.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.InputDataReference != null ? this.InputDataReference.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.StripeChargeId != null ? this.StripeChargeId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.TaxamoTransactionKey != null ? this.TaxamoTransactionKey.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(Item other)
            {
                if (!object.Equals(this.Id, other.Id))
                {
                    return false;
                }
            
                if (!object.Equals(this.AccountOwnerId, other.AccountOwnerId))
                {
                    return false;
                }
            
                if (!object.Equals(this.AccountOwnerUsername, other.AccountOwnerUsername))
                {
                    return false;
                }
            
                if (!object.Equals(this.CounterpartyId, other.CounterpartyId))
                {
                    return false;
                }
            
                if (!object.Equals(this.CounterpartyUsername, other.CounterpartyUsername))
                {
                    return false;
                }
            
                if (!object.Equals(this.Timestamp, other.Timestamp))
                {
                    return false;
                }
            
                if (!object.Equals(this.Amount, other.Amount))
                {
                    return false;
                }
            
                if (!object.Equals(this.AccountType, other.AccountType))
                {
                    return false;
                }
            
                if (!object.Equals(this.TransactionType, other.TransactionType))
                {
                    return false;
                }
            
                if (!object.Equals(this.TransactionReference, other.TransactionReference))
                {
                    return false;
                }
            
                if (!object.Equals(this.InputDataReference, other.InputDataReference))
                {
                    return false;
                }
            
                if (!object.Equals(this.Comment, other.Comment))
                {
                    return false;
                }
            
                if (!object.Equals(this.StripeChargeId, other.StripeChargeId))
                {
                    return false;
                }
            
                if (!object.Equals(this.TaxamoTransactionKey, other.TaxamoTransactionKey))
                {
                    return false;
                }
            
                return true;
            }
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Channels.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Payments.Snapshots;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Api.Azure;
    using Fifthweek.Payments.Pipeline;

    public partial class CommittedAccountBalance 
    {
        public override string ToString()
        {
            return string.Format("CommittedAccountBalance({0})", this.Amount == null ? "null" : this.Amount.ToString());
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
        
            return this.Equals((CommittedAccountBalance)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CommittedAccountBalance other)
        {
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            return true;
        }
    }
}


