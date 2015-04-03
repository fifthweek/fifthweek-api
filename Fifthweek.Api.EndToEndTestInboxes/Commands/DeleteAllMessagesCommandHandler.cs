namespace Fifthweek.Api.EndToEndTestMailboxes.Commands
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteAllMessagesCommandHandler : ICommandHandler<DeleteAllMessagesCommand>
    {
        private static readonly string Sql = string.Format(
            "DELETE FROM {0} WHERE {1} = @Mailbox",
            EndToEndTestEmail.Table,
            EndToEndTestEmail.Fields.Mailbox);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(DeleteAllMessagesCommand command)
        {
            command.AssertNotNull("command");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { Mailbox = command.MailboxName.Value });
            }
        }
    }
}