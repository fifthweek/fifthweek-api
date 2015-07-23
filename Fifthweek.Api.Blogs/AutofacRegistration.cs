namespace Fifthweek.Api.Blogs
{
    using Autofac;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlogOwnership>().As<IBlogOwnership>();
            builder.RegisterType<BlogSecurity>().As<IBlogSecurity>();
            builder.RegisterType<GetBlogChannelsAndCollectionsDbStatement>().As<IGetBlogChannelsAndCollectionsDbStatement>();
            builder.RegisterType<UpdateFreeAccessUsersDbStatement>().As<IUpdateFreeAccessUsersDbStatement>();
            builder.RegisterType<GetFreeAccessUsersDbStatement>().As<IGetFreeAccessUsersDbStatement>();
            builder.RegisterType<GetUserSubscriptionsDbStatement>().As<IGetUserSubscriptionsDbStatement>();
            builder.RegisterType<GetLandingPageDbStatement>().As<IGetLandingPageDbStatement>();
            builder.RegisterType<AcceptChannelSubscriptionPriceChangeDbStatement>().As<IAcceptChannelSubscriptionPriceChangeDbStatement>();
            builder.RegisterType<UnsubscribeFromChannelDbStatement>().As<IUnsubscribeFromChannelDbStatement>();
            builder.RegisterType<UpdateBlogSubscriptionsDbStatement>().As<IUpdateBlogSubscriptionsDbStatement>();
            builder.RegisterType<GetIsTestUserBlogDbStatement>().As<IGetIsTestUserBlogDbStatement>();
            builder.RegisterType<GetBlogSubscriberInformationDbStatement>().As<IGetBlogSubscriberInformationDbStatement>();
            builder.RegisterType<GetCreatorRevenueDbStatement>().As<IGetCreatorRevenueDbStatement>();
        }
    }
}