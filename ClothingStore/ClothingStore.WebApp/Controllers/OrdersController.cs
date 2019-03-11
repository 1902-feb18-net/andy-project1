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

            decimal? totalSum = 0;
            foreach(var item in viewModels)
            {
                totalSum += item.Total;
            }
            ViewBag.totalSum = totalSum;
            ViewBag.averageSum = totalSum / viewModels.Count();

            return View(viewModels);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<Lib.Customer> customers = CRepo.GetCustomers();
            IEnumerable<Lib.Store> stores = SRepo.GetStores();
            IEnumerable<Lib.Order> orders = ORepo.GetOrders();
            //List<Lib.Order> orders = ORepo.GetOrders().OrderBy(o => o.OrderId).ToList();
            List <Lib.OrderList> orderLists = ORepo.GetOrderLists(id).OrderBy(o => o.OrderId).ToList();
            List <Lib.Products> products = PRepo.GetProducts().OrderBy(p => p.ItemId).ToList();
            //List<Lib.OrderList> orderLists 
            Lib.Order order = ORepo.GetOrderByOrderId(id);
            //Lib.OrderList orderList = ORepo.GetOrderLists(id);

            var viewModels = orders.Select(o => new Orders
            {
                OrderId = o.OrderId,
                StoreId = o.StoreId,
                CustomerId = o.CustomerId,
                DatePurchased = o.DatePurchased,
                Total = o.Total,
                //double total = db.tblCustomerOrders.Where(x => x.CustomerID == customer.CustomerID).Select(t => t.Amount ?? 0).Sum();
                //Total = orderLists.Where(ol => ol.OrderId == o.CustomerId).Where(i => i.ItemId == )
                //Total = ORepo.GetOrderByOrderId(order.OrderId).GetTotal(ORepo.GetOrderById(order.OrderId).ToList(), products.ToList()),
                //Total = ORepo.GetOrderByOrderId(order.OrderId).GetTotal(ORepo.GetProductsOfOrders(order.OrderId).ToList(), products.ToList())
                //Total = ORepo.GetOrderByOrderId(order.OrderId).GetTotal(ORepo.GetOrderById(order.OrderId).ToList(), products.ToList())
                //Total = ORepo.GetOrderByOrderId(order.OrderId).GetTotal(ORepo.GetOrderById(order.OrderId).ToList(), products.ToList())
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

            var viewModel = new Orders
            {
                Stores = SRepo.GetStores().ToList(),
                Customers = CRepo.GetCustomers().ToList(),
                Products = products,
                OrderLists = orderLists
            };
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

    }
}