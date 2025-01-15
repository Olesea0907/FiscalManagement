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

        [BindProperty]
        public Taskuri Task { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Eroare: " + err.ErrorMessage);
                }
                return Page();
            }


            _context.Taskuri.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
