﻿namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteTestUserAccountsDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime EndDate = new SqlDateTime(Now.AddDays(-1)).Value;
        private static readonly DateTime IncludedDate = new SqlDateTime(EndDate.AddDays(-1)).Value;

        private static readonly UserId TestUserId1 = new UserId(Guid.NewGuid());
        private static readonly UserId TestUserId2 = new UserId(Guid.NewGuid());
        private static readonly UserId TestUserId3 = new UserId(Guid.NewGuid());

        private DeleteTestUserAccountsDbStatement target;

        [TestMethod]
        public async Task WhenDeletingTestUsers_ItShouldRemoveTheExpectedEntities()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeleteTestUserAccountsDbStatement(testDatabase);
                var expectedDeletions = await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(EndDate);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                };
            });
        }

        private async Task<List<IIdentityEquatable>> CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();

            using (var connection = testDatabase.CreateConnection())
            {
                var expectedDeletions = new List<IIdentityEquatable>();
                await this.AddUser(connection, random, UserId.Random(), IncludedDate, false);
                expectedDeletions.AddRange(await this.AddUser(connection, random, TestUserId1, IncludedDate, true));
                var channelId = new ChannelId(expectedDeletions.OfType<Channel>().First().Id);
                expectedDeletions.AddRange(await this.AddUser(connection, random, TestUserId2, IncludedDate, true, TestUserId1, channelId));
                
                var nonDeletedUserEntities = await this.AddUser(connection, random, TestUserId3, EndDate, true, TestUserId1, channelId);
                expectedDeletions.AddRange(nonDeletedUserEntities.OfType<ChannelSubscription>());

                // Files shouldn't be deleted.
                return expectedDeletions.Where(v => !(v is File)).ToList();
            }
        }

        private async Task<IEnumerable<IIdentityEquatable>> AddUser(DbConnection connection, Random random, UserId userId, DateTime registrationDate, bool isTestUser, UserId subscriberId = null, ChannelId subscribedToId = null)
        {
            var users = new List<FifthweekUser>();
            var user = UserTests.UniqueEntity(random);
            user.Id = userId.Value;
            user.RegistrationDate = registrationDate;
            users.Add(user);
            await connection.InsertAsync(users, false);

            var userRoles = new List<FifthweekUserRole>();
            userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, user.Id));
            if (isTestUser)
            {
                userRoles.Add(new FifthweekUserRole(FifthweekRole.TestUserId, user.Id));
            }
            await connection.InsertAsync(userRoles, false);

            var blogs = new List<Blog>();
            var blog = BlogTests.UniqueEntity(random);
            blog.CreatorId = userId.Value;
            blogs.Add(blog);
            await connection.InsertAsync(blogs);

            var channels = new List<Channel>();
            var channel = ChannelTests.UniqueEntity(random);
            channel.BlogId = blog.Id;
            channels.Add(channel);
            await connection.InsertAsync(channels);

            var queues = new List<Queue>();
            var queue = QueueTests.UniqueEntity(random);
            queue.BlogId = blog.Id;
            queues.Add(queue);
            await connection.InsertAsync(queues);
            
            var files = new List<File>();
            var file = FileTests.UniqueEntity(random);
            file.ChannelId = channel.Id;
            file.UserId = userId.Value;
            files.Add(file);
            await connection.InsertAsync(files);

            var posts = new List<Post>();
            var post = PostTests.UniqueFileOrImage(random);
            post.ChannelId = channel.Id;
            post.PreviewImageId = file.Id;
            post.ChannelId = channel.Id;
            posts.Add(post);
            await connection.InsertAsync(posts);

            var postFiles = new List<PostFile>();
            postFiles.Add(new PostFile(post.Id, file.Id));
            await connection.InsertAsync(postFiles);

            var channelSubscriptions = new List<ChannelSubscription>();
            if (subscriberId != null)
            {
                channelSubscriptions.Add(new ChannelSubscription(channel.Id, null, subscriberId.Value, null, 100, Now, Now));
                await connection.InsertAsync(channelSubscriptions);
            }
            if (subscribedToId != null)
            {
                channelSubscriptions.Add(new ChannelSubscription(subscribedToId.Value, null, userId.Value, null, 100, Now, Now));
                await connection.InsertAsync(channelSubscriptions);
            }

            var calculatedAccountBalances = new List<CalculatedAccountBalance>
            {
                new CalculatedAccountBalance(user.Id, LedgerAccountType.FifthweekCredit, Now),
            };
            await connection.InsertAsync(calculatedAccountBalances);

            var subscriberSnapshots = new List<SubscriberSnapshot>
            {
                new SubscriberSnapshot(Now, user.Id, "email"),
            };
            await connection.InsertAsync(subscriberSnapshots);

            var subscriberChannelSnapshots = new List<SubscriberChannelsSnapshot>
            {
                new SubscriberChannelsSnapshot(Guid.NewGuid(), Now, user.Id),
            };
            await connection.InsertAsync(subscriberChannelSnapshots);
            
            var subscriberChannelSnapshotItems = new List<SubscriberChannelsSnapshotItem>
            {
                new SubscriberChannelsSnapshotItem(subscriberChannelSnapshots[0].Id, null, channel.Id, user.Id, 100, Now),
            };
            await connection.InsertAsync(subscriberChannelSnapshotItems);

            var creatorChannelSnapshots = new List<CreatorChannelsSnapshot>
            {
                new CreatorChannelsSnapshot(Guid.NewGuid(), Now, user.Id),
            };
            await connection.InsertAsync(creatorChannelSnapshots);
            
            var creatorChannelSnapshotItems = new List<CreatorChannelsSnapshotItem>
            {
                new CreatorChannelsSnapshotItem(creatorChannelSnapshots[0].Id, null, channel.Id, 100),
            };
            await connection.InsertAsync(creatorChannelSnapshotItems);

            var creatorFreeAccessUsersSnapshots = new List<CreatorFreeAccessUsersSnapshot>
            {
                new CreatorFreeAccessUsersSnapshot(Guid.NewGuid(), Now, user.Id),
            };
            await connection.InsertAsync(creatorFreeAccessUsersSnapshots);
                        
            var creatorFreeAccessUsersSnapshotItems = new List<CreatorFreeAccessUsersSnapshotItem>
            {
                new CreatorFreeAccessUsersSnapshotItem(creatorFreeAccessUsersSnapshots[0].Id, null, "email"),
            };
            await connection.InsertAsync(creatorFreeAccessUsersSnapshotItems);

            var userPaymentOrigins = new List<UserPaymentOrigin>
            {
                new UserPaymentOrigin(userId.Value, null, "paymentOriginKey", PaymentOriginKeyType.Stripe, null, null, null, null, PaymentStatus.None),
            };
            await connection.InsertAsync(userPaymentOrigins);

            return users.Cast<IIdentityEquatable>()
                .Concat(userRoles)
                .Concat(blogs)
                .Concat(channels)
                .Concat(queues)
                .Concat(files)
                .Concat(posts)
                .Concat(channelSubscriptions)
                .Concat(calculatedAccountBalances)
                .Concat(subscriberSnapshots)
                .Concat(subscriberChannelSnapshots)
                .Concat(subscriberChannelSnapshotItems)
                .Concat(creatorChannelSnapshots)
                .Concat(creatorChannelSnapshotItems)
                .Concat(creatorFreeAccessUsersSnapshots)
                .Concat(creatorFreeAccessUsersSnapshotItems)
                .Concat(userPaymentOrigins)
                .Concat(postFiles);
        }
    }
}