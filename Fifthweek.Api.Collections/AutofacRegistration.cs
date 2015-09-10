namespace Fifthweek.Api.Collections
{
    using Autofac;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<QueueOwnership>().As<IQueueOwnership>();
            builder.RegisterType<QueueSecurity>().As<IQueueSecurity>();
            builder.RegisterType<GetNewQueuedPostLiveDateLowerBoundDbStatement>().As<IGetNewQueuedPostLiveDateLowerBoundDbStatement>();
            builder.RegisterType<GetLiveDateOfNewQueuedPostDbStatement>().As<IGetLiveDateOfNewQueuedPostDbStatement>();
            builder.RegisterType<GetWeeklyReleaseScheduleDbStatement>().As<IGetWeeklyReleaseScheduleDbStatement>();
            builder.RegisterType<QueuedPostLiveDateCalculator>().As<IQueuedPostLiveDateCalculator>();
            builder.RegisterType<DeleteQueueDbStatement>().As<IDeleteQueueDbStatement>();
            builder.RegisterType<DefragmentQueueDbStatement>().As<IDefragmentQueueDbStatement>();
            builder.RegisterType<GetQueueSizeDbStatement>().As<IGetQueueSizeDbStatement>();
            builder.RegisterType<UpdateAllLiveDatesInQueueDbStatement>().As<IUpdateAllLiveDatesInQueueDbStatement>();
            builder.RegisterType<UpdateWeeklyReleaseScheduleDbStatement>().As<IUpdateWeeklyReleaseScheduleDbStatement>();
            builder.RegisterType<ReplaceWeeklyReleaseTimesDbStatement>().As<IReplaceWeeklyReleaseTimesDbStatement>();
            builder.RegisterType<UpdateQueueFieldsDbStatement>().As<IUpdateQueueFieldsDbStatement>();
        }
    }
}