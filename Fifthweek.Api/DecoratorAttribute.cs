namespace Fifthweek.Api
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DecoratorAttribute : Attribute
    {
        public DecoratorAttribute(Type decoratorType)
        {
            if (decoratorType.IsGenericType)
            {
                this.DecoratorType = decoratorType.GetGenericTypeDefinition();
            }
            else if (decoratorType.IsGenericTypeDefinition)
            {
                this.DecoratorType = decoratorType;
            }
            else
            {
                throw new ArgumentException("The decoratorType should be a generic type or an open generic type.");
            }
        }

        public Type DecoratorType { get; private set; }
    }
}