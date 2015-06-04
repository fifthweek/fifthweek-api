using System;
using System.Linq;

//// Generated on 03/06/2015 16:39:25 (UTC)
//// Mapped solution in 10.66s


namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class AggregateCostSummary 
    {
        public AggregateCostSummary(
            System.Decimal cost)
        {
            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class CostPeriod 
    {
        public CostPeriod(
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            System.Int32 cost)
        {
            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class CreatorChannelSnapshot 
    {
        public CreatorChannelSnapshot(
            System.Guid channelId,
            System.Int32 price)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            this.ChannelId = channelId;
            this.Price = price;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class CreatorGuestListSnapshot 
    {
        public CreatorGuestListSnapshot(
            System.DateTime timestamp,
            System.Guid creatorId,
            System.Collections.Generic.IReadOnlyList<System.String> guestListEmails)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (guestListEmails == null)
            {
                throw new ArgumentNullException("guestListEmails");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.GuestListEmails = guestListEmails;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class CreatorSnapshot 
    {
        public CreatorSnapshot(
            System.DateTime timestamp,
            System.Guid creatorId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.CreatorChannelSnapshot> creatorChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (creatorChannels == null)
            {
                throw new ArgumentNullException("creatorChannels");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.CreatorChannels = creatorChannels;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class MergedSnapshot 
    {
        public MergedSnapshot(
            System.DateTime timestamp,
            Fifthweek.Payments.CreatorSnapshot creator,
            Fifthweek.Payments.CreatorGuestListSnapshot creatorGuestList,
            Fifthweek.Payments.SubscriberSnapshot subscriber)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creator == null)
            {
                throw new ArgumentNullException("creator");
            }

            if (creatorGuestList == null)
            {
                throw new ArgumentNullException("creatorGuestList");
            }

            if (subscriber == null)
            {
                throw new ArgumentNullException("subscriber");
            }

            this.Timestamp = timestamp;
            this.Creator = creator;
            this.CreatorGuestList = creatorGuestList;
            this.Subscriber = subscriber;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;

    public partial class PaymentProcessor 
    {
        public PaymentProcessor(
            Fifthweek.Payments.Pipeline.ILoadSnapshotsExecutor loadSnapshots,
            Fifthweek.Payments.Pipeline.IVerifySnapshotsExecutor verifySnapshots,
            Fifthweek.Payments.Pipeline.IMergeSnapshotsExecutor mergeSnapshots,
            Fifthweek.Payments.Pipeline.IRollBackSubscriptionsExecutor rollBackSubscriptions,
            Fifthweek.Payments.Pipeline.IRollForwardSubscriptionsExecutor rollForwardSubscriptions,
            Fifthweek.Payments.Pipeline.ITrimSnapshotsExecutor trimSnapshots,
            Fifthweek.Payments.Pipeline.ICalculateCostPeriodsExecutor calculateCostPeriods,
            Fifthweek.Payments.Pipeline.IAggregateCostPeriodsExecutor aggregateCostPeriods)
        {
            if (loadSnapshots == null)
            {
                throw new ArgumentNullException("loadSnapshots");
            }

            if (verifySnapshots == null)
            {
                throw new ArgumentNullException("verifySnapshots");
            }

            if (mergeSnapshots == null)
            {
                throw new ArgumentNullException("mergeSnapshots");
            }

            if (rollBackSubscriptions == null)
            {
                throw new ArgumentNullException("rollBackSubscriptions");
            }

            if (rollForwardSubscriptions == null)
            {
                throw new ArgumentNullException("rollForwardSubscriptions");
            }

            if (trimSnapshots == null)
            {
                throw new ArgumentNullException("trimSnapshots");
            }

            if (calculateCostPeriods == null)
            {
                throw new ArgumentNullException("calculateCostPeriods");
            }

            if (aggregateCostPeriods == null)
            {
                throw new ArgumentNullException("aggregateCostPeriods");
            }

            this.loadSnapshots = loadSnapshots;
            this.verifySnapshots = verifySnapshots;
            this.mergeSnapshots = mergeSnapshots;
            this.rollBackSubscriptions = rollBackSubscriptions;
            this.rollForwardSubscriptions = rollForwardSubscriptions;
            this.trimSnapshots = trimSnapshots;
            this.calculateCostPeriods = calculateCostPeriods;
            this.aggregateCostPeriods = aggregateCostPeriods;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;

    public partial class CalculateCostPeriodsExecutor 
    {
        public CalculateCostPeriodsExecutor(
            Fifthweek.Payments.Pipeline.ICalculateSnapshotCostExecutor costCalculator)
        {
            if (costCalculator == null)
            {
                throw new ArgumentNullException("costCalculator");
            }

            this.costCalculator = costCalculator;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class SubscriberChannelSnapshot 
    {
        public SubscriberChannelSnapshot(
            System.Guid channelId,
            System.Int32 acceptedPrice,
            System.DateTime subscriptionStartDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.ChannelId = channelId;
            this.AcceptedPrice = acceptedPrice;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class SubscriberSnapshot 
    {
        public SubscriberSnapshot(
            System.DateTime timestamp,
            System.Guid subscriberId,
            System.String email,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.SubscriberChannelSnapshot> subscribedChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            if (subscribedChannels == null)
            {
                throw new ArgumentNullException("subscribedChannels");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.Email = email;
            this.SubscribedChannels = subscribedChannels;
        }
    }
}

namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class AggregateCostSummary 
    {
        public override string ToString()
        {
            return string.Format("AggregateCostSummary({0})", this.Cost == null ? "null" : this.Cost.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((AggregateCostSummary)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AggregateCostSummary other)
        {
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class CostPeriod 
    {
        public override string ToString()
        {
            return string.Format("CostPeriod({0}, {1}, {2})", this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.Cost == null ? "null" : this.Cost.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CostPeriod)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CostPeriod other)
        {
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class CreatorChannelSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelSnapshot({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Price == null ? "null" : this.Price.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorChannelSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelSnapshot other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class CreatorGuestListSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorGuestListSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.GuestListEmails == null ? "null" : this.GuestListEmails.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorGuestListSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.GuestListEmails != null 
        			? this.GuestListEmails.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorGuestListSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.GuestListEmails != null && other.GuestListEmails != null)
            {
                if (!this.GuestListEmails.SequenceEqual(other.GuestListEmails))
                {
                    return false;    
                }
            }
            else if (this.GuestListEmails != null || other.GuestListEmails != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class CreatorSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.CreatorChannels == null ? "null" : this.CreatorChannels.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannels != null 
        			? this.CreatorChannels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.CreatorChannels != null && other.CreatorChannels != null)
            {
                if (!this.CreatorChannels.SequenceEqual(other.CreatorChannels))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannels != null || other.CreatorChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class MergedSnapshot 
    {
        public override string ToString()
        {
            return string.Format("MergedSnapshot({0}, {1}, {2}, {3})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.Creator == null ? "null" : this.Creator.ToString(), this.CreatorGuestList == null ? "null" : this.CreatorGuestList.ToString(), this.Subscriber == null ? "null" : this.Subscriber.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((MergedSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Creator != null ? this.Creator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorGuestList != null ? this.CreatorGuestList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscriber != null ? this.Subscriber.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(MergedSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.Creator, other.Creator))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorGuestList, other.CreatorGuestList))
            {
                return false;
            }
        
            if (!object.Equals(this.Subscriber, other.Subscriber))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;

    public partial class SubscriberChannelSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelSnapshot({0}, {1}, {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((SubscriberChannelSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelSnapshot other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class SubscriberSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberSnapshot({0}, {1}, \"{2}\", {3})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.SubscribedChannels == null ? "null" : this.SubscribedChannels.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((SubscriberSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscribedChannels != null 
        			? this.SubscribedChannels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            if (this.SubscribedChannels != null && other.SubscribedChannels != null)
            {
                if (!this.SubscribedChannels.SequenceEqual(other.SubscribedChannels))
                {
                    return false;    
                }
            }
            else if (this.SubscribedChannels != null || other.SubscribedChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}


