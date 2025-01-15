using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Data;
using FiscalManagement.Models;

namespace FiscalManagement.Services
{
    public class TaskService
    {
        private readonly FiscalDbContext _context;

        public TaskService(FiscalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Taskuri>> GetAllTasksAsync()
        {
            return await _context.Taskuri.ToListAsync();
        }

        public async Task<List<Taskuri>> GetTasksForInspectorAsync(string inspectorUserName)
        {
            return await _context.Taskuri
                .Where(t => t.AlocatLa == inspectorUserName)
                .ToListAsync();
        }

        public async Task CreateTaskAsync(Taskuri model, string creatorUserName)
        {
            model.CreatDe = creatorUserName;
            model.DataCreare = DateTime.Now;
            model.Status = "Neînceput";

            _context.Taskuri.Add(model);
            await _context.SaveChangesAsync();
        }

        // Găsește o sarcină după ID
        public async Task<Taskuri> GetByIdAsync(int id)
        {
            return await _context.Taskuri.FindAsync(id);
        }

        // Editează o sarcină
        public async Task UpdateTaskAsync(Taskuri model)
        {
            _context.Taskuri.Update(model);
            await _context.SaveChangesAsync();
        }

        // Șterge o sarcină
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Taskuri.FindAsync(id);
            if (task != null)
            {
                _context.Taskuri.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        // Inspectorul actualizează statusul
        public async Task UpdateStatusAsync(int taskId, string newStatus, string inspectorUserName)
        {
            var task = await _context.Taskuri.FindAsync(taskId);
            if (task != null && task.AlocatLa == inspectorUserName)
            {
                task.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
