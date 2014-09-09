namespace Dexter.Api.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Commands;
    using Dexter.Api.Repositories;

    public class SaveChangesCommandHandler : ICommandHandler<SaveChangesCommand>
    {
        private readonly IDexterDbContext dexterDbContext;

        public SaveChangesCommandHandler(IDexterDbContext dexterDbContext)
        {
            this.dexterDbContext = dexterDbContext;
        }

        public async Task HandleAsync(SaveChangesCommand command)
        {
            var result = await this.dexterDbContext.SaveChangesAsync();
            
            if (result == 0)
            {
                throw new ApplicationException("Failed to save changes.");
            }
        }
    }
}