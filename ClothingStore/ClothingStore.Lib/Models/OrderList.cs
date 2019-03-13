using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public class OrderList
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ItemBought { get; set; }
        public int OrderListId { get; set; }
        public int Special { get; set; }
        public decimal Price { get; set; }
        public Lib.Products Product { get; set; }

        public decimal GetCostOfPurchase()
        {
            switch (Special)
            {
                case 1:
                    Price = ItemBought * Product.ItemPrice;
                    break;
                case 2:
                    Price = ItemBought * Product.ItemPrice + 10.00m;
                    break;
                case 3:
                    Price = Price = ItemBought * Product.ItemPrice + 20.00m;
                    break;
                default:
                    throw new ArgumentException("Special must be 1 - 3");
            }
            return Price;
        }
    }
}
