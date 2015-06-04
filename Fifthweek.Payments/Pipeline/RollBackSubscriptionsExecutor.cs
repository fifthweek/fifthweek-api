namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    public class RollBackSubscriptionsExecutor : IRollBackSubscriptionsExecutor
    {
        public IReadOnlyList<MergedSnapshot> Execute(IReadOnlyList<MergedSnapshot> snapshots)
        {
            // This is where we could remove subscriptions that are so short they are considered
            // accidental.
            // However I think there are issues with this... for example how many seconds does it
            // take to click "subscribe" then click on a download link then click "unsubscribe".
            // Or you could automate downloading someone's blog with lots of very quick subscriptions.
            // So for now I'm not implementing this.
            return snapshots;
        }
    }
}