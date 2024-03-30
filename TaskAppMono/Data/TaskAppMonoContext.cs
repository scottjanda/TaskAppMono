using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAppMono.Models;

namespace TaskAppMono.Data
{
    public class TaskAppMonoContext : DbContext
    {
        public TaskAppMonoContext (DbContextOptions<TaskAppMonoContext> options)
            : base(options)
        {
        }

        public DbSet<TaskAppMono.Models.TaskItem> TaskItem { get; set; } = default!;
    }
}
