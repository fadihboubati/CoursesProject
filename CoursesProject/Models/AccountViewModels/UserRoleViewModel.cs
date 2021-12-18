using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Models.AccountViewModels
{
    // Importent Note:
    // This is a view model
    // So this will not be a table
    // we will use it to just make an object from the data that we will get it from a form(view)(cshtml) when we do the submit
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool isSelected{ get; set; }
    }
}
