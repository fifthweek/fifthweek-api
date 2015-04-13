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
            builder.RegisterType<GetBlogDbStatement>().As<IGetBlogDbStatement>();
            builder.RegisterType<UpdateFreeAccessUsersDbStatement>().As<IUpdateFreeAccessUsersDbStatement>();
            builder.RegisterType<DoesUserHaveFreeAccessDbStatement>().As<IDoesUserHaveFreeAccessDbStatement>();
            builder.RegisterType<GetFreeAccessUsersDbStatement>().As<IGetFreeAccessUsersDbStatement>();
            builder.RegisterType<GetBlogSubscriptionsDbStatement>().As<IGetBlogSubscriptionsDbStatement>();
        }
    }
}