using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                //Total = orders.Select(ORepo.GetOrderLists(o.OrderId).)
                StoreName = stores.Single(s => s.Id == o.StoreId).Name,
                FirstName = customers.Single(c => c.Id == o.CustomerId).FirstName,
                LastName = customers.Single(c => c.Id == o.CustomerId).LastName,
                FullName = customers.Single(c => c.Id == o.CustomerId).FirstName + " " + customers.Single(c => c.Id == o.CustomerId).LastName
            }).ToList();

            decimal? totalSum = 0;
            decimal? maxNum = viewModels.Any() ? viewModels.Max(ma => ma.Total) : 0;
            decimal? minNum = viewModels.Any() ? viewModels.Min(ma => ma.Total) : 0;

            foreach(var item in viewModels)
            {
                totalSum += item.Total;
            }
            ViewBag.totalSum = totalSum;
            ViewBag.averageSum = totalSum / viewModels.Count();
            ViewBag.maxNum = maxNum;
            ViewBag.minNum = minNum;

            return View(viewModels);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<Lib.Customer> customers = CRepo.GetCustomers();
            IEnumerable<Lib.Store> stores = SRepo.GetStores();
            IEnumerable<Lib.Order> orders = ORepo.GetOrders();
            List <Lib.OrderList> orderLists = ORepo.GetOrderLists(id).OrderBy(o => o.OrderId).ToList();
            List <Lib.Products> products = PRepo.GetProducts().OrderBy(p => p.ItemId).ToList();
            Lib.Order order = ORepo.GetOrderByOrderId(id);



            var viewModels = orders.Select(o => new Orders
            {
                OrderId = o.OrderId,
                StoreId = o.StoreId,
                CustomerId = o.CustomerId,
                DatePurchased = o.DatePurchased,
                Total = o.Total,
                //double total = db.tblCustomerOrders.Where(x => x.CustomerID == customer.CustomerID).Select(t => t.Amount ?? 0).Sum();
                
                StoreName = stores.Single(s => s.Id == o.StoreId).Name,
                FirstName = customers.Single(c => c.Id == o.CustomerId).FirstName,
                LastName = customers.Single(c => c.Id == o.CustomerId).LastName,
                FullName = customers.Single(c => c.Id == o.CustomerId).FirstName + " " + customers.Single(c => c.Id == o.CustomerId).LastName
            }).ToList();

            return View();
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            List<Lib.Products> products = PRepo.GetProducts().OrderBy(p => p.ItemId).ToList();
            List<Lib.OrderList> orderLists = new List<Lib.OrderList>();
            List<Lib.Customer> Customers = CRepo.GetCustomers().ToList();

            foreach (var product in products)
            {
                orderLists.Add(new Lib.OrderList
                {
                    OrderId = 0,
                    ItemId = 0,
                    OrderListId = 0,
                    ItemBought = 0
                });
            }

            List<SelectListItem> customerItems = new List<SelectListItem>();
            foreach(var customer in Customers)
            {
                customerItems.Add(new SelectListItem() { Text = customer.FirstName + " " + customer.LastName, Value = customer.Id.ToString() });

            }

            var viewModel = new Orders
            {
                Stores = SRepo.GetStores().ToList(),
                CustomerItems = customerItems,
                Products = products,
                OrderLists = orderLists
            };
            
            return View(viewModel);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Orders order)
        {
            try
            {
                // TODO: Add insert logic here
                var stores = SRepo.GetStores().ToList();
                order.Stores = new List<Lib.Store>();

                if (order.Stores == null)
                {
                    foreach (var s in stores)
                    {
                        var tmpStore = new Lib.Store
                        {
                            Id = s.Id,
                            Name = s.Name
                        };
                        order.Stores.Add(tmpStore);
                    }
                }

                var ord = new Lib.Order
                {
                    StoreId = order.StoreId,
                    DatePurchased = DateTime.Now,
                    CustomerId = order.CustomerId,
                    Total = order.Total
                };

                ORepo.InsertOrder(ord);

                


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}