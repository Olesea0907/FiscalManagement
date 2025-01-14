using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Cereri
{
    public class IndexModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public IndexModel(FiscalDbContext context)
        {
            _context = context;
        }

        public IList<Cerere> Cereri { get; set; } = new List<Cerere>();

        public async Task OnGetAsync()
        {
            Cereri = await _context.Cereri
                .Include(c => c.Contribuabil)
                .ToListAsync();
            Console.WriteLine($"Număr de cereri găsite: {Cereri.Count}");
        }

    }
}
