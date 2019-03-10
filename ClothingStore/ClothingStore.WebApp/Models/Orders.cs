using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public decimal? Total { get; set; }

        //public virtual Customer Customer { get; set; }
        //public virtual Location Store { get; set; }
        //public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
