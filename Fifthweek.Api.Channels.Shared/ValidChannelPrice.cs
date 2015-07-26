namespace Fifthweek.Api.Channels.Shared
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ValidChannelPrice : ValidChannelPriceBase<ValidChannelPrice>
    {
        public static readonly int MinValue = 1;
        private static readonly Func<ValidChannelPrice> Construct = () => new ValidChannelPrice();

        private ValidChannelPrice()
        {
        }

        public override int Value { get; protected set; }

        public static ValidChannelPrice Parse(int value)
        {
            return Parse(value, MinValue, Construct);
        }

        public static bool TryParse(int value, out ValidChannelPrice weeklySubscriptionPrice)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice);
        }

        public static bool TryParse(int value, out ValidChannelPrice weeklySubscriptionPrice, out IReadOnlyCollection<string> errorMessages)
        {
            return TryParse(value, MinValue, Construct, out weeklySubscriptionPrice, out errorMessages);
        }
    }
}