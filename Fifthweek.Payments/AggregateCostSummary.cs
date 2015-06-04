namespace Fifthweek.Payments
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class AggregateCostSummary
    {
        public decimal Cost { get; private set; }
    }
}