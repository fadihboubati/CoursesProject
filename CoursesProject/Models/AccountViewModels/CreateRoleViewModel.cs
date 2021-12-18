using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models.AccountViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Enter the Role Name")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
