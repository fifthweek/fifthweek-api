namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Threading.Tasks;

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

        private readonly Func<IFifthweekDbContext> databaseContextFactory;

        public async Task PopulateWithDummyEntitiesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            using (var databaseContext = this.databaseContextFactory())
            {
                await databaseContext.Database.Connection.OpenAsync();
                try
                {
                    await this.CreateUsersAsync(databaseContext.Database.Connection);
                }
                finally 
                {
                    databaseContext.Database.Connection.Close();
                }
            }

            var secondsElapsed = Math.Round(stopwatch.Elapsed.TotalSeconds, 2);
            Trace.WriteLine(string.Format("Seeded database in {0}s", secondsElapsed));
        }

        private async Task CreateUsersAsync(DbConnection connection)
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

                await connection.InsertAsync(user, false);

                if (i < Creators)
                {
                    await this.CreateSubscriptionsAsync(connection, user);
                }
            }
        }

        private async Task CreateSubscriptionsAsync(DbConnection connection, FifthweekUser creator)
        {
            for (var subscriptionIndex = 0; subscriptionIndex < SubscriptionsPerCreator; subscriptionIndex++)
            {
                var subscription = SubscriptionTests.UniqueEntity(Random);

                if (subscription.HeaderImageFile != null)
                {
                    var file = subscription.HeaderImageFile;
                    file.User = creator;
                    file.UserId = creator.Id;
                    await connection.InsertAsync(file, false);
                }

                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;
                await connection.InsertAsync(subscription, false);

                await this.CreateChannelsAsync(connection, subscription);
            }
        }

        private async Task CreateChannelsAsync(DbConnection connection, Subscription subscription)
        {
            for (var channelIndex = 0; channelIndex < ChannelsPerSubscription; channelIndex++)
            {
                var channel = ChannelTests.UniqueEntity(Random, false);
                channel.Subscription = subscription;
                channel.SubscriptionId = subscription.Id;
                await connection.InsertAsync(channel, false);

                await this.CreateNotesAsync(connection, channel);
                await this.CreateCollectionsAsync(connection, channel);
            }
        }

        private async Task CreateNotesAsync(DbConnection connection, Channel channel)
        {
            for (var postIndex = 0; postIndex < NotesPerChannel; postIndex++)
            {
                var post = PostTests.UniqueNote(Random, false);
                post.Channel = channel;
                post.ChannelId = channel.Id;
                await connection.InsertAsync(post, false);
            }
        }

        private async Task CreateCollectionsAsync(DbConnection connection, Channel channel)
        {
            for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
            {
                var collection = CollectionTests.UniqueEntity(Random, false);
                collection.Channel = channel;
                collection.ChannelId = channel.Id;
                await connection.InsertAsync(collection, false);
                await this.CreateFileAndImagePostsAsync(connection, collection);
            }
        }

        private async Task CreateFileAndImagePostsAsync(DbConnection connection, Collection collection)
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

                await connection.InsertAsync(file, false);
                await connection.InsertAsync(post, false);
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

                await connection.InsertAsync(file, false);
                await connection.InsertAsync(post, false);
            }
        }
    }
}