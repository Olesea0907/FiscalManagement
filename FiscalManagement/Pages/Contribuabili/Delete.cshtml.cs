using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Contribuabili
{
    public class DeleteModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DeleteModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contribuabil Contribuabil { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contribuabil = await _context.Contribuabili.FirstOrDefaultAsync(m => m.ContribuabilID == id);

            if (Contribuabil == null)
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

            var contribuabil = await _context.Contribuabili.FindAsync(id);

            if (contribuabil != null)
            {
                _context.Contribuabili.Remove(contribuabil);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
