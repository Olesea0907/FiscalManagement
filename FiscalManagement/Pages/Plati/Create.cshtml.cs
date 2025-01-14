using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Plati
{
    public class CreateModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public CreateModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ContribuabilID"] = new SelectList(_context.Contribuabili, "ContribuabilID", "CNP");
            return Page();
        }

        [BindProperty]
        public Plata Plata { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Plati.Add(Plata);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
