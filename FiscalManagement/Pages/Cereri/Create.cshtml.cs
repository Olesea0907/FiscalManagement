using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Cereri
{
    public class CreateModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public CreateModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cerere Cerere { get; set; }

        public SelectList ContribuabiliSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var contribuabili = await _context.Contribuabili
                .Select(c => new
                {
                    c.ContribuabilID,
                    DisplayName = $"{c.Nume} {c.Prenume} (CNP: {c.CNP})"
                })
                .ToListAsync();

            ContribuabiliSelectList = new SelectList(contribuabili, "ContribuabilID", "DisplayName");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"ContribuabilID: {Cerere.ContribuabilID}");
            Console.WriteLine($"TipCerere: {Cerere.TipCerere}");
            Console.WriteLine($"DataDepunerii: {Cerere.DataDepunerii}");
            Console.WriteLine($"Status: {Cerere.Status}");

            if (Cerere.ContribuabilID <= 0)
            {
                ModelState.AddModelError(string.Empty, "Contribuabilul nu este valid.");
                return Page();
            }

            _context.Cereri.Add(Cerere);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


    }
}
