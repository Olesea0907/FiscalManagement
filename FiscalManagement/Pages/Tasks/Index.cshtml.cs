using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Models;
using FiscalManagement.Data;

namespace FiscalManagement.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public IndexModel(FiscalDbContext context)
        {
            _context = context;
        }

        public List<Taskuri> Taskuri { get; set; }

        public async Task OnGetAsync()
        {
            Taskuri = await _context.Taskuri
                .OrderBy(t => t.DataLimita) 
                .ToListAsync();
        }
    }
}
