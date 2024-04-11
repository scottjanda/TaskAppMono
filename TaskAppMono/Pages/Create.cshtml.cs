using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskAppMono.Data;
using TaskAppMono.Models;

namespace TaskAppMono.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TaskAppMono.Data.TaskAppMonoContext _context;

        public CreateModel(TaskAppMono.Data.TaskAppMonoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            TaskItem = new TaskItem
            {
                //UserEmail = "",
                UserEmail = User.Identities.FirstOrDefault().Name,
                Description = "",
                DueDate = DateTime.Now,
                Sensative = false,
                FrequencyType = "Single"
            };

            return Page();
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TaskItem.Add(TaskItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
