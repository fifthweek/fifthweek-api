﻿namespace Fifthweek.Api.Core
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand>
    {
        /// <remarks>
        /// Return type should be `void` in future for true asynchrony. Until we have a separate back-channel, exceptions and completion are 
        /// synchronized back through the task.
        /// </remarks>
        Task HandleAsync(TCommand command);
    }
}