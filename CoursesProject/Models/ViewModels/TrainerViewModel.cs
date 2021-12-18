using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models.ViewModels
{
    public class TrainerViewModel
    {
        [Display(Name = "Trainer's name")]
        [Required(ErrorMessage = "Please enter the trainer name")]
        public string TrainerName { get; set; }
        public IFormFile attachment { get; set; }
    }
}
