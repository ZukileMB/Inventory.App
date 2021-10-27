using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.App.Models
{
    public class InvoiceDetails
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
    }
}