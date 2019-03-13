using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public interface IOrdersRepo
    {
        IEnumerable<Products> GetProductsOfOrders(int OrderId);
        IEnumerable<Order> GetOrders();
        //IEnumerable<Order> DisplayOrderHistoryStore(int store);
        //IEnumerable<Order> DisplayOrderHistoryCustomer(int customer);
        //IEnumerable<Order> DisplayOrderHistory(string sortOrder);
        IEnumerable<Order> GetOrderById(int orderId);
        IEnumerable<OrderList> GetOrderLists(int orderId);
        void InsertOrder(Order order);
        void InsertOrderlist(OrderList orderlist);
        //void DeleteOrder(int orderId);
        void UpdateOrder(Order order);
        //void DisplayOrderDetails(int orderId);
        void Save();
        Order GetOrderByOrderId(int orderId);
        int LastId();
        IEnumerable<OrderList> GetOrderListById(int id);
    }
}
