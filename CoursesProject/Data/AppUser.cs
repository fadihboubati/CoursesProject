using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Data
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public appUserGender Gender { get; set; }
    }
    public enum appUserGender
    {
        Male, Female
    }

}
