namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidChannelPriceInUsCentsPerWeek : ValidChannelPriceInUsCentsPerWeekBase<ValidChannelPriceInUsCentsPerWeek>
    {
        public static readonly int MinValue = 1;
        private static readonly Func<ValidChannelPriceInUsCentsPerWeek> Construct = () => new ValidChannelPriceInUsCentsPerWeek();

        private ValidChannelPriceInUsCentsPerWeek()
        {
        }

        public static ValidChannelPriceInUsCentsPerWeek Parse(int value)
        {
            return Parse(value, MinValue, Construct);
        }

        public static bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek weeklySubscriptionPrice)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice);
        }

        public static bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice, out errorMessages);
        }
    }
}