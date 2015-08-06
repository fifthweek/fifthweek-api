using System;
using System.Linq;

//// Generated on 04/08/2015 09:55:54 (UTC)
//// Mapped solution in 9.08s


namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class DeleteFileDbStatement 
    {
        public DeleteFileDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class DeleteTestUserAccountsDbStatement 
    {
        public DeleteTestUserAccountsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class GetAllChannelIdsDbStatement 
    {
        public GetAllChannelIdsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class GetFilesEligibleForGarbageCollectionDbStatement 
    {
        public GetFilesEligibleForGarbageCollectionDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class OrphanedFileData 
    {
        public OrphanedFileData(
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String purpose)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.FileId = fileId;
            this.ChannelId = channelId;
            this.Purpose = purpose;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;

    public partial class DeleteBlobsForFile 
    {
        public DeleteBlobsForFile(
            Fifthweek.Api.FileManagement.Shared.IBlobLocationGenerator blobLocationGenerator,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (blobLocationGenerator == null)
            {
                throw new ArgumentNullException("blobLocationGenerator");
            }

            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.blobLocationGenerator = blobLocationGenerator;
            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage.Blob;

    public partial class DeleteOrphanedBlobContainers 
    {
        public DeleteOrphanedBlobContainers(
            Fifthweek.GarbageCollection.IGetAllChannelIdsDbStatement getAllChannelIds,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (getAllChannelIds == null)
            {
                throw new ArgumentNullException("getAllChannelIds");
            }

            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.getAllChannelIds = getAllChannelIds;
            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}
namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    public partial class RunGarbageCollection 
    {
        public RunGarbageCollection(
            ITimestampCreator timestampCreator,
            Fifthweek.GarbageCollection.IDeleteTestUserAccountsDbStatement deleteTestUserAccounts,
            Fifthweek.GarbageCollection.IGetFilesEligibleForGarbageCollectionDbStatement getFilesEligibleForGarbageCollection,
            Fifthweek.GarbageCollection.IDeleteBlobsForFile deleteBlobsForFile,
            Fifthweek.GarbageCollection.IDeleteFileDbStatement deleteFileDbStatement,
            Fifthweek.GarbageCollection.IDeleteOrphanedBlobContainers deleteOrphanedBlobContainers)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (deleteTestUserAccounts == null)
            {
                throw new ArgumentNullException("deleteTestUserAccounts");
            }

            if (getFilesEligibleForGarbageCollection == null)
            {
                throw new ArgumentNullException("getFilesEligibleForGarbageCollection");
            }

            if (deleteBlobsForFile == null)
            {
                throw new ArgumentNullException("deleteBlobsForFile");
            }

            if (deleteFileDbStatement == null)
            {
                throw new ArgumentNullException("deleteFileDbStatement");
            }

            if (deleteOrphanedBlobContainers == null)
            {
                throw new ArgumentNullException("deleteOrphanedBlobContainers");
            }

            this.timestampCreator = timestampCreator;
            this.deleteTestUserAccounts = deleteTestUserAccounts;
            this.getFilesEligibleForGarbageCollection = getFilesEligibleForGarbageCollection;
            this.deleteBlobsForFile = deleteBlobsForFile;
            this.deleteFileDbStatement = deleteFileDbStatement;
            this.deleteOrphanedBlobContainers = deleteOrphanedBlobContainers;
        }
    }
}

namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using System.Collections.Generic;

    public partial class OrphanedFileData 
    {
        public override string ToString()
        {
            return string.Format("OrphanedFileData({0}, {1}, \"{2}\")", this.FileId == null ? "null" : this.FileId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
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
        
            return this.Equals((OrphanedFileData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(OrphanedFileData other)
        {
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
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


