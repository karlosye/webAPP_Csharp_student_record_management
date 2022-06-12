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

        public List<Student> StudentsList { get; set; } = default!;
        public List<AcademicRecord> AcademicRecordsList { get; set; } = default!;

        public string OrderBy { get; set; }

        public async Task OnGetAsync(string orderby)
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
        }


    }
}
