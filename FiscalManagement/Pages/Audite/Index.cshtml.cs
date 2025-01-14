using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Audite
{
    public class IndexModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public IndexModel(FiscalDbContext context)
        {
            _context = context;
        }

        public IList<Audit> Audite { get; set; }

        public async Task OnGetAsync()
        {
            Audite = await _context.Audite.ToListAsync();
        }
    }
}
