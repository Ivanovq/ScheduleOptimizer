using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly ScheduleDbContext _context;

        public EfTaskRepository(ScheduleDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync() =>
            await _context.Tasks.ToListAsync();

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TaskItem> tasks)
        {
            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            _context.Tasks.RemoveRange(_context.Tasks);
            await _context.SaveChangesAsync();
        }

        public List<TaskItem> GetAll()
        {
           return _context.Tasks.ToList();
        }
    }

}
