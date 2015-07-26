namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidAcceptedChannelPrice : ValidChannelPriceBase<ValidAcceptedChannelPrice>
    {
        public static readonly int MinValue = 0;
        private static readonly Func<ValidAcceptedChannelPrice> Construct = () => new ValidAcceptedChannelPrice();

        private ValidAcceptedChannelPrice()
        {
        }

        public override int Value { get; protected set; }

        public static ValidAcceptedChannelPrice Parse(int value)
        {
            return Parse(value, MinValue, Construct);
        }

        public static bool TryParse(int value, out ValidAcceptedChannelPrice weeklySubscriptionPrice)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice);
        }

        public static bool TryParse(int value, out ValidAcceptedChannelPrice weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice, out errorMessages);
        }
    }
}