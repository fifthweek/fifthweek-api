namespace Fifthweek.Api.CommandHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    public class AddRefreshTokenCommandHandler : ICommandHandler<AddRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        private readonly ICommandHandler<SaveChangesCommand> saveChanges;

        private readonly ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken;

        public AddRefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            ICommandHandler<SaveChangesCommand> saveChanges,
            ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.saveChanges = saveChanges;
            this.removeRefreshToken = removeRefreshToken;
        }

        public async Task HandleAsync(AddRefreshTokenCommand command)
        {
            var token = command.RefreshToken;

            var existingToken = await this.refreshTokenRepository.TryGetRefreshTokenAsync(token.Username, token.ClientId);

            if (existingToken != null)
            {
                await this.removeRefreshToken.HandleAsync(new RemoveRefreshTokenCommand(new HashedRefreshTokenId(existingToken.HashedId)));
            }

            await this.refreshTokenRepository.AddRefreshTokenAsync(token);

            await this.saveChanges.HandleAsync(SaveChangesCommand.Default);
        }
    }
}