namespace Fifthweek.Payments
{
    using Autofac;

    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
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
        }
    }
}