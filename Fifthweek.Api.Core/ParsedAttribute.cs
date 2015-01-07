using System;

namespace Fifthweek.Api.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParsedAttribute : Attribute
    {
        public ParsedAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }

        public bool UseConstructor { get; set; }
    }
}