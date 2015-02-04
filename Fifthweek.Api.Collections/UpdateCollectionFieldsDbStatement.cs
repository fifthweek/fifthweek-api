namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpdateCollectionFieldsDbStatement : IUpdateCollectionFieldsDbStatement
    {
        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(Collection collection)
        {
            collection.AssertNotNull("collection");
            
            return this.databaseContext.Database.Connection.UpdateAsync(collection, Collection.Fields.Name | Collection.Fields.ChannelId);
        }
    }
}