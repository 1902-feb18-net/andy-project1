using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.WebApp.Models
{
    public class Stores
    {
        [Display(Name ="ID")]
        public int LocationId { get; set; }
        [Required]
        public string StoreName { get; set; }

        public IEnumerable<Customer> Customer { get; set; }
    }
}
