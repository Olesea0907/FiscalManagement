using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Cereri
{
    public class EditModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public EditModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CerereEditDto CerereDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var cerere = await _context.Cereri
                        .Include(c => c.Contribuabil)
                        .FirstOrDefaultAsync(c => c.CerereID == id);

            if (cerere == null)
                return NotFound();

            CerereDto = new CerereEditDto
            {
                CerereID = cerere.CerereID,
                TipCerere = cerere.TipCerere,
                DataDepunerii = cerere.DataDepunerii,
                Status = cerere.Status,
                ContribuabilID = cerere.ContribuabilID
            };

            ViewData["Contribuabil"] = cerere.Contribuabil;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.ContainsKey("CerereDto.ContribuabilID"))
                ModelState["CerereDto.ContribuabilID"].Errors.Clear();

            if (!ModelState.IsValid)
            {
                var cerere = await _context.Cereri
                            .Include(c => c.Contribuabil)
                            .FirstOrDefaultAsync(c => c.CerereID == CerereDto.CerereID);
                ViewData["Contribuabil"] = cerere?.Contribuabil;
                return Page();
            }

            var cerereExistenta = await _context.Cereri.FindAsync(CerereDto.CerereID);
            if (cerereExistenta == null)
                return NotFound();

            cerereExistenta.TipCerere = CerereDto.TipCerere;
            cerereExistenta.DataDepunerii = CerereDto.DataDepunerii;
            cerereExistenta.Status = CerereDto.Status;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Eroare la salvare: {ex.Message}");
            }

            var existing = await _context.Cereri
                                .Include(c => c.Contribuabil)
                                .FirstOrDefaultAsync(c => c.CerereID == CerereDto.CerereID);
            ViewData["Contribuabil"] = existing?.Contribuabil;

            return Page();
        }
    }
}
