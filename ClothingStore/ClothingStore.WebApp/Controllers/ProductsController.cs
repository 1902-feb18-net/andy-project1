using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        public Lib.IProductsRepo PRepo { get; }
        public ProductsController(Lib.IProductsRepo productsRepo)
        {
            PRepo = productsRepo;
        }
        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<Lib.Products> products = PRepo.GetProducts();
            var viewModels = products.Select(p => new Product
            {
                ItemId = p.ItemId,
                ItemName = p.ItemName,
                ItemDescription = p.ItemDescription,
                ItemPrice = p.ItemPrice
            }).ToList();
            return View(viewModels);
        }

        // Here maybe I'll access the inventory and display list of stores and how many are left
        // in each store?
        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}