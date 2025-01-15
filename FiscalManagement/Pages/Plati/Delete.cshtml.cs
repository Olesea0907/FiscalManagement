using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class DeleteModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DeleteModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Plata = await _context.Plati.FindAsync(id);

            if (Plata != null)
            {
                _context.Plati.Remove(Plata);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
