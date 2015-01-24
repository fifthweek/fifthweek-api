namespace Fifthweek.Api.Persistence
{
    using Dapper;

    /// <summary>
    /// Type handler that is automatically registered by <see cref="DapperTypeHandlerRegistration"/>.
    /// </summary>
    public interface IAutoRegisteredTypeHandler<T> : SqlMapper.ITypeHandler
    {
    }
}