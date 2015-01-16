namespace Fifthweek.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class ExpectedException<TExpectedException>
            where TExpectedException : Exception
    {
        public static TExpectedException Get(Action action)
        {
            try
            {
                action();
            }
            catch (TExpectedException t)
            {
                return t;
            }

            return null;
        }

        public static async Task<TExpectedException> GetAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (TExpectedException t)
            {
                return t;
            }

            return null;
        }

        public static TExpectedException Assert(Action action)
        {
            var exception = Get(action);

            if (exception == null)
            {
                throw new Exception("Expected exception was not thrown: " + typeof(TExpectedException).Name);
            }

            return exception;
        }

        public static async Task<TExpectedException> AssertAsync(Func<Task> action)
        {
            var exception = await GetAsync(action);

            if (exception == null)
            {
                throw new Exception("Expected exception was not thrown: " + typeof(TExpectedException).Name);
            }

            return exception;
        }
    }
}