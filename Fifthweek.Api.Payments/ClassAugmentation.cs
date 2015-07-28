using System;
using System.Linq;

//// Generated on 28/07/2015 18:51:04 (UTC)
//// Mapped solution in 14.21s


namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class ApplyCreditRequestCommand 
    {
        public ApplyCreditRequestCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            System.DateTime timestamp,
            Fifthweek.Payments.Shared.TransactionReference transactionReference,
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

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (transactionReference == null)
            {
                throw new ArgumentNullException("transactionReference");
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
            this.Timestamp = timestamp;
            this.TransactionReference = transactionReference;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class ApplyCreditRequestCommandHandler 
    {
        public ApplyCreditRequestCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Shared.IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure,
            Fifthweek.Payments.Services.Credit.IApplyUserCredit applyUserCredit,
            Fifthweek.Payments.Services.Credit.IFailPaymentStatusDbStatement failPaymentStatus)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (retryOnTransientFailure == null)
            {
                throw new ArgumentNullException("retryOnTransientFailure");
            }

            if (applyUserCredit == null)
            {
                throw new ArgumentNullException("applyUserCredit");
            }

            if (failPaymentStatus == null)
            {
                throw new ArgumentNullException("failPaymentStatus");
            }

            this.requesterSecurity = requesterSecurity;
            this.retryOnTransientFailure = retryOnTransientFailure;
            this.applyUserCredit = applyUserCredit;
            this.failPaymentStatus = failPaymentStatus;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class UpdatePaymentOriginCommand 
    {
        public UpdatePaymentOriginCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Payments.ValidStripeToken stripeToken,
            Fifthweek.Api.Payments.ValidCountryCode countryCode,
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
            this.CountryCode = countryCode;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class UpdatePaymentOriginCommandHandler 
    {
        public UpdatePaymentOriginCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Payments.ISetUserPaymentOriginDbStatement setUserPaymentOrigin,
            Fifthweek.Payments.Services.Credit.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Payments.Services.Credit.Stripe.ICreateStripeCustomer createStripeCustomer,
            Fifthweek.Payments.Services.Credit.Stripe.IUpdateStripeCustomerCreditCard updateStripeCustomerCreditCard)
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class CreditRequestSummary 
    {
        public CreditRequestSummary(
            System.Int32 subscriptionsAmount,
            Fifthweek.Payments.Services.Credit.Taxamo.TaxamoCalculationResult calculation)
        {
            if (subscriptionsAmount == null)
            {
                throw new ArgumentNullException("subscriptionsAmount");
            }

            if (calculation == null)
            {
                throw new ArgumentNullException("calculation");
            }

            this.SubscriptionsAmount = subscriptionsAmount;
            this.Calculation = calculation;
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentOriginData 
    {
        public PaymentOriginData(
            System.String stripeToken,
            System.String countryCode,
            System.String creditCardPrefix,
            System.String ipAddress)
        {
            this.StripeToken = stripeToken;
            this.CountryCode = countryCode;
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentsController 
    {
        public PaymentsController(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Payments.Queries.GetCreditRequestSummaryQuery,Fifthweek.Api.Payments.Controllers.CreditRequestSummary> getCreditRequestSummary,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.UpdatePaymentOriginCommand> updatePaymentsOrigin,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.ApplyCreditRequestCommand> applyCreditRequest,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.DeletePaymentInformationCommand> deletePaymentInformation,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.CreateCreditRefundCommand> createCreditRefund,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Payments.Commands.CreateTransactionRefundCommand> createTransactionRefund,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Shared.IGuidCreator guidCreator)
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

            if (deletePaymentInformation == null)
            {
                throw new ArgumentNullException("deletePaymentInformation");
            }

            if (createCreditRefund == null)
            {
                throw new ArgumentNullException("createCreditRefund");
            }

            if (createTransactionRefund == null)
            {
                throw new ArgumentNullException("createTransactionRefund");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.requesterContext = requesterContext;
            this.getCreditRequestSummary = getCreditRequestSummary;
            this.updatePaymentsOrigin = updatePaymentsOrigin;
            this.applyCreditRequest = applyCreditRequest;
            this.deletePaymentInformation = deletePaymentInformation;
            this.createCreditRefund = createCreditRefund;
            this.createTransactionRefund = createTransactionRefund;
            this.timestampCreator = timestampCreator;
            this.guidCreator = guidCreator;
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
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    public partial class GetCreditRequestSummaryQuery 
    {
        public GetCreditRequestSummaryQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Payments.Queries.GetCreditRequestSummaryQuery.LocationData locationDataOverride)
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
            this.LocationDataOverride = locationDataOverride;
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
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    public partial class GetCreditRequestSummaryQueryHandler 
    {
        public GetCreditRequestSummaryQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Payments.Services.Credit.IGetUserPaymentOriginDbStatement getUserPaymentOrigin,
            Fifthweek.Payments.Services.Credit.Taxamo.IGetTaxInformation getTaxInformation,
            Fifthweek.Payments.Services.Credit.IGetUserWeeklySubscriptionsCost getUserWeeklySubscriptionsCost)
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

            if (getUserWeeklySubscriptionsCost == null)
            {
                throw new ArgumentNullException("getUserWeeklySubscriptionsCost");
            }

            this.requesterSecurity = requesterSecurity;
            this.getUserPaymentOrigin = getUserPaymentOrigin;
            this.getTaxInformation = getTaxInformation;
            this.getUserWeeklySubscriptionsCost = getUserWeeklySubscriptionsCost;
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentLocationData 
    {
        public PaymentLocationData(
            System.String countryCode,
            System.String creditCardPrefix,
            System.String ipAddress)
        {
            this.CountryCode = countryCode;
            this.CreditCardPrefix = creditCardPrefix;
            this.IpAddress = ipAddress;
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
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    public partial class GetCreditRequestSummaryQuery
    {
        public partial class LocationData 
        {
            public LocationData(
                Fifthweek.Api.Payments.ValidCountryCode countryCode,
                Fifthweek.Api.Payments.ValidCreditCardPrefix creditCardPrefix,
                Fifthweek.Api.Payments.ValidIpAddress ipAddress)
            {
                this.CountryCode = countryCode;
                this.CreditCardPrefix = creditCardPrefix;
                this.IpAddress = ipAddress;
            }
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class DeletePaymentInformationCommand 
    {
        public DeletePaymentInformationCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId)
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class DeletePaymentInformationCommandHandler 
    {
        public DeletePaymentInformationCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Payments.IDeleteUserPaymentInformationDbStatement deleteUserPaymentInformation)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (deleteUserPaymentInformation == null)
            {
                throw new ArgumentNullException("deleteUserPaymentInformation");
            }

            this.requesterSecurity = requesterSecurity;
            this.deleteUserPaymentInformation = deleteUserPaymentInformation;
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

    public partial class DeleteUserPaymentInformationDbStatement 
    {
        public DeleteUserPaymentInformationDbStatement(
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
namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateTransactionRefundCommandHandler 
    {
        public CreateTransactionRefundCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Payments.Services.Refunds.ICreateTransactionRefund createTransactionRefund,
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (createTransactionRefund == null)
            {
                throw new ArgumentNullException("createTransactionRefund");
            }

            if (updateAccountBalances == null)
            {
                throw new ArgumentNullException("updateAccountBalances");
            }

            this.requesterSecurity = requesterSecurity;
            this.createTransactionRefund = createTransactionRefund;
            this.updateAccountBalances = updateAccountBalances;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateCreditRefundCommandHandler 
    {
        public CreateCreditRefundCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Payments.Services.Refunds.ICreateCreditRefund createCreditRefund,
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (createCreditRefund == null)
            {
                throw new ArgumentNullException("createCreditRefund");
            }

            if (updateAccountBalances == null)
            {
                throw new ArgumentNullException("updateAccountBalances");
            }

            this.requesterSecurity = requesterSecurity;
            this.createCreditRefund = createCreditRefund;
            this.updateAccountBalances = updateAccountBalances;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateCreditRefundCommand 
    {
        public CreateCreditRefundCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Payments.Shared.TransactionReference transactionReference,
            System.DateTime timestamp,
            Fifthweek.Shared.PositiveInt refundCreditAmount,
            Fifthweek.Payments.Services.Refunds.RefundCreditReason reason,
            System.String comment)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (transactionReference == null)
            {
                throw new ArgumentNullException("transactionReference");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (refundCreditAmount == null)
            {
                throw new ArgumentNullException("refundCreditAmount");
            }

            if (reason == null)
            {
                throw new ArgumentNullException("reason");
            }

            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            this.Requester = requester;
            this.TransactionReference = transactionReference;
            this.Timestamp = timestamp;
            this.RefundCreditAmount = refundCreditAmount;
            this.Reason = reason;
            this.Comment = comment;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateTransactionRefundCommand 
    {
        public CreateTransactionRefundCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Payments.Shared.TransactionReference transactionReference,
            System.DateTime timestamp,
            System.String comment)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (transactionReference == null)
            {
                throw new ArgumentNullException("transactionReference");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            this.Requester = requester;
            this.TransactionReference = transactionReference;
            this.Timestamp = timestamp;
            this.Comment = comment;
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class TransactionRefundData 
    {
        public TransactionRefundData(
            System.String comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            this.Comment = comment;
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class CreditRefundData 
    {
        public CreditRefundData(
            System.Int32 refundCreditAmount,
            Fifthweek.Payments.Services.Refunds.RefundCreditReason reason,
            System.String comment)
        {
            if (refundCreditAmount == null)
            {
                throw new ArgumentNullException("refundCreditAmount");
            }

            if (reason == null)
            {
                throw new ArgumentNullException("reason");
            }

            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            this.RefundCreditAmount = refundCreditAmount;
            this.Reason = reason;
            this.Comment = comment;
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class ApplyCreditRequestCommand 
    {
        public override string ToString()
        {
            return string.Format("ApplyCreditRequestCommand({0}, {1}, {2}, {3}, {4}, {5})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.TransactionReference == null ? "null" : this.TransactionReference.ToString(), this.Amount == null ? "null" : this.Amount.ToString(), this.ExpectedTotalAmount == null ? "null" : this.ExpectedTotalAmount.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TransactionReference != null ? this.TransactionReference.GetHashCode() : 0);
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
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.TransactionReference, other.TransactionReference))
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class UpdatePaymentOriginCommand 
    {
        public override string ToString()
        {
            return string.Format("UpdatePaymentOriginCommand({0}, {1}, {2}, {3}, {4}, {5})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.StripeToken == null ? "null" : this.StripeToken.ToString(), this.CountryCode == null ? "null" : this.CountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class CreditRequestSummary 
    {
        public override string ToString()
        {
            return string.Format("CreditRequestSummary({0}, {1})", this.SubscriptionsAmount == null ? "null" : this.SubscriptionsAmount.ToString(), this.Calculation == null ? "null" : this.Calculation.ToString());
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
                hashCode = (hashCode * 397) ^ (this.SubscriptionsAmount != null ? this.SubscriptionsAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Calculation != null ? this.Calculation.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreditRequestSummary other)
        {
            if (!object.Equals(this.SubscriptionsAmount, other.SubscriptionsAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.Calculation, other.Calculation))
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentOriginData 
    {
        public override string ToString()
        {
            return string.Format("PaymentOriginData(\"{0}\", \"{1}\", \"{2}\", \"{3}\")", this.StripeToken == null ? "null" : this.StripeToken.ToString(), this.CountryCode == null ? "null" : this.CountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
                hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
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
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    public partial class GetCreditRequestSummaryQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreditRequestSummaryQuery({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.LocationDataOverride == null ? "null" : this.LocationDataOverride.ToString());
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
                hashCode = (hashCode * 397) ^ (this.LocationDataOverride != null ? this.LocationDataOverride.GetHashCode() : 0);
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
        
            if (!object.Equals(this.LocationDataOverride, other.LocationDataOverride))
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentLocationData 
    {
        public override string ToString()
        {
            return string.Format("PaymentLocationData(\"{0}\", \"{1}\", \"{2}\")", this.CountryCode == null ? "null" : this.CountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
        
            return this.Equals((PaymentLocationData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentLocationData other)
        {
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
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    public partial class GetCreditRequestSummaryQuery
    {
        public partial class LocationData 
        {
            public override string ToString()
            {
                return string.Format("LocationData({0}, {1}, {2})", this.CountryCode == null ? "null" : this.CountryCode.ToString(), this.CreditCardPrefix == null ? "null" : this.CreditCardPrefix.ToString(), this.IpAddress == null ? "null" : this.IpAddress.ToString());
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
            
                return this.Equals((LocationData)obj);
            }
            
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 0;
                    hashCode = (hashCode * 397) ^ (this.CountryCode != null ? this.CountryCode.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.CreditCardPrefix != null ? this.CreditCardPrefix.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.IpAddress != null ? this.IpAddress.GetHashCode() : 0);
                    return hashCode;
                }
            }
            
            protected bool Equals(LocationData other)
            {
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
            
                return true;
            }
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class DeletePaymentInformationCommand 
    {
        public override string ToString()
        {
            return string.Format("DeletePaymentInformationCommand({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.UserId == null ? "null" : this.UserId.ToString());
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
        
            return this.Equals((DeletePaymentInformationCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeletePaymentInformationCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateCreditRefundCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateCreditRefundCommand({0}, {1}, {2}, {3}, {4}, \"{5}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.TransactionReference == null ? "null" : this.TransactionReference.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.RefundCreditAmount == null ? "null" : this.RefundCreditAmount.ToString(), this.Reason == null ? "null" : this.Reason.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((CreateCreditRefundCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TransactionReference != null ? this.TransactionReference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RefundCreditAmount != null ? this.RefundCreditAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Reason != null ? this.Reason.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateCreditRefundCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.TransactionReference, other.TransactionReference))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.RefundCreditAmount, other.RefundCreditAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.Reason, other.Reason))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
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
    using Newtonsoft.Json;
    using Fifthweek.Payments.Services;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    public partial class CreateTransactionRefundCommand 
    {
        public override string ToString()
        {
            return string.Format("CreateTransactionRefundCommand({0}, {1}, {2}, \"{3}\")", this.Requester == null ? "null" : this.Requester.ToString(), this.TransactionReference == null ? "null" : this.TransactionReference.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((CreateTransactionRefundCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TransactionReference != null ? this.TransactionReference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreateTransactionRefundCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.TransactionReference, other.TransactionReference))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class TransactionRefundData 
    {
        public override string ToString()
        {
            return string.Format("TransactionRefundData(\"{0}\")", this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((TransactionRefundData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(TransactionRefundData other)
        {
            if (!object.Equals(this.Comment, other.Comment))
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class CreditRefundData 
    {
        public override string ToString()
        {
            return string.Format("CreditRefundData({0}, {1}, \"{2}\")", this.RefundCreditAmount == null ? "null" : this.RefundCreditAmount.ToString(), this.Reason == null ? "null" : this.Reason.ToString(), this.Comment == null ? "null" : this.Comment.ToString());
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
        
            return this.Equals((CreditRefundData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.RefundCreditAmount != null ? this.RefundCreditAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Reason != null ? this.Reason.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreditRefundData other)
        {
            if (!object.Equals(this.RefundCreditAmount, other.RefundCreditAmount))
            {
                return false;
            }
        
            if (!object.Equals(this.Reason, other.Reason))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentOriginData 
    {
        public class Parsed
        {
            public Parsed(
                ValidStripeToken stripeToken,
                ValidCountryCode countryCode,
                ValidCreditCardPrefix creditCardPrefix,
                ValidIpAddress ipAddress)
            {
                this.StripeToken = stripeToken;
                this.CountryCode = countryCode;
                this.CreditCardPrefix = creditCardPrefix;
                this.IpAddress = ipAddress;
            }
        
            public ValidStripeToken StripeToken { get; private set; }
        
            public ValidCountryCode CountryCode { get; private set; }
        
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
            if (!ValidCountryCode.IsEmpty(target.CountryCode))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidCountryCode.TryParse(target.CountryCode, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("CountryCode", modelState);
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class PaymentLocationData 
    {
        public class Parsed
        {
            public Parsed(
                ValidCountryCode countryCode,
                ValidCreditCardPrefix creditCardPrefix,
                ValidIpAddress ipAddress)
            {
                this.CountryCode = countryCode;
                this.CreditCardPrefix = creditCardPrefix;
                this.IpAddress = ipAddress;
            }
        
            public ValidCountryCode CountryCode { get; private set; }
        
            public ValidCreditCardPrefix CreditCardPrefix { get; private set; }
        
            public ValidIpAddress IpAddress { get; private set; }
        }
    }

    public static partial class PaymentLocationDataExtensions
    {
        public static PaymentLocationData.Parsed Parse(this PaymentLocationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidCountryCode parsed0 = null;
            if (!ValidCountryCode.IsEmpty(target.CountryCode))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidCountryCode.TryParse(target.CountryCode, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("CountryCode", modelState);
                }
            }

            ValidCreditCardPrefix parsed1 = null;
            if (!ValidCreditCardPrefix.IsEmpty(target.CreditCardPrefix))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
                if (!ValidCreditCardPrefix.TryParse(target.CreditCardPrefix, out parsed1, out parsed1Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed1Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("CreditCardPrefix", modelState);
                }
            }

            ValidIpAddress parsed2 = null;
            if (!ValidIpAddress.IsEmpty(target.IpAddress))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed2Errors;
                if (!ValidIpAddress.TryParse(target.IpAddress, out parsed2, out parsed2Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed2Errors)
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
        
            return new PaymentLocationData.Parsed(
                parsed0,
                parsed1,
                parsed2);
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
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Services.Refunds;

    public partial class CreditRefundData 
    {
        public class Parsed
        {
            public Parsed(
                PositiveInt refundCreditAmount,
                Fifthweek.Payments.Services.Refunds.RefundCreditReason reason,
                System.String comment)
            {
                if (refundCreditAmount == null)
                {
                    throw new ArgumentNullException("refundCreditAmount");
                }

                if (reason == null)
                {
                    throw new ArgumentNullException("reason");
                }

                if (comment == null)
                {
                    throw new ArgumentNullException("comment");
                }

                this.RefundCreditAmount = refundCreditAmount;
                this.Reason = reason;
                this.Comment = comment;
            }
        
            public PositiveInt RefundCreditAmount { get; private set; }
        
            public Fifthweek.Payments.Services.Refunds.RefundCreditReason Reason { get; private set; }
        
            public System.String Comment { get; private set; }
        }
    }

    public static partial class CreditRefundDataExtensions
    {
        public static CreditRefundData.Parsed Parse(this CreditRefundData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            PositiveInt parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!PositiveInt.TryParse(target.RefundCreditAmount, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("RefundCreditAmount", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CreditRefundData.Parsed(
                parsed0,
                target.Reason,
                target.Comment);
        }    
    }
}


