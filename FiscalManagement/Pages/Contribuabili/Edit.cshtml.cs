using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiscalManagement.Data;
using FiscalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalManagement.Pages.Contribuabili
{
    public class EditModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public EditModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contribuabil Contribuabil { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contribuabil = await _context.Contribuabili.FindAsync(id);

            if (Contribuabil == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contribuabilToUpdate = await _context.Contribuabili.FindAsync(Contribuabil.ContribuabilID);

            if (contribuabilToUpdate == null)
            {
                return NotFound();
            }

            contribuabilToUpdate.Nume = Contribuabil.Nume;
            contribuabilToUpdate.Prenume = Contribuabil.Prenume;
            contribuabilToUpdate.CNP = Contribuabil.CNP;
            contribuabilToUpdate.Adresa = Contribuabil.Adresa;
            contribuabilToUpdate.Telefon = Contribuabil.Telefon;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContribuabilExists(Contribuabil.ContribuabilID))
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

        private bool ContribuabilExists(int id)
        {
            return _context.Contribuabili.Any(e => e.ContribuabilID == id);
        }
    }
}
