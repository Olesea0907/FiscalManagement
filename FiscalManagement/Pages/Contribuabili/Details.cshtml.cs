using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Contribuabili
{
    public class DetailsModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public DetailsModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        public Contribuabil Contribuabil { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribuabil = await _context.Contribuabili.FirstOrDefaultAsync(m => m.ContribuabilID == id);
            if (contribuabil == null)
            {
                return NotFound();
            }
            else
            {
                Contribuabil = contribuabil;
            }
            return Page();
        }
    }
}
