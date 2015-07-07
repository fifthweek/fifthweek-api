using System;
using System.Linq;

//// Generated on 06/07/2015 19:10:28 (UTC)
//// Mapped solution in 12.67s


namespace Fifthweek.Api.Payments
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class AmountInUsCents 
    {
        public AmountInUsCents(
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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class ApplyCreditRequestCommand 
    {
        public ApplyCreditRequestCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Shared.PositiveInt amount,
            Fifthweek.Shared.PositiveInt expectedTotalAmount)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            if (expectedTotalAmount == null)
            {
                throw new ArgumentNullException("expectedTotalAmount");
            }

            this.Requester = requester;
            this.UserId = userId;
            this.Amount = amount;
            this.ExpectedTotalAmount = expectedTotalAmount;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class ApplyCreditRequestCommandHandler 
    {
        public ApplyCreditRequestCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Payments.Commands.IInitializeCreditRequest initializeCreditRequest,
            Fifthweek.Api.Payments.Commands.IPerformCreditRequest performCreditRequest,
            Fifthweek.Api.Payments.Commands.ICommitCreditToDatabase commitCreditToDatabase,
            Fifthweek.Api.Core.IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure,
            Fifthweek.Api.Payments.Taxamo.ICommitTaxamoTransaction commitTaxamoTransaction,
            Fifthweek.Api.Payments.Commands.ICommitTestUserCreditToDatabase commitTestUserCreditToDatabase)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

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

            if (retryOnTransientFailure == null)
            {
                throw new ArgumentNullException("retryOnTransientFailure");
            }

            if (commitTaxamoTransaction == null)
            {
                throw new ArgumentNullException("commitTaxamoTransaction");
            }

            if (commitTestUserCreditToDatabase == null)
            {
                throw new ArgumentNullException("commitTestUserCreditToDatabase");
            }

            this.requesterSecurity = requesterSecurity;
            this.initializeCreditRequest = initializeCreditRequest;
            this.performCreditRequest = performCreditRequest;
            this.commitCreditToDatabase = commitCreditToDatabase;
            this.retryOnTransientFailure = retryOnTransientFailure;
            this.commitTaxamoTransaction = commitTaxamoTransaction;
            this.commitTestUserCreditToDatabase = commitTestUserCreditToDatabase;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class InitializeCreditRequestResult 
    {
        public InitializeCreditRequestResult(
            Fifthweek.Api.Payments.Taxamo.TaxamoTransactionResult taxamoTransaction,
            Fifthweek.Api.Payments.UserPaymentOriginResult origin)
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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class StripeTransactionResult 
    {
        public StripeTransactionResult(
            System.DateTime timestamp,
            System.Guid transactionReference,
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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdatePaymentOriginCommand 
    {
        public UpdatePaymentOriginCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Payments.ValidStripeToken stripeToken,
            Fifthweek.Api.Payments.ValidCountryCode billingCountryCode,
            Fifthweek.Api.Payments.ValidCreditCardPrefix creditCardPrefix,
            Fifthweek.Api.Payments.ValidIpAddress ipAddress)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.Requester = requester;
            this.UserId = userId;
            this.StripeToken = stripeToken;
            this.BillingCountryCode = billingCountryCode;
            this.CreditCardPrefix = creditCardPrefix;
            this.IpAddress = ipAddress;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdatePaymentOriginCommandHandler 
    {
        public UpdatePaymentOriginCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Payments.ISetUserPaymentOriginDbStatement setUserPaymentOrigin,
            Fifthweek.Api.Payments.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Api.Payments.Stripe.ICreateStripeCustomer createStripeCustomer,
            Fifthweek.Api.Payments.Stripe.IUpdateStripeCustomerCreditCard updateStripeCustomerCreditCard)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (setUserPaymentOrigin == null)
            {
                throw new ArgumentNullException("setUserPaymentOrigin");
            }

            if (getUserPaymentOrigin == null)
            {
                throw new ArgumentNullException("getUserPaymentOrigin");
            }

            if (createStripeCustomer == null)
            {
                throw new ArgumentNullException("createStripeCustomer");
            }

            if (updateStripeCustomerCreditCard == null)
            {
                throw new ArgumentNullException("updateStripeCustomerCreditCard");
            }

            this.requesterSecurity = requesterSecurity;
            this.setUserPaymentOrigin = setUserPaymentOrigin;
            this.getUserPaymentOrigin = getUserPaymentOrigin;
            this.createStripeCustomer = createStripeCustomer;
            this.updateStripeCustomerCreditCard = updateStripeCustomerCreditCard;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class CreditRequestData 
    {
        public CreditRequestData(
            System.Int32 amount,
            System.Int32 expectedTotalAmount)
        {
            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            if (expectedTotalAmount == null)
            {
                throw new ArgumentNullException("expectedTotalAmount");
            }

            this.Amount = amount;
            this.ExpectedTotalAmount = expectedTotalAmount;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class CreditRequestSummary 
    {
        public CreditRequestSummary(
            System.Int32 amount,
            System.Int32 totalAmount,
            System.Int32 taxAmount,
            System.Decimal taxRate,
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

            if (taxRate == null)
            {
                throw new ArgumentNullException("taxRate");
            }

            if (taxName == null)
            {
                throw new ArgumentNullException("taxName");
            }

            if (taxEntityName == null)
            {
                throw new ArgumentNullException("taxEntityName");
            }

            if (countryName == null)
            {
                throw new ArgumentNullException("countryName");
            }

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
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class PaymentOriginData 
    {
        public PaymentOriginData(
            System.String stripeToken,
            System.String billingCountryCode,
            System.String creditCardPrefix,
            System.String ipAddress)
        {
            this.StripeToken = stripeToken;
            this.BillingCountryCode = billingCountryCode;
            this.CreditCardPrefix = creditCardPrefix;
            this.IpAddress = ipAddress;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class PaymentsController 
    {
        public PaymentsController(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Payments.Queries.GetCreditRequestSummaryQuery,Fifthweek.Api.Payments.Controllers.CreditRequestSummary> getCreditRequestSummary,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.UpdatePaymentOriginCommand> updatePaymentsOrigin,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.ApplyCreditRequestCommand> applyCreditRequest)
        {
            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (getCreditRequestSummary == null)
            {
                throw new ArgumentNullException("getCreditRequestSummary");
            }

            if (updatePaymentsOrigin == null)
            {
                throw new ArgumentNullException("updatePaymentsOrigin");
            }

            if (applyCreditRequest == null)
            {
                throw new ArgumentNullException("applyCreditRequest");
            }

            this.requesterContext = requesterContext;
            this.getCreditRequestSummary = getCreditRequestSummary;
            this.updatePaymentsOrigin = updatePaymentsOrigin;
            this.applyCreditRequest = applyCreditRequest;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

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
namespace Fifthweek.Api.Payments.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Payments.Taxamo;

    public partial class GetCreditRequestSummaryQuery 
    {
        public GetCreditRequestSummaryQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Shared.PositiveInt amount)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            this.Requester = requester;
            this.UserId = userId;
            this.Amount = amount;
        }
    }
}
namespace Fifthweek.Api.Payments.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Payments.Taxamo;

    public partial class GetCreditRequestSummaryQueryHandler 
    {
        public GetCreditRequestSummaryQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Payments.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Api.Payments.Taxamo.IGetTaxInformation getTaxInformation)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (getUserPaymentOrigin == null)
            {
                throw new ArgumentNullException("getUserPaymentOrigin");
            }

            if (getTaxInformation == null)
            {
                throw new ArgumentNullException("getTaxInformation");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserPaymentOrigin = getUserPaymentOrigin;
            this.getTaxInformation = getTaxInformation;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class SetUserPaymentOriginDbStatement 
    {
        public SetUserPaymentOriginDbStatement(
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
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

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
namespace Fifthweek.Api.Payments.Taxamo
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class TaxamoTransactionResult 
    {
        public TaxamoTransactionResult(
            System.String key,
            Fifthweek.Api.Payments.AmountInUsCents amount,
            Fifthweek.Api.Payments.AmountInUsCents totalAmount,
            Fifthweek.Api.Payments.AmountInUsCents taxAmount,
            System.Decimal taxRate,
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

            if (taxRate == null)
            {
                throw new ArgumentNullException("taxRate");
            }

            if (taxName == null)
            {
                throw new ArgumentNullException("taxName");
            }

            if (taxEntityName == null)
            {
                throw new ArgumentNullException("taxEntityName");
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
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class UserPaymentOriginResult 
    {
        public UserPaymentOriginResult(
            System.String stripeCustomerId,
            System.String billingCountryCode,
            System.String creditCardPrefix,
            System.String ipAddress,
            System.String originalTaxamoTransactionKey)
        {
            this.StripeCustomerId = stripeCustomerId;
            this.BillingCountryCode = billingCountryCode;
            this.CreditCardPrefix = creditCardPrefix;
            this.IpAddress = ipAddress;
            this.OriginalTaxamoTransactionKey = originalTaxamoTransactionKey;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CommitCreditToDatabase 
    {
        public CommitCreditToDatabase(
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances,
            Fifthweek.Api.Payments.ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement setUserPaymentOriginOriginalTaxamoTransactionKey,
            Fifthweek.Api.Payments.ISaveCustomerCreditToLedgerDbStatement saveCustomerCreditToLedger)
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

            this.updateAccountBalances = updateAccountBalances;
            this.setUserPaymentOriginOriginalTaxamoTransactionKey = setUserPaymentOriginOriginalTaxamoTransactionKey;
            this.saveCustomerCreditToLedger = saveCustomerCreditToLedger;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class InitializeCreditRequest 
    {
        public InitializeCreditRequest(
            Fifthweek.Api.Payments.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Api.Payments.Taxamo.IDeleteTaxamoTransaction deleteTaxamoTransaction,
            Fifthweek.Api.Payments.Taxamo.ICreateTaxamoTransaction createTaxamoTransaction)
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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PerformCreditRequest 
    {
        public PerformCreditRequest(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Api.Payments.Stripe.IPerformStripeCharge performStripeCharge,
            Fifthweek.Shared.IGuidCreator guidCreator)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (performStripeCharge == null)
            {
                throw new ArgumentNullException("performStripeCharge");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.timestampCreator = timestampCreator;
            this.performStripeCharge = performStripeCharge;
            this.guidCreator = guidCreator;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CommitTestUserCreditToDatabase 
    {
        public CommitTestUserCreditToDatabase(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Api.Payments.ISetTestUserAccountBalanceDbStatement setTestUserAccountBalance)
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
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

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

namespace Fifthweek.Api.Payments
{
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class AmountInUsCents 
    {
        public override string ToString()
        {
            return string.Format("AmountInUsCents({0})", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((AmountInUsCents)obj);
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
        
        protected bool Equals(AmountInUsCents other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class ApplyCreditRequestCommand 
    {
        public override string ToString()
        {
            return string.Format("ApplyCreditRequestCommand({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Amount == null ? "null" : this.Amount.ToString(), this.ExpectedTotalAmount == null ? "null" : this.ExpectedTotalAmount.ToString());
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
        
            return this.Equals((ApplyCreditRequestCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpectedTotalAmount != null ? this.ExpectedTotalAmount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ApplyCreditRequestCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpectedTotalAmount, other.ExpectedTotalAmount))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Newtonsoft.Json;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdatePaymentOriginCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdatePaymentOriginCommand({0}, {1}, {2}, {3}, {4}, {5})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.StripeToken == null ? "null" : this.StripeToken.ToString(), this.BillingCountryCode == null ? "null" : this.BillingCountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
        
            return this.Equals((UpdatePaymentOriginCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StripeToken != null ? this.StripeToken.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BillingCountryCode != null ? this.BillingCountryCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UpdatePaymentOriginCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.StripeToken, other.StripeToken))
            {
                return false;
            }
        
            if (!object.Equals(this.BillingCountryCode, other.BillingCountryCode))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class CreditRequestData 
    {
        public override string ToString()
        {
            return string.Format("CreditRequestData({0}, {1})", this.Amount == null ? "null" : this.Amount.ToString(), this.ExpectedTotalAmount == null ? "null" : this.ExpectedTotalAmount.ToString());
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
        
            return this.Equals((CreditRequestData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpectedTotalAmount != null ? this.ExpectedTotalAmount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreditRequestData other)
        {
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpectedTotalAmount, other.ExpectedTotalAmount))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class CreditRequestSummary 
    {
        public override string ToString()
        {
            return string.Format("CreditRequestSummary({0}, {1}, {2}, {3}, \"{4}\", \"{5}\", \"{6}\")", this.Amount == null ? "null" : this.Amount.ToString(), this.TotalAmount == null ? "null" : this.TotalAmount.ToString(), this.TaxAmount == null ? "null" : this.TaxAmount.ToString(), this.TaxRate == null ? "null" : this.TaxRate.ToString(), this.TaxName == null ? "null" : this.TaxName.ToString(), this.TaxEntityName == null ? "null" : this.TaxEntityName.ToString(), this.CountryName == null ? "null" : this.CountryName.ToString());
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
        
            return this.Equals((CreditRequestSummary)obj);
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
                return hashCode;
            }
        }
        
        protected bool Equals(CreditRequestSummary other)
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class PaymentOriginData 
    {
        public override string ToString()
        {
            return string.Format("PaymentOriginData(\"{0}\", \"{1}\", \"{2}\", \"{3}\")", this.StripeToken == null ? "null" : this.StripeToken.ToString(), this.BillingCountryCode == null ? "null" : this.BillingCountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
        
            return this.Equals((PaymentOriginData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StripeToken != null ? this.StripeToken.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BillingCountryCode != null ? this.BillingCountryCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentOriginData other)
        {
            if (!object.Equals(this.StripeToken, other.StripeToken))
            {
                return false;
            }
        
            if (!object.Equals(this.BillingCountryCode, other.BillingCountryCode))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Payments.Taxamo;

    public partial class GetCreditRequestSummaryQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreditRequestSummaryQuery({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Amount == null ? "null" : this.Amount.ToString());
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
        
            return this.Equals((GetCreditRequestSummaryQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(GetCreditRequestSummaryQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
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
namespace Fifthweek.Api.Payments.Taxamo
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;

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
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class UserPaymentOriginResult 
    {
        public override string ToString()
        {
            return string.Format("UserPaymentOriginResult(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\")", this.StripeCustomerId == null ? "null" : this.StripeCustomerId.ToString(), this.BillingCountryCode == null ? "null" : this.BillingCountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString(), this.OriginalTaxamoTransactionKey == null ? "null" : this.OriginalTaxamoTransactionKey.ToString());
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
                hashCode = (hashCode * 397) ^ (this.StripeCustomerId != null ? this.StripeCustomerId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BillingCountryCode != null ? this.BillingCountryCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.OriginalTaxamoTransactionKey != null ? this.OriginalTaxamoTransactionKey.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(UserPaymentOriginResult other)
        {
            if (!object.Equals(this.StripeCustomerId, other.StripeCustomerId))
            {
                return false;
            }
        
            if (!object.Equals(this.BillingCountryCode, other.BillingCountryCode))
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
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class ValidCountryCode 
    {
        public override string ToString()
        {
            return string.Format("ValidCountryCode(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidCountryCode)obj);
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
        
        protected bool Equals(ValidCountryCode other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class ValidCreditCardPrefix 
    {
        public override string ToString()
        {
            return string.Format("ValidCreditCardPrefix(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidCreditCardPrefix)obj);
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
        
        protected bool Equals(ValidCreditCardPrefix other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class ValidIpAddress 
    {
        public override string ToString()
        {
            return string.Format("ValidIpAddress(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidIpAddress)obj);
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
        
        protected bool Equals(ValidIpAddress other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;
    using System.Collections.Generic;
    using System.Net;

    public partial class ValidStripeToken 
    {
        public override string ToString()
        {
            return string.Format("ValidStripeToken(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((ValidStripeToken)obj);
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
        
        protected bool Equals(ValidStripeToken other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class CreditRequestData 
    {
        public class Parsed
        {
            public Parsed(
                PositiveInt amount,
                PositiveInt expectedTotalAmount)
            {
                if (amount == null)
                {
                    throw new ArgumentNullException("amount");
                }

                if (expectedTotalAmount == null)
                {
                    throw new ArgumentNullException("expectedTotalAmount");
                }

                this.Amount = amount;
                this.ExpectedTotalAmount = expectedTotalAmount;
            }
        
            public PositiveInt Amount { get; private set; }
        
            public PositiveInt ExpectedTotalAmount { get; private set; }
        }
    }

    public static partial class CreditRequestDataExtensions
    {
        public static CreditRequestData.Parsed Parse(this CreditRequestData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            PositiveInt parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!PositiveInt.TryParse(target.Amount, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Amount", modelState);
            }

            PositiveInt parsed1 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
            if (!PositiveInt.TryParse(target.ExpectedTotalAmount, out parsed1, out parsed1Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed1Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("ExpectedTotalAmount", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CreditRequestData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}
namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;

    public partial class PaymentOriginData 
    {
        public class Parsed
        {
            public Parsed(
                ValidStripeToken stripeToken,
                ValidCountryCode billingCountryCode,
                ValidCreditCardPrefix creditCardPrefix,
                ValidIpAddress ipAddress)
            {
                this.StripeToken = stripeToken;
                this.BillingCountryCode = billingCountryCode;
                this.CreditCardPrefix = creditCardPrefix;
                this.IpAddress = ipAddress;
            }
        
            public ValidStripeToken StripeToken { get; private set; }
        
            public ValidCountryCode BillingCountryCode { get; private set; }
        
            public ValidCreditCardPrefix CreditCardPrefix { get; private set; }
        
            public ValidIpAddress IpAddress { get; private set; }
        }
    }

    public static partial class PaymentOriginDataExtensions
    {
        public static PaymentOriginData.Parsed Parse(this PaymentOriginData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidStripeToken parsed0 = null;
            if (!ValidStripeToken.IsEmpty(target.StripeToken))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidStripeToken.TryParse(target.StripeToken, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("StripeToken", modelState);
                }
            }

            ValidCountryCode parsed1 = null;
            if (!ValidCountryCode.IsEmpty(target.BillingCountryCode))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidCountryCode.TryParse(target.BillingCountryCode, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("BillingCountryCode", modelState);
                }
            }

            ValidCreditCardPrefix parsed2 = null;
            if (!ValidCreditCardPrefix.IsEmpty(target.CreditCardPrefix))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidCreditCardPrefix.TryParse(target.CreditCardPrefix, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("CreditCardPrefix", modelState);
                }
            }

            ValidIpAddress parsed3 = null;
            if (!ValidIpAddress.IsEmpty(target.IpAddress))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed3Errors;
                if (!ValidIpAddress.TryParse(target.IpAddress, out parsed3, out parsed3Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed3Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("IpAddress", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new PaymentOriginData.Parsed(
                parsed0,
                parsed1,
                parsed2,
                parsed3);
        }    
    }
}


