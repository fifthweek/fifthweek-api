namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class ChannelSubscriptionTests
    {
        public static ChannelSubscription UniqueEntity(Random random)
        {
            return new ChannelSubscription(
                Guid.NewGuid(),
                null,
                Guid.NewGuid(),
                null,
                random.Next(1, 1024),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestChannelSubscriptionWithExistingReferences(this IFifthweekDbContext databaseContext, Guid existingUserId, Guid existingChannelId, DateTime? subscriptionStartDate = null, int? acceptedPrice = null)
        {
            var random = new Random();

            var item = UniqueEntity(random);
            item.ChannelId = existingChannelId;
            item.UserId = existingUserId;

            if (subscriptionStartDate != null)
            {
                item.SubscriptionStartDate = subscriptionStartDate.Value;
            }

            if (acceptedPrice != null)
            {
                item.AcceptedPriceInUsCentsPerWeek = acceptedPrice.Value;
            }

            return databaseContext.Database.Connection.InsertAsync(item);
        }
    }
}