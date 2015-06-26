namespace Fifthweek.Payments.Shared
{
    using Fifthweek.CodeGeneration;

    public class ProcessPaymentsMessage
    {
        public static readonly ProcessPaymentsMessage Default = new ProcessPaymentsMessage();

        public ProcessPaymentsMessage()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is ProcessPaymentsMessage;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}