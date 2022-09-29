using Inventory.App.Helpers;
using Inventory.App.Models;
using Inventory.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Inventory.App.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        [Authorize]
        public ActionResult Index()
        {
            var list = new List<InvoicesViewModel>();
            try
            {
                var DBcontext = new DatabaseContext();

                list = DBcontext.Invoices.Join(DBcontext.User,
                    inv => inv.UserId,
                    usr => usr.UserId,
                    (inv, usr) => new InvoicesViewModel
                    {
                        InvoiceId = inv.InvoiceId,
                        InvoiceDate = inv.InvoiceDate,
                        UserId = inv.UserId,
                        Total = inv.Total,
                        UserViewModel = new UserViewModel
                        {
                            UserId = usr.UserId,
                            FullName = (usr.FirstName + " " + usr.LastName)
                        }
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot open Invoice Details page");
            }
            return View(list);

        }
        public ActionResult Create()
        {
            SessionContext.TempInvoice.InvoiceDate = DateTime.Now;
            SessionContext.TempInvoice.UserId = SessionContext.UserId;

            //DbContext.Invoices.Add(invoice);
            //DbContext.SaveChanges();AddToInvoiceAction
            return RedirectToAction("AddItems");
        }

        /// <summary>
        /// Partial view to display invoice
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        public PartialViewResult AddToInvoiceAction(ProductsViewModel productsViewModel)
        {

            return PartialView("~/Views/Invoices/PartialViews/_AddToInvoicePartial.cshtml", productsViewModel);
        }

        /// <summary>
        /// Add new products 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddItems()
        {
            var list = new List<ProductsViewModel>();
            try
            {
                DatabaseContext DBcontext = new DatabaseContext();
                List<Products> ProductList = DBcontext.Products.ToList();

                foreach (Products product in ProductList)
                {
                    ProductsViewModel tempProduct = new ProductsViewModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        QuantityInStock = product.QuantityInStock,
                        UnitPrice = product.UnitPrice
                    };
                    list.Add(tempProduct);
                }
            }
            catch
            {

            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItems(InvoiceDetailsVewModel invoiceDetailsVewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(" ", "Incorrect details");
                }
            }
            catch
            {
                ModelState.AddModelError(" ", "error when trying to save details");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToInvoice(ProductsViewModel productsViewModel)
        {

            try
            {
                List<ProductsViewModel> lstProd = SessionContext.InvoiceItems.ToList();
                //if (ModelState.IsValid)
                lstProd.Add(productsViewModel);
                SessionContext.InvoiceItems = lstProd;

                // return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK, "Item was Added successfully");

            }
            catch
            {
                //   return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Item was Not Added");

            }
            return RedirectToAction("AddItems");
        }

        /// <summary>
        /// Partial that display Items in the Invoice view
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DisplayInvoiceItems()
        {
            List<InvoiceDetailsVewModel> lstInvoiceDetails = SessionContext.InvoiceItems.GroupBy(m => m.ProductId).Select(prd => new InvoiceDetailsVewModel
            {
                ProductId = prd.First().ProductId,
                QuantitySold = prd.Count(),
                TotalAmount = prd.Sum(c => c.UnitPrice)
            }).ToList();
            return PartialView("~/Views/Invoices/PartialViews/_DisplayInvoicePartial.cshtml", lstInvoiceDetails);
        }
    }
}