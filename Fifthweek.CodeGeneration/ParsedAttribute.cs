namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ParsedAttribute : Attribute
    {
        public ParsedAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}