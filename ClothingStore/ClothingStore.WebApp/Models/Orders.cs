using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClothingStore.Lib;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothingStore.WebApp.Models
{
    public class Orders
    {
        [Display(Name ="Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Store ID")]
        public int StoreId { get; set; }

        [Display(Name ="Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name ="Date Purchased")]
        public DateTime DatePurchased { get; set; }

        [Display(Name = "Total Costs")]
        [Range(double.Epsilon, double.MaxValue)]
        public decimal? Total { get; set; }

        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public List<Store> Stores { get; internal set; }
        public List<Lib.Customer> Customers { get; internal set; }
        public List<Products> Products { get; internal set; }
        public List<OrderList> OrderLists { get; internal set; }
        public List<SelectListItem> customerItems { get; internal set; }

        //public virtual Customer Customer { get; set; }
        //public virtual Location Store { get; set; }
        //public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
