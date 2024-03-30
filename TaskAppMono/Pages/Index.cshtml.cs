using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskAppMono.Data;
using TaskAppMono.Models;

namespace TaskAppMono.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TaskAppMono.Data.TaskAppMonoContext _context;

        public IndexModel(TaskAppMono.Data.TaskAppMonoContext context)
        {
            _context = context;
        }

        public IList<TaskItem> TaskItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userEmail = User.Identities.FirstOrDefault().Name;

            TaskItem = await _context.TaskItem
                .Where(item => item.UserEmail == userEmail)
                .ToListAsync();
        }
    }
}
