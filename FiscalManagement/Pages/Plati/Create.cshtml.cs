using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class CreateModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public CreateModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plata Plata { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Contribuabili"] = new SelectList(_context.Contribuabili, "ContribuabilID", "Nume");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Contribuabili"] = new SelectList(_context.Contribuabili, "ContribuabilID", "Nume");
                return Page();
            }

            // Procesare fișier
            if (Plata.FisierUpload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Plata.FisierUpload.CopyToAsync(memoryStream);
                    Plata.Fisier = memoryStream.ToArray();
                }
            }

            _context.Plati.Add(Plata);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}
