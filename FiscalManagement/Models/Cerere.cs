using System;
using System.ComponentModel.DataAnnotations;

namespace FiscalManagement.Models
{
    public class Cerere
    {
        public int CerereID { get; set; }

        [Required(ErrorMessage = "Tipul cererii este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Tipul cererii nu poate depăși 100 de caractere.")]
        public string TipCerere { get; set; }

        [Required(ErrorMessage = "Data depunerii este obligatorie.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(CerereValidator), nameof(CerereValidator.ValidateDate))]
        public DateTime DataDepunerii { get; set; }

        [Required(ErrorMessage = "Statusul cererii este obligatoriu.")]
        [StringLength(50, ErrorMessage = "Statusul cererii nu poate depăși 50 de caractere.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Contribuabilul este obligatoriu.")]
        public int ContribuabilID { get; set; }

        public Contribuabil Contribuabil { get; set; }
    }

    public class CerereValidator
    {
        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            DateTime today = DateTime.Now;
            DateTime fiveYearsAgo = today.AddYears(-5);

            if (date > today)
            {
                return new ValidationResult("Data depunerii nu poate fi în viitor.");
            }

            if (date < fiveYearsAgo)
            {
                return new ValidationResult($"Data depunerii nu poate fi mai veche de {fiveYearsAgo.ToShortDateString()}.");
            }

            return ValidationResult.Success;
        }
    }
}
