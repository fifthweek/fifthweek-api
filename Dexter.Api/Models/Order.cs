namespace Dexter.Api.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string ShipperCity { get; set; }

        public bool IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            var orderList = new List<Order>
            {
                new Order
                    {
                        OrderId = 10248,
                        CustomerName = "Taiseer Joudeh",
                        ShipperCity = "Amman",
                        IsShipped = true
                    },
                new Order
                    {
                        OrderId = 10249,
                        CustomerName = "Ahmad Hasan",
                        ShipperCity = "Dubai",
                        IsShipped = false
                    },
                new Order
                    {
                        OrderId = 10250,
                        CustomerName = "Tamer Yaser",
                        ShipperCity = "Jeddah",
                        IsShipped = false
                    },
                new Order
                    {
                        OrderId = 10251,
                        CustomerName = "Lina Majed",
                        ShipperCity = "Abu Dhabi",
                        IsShipped = false
                    },
                new Order
                    {
                        OrderId = 10252,
                        CustomerName = "Yasmeen Rami",
                        ShipperCity = "Kuwait",
                        IsShipped = true
                    }
            };

            return orderList;
        }
    }
}