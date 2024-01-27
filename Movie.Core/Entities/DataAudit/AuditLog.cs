using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Entities.DataAudit
{
    public class AuditLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(200)]
        public string? EntityName { get; set; }

        [MaxLength(50)]
        public string? ChangeType { get; set; }

        public DateTime? ChangeDate { get; set; }

        public string? OriginalValues { get; set; }

        public string? ChangedValues { get; set; }
    }
}
