using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models.StudentViewModels
{
    public class StudentViewModel
    {
        public string StudentName { get; set; }
        public int Age { get; set; }
        public int[] Courses { get; set; }
    }
}
