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
            Console.WriteLine("Metoda OnPostAsync a fost apelată.");

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Eroare pentru cheia '{key}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }


            _context.Contribuabili.Add(Contribuabil);
            await _context.SaveChangesAsync();

            Console.WriteLine("Contribuabil adăugat cu succes.");
            return RedirectToPage("./Index");
        }

    }
}
