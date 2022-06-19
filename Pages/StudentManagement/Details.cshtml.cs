using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab4.DataAccess;

namespace lab4.Pages.StudentManagement
{
    public class DetailsModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;
        public List<AcademicRecord> AcademicRecordsList { get; set; } = default!;

        public List<Course> AllAvailableCoursesList { get; set; } = default!;

        public DetailsModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public string OrderBy { get; set; }

        public async Task<IActionResult> OnGetAsync(string id, string orderby)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();

            }
            else
            {
                Student = student;

                AcademicRecordsList = await _context.AcademicRecords.Where(m => m.StudentId == id).ToListAsync();

                AllAvailableCoursesList = await _context.Courses.ToListAsync();
            }

            if (orderby == "Course")
            {

                AllAvailableCoursesList.Sort((c1, c2) => c1.Title.CompareTo(c2.Title));

                OrderBy = orderby;
            }

            if (orderby == "Grade")
            {

                AcademicRecordsList.Sort((record1, record2) => { return (int)record1.Grade! - (int)record2.Grade!; });

                OrderBy = orderby;
            }

            return Page();
        }
    }
}
