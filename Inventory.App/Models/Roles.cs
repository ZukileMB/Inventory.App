using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.App.Models
{
    public class Roles
    { 
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}