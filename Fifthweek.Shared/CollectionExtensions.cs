namespace Fifthweek.Shared
{
    using System;
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> self, T element)
        {
            return IndexOf(self, _ => object.Equals(_, element));
        }

        public static int IndexOf<T>(this IReadOnlyList<T> self, Predicate<T> foundElement)
        {
            if (foundElement == null)
            {
                throw new ArgumentNullException("foundElement");
            }

            for (var i = 0; i < self.Count; i++)
            {
                if (foundElement(self[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}