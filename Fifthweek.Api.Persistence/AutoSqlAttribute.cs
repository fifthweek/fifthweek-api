namespace Fifthweek.Api.Persistence
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoSqlAttribute : Attribute
    {
        public string Table { get; set; }
    }
}
