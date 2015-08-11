namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetFilesEligibleForGarbageCollectionDbStatement : IGetFilesEligibleForGarbageCollectionDbStatement
    {
        public static readonly string Sql = string.Format(
            @"SELECT f.{0}, f.{1}, f.{2}
            FROM {3} f
            WHERE UploadStartedDate<@EndDateExclusive
                AND NOT EXISTS
                (
                    SELECT * FROM {4} p WHERE f.{0}=p.{5} OR f.{0}=p.{6}
                )
                AND NOT EXISTS
                (
                    SELECT * FROM {7} b WHERE f.{0}=b.{8}
                )
                AND NOT EXISTS
                (
                    SELECT * FROM {9} u WHERE f.{0}=u.{10}
                )",
            File.Fields.Id,
            File.Fields.ChannelId,
            File.Fields.Purpose,
            File.Table,
            Post.Table,
            Post.Fields.FileId,
            Post.Fields.ImageId,
            Blog.Table,
            Blog.Fields.HeaderImageFileId,
            FifthweekUser.Table,
            FifthweekUser.Fields.ProfileImageFileId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<OrphanedFileData>> ExecuteAsync(DateTime endDateExclusive)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var items = await connection.QueryAsync<File>(
                    Sql,
                    new { EndDateExclusive = endDateExclusive },
                    commandTimeout: 600);

                return items.Select(v => new OrphanedFileData(
                    new FileId(v.Id),
                    v.ChannelId == null ? null : new ChannelId(v.ChannelId.Value),
                    v.Purpose)).ToList();
            }
        }
    }
}