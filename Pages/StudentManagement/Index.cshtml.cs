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
    public class IndexModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public List<Student> StudentsList { get; set; } = default!;
        public List<AcademicRecord> AcademicRecordsList { get; set; } = default!;

        public string OrderBy { get; set; }

        public async Task OnGetAsync(string orderby, string id)
        {
            if (_context.Students != null)
            {
                StudentsList = await _context.Students.ToListAsync();
                AcademicRecordsList = await _context.AcademicRecords.ToListAsync();

            }

            if (orderby != null)
            {
                OrderBy = orderby;
            }

            if (id != null)
            {
                Console.WriteLine(id);

                var student = await _context.Students!.FindAsync(id);

                if (student != null)
                {
                    while (true)
                    {
                        var academicrecord = await _context.AcademicRecords.FirstOrDefaultAsync(m => m.StudentId == id);

                        if (academicrecord == null) { break; }

                        if (academicrecord != null)
                        {
                            _context.AcademicRecords.Remove(academicrecord);
                            await _context.SaveChangesAsync();
                        }
                    }

                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();

                    StudentsList = await _context.Students.ToListAsync();
                }
            }
        }


    }
}
