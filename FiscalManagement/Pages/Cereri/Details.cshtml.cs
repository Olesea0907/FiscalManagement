using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Cereri
{
    public class DetailsModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DetailsModel(FiscalDbContext context)
        {
            _context = context;
        }

        public Cerere Cerere { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cerere = await _context.Cereri
                .Include(c => c.Contribuabil)
                .FirstOrDefaultAsync(m => m.CerereID == id);

            if (Cerere == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
