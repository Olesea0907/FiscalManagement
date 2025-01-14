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
    public class DetailsModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public DetailsModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        public Plata Plata { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plati.FirstOrDefaultAsync(m => m.PlataID == id);
            if (plata == null)
            {
                return NotFound();
            }
            else
            {
                Plata = plata;
            }
            return Page();
        }
    }
}
