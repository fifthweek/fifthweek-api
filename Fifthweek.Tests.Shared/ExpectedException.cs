namespace Fifthweek.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class ExpectedException
    {
        public static void AssertException<TException>(this Action action) where TException : Exception
        {
            GetException<TException>(action);
        }

        public static Task AssertExceptionAsync<TException>(this Func<Task> action) where TException : Exception
        {
            return GetExceptionAsync<TException>(action);
        }

        public static TException GetException<TException>(this Action action) where TException : Exception
        {
            var exception = TryGetException<TException>(action);

            if (exception == null)
            {
                Assert.Fail("Expected exception was not thrown: " + typeof(TException).Name);
            }

            return exception;
        }

        public static async Task<TException> GetExceptionAsync<TException>(this Func<Task> action) where TException : Exception
        {
            var exception = await TryGetException<TException>(action);

            if (exception == null)
            {
                Assert.Fail("Expected exception was not thrown: " + typeof(TException).Name);
            }

            return exception;
        }

        private static TException TryGetException<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException t)
            {
                return t;
            }

            return null;
        }

        private static async Task<TException> TryGetException<TException>(Func<Task> action) where TException : Exception
        {
            try
            {
                await action();
            }
            catch (TException t)
            {
                return t;
            }

            return null;
        }
    }
}