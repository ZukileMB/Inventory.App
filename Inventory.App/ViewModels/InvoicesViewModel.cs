using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.App.ViewModels
{
    public class InvoicesViewModel
    {
        public InvoicesViewModel()
        {
            UserViewModel = new UserViewModel();
        }
        public int InvoiceId { get; set; }
        [Required]
        [Display(Name = "Invoice Date ")]
        public DateTime InvoiceDate { get; set; }
        public int UserId { get; set; }
        public UserViewModel UserViewModel { get; set; }

        [Required]
        [Display(Name = "Total ")]
        public decimal Total { get; set; }


    }
}