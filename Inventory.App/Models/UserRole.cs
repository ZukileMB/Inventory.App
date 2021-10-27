using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.App.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey ("UserId")]
        public virtual User User { get; set; }
        [ForeignKey ("RoleId")]
        public virtual Roles Roles { get; set; }

    }
}