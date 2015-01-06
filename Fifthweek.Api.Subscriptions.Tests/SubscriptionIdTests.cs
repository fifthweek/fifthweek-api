using System;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionIdTests : EqualityTests<SubscriptionId>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        protected override SubscriptionId ObjectA
        {
            get { return SubscriptionId.Parse(Guid.Parse("{6BE94E94-6280-414A-A189-41145C4223A2}")); }
        }

        protected override SubscriptionId ObjectB
        {
            get { return SubscriptionId.Parse(Guid.Parse("{57A2997D-1944-4D59-94CC-6E3B7973C507}")); }
        }
    }
}