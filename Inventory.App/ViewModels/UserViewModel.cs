using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.App.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
       

    }
}