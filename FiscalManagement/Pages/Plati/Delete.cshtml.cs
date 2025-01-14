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
    public class DeleteModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public DeleteModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plati.FindAsync(id);
            if (plata != null)
            {
                Plata = plata;
                _context.Plati.Remove(Plata);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
