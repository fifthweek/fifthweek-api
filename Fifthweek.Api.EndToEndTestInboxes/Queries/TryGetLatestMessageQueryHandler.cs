namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class TryGetLatestMessageQueryHandler : IQueryHandler<TryGetLatestMessageQuery, Message>
    {
        private static readonly string Sql = string.Format(
           "SELECT * FROM {0} WHERE {1} = @Mailbox",
           EndToEndTestEmail.Table,
           EndToEndTestEmail.Fields.Mailbox);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<Message> HandleAsync(TryGetLatestMessageQuery query)
        {
            query.AssertNotNull("query");

            EndToEndTestEmail entity;
            using (var connection = this.connectionFactory.CreateConnection())
            {
                entity = (await connection.QueryAsync<EndToEndTestEmail>(Sql, new { Mailbox = query.MailboxName.Value })).FirstOrDefault();
            }

            return entity == null ? null : new Message(entity.Subject, entity.Body);
        }
    }
}