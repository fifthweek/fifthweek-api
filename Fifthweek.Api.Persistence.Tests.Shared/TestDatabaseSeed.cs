namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// WHEN ADDING ENTITIES:
    /// 1) Add the seed entities in this class.
    /// 2) Update `TestDatabaseSnapshot` with the single line required to track those entities.
    /// 3) Increment `TestDatabase.SeedStateVersion`.
    /// </summary>
    public class TestDatabaseSeed
    {
        public const int CreatorChannelSnapshots = 1;
        public const int SubscriberChannelSnapshots = 1;
        public const int Users = 10;

        private const int EndToEndTestEmails = 10;
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
        private readonly List<Queue> queues = new List<Queue>();
        private readonly List<WeeklyReleaseTime> weeklyReleaseTimes = new List<WeeklyReleaseTime>();
        private readonly List<Post> posts = new List<Post>();
        private readonly List<File> files = new List<File>();
        private readonly List<RefreshToken> refreshTokens = new List<RefreshToken>();
        private readonly List<FreeAccessUser> freeAccessUsers = new List<FreeAccessUser>();
        private readonly List<ChannelSubscription> subscriptions = new List<ChannelSubscription>();
        private readonly List<CreatorChannelsSnapshot> creatorChannelsSnapshots = new List<CreatorChannelsSnapshot>();
        private readonly List<CreatorChannelsSnapshotItem> creatorChannelsSnapshotItems = new List<CreatorChannelsSnapshotItem>();
        private readonly List<CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots = new List<CreatorFreeAccessUsersSnapshot>();
        private readonly List<CreatorFreeAccessUsersSnapshotItem> creatorFreeAccessUsersSnapshotItems = new List<CreatorFreeAccessUsersSnapshotItem>();
        private readonly List<SubscriberChannelsSnapshot> subscriberChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
        private readonly List<SubscriberChannelsSnapshotItem> subscriberChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();
        private readonly List<SubscriberSnapshot> subscriberSnapshots = new List<SubscriberSnapshot>();
        private readonly List<AppendOnlyLedgerRecord> appendOnlyLedgerRecord = new List<AppendOnlyLedgerRecord>();
        private readonly List<CalculatedAccountBalance> calculatedAccountBalance = new List<CalculatedAccountBalance>();
        private readonly List<CreatorPercentageOverride> creatorPercentageOverride = new List<CreatorPercentageOverride>();
        private readonly List<UncommittedSubscriptionPayment> uncommittedSubscriptionPayment = new List<UncommittedSubscriptionPayment>();
        private readonly List<UserPaymentOrigin> userPaymentOrigins = new List<UserPaymentOrigin>();
        private readonly List<Comment> comments = new List<Comment>();
        private readonly List<Like> likes = new List<Like>();

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
            this.CreateSnapshots();
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
                    await connection.InsertAsync(this.queues, false);
                    await connection.InsertAsync(this.weeklyReleaseTimes, false);
                    await connection.InsertAsync(this.posts, false);
                    await connection.InsertAsync(this.refreshTokens, false);
                    await connection.InsertAsync(this.freeAccessUsers, false);
                    await connection.InsertAsync(this.subscriptions, false);
                    await connection.InsertAsync(this.creatorChannelsSnapshots, false);
                    await connection.InsertAsync(this.creatorChannelsSnapshotItems, false);
                    await connection.InsertAsync(this.creatorFreeAccessUsersSnapshots, false);
                    await connection.InsertAsync(this.creatorFreeAccessUsersSnapshotItems, false);
                    await connection.InsertAsync(this.subscriberChannelsSnapshots, false);
                    await connection.InsertAsync(this.subscriberChannelsSnapshotItems, false);
                    await connection.InsertAsync(this.subscriberSnapshots, false);
                    await connection.InsertAsync(this.appendOnlyLedgerRecord, false);
                    await connection.InsertAsync(this.calculatedAccountBalance, false);
                    await connection.InsertAsync(this.creatorPercentageOverride, false);
                    await connection.InsertAsync(this.uncommittedSubscriptionPayment, false);
                    await connection.InsertAsync(this.userPaymentOrigins, false);
                    await connection.InsertAsync(this.comments, false);
                    await connection.InsertAsync(this.likes, false);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void CreateSnapshots()
        {
            for (int i = 0; i < CreatorChannelSnapshots; i++)
            {
                this.creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                this.creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(this.creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100));
            }

            this.creatorFreeAccessUsersSnapshots.Add(new CreatorFreeAccessUsersSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
            this.creatorFreeAccessUsersSnapshotItems.Add(new CreatorFreeAccessUsersSnapshotItem(this.creatorFreeAccessUsersSnapshots[0].Id, null, "email"));
            
            for (int i = 0; i < SubscriberChannelSnapshots; i++)
            {
                this.subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                this.subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(this.subscriberChannelsSnapshots[0].Id, null, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.UtcNow));
            }

            this.subscriberSnapshots.Add(new SubscriberSnapshot(DateTime.UtcNow, Guid.NewGuid(), "email"));
        }

        private void CreatePaymentInformation()
        {

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
                    file.UserId = user.Id;
                    user.ProfileImageFile = file;
                    user.ProfileImageFileId = file.Id;
                }

                this.users.Add(user);

                if (i < Creators)
                {
                    this.CreateBlogs(user);
                }
                else
                {
                    this.CreateFreeAccess(user);
                    this.CreateSubscriptions(user);
                }

                this.CreateRefreshTokens(user);

                if (i > 0)
                {
                    this.CreatePaymentInformation(user, this.users[i - 1].Id, i);
                }

                if (i % 2 == 0)
                {
                    this.userPaymentOrigins.Add(
                        new UserPaymentOrigin(
                            user.Id,
                            user,
                            Guid.NewGuid().ToString(),
                            (PaymentOriginKeyType)Random.Next(1, 1 + (int)PaymentOriginKeyType.Stripe),
                            "USA",
                            "123243",
                            "1.2.3.4",
                            Guid.NewGuid().ToString(),
                            (PaymentStatus)Random.Next(0, 1 + (int)PaymentStatus.Failed)));
                }
            }

            // Create some records for deleted users.
            this.CreateAppendOnlyLedgerRecord(Guid.NewGuid(), Guid.NewGuid());
            this.CreateAppendOnlyLedgerRecord(Guid.NewGuid(), Guid.NewGuid());
            this.uncommittedSubscriptionPayment.Add(new UncommittedSubscriptionPayment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddMinutes(-30), DateTime.Now, 10m, Guid.NewGuid()));
            this.uncommittedSubscriptionPayment.Add(new UncommittedSubscriptionPayment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddMinutes(-30), DateTime.Now, 10m, Guid.NewGuid()));
        }

        private void CreatePaymentInformation(FifthweekUser user, Guid previousUserId, int userIndex)
        {
            this.CreateAppendOnlyLedgerRecord(user.Id, previousUserId);

            // Add credit.
            var transactionReference2 = Guid.NewGuid();
            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    user.Id,
                    null,
                    DateTime.UtcNow,
                    -1200,
                    LedgerAccountType.Stripe,
                    LedgerTransactionType.CreditAddition,
                    transactionReference2,
                    Guid.NewGuid(),
                    "comment",
                    "stripe",
                    "taxamo"));

            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    user.Id,
                    null,
                    DateTime.UtcNow,
                    1000,
                    LedgerAccountType.FifthweekCredit,
                    LedgerTransactionType.CreditAddition,
                    transactionReference2,
                    Guid.NewGuid(),
                    "comment",
                    "stripe",
                    "taxamo"));

            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    user.Id,
                    null,
                    DateTime.UtcNow,
                    200,
                    LedgerAccountType.SalesTax,
                    LedgerTransactionType.CreditAddition,
                    transactionReference2,
                    Guid.NewGuid(), 
                    "comment",
                    "stripe", 
                    "taxamo"));

            this.calculatedAccountBalance.Add(new CalculatedAccountBalance(user.Id, LedgerAccountType.FifthweekCredit, DateTime.UtcNow.AddDays(-1), 1000));
            this.calculatedAccountBalance.Add(new CalculatedAccountBalance(user.Id, LedgerAccountType.Stripe, DateTime.UtcNow.AddDays(-1), -1100));
            this.calculatedAccountBalance.Add(new CalculatedAccountBalance(user.Id, LedgerAccountType.SalesTax, DateTime.UtcNow.AddDays(-1), 100));
            this.calculatedAccountBalance.Add(new CalculatedAccountBalance(user.Id, LedgerAccountType.FifthweekRevenue, DateTime.UtcNow.AddDays(-1), 10000));

            if (userIndex % 3 == 0)
            {
                this.creatorPercentageOverride.Add(
                    new CreatorPercentageOverride(
                        user.Id,
                        0.8m,
                        userIndex % 2 == 0 ? (DateTime?)null : DateTime.UtcNow.AddDays(userIndex - (Users / 2))));
            }

            this.uncommittedSubscriptionPayment.Add(
                new UncommittedSubscriptionPayment(user.Id, previousUserId, DateTime.Now.AddMinutes(-30), DateTime.Now, 10m, Guid.NewGuid()));
        }

        private void CreateAppendOnlyLedgerRecord(Guid subscriberId, Guid creatorId)
        {
            // Payment to another creator.
            var transactionReference = Guid.NewGuid();
            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    subscriberId,
                    creatorId,
                    DateTime.UtcNow,
                    -100,
                    LedgerAccountType.FifthweekCredit,
                    LedgerTransactionType.SubscriptionPayment,
                    transactionReference,
                    Guid.NewGuid(), "comment", "stripe", "taxamo"));
            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    Guid.Empty,
                    creatorId,
                    DateTime.UtcNow,
                    100,
                    LedgerAccountType.FifthweekRevenue,
                    LedgerTransactionType.SubscriptionPayment,
                    transactionReference,
                    Guid.NewGuid(), "comment", "stripe", "taxamo"));
            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    Guid.Empty,
                    creatorId,
                    DateTime.UtcNow,
                    -70,
                    LedgerAccountType.FifthweekRevenue,
                    LedgerTransactionType.SubscriptionPayment,
                    transactionReference,
                    Guid.NewGuid(), "comment", "stripe", "taxamo"));
            this.appendOnlyLedgerRecord.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    creatorId,
                    creatorId,
                    DateTime.UtcNow,
                    70,
                    LedgerAccountType.FifthweekRevenue,
                    LedgerTransactionType.SubscriptionPayment,
                    transactionReference,
                    Guid.NewGuid(), "comment", "stripe", "taxamo"));
        }

        private void CreateSubscriptions(FifthweekUser user)
        {
            var subscriptionIndicies = this.GenerageUniqueIndexes(this.channels.Count, Random.Next(0, SubscriptionsPerUser + 1));

            foreach (var channelIndex in subscriptionIndicies)
            {
                var subscription = new ChannelSubscription(this.channels[channelIndex].Id, null, user.Id, null, Random.Next(1, 500), DateTime.UtcNow.AddDays(-10 - Random.Next(0, 5)), DateTime.UtcNow.AddDays(Random.Next(1, 5)));
                this.subscriptions.Add(subscription);
            }
        }

        private void CreateFreeAccess(FifthweekUser user)
        {
            var freeAccessIndicies = this.GenerageUniqueIndexes(this.blogs.Count, Random.Next(0, FreeAccessPerUser + 1));

            foreach (var blogIndex in freeAccessIndicies)
            {
                var freeAccess = new FreeAccessUser(this.blogs[blogIndex].Id, user.Email);
                this.freeAccessUsers.Add(freeAccess);
            }
        }

        private void CreateRefreshTokens(FifthweekUser user)
        {
            for (int i = 0; i < RefreshTokensPerCreator; i++)
            {
                this.refreshTokens.Add(
                    new RefreshToken(user.UserName,
                        "client_" + i,
                        "hash_" + user.Id + i,
                        DateTime.UtcNow.AddSeconds(-100),
                        DateTime.UtcNow, "protected_" + i));
            }
        }

        private void CreateBlogs(FifthweekUser creator)
        {
            var userBlogs = new List<Blog>();
            var userChannels = new List<Channel>();
            var userQueues = new List<Queue>();
            for (var blogIndex = 0; blogIndex < BlogsPerCreator; blogIndex++)
            {
                var blog = BlogTests.UniqueEntity(Random);

                if (blog.HeaderImageFile != null)
                {
                    var file = blog.HeaderImageFile;
                    file.UserId = creator.Id;

                    this.files.Add(file);
                }

                blog.Creator = creator;
                blog.CreatorId = creator.Id;
                userBlogs.Add(blog);
                this.blogs.Add(blog);

                this.CreateChannels(blog, userChannels);
                this.CreateQueues(blog, userQueues);
                this.CreatePosts(blog, userChannels, userQueues);

            }
        }

        private void CreateChannels(Blog blog, List<Channel> userChannels)
        {
            for (var channelIndex = 0; channelIndex < ChannelsPerSubscription; channelIndex++)
            {
                var channel = ChannelTests.UniqueEntity(Random);
                channel.Blog = blog;
                channel.BlogId = blog.Id;
                this.channels.Add(channel);
                userChannels.Add(channel);
            }
        }

        private void CreateQueues(Blog blog, List<Queue> userQueues)
        {
            for (var i = 0; i < CollectionsPerChannel; i++)
            {
                var queue = QueueTests.UniqueEntity(Random);
                queue.Blog = blog;
                queue.BlogId = blog.Id;
                this.queues.Add(queue);
                userQueues.Add(queue);

                // At least one weekly release time is required per collection.
                var weeklyReleaseTime = WeeklyReleaseTimeTests.UniqueEntity(Random, queue.Id);
                this.weeklyReleaseTimes.Add(weeklyReleaseTime);
            }
        }

        private void CreatePosts(Blog blog, IReadOnlyList<Channel> userChannels, IReadOnlyList<Queue> userQueues)
        {
            for (var postIndex = 0; postIndex < NotesPerChannel; postIndex++)
            {
                var post = PostTests.UniqueNote(Random);
                post.Channel = userChannels[postIndex % userChannels.Count];
                post.ChannelId = userChannels[postIndex % userChannels.Count].Id;
                post.Queue = userQueues[postIndex % userChannels.Count];
                post.QueueId = userQueues[postIndex % userChannels.Count].Id;
                this.posts.Add(post);
            }

            for (var postIndex = 0; postIndex < ImagesPerCollection; postIndex++)
            {
                var post = PostTests.UniqueFileOrImage(Random);
                post.Channel = userChannels[postIndex % userChannels.Count];
                post.ChannelId = userChannels[postIndex % userChannels.Count].Id;
                post.Queue = userQueues[postIndex % userChannels.Count];
                post.QueueId = userQueues[postIndex % userChannels.Count].Id;

                var file = FileTests.UniqueEntity(Random);
                file.UserId = blog.Creator.Id;
                post.Image = file;
                post.ImageId = file.Id;

                this.posts.Add(post);
                this.files.Add(file);
            }

            for (var postIndex = 0; postIndex < FilesPerCollection; postIndex++)
            {
                var post = PostTests.UniqueFileOrImage(Random);
                post.Channel = userChannels[postIndex % userChannels.Count];
                post.ChannelId = userChannels[postIndex % userChannels.Count].Id;
                post.Queue = userQueues[postIndex % userChannels.Count];
                post.QueueId = userQueues[postIndex % userChannels.Count].Id;

                var file = FileTests.UniqueEntity(Random);
                file.UserId = blog.Creator.Id;
                post.File = file;
                post.FileId = file.Id;

                this.posts.Add(post);
                this.files.Add(file);
            }
      
            this.comments.Add(new Comment(Guid.NewGuid(), this.posts.Last().Id, null, this.users.First().Id, null, "Test comment", DateTime.UtcNow));
            this.likes.Add(new Like(this.posts.Last().Id, null, this.users.First().Id, null, DateTime.UtcNow));
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