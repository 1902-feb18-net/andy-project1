using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.WebApp.Controllers;
using ClothingStore.WebApp.Models;
using ClothingStore.Lib;
using System.Collections.Generic;
using Moq;

namespace ClothingStore.Testing
{
    public class CustomerTest
    {
        [Fact]
        public void Test1()
        {

        }

        public void GetCreateReturnsCustomerView()
        {
            // arrange
            var customers = new List<Lib.Customer> { new Lib.Customer { Id = 1, DefaultStoreId = 1, FirstName = "Bob", LastName = "Lee" } };
            var mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(c => c.GetCustomers()).Returns(customers);
            //var sut = new CustomersController(mockRepo.Object);
        }
    }
}
