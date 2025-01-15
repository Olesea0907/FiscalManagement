using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiscalManagement.Models
{
    [Table("Taskuri")]
    public class Taskuri
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titlu { get; set; }

        [MaxLength(500)]
        public string Descriere { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Neînceput";

        public DateTime DataCreare { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Limită")]
        [FutureDate(ErrorMessage = "Data limita trebuie să fie în viitor.")]
        public DateTime? DataLimita { get; set; }

        [MaxLength(100)]
        public string AlocatLa { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prioritate { get; set; } = "Mediu";

        [MaxLength(100)]
        public string CreatDe { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Data trebuie să fie în viitor.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
