namespace Fifthweek.Tests.Shared
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using Moq.Language.Flow;

    public static class MoqExtensions
    {
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup, params object[] results) where T : class
        {
            var queue = new Queue(results);
            setup.Returns(() =>
            {
                var result = queue.Dequeue();
                if (result is Exception)
                {
                    throw result as Exception;
                }

                return (TResult)result;
            });
        }

        public static void ReturnsInOrderAsync<T, TResult>(this ISetup<T, Task<TResult>> setup, params object[] results) where T : class
        {
            var queue = new Queue(results);
            setup.Returns(() =>
            {
                var result = queue.Dequeue();
                if (result is Exception)
                {
                    var tcs = new TaskCompletionSource<TResult>();
                    tcs.SetException(result as Exception);
                    return tcs.Task;
                }

                return Task.FromResult((TResult)result);
            });
        }
    }
}