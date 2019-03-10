using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.WebApp.Models
{
    public class Customer
    {
        [Display(Name = "ID")]
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Display(Name = "Default Store")]
        [Range(1,3, ErrorMessage = "1 for MallWart, 2 for CJNickel, 3 for Apple Reublic")]
        public int DefaultStoreId { get; set; }

        [Display(Name = "Store Location")]
        public string DefaultStoreName { get; set; }

        public List<Lib.Store> Stores { get; set; }

    }
}
