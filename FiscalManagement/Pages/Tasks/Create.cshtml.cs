using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Tasks
{
    [Authorize(Roles = "SefDeSectie")]
    public class CreateModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public CreateModel(FiscalDbContext context)
        {
            _context = context;
        }

        // Obiectul de tip 'Taskuri' pe care îl creăm
        [BindProperty]
        public Taskuri Task { get; set; }

        // Opțional, dacă vrei să afișezi un formular înainte de post
        public IActionResult OnGet()
        {
            return Page();
        }

        // Când apeși "Submit" în formular (POST):
        public async Task<IActionResult> OnPostAsync()
        {
            // Dacă validarea eșuează (ex. câmpuri [Required] necompletate),
            // rămânem pe pagină pentru a afișa erorile
            if (!ModelState.IsValid)
            {
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Eroare: " + err.ErrorMessage);
                }
                return Page();
            }


            // Inserăm noul Task în context
            _context.Taskuri.Add(Task);
            await _context.SaveChangesAsync();

            // După salvare, mergem la Index
            return RedirectToPage("Index");
        }
    }
}
