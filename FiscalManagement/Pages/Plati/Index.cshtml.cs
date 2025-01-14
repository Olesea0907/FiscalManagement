using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class IndexModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public IndexModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        public IList<Plata> Plata { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Plata = await _context.Plati
                .Include(p => p.Contribuabil).ToListAsync();
        }
    }
}
