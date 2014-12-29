namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DecoratorAttribute : Attribute
    {
        public DecoratorAttribute(params Type[] decoratorTypes)
        {
            foreach (var item in decoratorTypes)
            {
                if (!item.IsGenericTypeDefinition)
                {
                    throw new ArgumentException("The decoratorTypes should be a open generic types.");
                }
            }

            this.DecoratorTypes = decoratorTypes.ToList().AsReadOnly();
        }

        public IReadOnlyList<Type> DecoratorTypes { get; private set; }
    }
}