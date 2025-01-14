using System;
using System.ComponentModel.DataAnnotations;

namespace FiscalManagement.Models
{
    public class Audit
    {
        public int AuditID { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        [StringLength(200, ErrorMessage = "Descrierea nu poate depăși 200 de caractere.")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Data auditului este obligatorie.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataAudit { get; set; }
    }
}
