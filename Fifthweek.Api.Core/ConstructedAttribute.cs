using System;

namespace Fifthweek.Api.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConstructedAttribute : Attribute
    {
        public ConstructedAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }

        public bool TypeAcceptsNull { get; set; }
    }
}