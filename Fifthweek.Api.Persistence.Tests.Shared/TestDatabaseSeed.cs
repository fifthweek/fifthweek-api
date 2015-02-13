namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// WHEN ADDING ENTITIES:
    /// 1) Add the seed entities in this class.
    /// 2) Update `TestDatabaseSnapshot` with the single line required to track those entities.
    /// 3) Increment `TestDatabase.SeedStateVersion`.
    /// </summary>
    public class TestDatabaseSeed
    {
        private const int EndToEndTestEmails = 10;
        private const int Users = 10;
        private const int Creators = 5;
        private const int SubscriptionsPerCreator = 1; // That's all our interface supports for now!
        private const int ChannelsPerSubscription = 3;
        private const int CollectionsPerChannel = 2;
        private const int NotesPerChannel = 3;
        private const int ImagesPerCollection = 3;
        private const int FilesPerCollection = 3;
        private const int RefreshTokensPerCreator = 2;

        private static readonly Random Random = new Random();

        private readonly Func<IFifthweekDbContext> databaseContextFactory;

        private readonly List<EndToEndTestEmail> endToEndTestEmails = new List<EndToEndTestEmail>();
        private readonly List<FifthweekUser> users = new List<FifthweekUser>();
        private readonly List<Subscription> subscriptions = new List<Subscription>();
        private readonly List<Channel> channels = new List<Channel>();
        private readonly List<Collection> collections = new List<Collection>();
        private readonly List<WeeklyReleaseTime> weeklyReleaseTimes = new List<WeeklyReleaseTime>();
        private readonly List<Post> posts = new List<Post>();
        private readonly List<File> files = new List<File>();
        private readonly List<RefreshToken> refreshTokens = new List<RefreshToken>();

        public TestDatabaseSeed(Func<IFifthweekDbContext> databaseContextFactory)
        {
            if (databaseContextFactory == null)
            {
                throw new ArgumentNullException("databaseContextFactory");
            }

            this.databaseContextFactory = databaseContextFactory;
        }

        public async Task AssertSeedStateUnchangedAsync()
        {
            var seedResetRequired = await this.SeedStateResetRequiredAsync();
            Assert.IsFalse(seedResetRequired, "It seems a test managed to change database seed state.");
        }

        public async Task PopulateWithDummyEntitiesAsync()
        {
            var seedResetRequired = await this.SeedStateResetRequiredAsync();
            if (!seedResetRequired)
            {
                Trace.WriteLine("Seed state matches - no reset required.");
                return;
            }

            Trace.WriteLine("Resetting database! This should only occur once after making changes to the seeding code.");

            var stopwatch = Stopwatch.StartNew();
            this.CreateEndToEndTestEmails();
            this.CreateUsers();
            Trace.WriteLine(string.Format("Generated in-memory entities in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));

            stopwatch = Stopwatch.StartNew();
            await this.FlushToDatabaseAsync();
            Trace.WriteLine(string.Format("Saved to database in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
        }

        private async Task<bool> SeedStateResetRequiredAsync()
        {
            bool retval;
            var stopwatch = Stopwatch.StartNew();
            using (var databaseContext = this.databaseContextFactory())
            {
                // Quick validation to ensure database hasn't deviated from seed state. If something is wrong with transaction scopes, it will likely
                // affect every test. We know that some tests add users, so this seems to be the fastest check (it's run before every test).
                retval = await databaseContext.Users.CountAsync() != Users;
            }

            Trace.WriteLine(string.Format("Checked database seed version in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
            return retval;
        }

        private async Task FlushToDatabaseAsync()
        {
            using (var databaseContext = this.databaseContextFactory())
            {
                var connection = databaseContext.Database.Connection;
                await connection.OpenAsync();
                try
                {
                    await connection.InsertAsync(this.endToEndTestEmails, false);
                    await connection.InsertAsync(this.users, false);
                    await connection.InsertAsync(this.files, false);
                    await connection.InsertAsync(this.subscriptions, false);
                    await connection.InsertAsync(this.channels, false);
                    await connection.InsertAsync(this.collections, false);
                    await connection.InsertAsync(this.weeklyReleaseTimes, false);
                    await connection.InsertAsync(this.posts, false);
                    await connection.InsertAsync(this.refreshTokens, false);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void CreateEndToEndTestEmails()
        {
            for (var i = 0; i < EndToEndTestEmails; i++)
            {
                var email = EndToEndTestEmailTests.UniqueEntity(Random);
                this.endToEndTestEmails.Add(email);
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

                this.CreateRefreshTokens(user);
            }
        }

        private void CreateRefreshTokens(FifthweekUser user)
        {
            for (int i = 0; i < RefreshTokensPerCreator; i++)
            {
                this.refreshTokens.Add(
                    new RefreshToken(
                        "hash_" + user.Id + i,
                        user.UserName,
                        "client_" + i,
                        DateTime.UtcNow.AddSeconds(-100),
                        DateTime.UtcNow,
                        "protected_" + i));
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
                var channel = ChannelTests.UniqueEntity(Random);
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
                var post = PostTests.UniqueNote(Random);
                post.Channel = channel;
                post.ChannelId = channel.Id;
                this.posts.Add(post);
            }
        }

        private void CreateCollections(Channel channel)
        {
            for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
            {
                var collection = CollectionTests.UniqueEntity(Random);
                collection.Channel = channel;
                collection.ChannelId = channel.Id;
                this.collections.Add(collection);

                // At least one weekly release time is required per collection.
                var weeklyReleaseTime = WeeklyReleaseTimeTests.UniqueEntity(Random, collection.Id);
                this.weeklyReleaseTimes.Add(weeklyReleaseTime);
                
                this.CreateFileAndImagePosts(collection);
            }
        }

        private void CreateFileAndImagePosts(Collection collection)
        {
            for (var postIndex = 0; postIndex < ImagesPerCollection; postIndex++)
            {
                var post = PostTests.UniqueFileOrImage(Random);
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
                var post = PostTests.UniqueFileOrImage(Random);
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