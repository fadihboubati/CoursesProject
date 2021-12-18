using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Display(Name = "Trainer's name")]
        [Required(ErrorMessage ="Please enter the trainer name")]
        public string TrainerName { get; set; }
        public string attachment { get; set; }

        //public List<TrainerStudentViewModel> TrainerStudents { get; set; }

    }
}
