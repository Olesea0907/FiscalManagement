using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly FiscalManagement.Data.FiscalDbContext _context;

        public DetailsModel(FiscalManagement.Data.FiscalDbContext context)
        {
            _context = context;
        }

        public Taskuri Taskuri { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskuri = await _context.Taskuri.FirstOrDefaultAsync(m => m.TaskID == id);
            if (taskuri == null)
            {
                return NotFound();
            }
            else
            {
                Taskuri = taskuri;
            }
            return Page();
        }
    }
}
