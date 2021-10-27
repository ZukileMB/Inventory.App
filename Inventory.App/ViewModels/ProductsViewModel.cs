using System.ComponentModel.DataAnnotations;

namespace Inventory.App.ViewModels
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        [Required]
        [Display (Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Quantity in Stock")]
        public int QuantityInStock { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        
    }
}