using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.WebApp.Models
{
    public class Product
    {
        [Display(Name ="ID")]
        public int ItemId { get; set; }
        [Display(Name ="Name")]
        public string ItemName { get; set; }
        [Display(Name ="Price")]
        public decimal ItemPrice { get; set; }
        [Display(Name ="Description")]
        public string ItemDescription { get; set; }
    }
}
