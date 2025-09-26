using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem task);
        Task AddRangeAsync(IEnumerable<TaskItem> tasks);
        Task ClearAsync();

        List<TaskItem> GetAll();
    }
}
