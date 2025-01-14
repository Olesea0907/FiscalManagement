using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Cereri
{
    public class DeleteModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DeleteModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cerere = await _context.Cereri.FindAsync(id);

            if (Cerere != null)
            {
                _context.Cereri.Remove(Cerere);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
