using System.ComponentModel.DataAnnotations;

namespace FiscalManagement.Models
{
    public class Plata
    {
        public int PlataID { get; set; }

        [Required]
        public decimal Suma { get; set; }

        [Required]
        public DateTime DataPlatii { get; set; }

        public int ContribuabilID { get; set; }
        public Contribuabil Contribuabil { get; set; }
    }
}
