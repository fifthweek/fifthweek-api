namespace Fifthweek.Api.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Repositories;

    public class SaveChangesCommandHandler : ICommandHandler<SaveChangesCommand>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public SaveChangesCommandHandler(IFifthweekDbContext fifthweekDbContext)
        {
            this.fifthweekDbContext = fifthweekDbContext;
        }

        public async Task HandleAsync(SaveChangesCommand command)
        {
            var result = await this.fifthweekDbContext.SaveChangesAsync();
            
            if (result == 0)
            {
                throw new ApplicationException("Failed to save changes.");
            }
        }
    }
}