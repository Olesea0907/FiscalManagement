using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Audite
{
    public class DetailsModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DetailsModel(FiscalDbContext context)
        {
            _context = context;
        }

        public Audit Audit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Audit = await _context.Audite.FindAsync(id);

            if (Audit == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
