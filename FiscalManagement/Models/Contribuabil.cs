using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiscalManagement.Models
{
    public class Contribuabil
    {
        public int ContribuabilID { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele nu poate depăși 100 de caractere.")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Prenumele nu poate depăși 100 de caractere.")]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "CNP-ul este obligatoriu.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNP-ul trebuie să aibă 13 caractere.")]
        [CustomValidation(typeof(CNPValidator), nameof(CNPValidator.IsUnique))]
        public string CNP { get; set; }

        public string Adresa { get; set; }

        [Required(ErrorMessage = "Numărul de telefon este obligatoriu.")]
        [RegularExpression(@"^0[0-9]{8}$", ErrorMessage = "Numărul de telefon trebuie să înceapă cu 0 și să conțină exact 9 cifre.")]
        public string Telefon { get; set; }

    }

    public class CNPValidator
    {
        public static ValidationResult IsUnique(string cnp, ValidationContext context)
        {
            var dbContext = (FiscalManagement.Data.FiscalDbContext)context.GetService(typeof(FiscalManagement.Data.FiscalDbContext));

            // Obținem ID-ul contribuabilului din context (dacă există)
            var instance = context.ObjectInstance as Contribuabil;
            int? currentContribuabilId = instance?.ContribuabilID;

            // Verificăm dacă CNP-ul există în baza de date, dar excludem contribuabilul curent
            if (dbContext.Contribuabili.Any(c => c.CNP == cnp && c.ContribuabilID != currentContribuabilId))
            {
                return new ValidationResult("CNP-ul este deja înregistrat.");
            }

            return ValidationResult.Success;
        }
    }

}
