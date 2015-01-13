using System;
using System.Linq;



namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.Linq;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel 
	{
        public Channel(
            System.Guid id, 
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
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
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
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription 
	{
        public Subscription(
            System.Guid id, 
            System.Guid creatorId, 
            Fifthweek.Api.Persistence.Identity.FifthweekUser creator, 
            System.String name, 
            System.String tagline, 
            System.String introduction, 
            System.String description, 
            System.String externalVideoUrl, 
            System.Nullable<System.Guid> headerImageFileId, 
            Fifthweek.Api.Persistence.File headerImageFile, 
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

            if (introduction == null)
            {
                throw new ArgumentNullException("introduction");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.CreatorId = creatorId;
            this.Creator = creator;
            this.Name = name;
            this.Tagline = tagline;
            this.Introduction = introduction;
            this.Description = description;
            this.ExternalVideoUrl = externalVideoUrl;
            this.HeaderImageFileId = headerImageFileId;
            this.HeaderImageFile = headerImageFile;
            this.CreationDate = creationDate;
        }
	}

}

namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.Linq;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel 
	{
		public override string ToString()
        {
			return string.Format("Channel({0}, {1}, {2}, {3})", this.Id == null ? "null" : this.Id.ToString(), this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.PriceInUsCentsPerWeek == null ? "null" : this.PriceInUsCentsPerWeek.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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

            return this.Equals((Channel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
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
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class File 
	{
		public override string ToString()
        {
			return string.Format("File({0}, {1}, {2}, {3}, {4}, {5}, {6}, \"{7}\", \"{8}\", {9}, \"{10}\")", this.Id == null ? "null" : this.Id.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.State == null ? "null" : this.State.ToString(), this.UploadStartedDate == null ? "null" : this.UploadStartedDate.ToString(), this.UploadCompletedDate == null ? "null" : this.UploadCompletedDate.ToString(), this.ProcessingStartedDate == null ? "null" : this.ProcessingStartedDate.ToString(), this.ProcessingCompletedDate == null ? "null" : this.ProcessingCompletedDate.ToString(), this.FileNameWithoutExtension == null ? "null" : this.FileNameWithoutExtension.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.BlobSizeBytes == null ? "null" : this.BlobSizeBytes.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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

            return this.Equals((File)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.State != null ? this.State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UploadStartedDate != null ? this.UploadStartedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UploadCompletedDate != null ? this.UploadCompletedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProcessingStartedDate != null ? this.ProcessingStartedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProcessingCompletedDate != null ? this.ProcessingCompletedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileNameWithoutExtension != null ? this.FileNameWithoutExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobSizeBytes != null ? this.BlobSizeBytes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(File other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }

            if (!object.Equals(this.State, other.State))
            {
                return false;
            }

            if (!object.Equals(this.UploadStartedDate, other.UploadStartedDate))
            {
                return false;
            }

            if (!object.Equals(this.UploadCompletedDate, other.UploadCompletedDate))
            {
                return false;
            }

            if (!object.Equals(this.ProcessingStartedDate, other.ProcessingStartedDate))
            {
                return false;
            }

            if (!object.Equals(this.ProcessingCompletedDate, other.ProcessingCompletedDate))
            {
                return false;
            }

            if (!object.Equals(this.FileNameWithoutExtension, other.FileNameWithoutExtension))
            {
                return false;
            }

            if (!object.Equals(this.FileExtension, other.FileExtension))
            {
                return false;
            }

            if (!object.Equals(this.BlobSizeBytes, other.BlobSizeBytes))
            {
                return false;
            }

            if (!object.Equals(this.Purpose, other.Purpose))
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
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription 
	{
		public override string ToString()
        {
			return string.Format("Subscription({0}, {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", {7}, {8})", this.Id == null ? "null" : this.Id.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.ExternalVideoUrl == null ? "null" : this.ExternalVideoUrl.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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

            return this.Equals((Subscription)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExternalVideoUrl != null ? this.ExternalVideoUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
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

            if (!object.Equals(this.Introduction, other.Introduction))
            {
                return false;
            }

            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }

            if (!object.Equals(this.ExternalVideoUrl, other.ExternalVideoUrl))
            {
                return false;
            }

            if (!object.Equals(this.HeaderImageFileId, other.HeaderImageFileId))
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
	using System.Linq;
	using Fifthweek.Api.Core;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	public partial class FifthweekUser 
	{
		public override string ToString()
        {
			return string.Format("FifthweekUser(\"{0}\", {1}, {2}, {3}, {4})", this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.RegistrationDate == null ? "null" : this.RegistrationDate.ToString(), this.LastSignInDate == null ? "null" : this.LastSignInDate.ToString(), this.LastAccessTokenDate == null ? "null" : this.LastAccessTokenDate.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
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

            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
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
	using Fifthweek.Api.Core;
	using System.Linq;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Channel  : IIdentityEquatable
	{
        public Channel(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }

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

            return true;
        }

		[Flags]
        public enum Fields
        {
			Empty = 0,
			Id = 1, 
			SubscriptionId = 2, 
			PriceInUsCentsPerWeek = 4, 
			CreationDate = 8
        }
	}

	public static partial class ChannelExtensions
	{
		public static System.Threading.Tasks.Task InsertAsync(this System.Data.Common.DbConnection connection, Channel entity, bool idempotent = true)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, InsertStatement(idempotent), new 
			{
				entity.Id, entity.SubscriptionId, entity.PriceInUsCentsPerWeek, entity.CreationDate
			});
		}

		public static System.Threading.Tasks.Task UpsertAsync(this System.Data.Common.DbConnection connection, Channel entity, Channel.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpsertStatement(fields), new 
			{
				entity.Id, entity.SubscriptionId, entity.PriceInUsCentsPerWeek, entity.CreationDate
			});
		}

		public static System.Threading.Tasks.Task UpdateAsync(this System.Data.Common.DbConnection connection, Channel entity, Channel.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields));
		}

		public static string InsertStatement(bool idempotent = true)
		{
			const string insert = "INSERT INTO Channels(Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate) VALUES(@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)";
			return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
		}

		public static string UpsertStatement(Channel.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append(
				@"MERGE Channels as Target
				USING (VALUES (@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)) AS Source (Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate)
				ON    (Target.Id = Source.Id)
				WHEN MATCHED THEN
					UPDATE
					SET		");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(
				@" WHEN NOT MATCHED THEN
					INSERT  (Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate)
					VALUES  (Source.Id, Source.SubscriptionId, Source.PriceInUsCentsPerWeek, Source.CreationDate);");
			return sql.ToString();
		}

		public static string UpdateStatement(Channel.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append("UPDATE Channels SET ");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(" WHERE Id = @Id");
			return sql.ToString();
		}

		private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Channel.Fields fields)
		{
			var fieldNames = new System.Collections.Generic.List<string>();
			fieldNames.Add("Id");
			if(fields.HasFlag(Channel.Fields.SubscriptionId))
			{
				fieldNames.Add("SubscriptionId");
			}

			if(fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
			{
				fieldNames.Add("PriceInUsCentsPerWeek");
			}

			if(fields.HasFlag(Channel.Fields.CreationDate))
			{
				fieldNames.Add("CreationDate");
			}

			return fieldNames;
		}

		private static object MaskParameters(Channel entity, Channel.Fields fields)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("Id", entity.Id);
			if(fields.HasFlag(Channel.Fields.SubscriptionId))
			{
				parameters.Add("SubscriptionId", entity.SubscriptionId);
			}

			if(fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
			{
				parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
			}

			if(fields.HasFlag(Channel.Fields.CreationDate))
			{
				parameters.Add("CreationDate", entity.CreationDate);
			}

			return parameters;
		}
	}
}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class File  : IIdentityEquatable
	{
        public File(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }

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

            return this.IdentityEquals((File)other);
        }

        protected bool IdentityEquals(File other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }

            return true;
        }

		[Flags]
        public enum Fields
        {
			Empty = 0,
			Id = 1, 
			UserId = 2, 
			State = 4, 
			UploadStartedDate = 8, 
			UploadCompletedDate = 16, 
			ProcessingStartedDate = 32, 
			ProcessingCompletedDate = 64, 
			FileNameWithoutExtension = 128, 
			FileExtension = 256, 
			BlobSizeBytes = 512, 
			Purpose = 1024
        }
	}

	public static partial class FileExtensions
	{
		public static System.Threading.Tasks.Task InsertAsync(this System.Data.Common.DbConnection connection, File entity, bool idempotent = true)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, InsertStatement(idempotent), new 
			{
				entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
			});
		}

		public static System.Threading.Tasks.Task UpsertAsync(this System.Data.Common.DbConnection connection, File entity, File.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpsertStatement(fields), new 
			{
				entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
			});
		}

		public static System.Threading.Tasks.Task UpdateAsync(this System.Data.Common.DbConnection connection, File entity, File.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields));
		}

		public static string InsertStatement(bool idempotent = true)
		{
			const string insert = "INSERT INTO Files(Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose) VALUES(@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)";
			return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
		}

		public static string UpsertStatement(File.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append(
				@"MERGE Files as Target
				USING (VALUES (@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)) AS Source (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose)
				ON    (Target.Id = Source.Id)
				WHEN MATCHED THEN
					UPDATE
					SET		");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(
				@" WHEN NOT MATCHED THEN
					INSERT  (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose)
					VALUES  (Source.Id, Source.UserId, Source.State, Source.UploadStartedDate, Source.UploadCompletedDate, Source.ProcessingStartedDate, Source.ProcessingCompletedDate, Source.FileNameWithoutExtension, Source.FileExtension, Source.BlobSizeBytes, Source.Purpose);");
			return sql.ToString();
		}

		public static string UpdateStatement(File.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append("UPDATE Files SET ");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(" WHERE Id = @Id");
			return sql.ToString();
		}

		private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(File.Fields fields)
		{
			var fieldNames = new System.Collections.Generic.List<string>();
			fieldNames.Add("Id");
			if(fields.HasFlag(File.Fields.UserId))
			{
				fieldNames.Add("UserId");
			}

			if(fields.HasFlag(File.Fields.State))
			{
				fieldNames.Add("State");
			}

			if(fields.HasFlag(File.Fields.UploadStartedDate))
			{
				fieldNames.Add("UploadStartedDate");
			}

			if(fields.HasFlag(File.Fields.UploadCompletedDate))
			{
				fieldNames.Add("UploadCompletedDate");
			}

			if(fields.HasFlag(File.Fields.ProcessingStartedDate))
			{
				fieldNames.Add("ProcessingStartedDate");
			}

			if(fields.HasFlag(File.Fields.ProcessingCompletedDate))
			{
				fieldNames.Add("ProcessingCompletedDate");
			}

			if(fields.HasFlag(File.Fields.FileNameWithoutExtension))
			{
				fieldNames.Add("FileNameWithoutExtension");
			}

			if(fields.HasFlag(File.Fields.FileExtension))
			{
				fieldNames.Add("FileExtension");
			}

			if(fields.HasFlag(File.Fields.BlobSizeBytes))
			{
				fieldNames.Add("BlobSizeBytes");
			}

			if(fields.HasFlag(File.Fields.Purpose))
			{
				fieldNames.Add("Purpose");
			}

			return fieldNames;
		}

		private static object MaskParameters(File entity, File.Fields fields)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("Id", entity.Id);
			if(fields.HasFlag(File.Fields.UserId))
			{
				parameters.Add("UserId", entity.UserId);
			}

			if(fields.HasFlag(File.Fields.State))
			{
				parameters.Add("State", entity.State);
			}

			if(fields.HasFlag(File.Fields.UploadStartedDate))
			{
				parameters.Add("UploadStartedDate", entity.UploadStartedDate);
			}

			if(fields.HasFlag(File.Fields.UploadCompletedDate))
			{
				parameters.Add("UploadCompletedDate", entity.UploadCompletedDate);
			}

			if(fields.HasFlag(File.Fields.ProcessingStartedDate))
			{
				parameters.Add("ProcessingStartedDate", entity.ProcessingStartedDate);
			}

			if(fields.HasFlag(File.Fields.ProcessingCompletedDate))
			{
				parameters.Add("ProcessingCompletedDate", entity.ProcessingCompletedDate);
			}

			if(fields.HasFlag(File.Fields.FileNameWithoutExtension))
			{
				parameters.Add("FileNameWithoutExtension", entity.FileNameWithoutExtension);
			}

			if(fields.HasFlag(File.Fields.FileExtension))
			{
				parameters.Add("FileExtension", entity.FileExtension);
			}

			if(fields.HasFlag(File.Fields.BlobSizeBytes))
			{
				parameters.Add("BlobSizeBytes", entity.BlobSizeBytes);
			}

			if(fields.HasFlag(File.Fields.Purpose))
			{
				parameters.Add("Purpose", entity.Purpose);
			}

			return parameters;
		}
	}
}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.Linq;
	using System.ComponentModel.DataAnnotations;
	using Fifthweek.Api.Core;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription  : IIdentityEquatable
	{
        public Subscription(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }

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

		[Flags]
        public enum Fields
        {
			Empty = 0,
			Id = 1, 
			CreatorId = 2, 
			Name = 4, 
			Tagline = 8, 
			Introduction = 16, 
			Description = 32, 
			ExternalVideoUrl = 64, 
			HeaderImageFileId = 128, 
			CreationDate = 256
        }
	}

	public static partial class SubscriptionExtensions
	{
		public static System.Threading.Tasks.Task InsertAsync(this System.Data.Common.DbConnection connection, Subscription entity, bool idempotent = true)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, InsertStatement(idempotent), new 
			{
				entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
			});
		}

		public static System.Threading.Tasks.Task UpsertAsync(this System.Data.Common.DbConnection connection, Subscription entity, Subscription.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpsertStatement(fields), new 
			{
				entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
			});
		}

		public static System.Threading.Tasks.Task UpdateAsync(this System.Data.Common.DbConnection connection, Subscription entity, Subscription.Fields fields)
		{
			return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields));
		}

		public static string InsertStatement(bool idempotent = true)
		{
			const string insert = "INSERT INTO Subscriptions(Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate) VALUES(@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)";
			return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
		}

		public static string UpsertStatement(Subscription.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append(
				@"MERGE Subscriptions as Target
				USING (VALUES (@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)) AS Source (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
				ON    (Target.Id = Source.Id)
				WHEN MATCHED THEN
					UPDATE
					SET		");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(
				@" WHEN NOT MATCHED THEN
					INSERT  (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
					VALUES  (Source.Id, Source.CreatorId, Source.Name, Source.Tagline, Source.Introduction, Source.Description, Source.ExternalVideoUrl, Source.HeaderImageFileId, Source.CreationDate);");
			return sql.ToString();
		}

		public static string UpdateStatement(Subscription.Fields fields)
		{
			var sql = new System.Text.StringBuilder();
			sql.Append("UPDATE Subscriptions SET ");
			sql.AppendUpdateParameters(GetFieldNames(fields));
			sql.Append(" WHERE Id = @Id");
			return sql.ToString();
		}

		private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Subscription.Fields fields)
		{
			var fieldNames = new System.Collections.Generic.List<string>();
			fieldNames.Add("Id");
			if(fields.HasFlag(Subscription.Fields.CreatorId))
			{
				fieldNames.Add("CreatorId");
			}

			if(fields.HasFlag(Subscription.Fields.Name))
			{
				fieldNames.Add("Name");
			}

			if(fields.HasFlag(Subscription.Fields.Tagline))
			{
				fieldNames.Add("Tagline");
			}

			if(fields.HasFlag(Subscription.Fields.Introduction))
			{
				fieldNames.Add("Introduction");
			}

			if(fields.HasFlag(Subscription.Fields.Description))
			{
				fieldNames.Add("Description");
			}

			if(fields.HasFlag(Subscription.Fields.ExternalVideoUrl))
			{
				fieldNames.Add("ExternalVideoUrl");
			}

			if(fields.HasFlag(Subscription.Fields.HeaderImageFileId))
			{
				fieldNames.Add("HeaderImageFileId");
			}

			if(fields.HasFlag(Subscription.Fields.CreationDate))
			{
				fieldNames.Add("CreationDate");
			}

			return fieldNames;
		}

		private static object MaskParameters(Subscription entity, Subscription.Fields fields)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("Id", entity.Id);
			if(fields.HasFlag(Subscription.Fields.CreatorId))
			{
				parameters.Add("CreatorId", entity.CreatorId);
			}

			if(fields.HasFlag(Subscription.Fields.Name))
			{
				parameters.Add("Name", entity.Name);
			}

			if(fields.HasFlag(Subscription.Fields.Tagline))
			{
				parameters.Add("Tagline", entity.Tagline);
			}

			if(fields.HasFlag(Subscription.Fields.Introduction))
			{
				parameters.Add("Introduction", entity.Introduction);
			}

			if(fields.HasFlag(Subscription.Fields.Description))
			{
				parameters.Add("Description", entity.Description);
			}

			if(fields.HasFlag(Subscription.Fields.ExternalVideoUrl))
			{
				parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
			}

			if(fields.HasFlag(Subscription.Fields.HeaderImageFileId))
			{
				parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
			}

			if(fields.HasFlag(Subscription.Fields.CreationDate))
			{
				parameters.Add("CreationDate", entity.CreationDate);
			}

			return parameters;
		}
	}
}

