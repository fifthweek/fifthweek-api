namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidAcceptedChannelPriceInUsCentsPerWeek : ValidChannelPriceInUsCentsPerWeekBase<ValidAcceptedChannelPriceInUsCentsPerWeek>
    {
        public static readonly int MinValue = 0;
        private static readonly Func<ValidAcceptedChannelPriceInUsCentsPerWeek> Construct = () => new ValidAcceptedChannelPriceInUsCentsPerWeek();

        private ValidAcceptedChannelPriceInUsCentsPerWeek()
        {
        }

        public static ValidAcceptedChannelPriceInUsCentsPerWeek Parse(int value)
        {
            return Parse(value, MinValue, Construct);
        }

        public static bool TryParse(int value, out ValidAcceptedChannelPriceInUsCentsPerWeek weeklySubscriptionPrice)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice);
        }

        public static bool TryParse(int value, out ValidAcceptedChannelPriceInUsCentsPerWeek weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice, out errorMessages);
        }
    }
}