namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ParsedCounterpartAttribute : Attribute
    {
        public ParsedCounterpartAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}