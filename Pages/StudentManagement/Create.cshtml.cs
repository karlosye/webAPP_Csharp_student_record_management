using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace lab4.Pages.StudentManagement
{
    public class CreateModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public CreateModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public string errorMsg { get; set; } = "";


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(Student.Id);

            if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }

            //StudentsList = await _context.Students.ToListAsync();

            var findStudent = await _context.Students!.FindAsync(Student.Id);

            if (findStudent != null)
            {
                errorMsg = $"Student Id, {Student.Id} already exist!";
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
