using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Contribuabili
{
    public class CreateModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public CreateModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contribuabil Contribuabil { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adaugă contribuabilul în baza de date
            _context.Contribuabili.Add(Contribuabil);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
