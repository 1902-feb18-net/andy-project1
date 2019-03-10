using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        public Lib.ICustomerRepo cRepo { get; }
        public Lib.IClothingStoreRepo sRepo { get; }
        public CustomersController(Lib.ICustomerRepo customerRepo, Lib.IClothingStoreRepo storeRepo)
        {
            cRepo = customerRepo;
            sRepo = storeRepo;
        }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Lib.Customer> customers = cRepo.GetCustomers();
            IEnumerable<Lib.Store> stores = sRepo.GetStores();

            var viewModels = customers.Select(c => new Customer
            {
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DefaultStoreId = c.DefaultStoreId
            }).ToList();

            return View(viewModels);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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