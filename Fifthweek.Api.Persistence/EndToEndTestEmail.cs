namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class EndToEndTestEmail
    {
        public EndToEndTestEmail()
        {
        }

        [Key]
        [Required]
        [MaxLength(16)] // See MailboxName
        public string Mailbox { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime DateReceived { get; set; }
    }
}