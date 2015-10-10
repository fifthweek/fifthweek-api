namespace Fifthweek.Api.Posts
{
    using Autofac;

    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GetNewsfeedDbStatement>().As<IGetNewsfeedDbStatement>();
            builder.RegisterType<GetPreviewNewsfeedDbStatement>().As<IGetPreviewNewsfeedDbStatement>();
            builder.RegisterType<GetCreatorBacklogDbStatement>().As<IGetCreatorBacklogDbStatement>();
            builder.RegisterType<DeletePostDbStatement>().As<IDeletePostDbStatement>();
            builder.RegisterType<PostToChannelDbStatement>().As<IPostToChannelDbStatement>();
            builder.RegisterType<PostToChannelDbSubStatements>().As<IPostToChannelDbSubStatements>();
            builder.RegisterType<TryGetPostQueueIdDbStatement>().As<ITryGetPostQueueIdStatement>();
            builder.RegisterType<PostFileTypeChecks>().As<IPostFileTypeChecks>();
            builder.RegisterType<PostSecurity>().As<IPostSecurity>();
            builder.RegisterType<IsPostOwnerDbStatement>().As<IIsPostOwnerDbStatement>();
            builder.RegisterType<ScheduledDateClippingFunction>().As<IScheduledDateClippingFunction>();
            builder.RegisterType<SetPostLiveDateDbStatement>().As<ISetPostLiveDateDbStatement>();
            builder.RegisterType<DefragmentQueueIfRequiredDbStatement>().As<IDefragmentQueueIfRequiredDbStatement>();
            builder.RegisterType<MovePostToQueueDbStatement>().As<IMovePostToQueueDbStatement>();
            builder.RegisterType<IsPostSubscriberDbStatement>().As<IIsPostSubscriberDbStatement>();
            builder.RegisterType<LikePostDbStatement>().As<ILikePostDbStatement>();
            builder.RegisterType<UnlikePostDbStatement>().As<IUnlikePostDbStatement>();
            builder.RegisterType<CommentOnPostDbStatement>().As<ICommentOnPostDbStatement>();
            builder.RegisterType<GetCommentsDbStatement>().As<IGetCommentsDbStatement>();
        }
    }
}