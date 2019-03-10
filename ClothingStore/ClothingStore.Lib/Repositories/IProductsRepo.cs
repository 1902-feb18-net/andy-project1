using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public interface IProductsRepo
    {
        IEnumerable<Products> GetProducts();
        Products GetProductsById(int productId);
        void Save();
    }
}
