using System;
using System.ComponentModel.DataAnnotations;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Cereri
{
    public class CerereEditDto
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

        public int ContribuabilID { get; set; }
    }
}
