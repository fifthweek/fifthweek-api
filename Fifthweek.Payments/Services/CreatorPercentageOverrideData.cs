namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorPercentageOverrideData
    {
        public decimal Percentage { get; private set; }

        [Optional]
        public DateTime? ExpiryDate { get; private set; }
    }
}