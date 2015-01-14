namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class SubscriptionTests
    {
        public static Subscription UniqueEntity(Random random)
        {
            var user = UserTests.UniqueEntity(random);
            var headerImage = random.Next(1) == 1 ? null : FileTests.UniqueEntity(random);
            return new Subscription(
                Guid.NewGuid(),
                user.Id,
                user,
                "Name " + random.Next(),
                "Subscription tagline " + random.Next(),
                "Subscription intro " + random.Next(),
                random.Next(1) == 1 ? null : "Subscription description " + random.Next(),
                random.Next(1) == 1 ? null : "http://external/video" + random.Next(),
                headerImage == null ? (Guid?)null : headerImage.Id,
                headerImage,
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestSubscriptionAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newSubscriptionId, Guid? headerImageFileId = null)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newSubscriptionId;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            if (headerImageFileId.HasValue)
            {
                var file = FileTests.UniqueEntity(random);
                file.Id = headerImageFileId.Value;
                file.User = creator;
                file.UserId = creator.Id;

                subscription.HeaderImageFile = file;
                subscription.HeaderImageFileId = file.Id;
            }

            databaseContext.Subscriptions.Add(subscription);
            return databaseContext.SaveChangesAsync();
        }
    }
}