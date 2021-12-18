using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Student Name")]
        public string StudentName { get; set; }
        public int Age { get; set; }

        [Display(Name = "Enrolment Course")]
        public List<CourseStudentViewModel> CourseStudents { get; set; }
        //public List<TrainerStudentViewModel> TrainerStudents { get; set; }
    }
}
