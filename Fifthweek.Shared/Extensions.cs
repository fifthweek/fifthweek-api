namespace Fifthweek.Shared
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static void AssertNotNull(this object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AssertUtc(this DateTime value, string argumentName)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Must be UTC", argumentName);
            }
        }

        public static void AssertNonEmpty<T>(this IReadOnlyCollection<T> self, string argumentName)
        {
            if (self.Count == 0)
            {
                throw new ArgumentException("Must be non-empty", argumentName);
            }
        }

        public static void AssertNonNegative(this int self, string argumentName)
        {
            if (self < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, "Must be non-negative");
            }
        }
    }
}