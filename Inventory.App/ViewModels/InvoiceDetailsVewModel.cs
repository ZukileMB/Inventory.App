using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.App.ViewModels
{
    public class InvoiceDetailsVewModel
    {
        public InvoiceDetailsVewModel()
        {
            productsViewModel = new ProductsViewModel();
        }
        public int Id { get; set; }
        [DisplayName("Invoice Id ")]
        public int InvoiceId { get; set; }
        [DisplayName("Product ")]
        public int ProductId { get; set; }
        [DisplayName("Quantity Sold ")]
        public int QuantitySold { get; set; }
        [DisplayName("Total Amount ")]
        public decimal TotalAmount { get; set; }

        public ProductsViewModel productsViewModel { get; set; }
    }
}