using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;
using System;
using System.Threading.Tasks;

namespace FiscalManagement.Pages.Audite
{
    public class EditModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public EditModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Audit Audit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Audit = await _context.Audite.AsNoTracking().FirstOrDefaultAsync(a => a.AuditID == id);

            if (Audit == null)
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

            try
            {
                var auditExistenta = await _context.Audite.FirstOrDefaultAsync(a => a.AuditID == Audit.AuditID);

                if (auditExistenta == null)
                {
                    return NotFound();
                }

                // Actualizează valorile
                auditExistenta.Descriere = Audit.Descriere;
                auditExistenta.DataAudit = Audit.DataAudit;

                // Atașare entitate și salvare modificări
                _context.Attach(auditExistenta).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "Eroare la salvarea datelor: " + ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
