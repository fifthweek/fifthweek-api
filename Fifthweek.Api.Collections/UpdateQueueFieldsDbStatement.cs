namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateQueueFieldsDbStatement : IUpdateQueueFieldsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(Queue queue)
        {
            queue.AssertNotNull("queue");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(
                    queue,
                    Queue.Fields.Name);
            }
        }
    }
}