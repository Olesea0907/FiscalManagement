using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class IndexModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public IndexModel(FiscalDbContext context)
        {
            _context = context;
        }

        public IList<Plata> Plati { get; set; }

        public async Task OnGetAsync()
        {
            Plati = await _context.Plati
                .Include(p => p.Contribuabil)
                .ToListAsync();
        }
    }
}
