using System;
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

        public IList<Student> Student { get; set; } = default!;
        public IList<AcademicRecord> AcademicRecord { get; set; } = default!;

        public string OrderBy { get; set; }

        public async Task OnGetAsync(string orderby)
        {
            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync();
                AcademicRecord = await _context.AcademicRecords.ToListAsync();

            }

            if (orderby != null)
            {
                OrderBy = orderby;
            }
        }


    }
}
