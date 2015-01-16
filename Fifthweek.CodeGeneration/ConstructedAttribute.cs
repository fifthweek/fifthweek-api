namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ConstructedAttribute : Attribute
    {
        public ConstructedAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }

        public bool TypeAcceptsNull { get; set; }

        public bool IsGuidBase64 { get; set; }
    }
}