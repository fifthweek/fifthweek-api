namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
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
        private const int BlogsPerCreator = 1; // That's all our interface supports for now!
        private const int ChannelsPerSubscription = 3;
        private const int CollectionsPerChannel = 2;
        private const int NotesPerChannel = 3;
        private const int ImagesPerCollection = 3;
        private const int FilesPerCollection = 3;
        private const int RefreshTokensPerCreator = 2;
        private const int FreeAccessPerUser = 3;
        private const int SubscriptionsPerUser = 3;

        private static readonly Random Random = new Random();

        private readonly Func<IFifthweekDbContext> databaseContextFactory;

        private readonly List<EndToEndTestEmail> endToEndTestEmails = new List<EndToEndTestEmail>();
        private readonly List<FifthweekUser> users = new List<FifthweekUser>();
        private readonly List<Blog> blogs = new List<Blog>();
        private readonly List<Channel> channels = new List<Channel>();
        private readonly List<Collection> collections = new List<Collection>();
        private readonly List<WeeklyReleaseTime> weeklyReleaseTimes = new List<WeeklyReleaseTime>();
        private readonly List<Post> posts = new List<Post>();
        private readonly List<File> files = new List<File>();
        private readonly List<RefreshToken> refreshTokens = new List<RefreshToken>();
        private readonly List<FreeAccessUser> freeAccessUsers = new List<FreeAccessUser>();
        private readonly List<ChannelSubscription> subscriptions = new List<ChannelSubscription>();

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
                    await connection.InsertAsync(this.blogs, false);
                    await connection.InsertAsync(this.channels, false);
                    await connection.InsertAsync(this.collections, false);
                    await connection.InsertAsync(this.weeklyReleaseTimes, false);
                    await connection.InsertAsync(this.posts, false);
                    await connection.InsertAsync(this.refreshTokens, false);
                    await connection.InsertAsync(this.freeAccessUsers, false);
                    await connection.InsertAsync(this.subscriptions, false);
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
            var blogIds = new List<Guid>();
            var channelIds = new List<Guid>();
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
                    this.CreateBlogs(user, blogIds, channelIds);
                }
                else
                {
                    this.CreateFreeAccess(blogIds, user);
                    this.CreateSubscriptions(channelIds, user);
                }

                this.CreateRefreshTokens(user);
            }
        }

        private void CreateSubscriptions(List<Guid> channelIds, FifthweekUser user)
        {
            var subscriptionIndicies = this.GenerageUniqueIndexes(channelIds.Count, Random.Next(0, SubscriptionsPerUser + 1));

            foreach (var channelIndex in subscriptionIndicies)
            {
                var subscription = new ChannelSubscription(channelIds[channelIndex], null, user.Id, null, Random.Next(1, 500), DateTime.UtcNow.AddDays(-10 - Random.Next(0, 5)), DateTime.UtcNow.AddDays(Random.Next(1, 5)));
                this.subscriptions.Add(subscription);
            }
        }

        private void CreateFreeAccess(List<Guid> blogIds, FifthweekUser user)
        {
            var freeAccessIndicies = this.GenerageUniqueIndexes(blogIds.Count, Random.Next(0, FreeAccessPerUser + 1));

            foreach (var blogIndex in freeAccessIndicies)
            {
                var freeAccess = new FreeAccessUser(blogIds[blogIndex], user.Email);
                this.freeAccessUsers.Add(freeAccess);
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

        private void CreateBlogs(FifthweekUser creator, List<Guid> blogIds, List<Guid> channelIds)
        {
            for (var blogIndex = 0; blogIndex < BlogsPerCreator; blogIndex++)
            {
                var blog = BlogTests.UniqueEntity(Random);
                blogIds.Add(blog.Id);

                if (blog.HeaderImageFile != null)
                {
                    var file = blog.HeaderImageFile;
                    file.User = creator;
                    file.UserId = creator.Id;

                    this.files.Add(file);
                }

                blog.Creator = creator;
                blog.CreatorId = creator.Id;
                this.blogs.Add(blog);

                this.CreateChannels(blog, channelIds);
            }
        }

        private void CreateChannels(Blog blog, List<Guid> channelIds)
        {
            for (var channelIndex = 0; channelIndex < ChannelsPerSubscription; channelIndex++)
            {
                var channel = ChannelTests.UniqueEntity(Random);
                channel.Blog = blog;
                channel.BlogId = blog.Id;
                this.channels.Add(channel);
                channelIds.Add(channel.Id);

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
                file.User = collection.Channel.Blog.Creator;
                file.UserId = collection.Channel.Blog.Creator.Id;
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
                file.User = collection.Channel.Blog.Creator;
                file.UserId = collection.Channel.Blog.Creator.Id;
                post.File = file;
                post.FileId = file.Id;

                this.posts.Add(post);
                this.files.Add(file);
            }
        }

        private IEnumerable<int> GenerageUniqueIndexes(int collectionCount, int indexCount)
        {
            if (collectionCount <= indexCount)
            {
                return Enumerable.Range(0, collectionCount);
            }

            var result = new HashSet<int>();
            while (result.Count < indexCount)
            {
                result.Add(Random.Next(0, collectionCount));
            }

            return result;
        }
    }
}