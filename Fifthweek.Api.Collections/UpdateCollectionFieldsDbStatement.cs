namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateCollectionFieldsDbStatement : IUpdateCollectionFieldsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(Collection collection)
        {
            collection.AssertNotNull("collection");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(
                    collection,
                    Collection.Fields.Name | Collection.Fields.ChannelId);
            }
        }
    }
}