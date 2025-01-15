using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Models;
using FiscalManagement.Data;

namespace FiscalManagement.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly FiscalDbContext _context;

        public EditModel(FiscalDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
                return Page();

            var existingTask = await _context.Taskuri.FindAsync(Task.TaskID);
            if (existingTask == null)
                return NotFound();

            if (User.IsInRole("Inspector"))
            {
                existingTask.Status = Task.Status;
            }
            else if (User.IsInRole("SefDeSectie"))
            {
                // Șeful modifică tot
                existingTask.Titlu = Task.Titlu;
                existingTask.Descriere = Task.Descriere;
                existingTask.Status = Task.Status;
                existingTask.DataLimita = Task.DataLimita;
                existingTask.AlocatLa = Task.AlocatLa;
                existingTask.Prioritate = Task.Prioritate;
            
            }
            else
            {
                // oricine altcineva -> Forbid (403)
                return Forbid();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Taskuri.Any(e => e.TaskID == Task.TaskID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }
    }
}
