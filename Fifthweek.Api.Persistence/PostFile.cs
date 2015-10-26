namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql, AutoCopy(RequiresBuilder = false)]
    public partial class PostFile
    {
        public PostFile()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid PostId { get; set; }

        [Required, Optional, NonEquatable]
        public Post Post { get; set; }

        [Required, Key, Column(Order = 1), Index]
        public Guid FileId { get; set; }

        [Required, Optional, NonEquatable]
        public File File { get; set; }
    }
}