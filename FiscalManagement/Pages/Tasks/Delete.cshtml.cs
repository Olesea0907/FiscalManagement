using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiscalManagement.Models;
using FiscalManagement.Data;
using Microsoft.AspNetCore.Authorization;

namespace FiscalManagement.Pages.Tasks
{
    [Authorize(Roles = "SefDeSectie")]

    public class DeleteModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public DeleteModel(FiscalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Taskuri Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Task = await _context.Taskuri.FindAsync(id);
            if (Task == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var task = await _context.Taskuri.FindAsync(id);
            if (task != null)
            {
                _context.Taskuri.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
