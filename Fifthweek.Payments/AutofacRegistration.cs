namespace Fifthweek.Payments
{
    using Autofac;

    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AggregateCostPeriodsExecutor>().As<IAggregateCostPeriodsExecutor>();
            builder.RegisterType<CalculateCostPeriodsExecutor>().As<ICalculateCostPeriodsExecutor>();
            builder.RegisterType<CalculateSnapshotCostExecutor>().As<ICalculateSnapshotCostExecutor>();
            builder.RegisterType<MergeSnapshotsExecutor>().As<IMergeSnapshotsExecutor>();
            builder.RegisterType<RollBackSubscriptionsExecutor>().As<IRollBackSubscriptionsExecutor>();
            builder.RegisterType<RollForwardSubscriptionsExecutor>().As<IRollForwardSubscriptionsExecutor>();
            builder.RegisterType<TrimSnapshotsExecutor>().As<ITrimSnapshotsExecutor>();
            builder.RegisterType<VerifySnapshotsExecutor>().As<IVerifySnapshotsExecutor>();
            builder.RegisterType<AddSnapshotsForBillingEndDatesExecutor>().As<IAddSnapshotsForBillingEndDatesExecutor>();

            builder.RegisterType<GetAllSubscribersDbStatement>().As<IGetAllSubscribersDbStatement>();
            builder.RegisterType<GetCreatorsAndFirstSubscribedDatesDbStatement>().As<IGetCreatorsAndFirstSubscribedDatesDbStatement>();
            builder.RegisterType<GetCreatorChannelsSnapshotsDbStatement>().As<IGetCreatorChannelsSnapshotsDbStatement>();
            builder.RegisterType<GetCreatorFreeAccessUsersSnapshotsDbStatement>().As<IGetCreatorFreeAccessUsersSnapshotsDbStatement>();
            builder.RegisterType<GetCreatorPostsDbStatement>().As<IGetCreatorPostsDbStatement>();
            builder.RegisterType<GetSubscriberChannelsSnapshotsDbStatement>().As<IGetSubscriberChannelsSnapshotsDbStatement>();
            builder.RegisterType<GetSubscriberSnapshotsDbStatement>().As<IGetSubscriberSnapshotsDbStatement>();
            builder.RegisterType<GetCreatorPercentageOverrideDbStatement>().As<IGetCreatorPercentageOverrideDbStatement>();
            builder.RegisterType<GetCalculatedAccountBalancesDbStatement>().As<IGetCalculatedAccountBalancesDbStatement>();
            builder.RegisterType<RequestProcessPaymentsService>().As<IRequestProcessPaymentsService>();
            builder.RegisterType<UpdateAccountBalancesDbStatement>().As<IUpdateAccountBalancesDbStatement>();
            builder.RegisterType<TopUpUserAccountsWithCredit>().As<ITopUpUserAccountsWithCredit>();
            builder.RegisterType<GetCommittedAccountBalanceDbStatement>().As<IGetCommittedAccountBalanceDbStatement>();
            
            builder.RegisterType<GetUserPaymentOriginDbStatement>().As<IGetUserPaymentOriginDbStatement>();
            builder.RegisterType<SetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement>().As<ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement>();
            builder.RegisterType<CreateStripeCustomer>().As<ICreateStripeCustomer>();
            builder.RegisterType<UpdateStripeCustomerCreditCard>().As<IUpdateStripeCustomerCreditCard>();
            builder.RegisterType<PerformStripeCharge>().As<IPerformStripeCharge>();
            builder.RegisterType<SaveCustomerCreditToLedgerDbStatement>().As<ISaveCustomerCreditToLedgerDbStatement>();
            builder.RegisterType<GetTaxInformation>().As<IGetTaxInformation>();
            builder.RegisterType<CreateTaxamoTransaction>().As<ICreateTaxamoTransaction>();
            builder.RegisterType<DeleteTaxamoTransaction>().As<IDeleteTaxamoTransaction>();
            builder.RegisterType<CommitTaxamoTransaction>().As<ICommitTaxamoTransaction>();
            builder.RegisterType<PerformCreditRequest>().As<IPerformCreditRequest>();
            builder.RegisterType<CommitCreditToDatabase>().As<ICommitCreditToDatabase>();
            builder.RegisterType<InitializeCreditRequest>().As<IInitializeCreditRequest>();
            builder.RegisterType<ApplyUserCredit>().As<IApplyUserCredit>();
            builder.RegisterType<GetUsersRequiringPaymentRetryDbStatement>().As<IGetUsersRequiringPaymentRetryDbStatement>();
            builder.RegisterType<GetUserWeeklySubscriptionsCost>().As<IGetUserWeeklySubscriptionsCost>();
            builder.RegisterType<ClearPaymentStatusDbStatement>().As<IClearPaymentStatusDbStatement>();
            builder.RegisterType<FailPaymentStatusDbStatement>().As<IFailPaymentStatusDbStatement>();
            builder.RegisterType<IncrementPaymentStatusDbStatement>().As<IIncrementPaymentStatusDbStatement>();

            builder.RegisterType<GetTransactionsDbStatement>().As<IGetTransactionsDbStatement>();
            builder.RegisterType<CreateCreditRefund>().As<ICreateCreditRefund>();
            builder.RegisterType<CreateTransactionRefund>().As<ICreateTransactionRefund>();
            builder.RegisterType<GetCreditTransactionInformation>().As<IGetCreditTransactionInformation>();
            builder.RegisterType<GetRecordsForTransactionDbStatement>().As<IGetRecordsForTransactionDbStatement>();
            builder.RegisterType<PersistCommittedRecordsDbStatement>().As<IPersistCommittedRecordsDbStatement>();
            builder.RegisterType<PersistCreditRefund>().As<IPersistCreditRefund>();

            builder.RegisterType<StripeApiKeyRepository>().As<IStripeApiKeyRepository>();
            builder.RegisterType<StripeService>().As<IStripeService>();

            builder.RegisterType<TaxamoApiKeyRepository>().As<ITaxamoApiKeyRepository>();
            builder.RegisterType<TaxamoService>().As<ITaxamoService>();
        }
    }
}