using Inventory.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;

namespace Inventory.App.Helpers
{
    public class SessionContext
    {
        public static int UserId
        {
            get { return HttpContext.Current.Session["UserId"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["UserId"]); }
            set { HttpContext.Current.Session["UserId"] = value; }
        }
        public static InvoicesViewModel TempInvoice
        {
            get { return HttpContext.Current.Session["TempInvoice"] as InvoicesViewModel ?? new InvoicesViewModel(); }
            set { HttpContext.Current.Session["TempInvoice"] = value; }
        }
        public static List<ProductsViewModel> InvoiceItems
        {
            get { return HttpContext.Current.Session["InvoiceItems"] as List<ProductsViewModel> ?? new List<ProductsViewModel>(); }
            set { HttpContext.Current.Session["InvoiceItems"] = value; }
        }
    }
}