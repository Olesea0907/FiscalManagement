using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class DetailsModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DetailsModel(FiscalDbContext context)
        {
            _context = context;
        }

        public Plata Plata { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Plata = await _context.Plati
                .Include(p => p.Contribuabil)
                .FirstOrDefaultAsync(p => p.PlataID == id);

            if (Plata == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
