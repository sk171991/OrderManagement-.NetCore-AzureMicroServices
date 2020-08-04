using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.UT.DataLayer
{
    [TestFixture]
    public class PlaceOrderDbAccessTests
    {
        [Test]
        public void Test_Place_Order()
        {
            TblFoodOrder OrderedFoodDetails = new TblFoodOrder()
            {
                TblRestaurantId = 10,
                TblCustomerId = 10,
                DeliveryAddress = "test address",
                TblFoodOrderMapping = new List<TblFoodOrderMapping>()
                {
                    new TblFoodOrderMapping()
                    {
                        TblMenuId = 1,
                        Price = 10000
                    }
                }
            };

            var options = new DbContextOptionsBuilder<OrderManagementContext>()
            .UseInMemoryDatabase(databaseName: "OrderManagement")
            .Options;

            PlaceOrderDbAccess placeOrderDbAccess = new PlaceOrderDbAccess(new OrderManagementContext(options));
            int OrderId = placeOrderDbAccess.PlaceOrder(OrderedFoodDetails);
            
            Assert.Greater(OrderId, 0);
        }
    }
}
