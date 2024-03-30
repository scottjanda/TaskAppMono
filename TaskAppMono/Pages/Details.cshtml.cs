using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskAppMono.Data;
using TaskAppMono.Models;

namespace TaskAppMono.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TaskAppMono.Data.TaskAppMonoContext _context;

        public DetailsModel(TaskAppMono.Data.TaskAppMonoContext context)
        {
            _context = context;
        }

        public TaskItem TaskItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskitem = await _context.TaskItem.FirstOrDefaultAsync(m => m.TaskItemId == id);
            if (taskitem == null)
            {
                return NotFound();
            }
            else
            {
                TaskItem = taskitem;
            }
            return Page();
        }
    }
}
