namespace Fifthweek.Api.Payments.Commands
{
    using System;

    public class LeaseConflictException : Exception
    {
        public LeaseConflictException(Exception t)
            : base("The lease could not be acquired.", t)
        {
        }
    }
}