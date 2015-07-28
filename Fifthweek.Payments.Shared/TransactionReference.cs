namespace Fifthweek.Payments.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class TransactionReference
    {
        public Guid Value { get; private set; }

        public static TransactionReference Random()
        {
            return new TransactionReference(Guid.NewGuid());
        }
    }
}