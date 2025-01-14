using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Contribuabili
{
    public class IndexModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public IndexModel(FiscalDbContext context)
        {
            _context = context;
        }

        public IList<Contribuabil> Contribuabili { get; set; } = new List<Contribuabil>();

        public async Task OnGetAsync()
        {
            try
            {
                Contribuabili = await _context.Contribuabili.AsNoTracking().ToListAsync();

                Console.WriteLine($"Contribuabili găsiți: {Contribuabili.Count}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Eroare la încărcarea contribuabililor: {ex.Message}");
            }
        }
    }
}
