using System;

namespace Fifthweek.Api.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StronglyTypedAttribute : Attribute
    {
        public StronglyTypedAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}