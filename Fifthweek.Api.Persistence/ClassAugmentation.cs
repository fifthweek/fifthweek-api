
using System;





namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel 
	{
        public Channel(
            System.Nullable<System.Guid> id, 
            System.Guid subscriptionId, 
            Fifthweek.Api.Persistence.Subscription subscription, 
            System.Int32 priceInUsCentsPerWeek, 
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            if (priceInUsCentsPerWeek == null)
            {
                throw new ArgumentNullException("priceInUsCentsPerWeek");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.SubscriptionId = subscriptionId;
            this.Subscription = subscription;
            this.PriceInUsCentsPerWeek = priceInUsCentsPerWeek;
            this.CreationDate = creationDate;
        }
	}

}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class File 
	{
        public File(
            System.Guid id, 
            Fifthweek.Api.Persistence.Identity.FifthweekUser user, 
            System.Guid userId, 
            Fifthweek.Api.Persistence.FileState state, 
            System.DateTime uploadStartedDate, 
            System.Nullable<System.DateTime> uploadCompletedDate, 
            System.Nullable<System.DateTime> processingStartedDate, 
            System.Nullable<System.DateTime> processingCompletedDate, 
            System.String fileNameWithoutExtension, 
            System.String fileExtension, 
            System.Int64 blobSizeBytes, 
            System.String purpose)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            if (uploadStartedDate == null)
            {
                throw new ArgumentNullException("uploadStartedDate");
            }

            if (fileNameWithoutExtension == null)
            {
                throw new ArgumentNullException("fileNameWithoutExtension");
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException("fileExtension");
            }

            if (blobSizeBytes == null)
            {
                throw new ArgumentNullException("blobSizeBytes");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.Id = id;
            this.User = user;
            this.UserId = userId;
            this.State = state;
            this.UploadStartedDate = uploadStartedDate;
            this.UploadCompletedDate = uploadCompletedDate;
            this.ProcessingStartedDate = processingStartedDate;
            this.ProcessingCompletedDate = processingCompletedDate;
            this.FileNameWithoutExtension = fileNameWithoutExtension;
            this.FileExtension = fileExtension;
            this.BlobSizeBytes = blobSizeBytes;
            this.Purpose = purpose;
        }
	}

}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription 
	{
        public Subscription(
            System.Guid id, 
            Fifthweek.Api.Persistence.Identity.FifthweekUser creator, 
            System.Guid creatorId, 
            System.String name, 
            System.String tagline, 
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.Creator = creator;
            this.CreatorId = creatorId;
            this.Name = name;
            this.Tagline = tagline;
            this.CreationDate = creationDate;
        }
	}

}

namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel 
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

            return this.Equals((Channel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscription != null ? this.Subscription.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceInUsCentsPerWeek != null ? this.PriceInUsCentsPerWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(Channel other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            if (!object.Equals(this.Subscription, other.Subscription))
            {
                return false;
            }

            if (!object.Equals(this.PriceInUsCentsPerWeek, other.PriceInUsCentsPerWeek))
            {
                return false;
            }

            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription 
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

            return this.Equals((Subscription)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Creator != null ? this.Creator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(Subscription other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            if (!object.Equals(this.Creator, other.Creator))
            {
                return false;
            }

            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }

            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }

            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }

            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }

            return true;
        }
	}

}
namespace Fifthweek.Api.Persistence.Identity
{
	using System;
	using Fifthweek.Api.Core;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Identity.EntityFramework;
	public partial class FifthweekUser 
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

            return this.Equals((FifthweekUser)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RegistrationDate != null ? this.RegistrationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LastSignInDate != null ? this.LastSignInDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LastAccessTokenDate != null ? this.LastAccessTokenDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(FifthweekUser other)
        {
            if (!object.Equals(this.ExampleWork, other.ExampleWork))
            {
                return false;
            }

            if (!object.Equals(this.RegistrationDate, other.RegistrationDate))
            {
                return false;
            }

            if (!object.Equals(this.LastSignInDate, other.LastSignInDate))
            {
                return false;
            }

            if (!object.Equals(this.LastAccessTokenDate, other.LastAccessTokenDate))
            {
                return false;
            }

            return true;
        }
	}

}

namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel  : IIdentityEquatable
	{
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.IdentityEquals((Channel)other);
        }

        protected bool IdentityEquals(Channel other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }

            return true;
        }
	}

	public static partial class ChannelExtensions
	{
		public static System.Threading.Tasks.Task InsertAsync(this System.Data.Common.DbConnection connection, Channel entity, bool idempotent = true)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, InsertStatement(idempotent), entity);
		}

		public static System.Threading.Tasks.Task UpsertAsync(this System.Data.Common.DbConnection connection, Channel entity, params string[] fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpsertStatement(fields), entity);
		}

		public static System.Threading.Tasks.Task UpdateAsync(this System.Data.Common.DbConnection connection, Channel entity, params string[] fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), entity);
		}

		public static string InsertStatement(bool idempotent = true)
		{
			const string insert = "INSERT INTO Channels(@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)";
			return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
		}

		public static string UpsertStatement(params string[] fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append(
				@"MERGE Channels as Target
				USING (VALUES (@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)) AS Source (Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate)
				ON    (Target.Id = Source.Id AND Target.SubscriptionId = Source.SubscriptionId)
				WHEN MATCHED THEN
					UPDATE
					SET		");
			sql.AppendUpdateParameters(fields);
			sql.Append(
				@"WHEN NOT MATCHED THEN
					INSERT  (@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)
					VALUES  (Source.Id, Source.SubscriptionId, Source.PriceInUsCentsPerWeek, Source.CreationDate)");
			return sql.ToString();
		}

		public static string UpdateStatement(params string[] fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append("UPDATE Channels SET ");
			sql.AppendUpdateParameters(fields);
			sql.Append("WHERE Id = @Id AND SubscriptionId = @SubscriptionId");
			return sql.ToString();
		}
	}
}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using System.Data.SqlClient;
	using System.Text;
	using System.Threading.Tasks;
	using Dapper;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription  : IIdentityEquatable
	{
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.IdentityEquals((Subscription)other);
        }

        protected bool IdentityEquals(Subscription other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            return true;
        }
	}

	public static partial class SubscriptionExtensions
	{
		public static System.Threading.Tasks.Task InsertAsync(this System.Data.Common.DbConnection connection, Subscription entity, bool idempotent = true)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, InsertStatement(idempotent), entity);
		}

		public static System.Threading.Tasks.Task UpsertAsync(this System.Data.Common.DbConnection connection, Subscription entity, params string[] fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpsertStatement(fields), entity);
		}

		public static System.Threading.Tasks.Task UpdateAsync(this System.Data.Common.DbConnection connection, Subscription entity, params string[] fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), entity);
		}

		public static string InsertStatement(bool idempotent = true)
		{
			const string insert = "INSERT INTO Subscriptions(@Id, @CreatorId, @Name, @Tagline, @CreationDate)";
			return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
		}

		public static string UpsertStatement(params string[] fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append(
				@"MERGE Subscriptions as Target
				USING (VALUES (@Id, @CreatorId, @Name, @Tagline, @CreationDate)) AS Source (Id, CreatorId, Name, Tagline, CreationDate)
				ON    (Target.Id = Source.Id)
				WHEN MATCHED THEN
					UPDATE
					SET		");
			sql.AppendUpdateParameters(fields);
			sql.Append(
				@"WHEN NOT MATCHED THEN
					INSERT  (@Id, @CreatorId, @Name, @Tagline, @CreationDate)
					VALUES  (Source.Id, Source.CreatorId, Source.Name, Source.Tagline, Source.CreationDate)");
			return sql.ToString();
		}

		public static string UpdateStatement(params string[] fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append("UPDATE Subscriptions SET ");
			sql.AppendUpdateParameters(fields);
			sql.Append("WHERE Id = @Id");
			return sql.ToString();
		}
	}
}
