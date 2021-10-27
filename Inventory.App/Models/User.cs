using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.App.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HASH { get; set; }

        public byte[] SALT { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}