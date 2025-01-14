using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Audite
{
    public class CreateModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public CreateModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Audit Audit { get; set; }

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

            _context.Audite.Add(Audit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
