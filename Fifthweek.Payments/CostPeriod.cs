namespace Fifthweek.Payments
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CostPeriod
    {
        public DateTime StartTimeInclusive { get; private set; }

        public DateTime EndTimeExclusive { get; private set; }

        public int Cost { get; private set; }
    }
}