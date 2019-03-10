﻿using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothingStore.Context
{
    public class ProductRepo : IProductsRepo
    {
        private readonly Project0Context _db;

        public ProductRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Products> GetProducts()
        {
            return Mapper.Map(_db.ItemProducts);
        }

        public Products GetProductsById(int productId)
        {
            IEnumerable<Products> productCollection = Mapper.Map(_db.ItemProducts);
            return productCollection.Where(p => p.ItemId == productId).First();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
