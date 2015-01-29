namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ParsedElementsAttribute : Attribute
    {
        public ParsedElementsAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}