using CoursesProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter Email Addrees")]
        public string UserEmail { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password")]
        public string UserPassword { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Confirm the Password")]
        [Compare("UserPassword", ErrorMessage = "Miss Match Password and Confirmation....")]
        public string ConfirmUserPassword { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public appUserGender Gender { get; set; }
    }
}
