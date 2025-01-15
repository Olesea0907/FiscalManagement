using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiscalManagement.Models
{
    public class Plata
    {
        public int PlataID { get; set; }

        [Required]
        [MaxLength(255)]
        public string DetaliiPlata { get; set; }

        [Required]
        public DateTime DataPlata { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Suma trebuie să fie mai mare decât 0.")]
        public decimal Suma { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipPlata { get; set; }

        [Required]
        public byte[] Fisier { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(ContribuabilID))]
        public Contribuabil Contribuabil { get; set; }

        [Required]
        public int ContribuabilID { get; set; }
        [NotMapped]
        public IFormFile FisierUpload { get; set; }
    }
}
