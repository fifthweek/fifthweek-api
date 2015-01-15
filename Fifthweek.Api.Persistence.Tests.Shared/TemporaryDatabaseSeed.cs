namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;

    public class TemporaryDatabaseSeed
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

        private readonly List<FifthweekUser> users = new List<FifthweekUser>();
        private readonly List<Subscription> subscriptions = new List<Subscription>();
        private readonly List<Channel> channels = new List<Channel>();
        private readonly List<Collection> collections = new List<Collection>();
        private readonly List<Post> posts = new List<Post>();
        private readonly List<File> files = new List<File>();

        public TemporaryDatabaseSeed(Func<IFifthweekDbContext> databaseContextFactory)
        {
            if (databaseContextFactory == null)
            {
                throw new ArgumentNullException("databaseContextFactory");
            }

            this.databaseContextFactory = databaseContextFactory;
        }

        public async Task PopulateWithDummyEntitiesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var seedResetRequired = await this.SeedStateResetRequiredAsync();
            Trace.WriteLine(string.Format("Checked database seed version in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));

            if (!seedResetRequired)
            {
                Trace.WriteLine("Seed state matched - no reset required.");
                return;
            }

            Trace.WriteLine("Resetting database! This should only occur once after making changes to the seeding code.");

            stopwatch = Stopwatch.StartNew();
            this.CreateUsers();
            Trace.WriteLine(string.Format("Generated in-memory entities in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));

            stopwatch = Stopwatch.StartNew();
            await this.FlushToDatabaseAsync();
            Trace.WriteLine(string.Format("Saved to database in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
        }

        private async Task<bool> SeedStateResetRequiredAsync()
        {
            using (var databaseContext = this.databaseContextFactory())
            {
                // We use the user count to discriminate between seed state versions.
                return await databaseContext.Users.CountAsync() == Users;
            }
        }

        private async Task FlushToDatabaseAsync()
        {
            using (var databaseContext = this.databaseContextFactory())
            {
                var connection = databaseContext.Database.Connection;
                await connection.OpenAsync();
                try
                {
                    await connection.InsertAsync(this.users, false);
                    await connection.InsertAsync(this.files, false);
                    await connection.InsertAsync(this.subscriptions, false);
                    await connection.InsertAsync(this.channels, false);
                    await connection.InsertAsync(this.collections, false);
                    await connection.InsertAsync(this.posts, false);
                }
                finally
                {
                    connection.Close();
                }
            }
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

                this.users.Add(user);

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

                    this.files.Add(file);
                }

                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;
                this.subscriptions.Add(subscription);

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
                this.channels.Add(channel);

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
                this.posts.Add(post);
            }
        }

        private void CreateCollections(Channel channel)
        {
            for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
            {
                var collection = CollectionTests.UniqueEntity(Random, false);
                collection.Channel = channel;
                collection.ChannelId = channel.Id;
                this.collections.Add(collection);
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

                this.posts.Add(post);
                this.files.Add(file);
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

                this.posts.Add(post);
                this.files.Add(file);
            }
        }
    }
}