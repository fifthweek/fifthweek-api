using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership
{
    [AutoEqualityMembers]
    public partial class UserId
    {
        protected UserId()
        {
        }

        public Guid Value { get; protected set; }

        public static UserId Parse(Guid value)
        {
            return new UserId
            {
                Value = value
            };
        }
    }
}