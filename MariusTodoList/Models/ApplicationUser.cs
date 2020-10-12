using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Username")]
        override public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public virtual ICollection<TasksModel> Tasks { get; set; }
        
    }
}
