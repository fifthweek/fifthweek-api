namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Common;
    using System.Threading.Tasks;

    public static class EndToEndTestEmailTests
    {
        public static EndToEndTestEmail UniqueEntity(Random random)
        {
            return new EndToEndTestEmail
            {
                Mailbox = string.Format("wd_{0}{1}", random.Next(100000000, 1000000000), random.Next(1000, 10000)),
                Subject = string.Format("Subject {0}", random.Next()),
                Body = string.Format("Body {0}", random.Next()),
                DateReceived = DateTime.UtcNow.AddDays(random.NextDouble() * -100)
            };
        }

        public static Task CreateEndToEndEmailAsync(this DbConnection connection, string mailboxName)
        {
            var entity = UniqueEntity(new Random());
            entity.Mailbox = mailboxName;
            return connection.InsertAsync(entity);
        }
    }
}