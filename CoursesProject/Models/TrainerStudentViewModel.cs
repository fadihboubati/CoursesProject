using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models
{
    public class TrainerStudentViewModel
    {
        public int TrainerId { get; set; }
        public int StudentId { get; set; }

        // Navigation Properties
        // Specifies our linkages between the tables
        public Student Student { get; set; }
        public Trainer Trainer { get; set; }
    }
}
