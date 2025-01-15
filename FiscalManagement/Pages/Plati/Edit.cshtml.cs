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
        private readonly FiscalDbContext _context;

        public EditModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plata Plata { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Plata = await _context.Plati.FindAsync(id);

            if (Plata == null)
            {
                return NotFound();
            }

            ViewData["Contribuabili"] = new SelectList(_context.Contribuabili, "ContribuabilID", "Nume", Plata.ContribuabilID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Contribuabili"] = new SelectList(_context.Contribuabili, "ContribuabilID", "Nume", Plata.ContribuabilID);
                return Page();
            }

            _context.Attach(Plata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Plati.Any(p => p.PlataID == Plata.PlataID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}
