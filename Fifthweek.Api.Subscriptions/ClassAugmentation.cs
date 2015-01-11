using System;
using System.Linq;



namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership;
	using Dapper;
	using Fifthweek.Api.Persistence;
	public partial class SetMandatorySubscriptionFieldsCommand 
	{
        public SetMandatorySubscriptionFieldsCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Subscriptions.SubscriptionId subscriptionId, 
            Fifthweek.Api.Subscriptions.SubscriptionName subscriptionName, 
            Fifthweek.Api.Subscriptions.Tagline tagline, 
            Fifthweek.Api.Subscriptions.ChannelPriceInUsCentsPerWeek basePrice)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (subscriptionName == null)
            {
                throw new ArgumentNullException("subscriptionName");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (basePrice == null)
            {
                throw new ArgumentNullException("basePrice");
            }

            this.Requester = requester;
            this.SubscriptionId = subscriptionId;
            this.SubscriptionName = subscriptionName;
            this.Tagline = tagline;
            this.BasePrice = basePrice;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership;
	using Dapper;
	using Fifthweek.Api.Persistence;
	public partial class SetMandatorySubscriptionFieldsCommandHandler 
	{
        public SetMandatorySubscriptionFieldsCommandHandler(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext, 
            Fifthweek.Api.Subscriptions.ISubscriptionSecurity subscriptionSecurity)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            if (subscriptionSecurity == null)
            {
                throw new ArgumentNullException("subscriptionSecurity");
            }

            this.fifthweekDbContext = fifthweekDbContext;
            this.subscriptionSecurity = subscriptionSecurity;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class SubscriptionId 
	{
        public SubscriptionId(
            System.Guid value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class SubscriptionSecurity 
	{
        public SubscriptionSecurity(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Http;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Subscriptions.Commands;
	using Fifthweek.Api.Identity.OAuth;
	public partial class SubscriptionController 
	{
        public SubscriptionController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Subscriptions.Commands.SetMandatorySubscriptionFieldsCommand> setMandatorySubscriptionFields, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext)
        {
            if (setMandatorySubscriptionFields == null)
            {
                throw new ArgumentNullException("setMandatorySubscriptionFields");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.setMandatorySubscriptionFields = setMandatorySubscriptionFields;
            this.userContext = userContext;
        }
	}

}

namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Collections.Generic;
	using Fifthweek.Api.Core;
	using System.Linq;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class ChannelPriceInUsCentsPerWeek 
	{
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

            return this.Equals((ChannelPriceInUsCentsPerWeek)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ChannelPriceInUsCentsPerWeek other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Commands
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Threading.Tasks;
	using Fifthweek.Api.Identity.Membership;
	using Dapper;
	using Fifthweek.Api.Persistence;
	public partial class SetMandatorySubscriptionFieldsCommand 
	{
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

            return this.Equals((SetMandatorySubscriptionFieldsCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(SetMandatorySubscriptionFieldsCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.BasePrice, other.BasePrice))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class SubscriptionId 
	{
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

            return this.Equals((SubscriptionId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(SubscriptionId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Http;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Subscriptions.Commands;
	public partial class MandatorySubscriptionData 
	{
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

            return this.Equals((MandatorySubscriptionData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriptionNameObject != null ? this.SubscriptionNameObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TaglineObject != null ? this.TaglineObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePriceObject != null ? this.BasePriceObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(MandatorySubscriptionData other)
        {
            if (!object.Equals(this.SubscriptionNameObject, other.SubscriptionNameObject))
            {
                return false;
            }

            if (!object.Equals(this.TaglineObject, other.TaglineObject))
            {
                return false;
            }

            if (!object.Equals(this.BasePriceObject, other.BasePriceObject))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionName, other.SubscriptionName))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.BasePrice, other.BasePrice))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class SubscriptionName 
	{
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

            return this.Equals((SubscriptionName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(SubscriptionName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Subscriptions
{
	using System;
	using System.Linq;
	using Fifthweek.Api.Core;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Identity.Membership;
	using Fifthweek.Api.Persistence;
	public partial class Tagline 
	{
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

            return this.Equals((Tagline)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(Tagline other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
	}

}

namespace Fifthweek.Api.Subscriptions.Controllers
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Http;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Subscriptions.Commands;
	public partial class MandatorySubscriptionData 
	{
		public SubscriptionName SubscriptionNameObject { get; set; }
		public Tagline TaglineObject { get; set; }
		public ChannelPriceInUsCentsPerWeek BasePriceObject { get; set; }

		public void Parse()
		{
			MandatorySubscriptionDataExtensions.Parse(this); // Avoid conflicts between property and type names.
		}
	}

	public static partial class MandatorySubscriptionDataExtensions
	{
		public static void Parse(MandatorySubscriptionData target)
		{
			var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

			if (true || !SubscriptionName.IsEmpty(target.SubscriptionName))
			{
				SubscriptionName @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (SubscriptionName.TryParse(target.SubscriptionName, out @object, out errorMessages))
				{
					target.SubscriptionNameObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("SubscriptionName", modelState);
				}
			}

			if (true || !Tagline.IsEmpty(target.Tagline))
			{
				Tagline @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (Tagline.TryParse(target.Tagline, out @object, out errorMessages))
				{
					target.TaglineObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("Tagline", modelState);
				}
			}

			if (true || !ChannelPriceInUsCentsPerWeek.IsEmpty(target.BasePrice))
			{
				ChannelPriceInUsCentsPerWeek @object;
				System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
				if (ChannelPriceInUsCentsPerWeek.TryParse(target.BasePrice, out @object, out errorMessages))
				{
					target.BasePriceObject = @object;
				}
				else
				{
					var modelState = new System.Web.Http.ModelBinding.ModelState();
					foreach (var errorMessage in errorMessages)
					{
						modelState.Errors.Add(errorMessage);
					}

					modelStateDictionary.Add("BasePrice", modelState);
				}
			}

			if (!modelStateDictionary.IsValid)
			{
				throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
			}
		}	
	}
}

