namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Diagnostics;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;

    [AutoConstructor]
    public partial class TemporaryDatabaseSeed
    {
        private const int Users = 10;
        private const int Creators = 5;
        private const int SubscriptionsPerCreator = 1; // That's all our interface supports for now!
        private const int ChannelsPerSubscription = 3;
        private const int CollectionsPerChannel = 4;
        private const int NotesPerChannel = 10;
        private const int ImagesPerCollection = 10;
        private const int FilesPerCollection = 10;

        private static readonly Random Random = new Random();

        private readonly FifthweekDbContext databaseContext;

        public void PopulateWithDummyEntities()
        {
            Trace.WriteLine(DateTime.Now.TimeOfDay);
            this.databaseContext.Configuration.AutoDetectChangesEnabled = false;
            this.CreateUsers();
            Trace.WriteLine(DateTime.Now.TimeOfDay);
        }

        private void CreateUsers()
        {
            for (var i = 0; i < Users; i++)
            {
                var user = UserTests.UniqueEntity(Random);

                if (Random.Next(1) == 1)
                {
                    var file = FileTests.UniqueEntity(Random);
                    file.User = user;
                    file.UserId = user.Id;
                    user.ProfileImageFile = file;
                    user.ProfileImageFileId = file.Id;
                }

                this.databaseContext.Users.Add(user);

                if (i < Creators)
                {
                    this.CreateSubscriptions(user);
                }
            }
        }

        private void CreateSubscriptions(FifthweekUser creator)
        {
            for (var subscriptionIndex = 0; subscriptionIndex < SubscriptionsPerCreator; subscriptionIndex++)
            {
                var subscription = SubscriptionTests.UniqueEntity(Random);

                if (subscription.HeaderImageFile != null)
                {
                    var file = subscription.HeaderImageFile;
                    file.User = creator;
                    file.UserId = creator.Id;
                    this.databaseContext.Files.Add(subscription.HeaderImageFile);
                }

                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;
                this.databaseContext.Subscriptions.Add(subscription);

                this.CreateChannels(subscription);
            }
        }

        private void CreateChannels(Subscription subscription)
        {
            for (var channelIndex = 0; channelIndex < ChannelsPerSubscription; channelIndex++)
            {
                var channel = ChannelTests.UniqueEntity(Random, false);
                channel.Subscription = subscription;
                channel.SubscriptionId = subscription.Id;
                this.databaseContext.Channels.Add(channel);

                this.CreateNotes(channel);
                this.CreateCollections(channel);
            }
        }

        private void CreateNotes(Channel channel)
        {
            for (var postIndex = 0; postIndex < NotesPerChannel; postIndex++)
            {
                var post = PostTests.UniqueNote(Random, false);
                post.Channel = channel;
                post.ChannelId = channel.Id;
                this.databaseContext.Posts.Add(post);
            }
        }

        private void CreateCollections(Channel channel)
        {
            for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
            {
                var collection = CollectionTests.UniqueEntity(Random, false);
                collection.Channel = channel;
                collection.ChannelId = channel.Id;
                this.databaseContext.Collections.Add(collection);

                this.CreateFileAndImagePosts(collection);
            }
        }

        private void CreateFileAndImagePosts(Collection collection)
        {
            for (var postIndex = 0; postIndex < ImagesPerCollection; postIndex++)
            {
                var post = PostTests.UniqueImage(Random, false);
                post.Channel = collection.Channel;
                post.ChannelId = collection.Channel.Id;
                post.Collection = collection;
                post.CollectionId = collection.Id;

                var file = FileTests.UniqueEntity(Random);
                file.User = collection.Channel.Subscription.Creator;
                file.UserId = collection.Channel.Subscription.Creator.Id;
                post.Image = file;
                post.ImageId = file.Id;

                this.databaseContext.Posts.Add(post);
            }

            for (var postIndex = 0; postIndex < FilesPerCollection; postIndex++)
            {
                var post = PostTests.UniqueFile(Random, false);
                post.Channel = collection.Channel;
                post.ChannelId = collection.Channel.Id;
                post.Collection = collection;
                post.CollectionId = collection.Id;

                var file = FileTests.UniqueEntity(Random);
                file.User = collection.Channel.Subscription.Creator;
                file.UserId = collection.Channel.Subscription.Creator.Id;
                post.File = file;
                post.FileId = file.Id;

                this.databaseContext.Posts.Add(post);
            }
        }
    }
}