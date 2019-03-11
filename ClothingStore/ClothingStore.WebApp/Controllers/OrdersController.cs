using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebApp.Controllers
{
    public class OrdersController : Controller
    {

        public Lib.ICustomerRepo CRepo { get; set; }
        public Lib.IClothingStoreRepo SRepo { get; set; }
        public Lib.IProductsRepo PRepo { get; set; }
        public Lib.IOrdersRepo ORepo { get; set; }

        public OrdersController(Lib.ICustomerRepo customerRepo, Lib.IClothingStoreRepo storeRepo,
            Lib.IProductsRepo productRepo, Lib.IOrdersRepo orderRepo)
        {
            CRepo = customerRepo;
            SRepo = storeRepo;
            PRepo = productRepo;
            ORepo = orderRepo;
        }

        // GET: Orders
        public ActionResult Index()
        {
            IEnumerable<Lib.Customer> customers = CRepo.GetCustomers();
            IEnumerable<Lib.Store> stores = SRepo.GetStores();
            IEnumerable<Lib.Products> products = PRepo.GetProducts();
            IEnumerable<Lib.Order> orders = ORepo.GetOrders();

            var viewModels = orders.Select(o => new Orders
            {
                OrderId = o.OrderId,
                StoreId = o.StoreId,
                CustomerId = o.CustomerId,
                DatePurchased = o.DatePurchased,
                Total = o.Total,
                StoreName = stores.Single(s => s.Id == o.StoreId).Name,
                FirstName = customers.Single(c => c.Id == o.CustomerId).FirstName,
                LastName = customers.Single(c => c.Id == o.CustomerId).LastName,
                FullName = customers.Single(c => c.Id == o.CustomerId).FirstName + " " + customers.Single(c => c.Id == o.CustomerId).LastName
            }).ToList();

            return View(viewModels);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}