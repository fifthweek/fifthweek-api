namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoSqlAttribute : Attribute
    {
        public string Table { get; set; }
    }
}
