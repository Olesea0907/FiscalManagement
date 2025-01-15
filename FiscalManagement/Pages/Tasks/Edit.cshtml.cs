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
            // Preluăm entitatea din DB
            Task = await _context.Taskuri.FindAsync(id);
            if (Task == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1) Verificăm dacă modelul (Task) a trecut de validare
            if (!ModelState.IsValid) 
                return Page();

            // 2) Căutăm task-ul în DB, după ID
            var existingTask = await _context.Taskuri.FindAsync(Task.TaskID);
            if (existingTask == null)
                return NotFound();

            // 3) Inspector vs. Șef de secție
            if (User.IsInRole("Inspector"))
            {
                // Inspectorul modifică doar Status
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

                // Dacă aveți câmpuri [Required] precum `CreatDe`, mențineți valoarea existentă
                // sau o puteți reseta, după logica voastră.
                // De ex.: existingTask.CreatDe = existingTask.CreatDe;
            }
            else
            {
                // oricine altcineva -> Forbid (403)
                return Forbid();
            }

            // 4) Salvăm modificările
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

            // 5) Redirect la Index
            return RedirectToPage("Index");
        }
    }
}
