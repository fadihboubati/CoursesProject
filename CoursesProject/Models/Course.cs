using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        // One Course has One Trainer
        [Required]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        // One Course has One Category
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        // Many Course have many Students
        public List<CourseStudentViewModel> CourseStudents { get; set; }
    }
}
