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
    public class DeleteModel : PageModel
    {
        private readonly TaskAppMono.Data.TaskAppMonoContext _context;

        public DeleteModel(TaskAppMono.Data.TaskAppMonoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskitem = await _context.TaskItem.FindAsync(id);
            if (taskitem != null)
            {
                TaskItem = taskitem;
                _context.TaskItem.Remove(TaskItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
