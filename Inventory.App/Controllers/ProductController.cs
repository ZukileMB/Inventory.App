using Inventory.App.Models;
using Inventory.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Inventory.App.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var list = new List<ProductsViewModel>();
            try
            {
                DatabaseContext DBcontext = new DatabaseContext();
                var ProductList = DBcontext.Products;

                foreach (var product in ProductList)
                {
                    var tempProduct = new ProductsViewModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        QuantityInStock = product.QuantityInStock,
                        UnitPrice = product.UnitPrice
                    };
                    list.Add(tempProduct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured can't load the page </ br>" + ex.Message);
            }
            return View(list);
        }


        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductsViewModel productsViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var DbContext = new DatabaseContext();
                    var product = new Products();

                    product.ProductName = productsViewModel.ProductName;
                    product.QuantityInStock = productsViewModel.QuantityInStock;
                    product.UnitPrice = productsViewModel.UnitPrice;


                    DbContext.Products.Add(product);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(" ", "Incorrect details");
            }
            catch
            {
                ModelState.AddModelError(" ", "error when trying to save details");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var productsViewModel = new ProductsViewModel();
            try
            {
                DatabaseContext DbContext = new DatabaseContext();
                Products product = DbContext.Products.Where(n => n.ProductId == id).FirstOrDefault();

                if (product != null)
                {
                    productsViewModel.ProductId = product.ProductId;
                    productsViewModel.ProductName = product.ProductName;
                    productsViewModel.QuantityInStock = product.QuantityInStock;
                    productsViewModel.UnitPrice = product.UnitPrice;
                }

            }
            catch
            {

            }

            return View(productsViewModel);
        }

        /// <summary>
        /// Edit records on the Cat
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsViewModel productsViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var DbContext = new DatabaseContext();
                    var product = DbContext.Products.Where(x => x.ProductId == productsViewModel.ProductId).FirstOrDefault();
                    if (product == null)
                    {
                        ModelState.AddModelError("", "Error when trying to edit record!");
                        return View();
                    }
                    product.ProductName = productsViewModel.ProductName;
                    product.QuantityInStock = productsViewModel.QuantityInStock;
                    product.UnitPrice = productsViewModel.UnitPrice;


                    //DbContext.Products.Add(product);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(" ", "Incorrect details");
            }
            catch
            {
                ModelState.AddModelError(" ", "error when trying to save details");
            }
            return View();
        }

        /// <summary>
        /// Delete Record  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {

            try
            {
                DatabaseContext DbContext = new DatabaseContext();
                Products product = DbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
                if (product == null)
                {
                    ModelState.AddModelError("", "Error when trying to delete record!");
                    return View();
                }
                DbContext.Products.Remove(product);
                DbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {

            }
            return View();
        }
    }
}