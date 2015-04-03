namespace Fifthweek.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity.Core;
    using System.Data.SqlClient;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    using Fifthweek.Shared;

    public class FifthweekTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        private static readonly SqlDatabaseTransientErrorDetectionStrategy SqlDatabaseStrategy = new SqlDatabaseTransientErrorDetectionStrategy();

        private static readonly StorageTransientErrorDetectionStrategy StorageStrategy = new StorageTransientErrorDetectionStrategy();

        public bool IsTransient(Exception outerException)
        {
            if (outerException == null)
            {
                return false;
            }

            var isTransient = false;
            foreach (var exception in this.Flatten(outerException))
            {
                isTransient = this.IsTransientInner(exception);

                if (isTransient)
                {
                    break;
                }
            }

            return isTransient;
        }

        private bool IsTransientInner(Exception exception)
        {
            return this.CustomStrategyIsTransient(exception) || SqlDatabaseStrategy.IsTransient(exception) || StorageStrategy.IsTransient(exception);
        }

        private bool CustomStrategyIsTransient(Exception exception)
        {
            bool isTransient = false;

            if (exception is System.Data.Entity.Core.OptimisticConcurrencyException || exception is Shared.OptimisticConcurrencyException)
            {
                return true;
            }

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                foreach (SqlError error in sqlException.Errors)
                {
                    switch (error.Number)
                    {
                        case RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode:
                        case RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode:
                            isTransient = true;
                            break;
                    }
                }
            }
            else
            {
                var entityException = exception as EntityException;
                if (entityException != null)
                {
                    if (entityException.ToString().Contains("transient failure"))
                    {
                        isTransient = true;
                    }
                }
                else
                {
                    var win32Exception = exception as Win32Exception;
                    if (win32Exception != null)
                    {
                        if (win32Exception.ToString().Contains("timed out"))
                        {
                            isTransient = true;
                        }
                    }
                }
            }

            return isTransient;
        }

        private IReadOnlyList<Exception> Flatten(Exception exception)
        {
            exception.AssertNotNull("exception");

            var output = new List<Exception>();
            this.Flatten(exception, output);
            return output;
        }

        private void Flatten(Exception exception, List<Exception> output)
        {
            output.Add(exception);

            var aggregateException = exception as AggregateException;
            if (aggregateException != null && aggregateException.InnerExceptions != null)
            {
                foreach (var child in aggregateException.InnerExceptions)
                {
                    if (child != null)
                    {
                        this.Flatten(child, output);
                    }
                }
            }
            else if (exception.InnerException != null)
            {
                this.Flatten(exception.InnerException, output);
            }
        }
    }
}