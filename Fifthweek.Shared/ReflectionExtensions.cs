namespace Fifthweek.Shared
{
    using System;

    public static class ReflectionExtensions
    {
        public static bool IsPrimitiveEx(this Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }
    }
}