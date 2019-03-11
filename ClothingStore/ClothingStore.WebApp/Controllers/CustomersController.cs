﻿using System;
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
        public Lib.ICustomerRepo CRepo { get; }
        public Lib.IClothingStoreRepo SRepo { get; }
        public CustomersController(Lib.ICustomerRepo customerRepo, Lib.IClothingStoreRepo storeRepo)
        {
            CRepo = customerRepo;
            SRepo = storeRepo;
        }

        

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Lib.Customer> customers = CRepo.GetCustomers();
            IEnumerable<Lib.Store> stores = SRepo.GetStores();

            var viewModels = customers.Select(c => new Customer
            {
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DefaultStoreId = c.DefaultStoreId,
                DefaultStoreName = stores.Single(s => s.Id == c.DefaultStoreId).Name
            }).ToList();

            ViewBag.numOfCustomers = customers.Count();
            ViewBag.numOfStores = stores.Count();

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
            var viewModel = new Customer
            {
                Stores = SRepo.GetStores().ToList()
            };
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lib.Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                var newCustomer = new Lib.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DefaultStoreId = customer.DefaultStoreId
                };

                CRepo.InsertCustomer(newCustomer);
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
            //Lib.Restaurant libRest = Repo.GetRestaurantById(id);
            //var webRest = new Restaurant
            //{
            //    Id = libRest.Id,
            //    Name = libRest.Name
            //};

            Lib.Customer libRest = CRepo.GetCustomerById(id);
            var webRest = new Customer
            {
                CustomerId = libRest.Id,
                FirstName = libRest.FirstName,
                LastName = libRest.LastName,
                DefaultStoreId = libRest.DefaultStoreId
            };
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[FromRoute] int id, [Bind("Name")]Restaurant restaurant
        public ActionResult Edit([FromRoute]int id, [Bind("Name")] Customer customer)
        {
            try
            {
                // edit does not work

                if (ModelState.IsValid)
                {
                    Lib.Customer libRest = CRepo.GetCustomerById(id);
                    libRest.FirstName = customer.FirstName;
                    libRest.LastName = customer.LastName;
                    libRest.DefaultStoreId = customer.DefaultStoreId;
                    CRepo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View(customer);
            }
        }
    }
}