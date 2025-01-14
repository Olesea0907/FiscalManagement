using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class EditModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public EditModel(FiscalManagement.Data.FiscalDbContext context)
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

            var plata =  await _context.Plati.FirstOrDefaultAsync(m => m.PlataID == id);
            if (plata == null)
            {
                return NotFound();
            }
            Plata = plata;
           ViewData["ContribuabilID"] = new SelectList(_context.Contribuabili, "ContribuabilID", "CNP");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Plata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlataExists(Plata.PlataID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlataExists(int id)
        {
            return _context.Plati.Any(e => e.PlataID == id);
        }
    }
}
