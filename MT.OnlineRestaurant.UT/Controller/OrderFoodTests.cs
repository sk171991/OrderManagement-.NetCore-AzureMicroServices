using Microsoft.AspNetCore.Mvc;
using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

//namespace MT.OnlineRestaurant.UT.Controller
//{
//    [TestFixture]
//    public class OrderFoodTests
//    {
//        [Test]
//        public async Task Test_Valid_Food_Order()
//        {
//            OrderEntity orderEntity = new OrderEntity()
//            {
//                RestaurantId = 1,
//                CustomerId = 1,
//                OrderMenuDetails = new List<OrderMenus>()
//                {
//                    new OrderMenus()
//                    {
//                        MenuId = 1,
//                        Price = 100
//                    },
//                    new OrderMenus()
//                    {
//                        MenuId = 2,
//                        Price = 200
//                    }
//                },
//                DeliveryAddress = "test address"
//            };

//            var mockOrder = new Mock<IPlaceOrderActions>();
//            mockOrder.Setup(x => x.PlaceOrder(orderEntity)).Returns(1);
//            var orderFoodControllerObject = new OrderFoodController(mockOrder.Object);
//            var data = await Task<OkObjectResult>.Run(() => orderFoodControllerObject.Post(orderEntity));

//            var okObjectResult = data as OkObjectResult;
//            Assert.AreEqual(200, okObjectResult.StatusCode);
//        }

//        [Test]
//        public async Task Test_InValid_Food_Order()
//        {
//            OrderEntity orderEntity = new OrderEntity()
//            {
//                RestaurantId = 0,
//                CustomerId = 1,
//                OrderMenuDetails = new List<OrderMenus>()
//                {
//                    new OrderMenus()
//                    {
//                        MenuId = 1,
//                        Price = 100
//                    },
//                    new OrderMenus()
//                    {
//                        MenuId = 2,
//                        Price = 200
//                    }
//                },
//                DeliveryAddress = "test address"
//            };

//            var mockOrder = new Mock<IPlaceOrderActions>();
//            var orderFoodControllerObject = new OrderFoodController(mockOrder.Object);
//            var data = await Task<BadRequestObjectResult>.Run(() => orderFoodControllerObject.Post(orderEntity));

//            var badRequestObjectResult = data as BadRequestObjectResult;
//            Assert.AreEqual(400, badRequestObjectResult.StatusCode);
//        }

//        [Test]
//        public async Task Test_Valid_Food_Order_With_Exception_Handling()
//        {
//            OrderEntity orderEntity = new OrderEntity()
//            {
//                RestaurantId = 1,
//                CustomerId = 1,
//                OrderMenuDetails = new List<OrderMenus>()
//                {
//                    new OrderMenus()
//                    {
//                        MenuId = 1,
//                        Price = 100
//                    },
//                    new OrderMenus()
//                    {
//                        MenuId = 2,
//                        Price = 200
//                    }
//                },
//                DeliveryAddress = "test address"
//            };

//            var mockOrder = new Mock<IPlaceOrderActions>();
//            mockOrder.Setup(x => x.PlaceOrder(orderEntity)).Returns(0);
//            var orderFoodControllerObject = new OrderFoodController(mockOrder.Object);
//            var data = await Task<BadRequestObjectResult>.Run(() => orderFoodControllerObject.Post(orderEntity));

//            var badRequestObjectResult = data as BadRequestObjectResult;
//            Assert.AreEqual(400, badRequestObjectResult.StatusCode);
//        }

//        [Test]
//        public void Test_Valid_Cancel_Order()
//        {
//            var mockOrder = new Mock<IPlaceOrderActions>();
//            mockOrder.Setup(x => x.CancelOrder(1)).Returns(1);
//            var orderFoodControllerObject = new OrderFoodController(mockOrder.Object);
//            var data = orderFoodControllerObject.Delete(1);

//            var okObjectResult = data as OkObjectResult;
//            Assert.AreEqual(200, okObjectResult.StatusCode);
//        }

//        [Test]
//        public void Test_InValid_Cancel_Order()
//        {
//            var mockOrder = new Mock<IPlaceOrderActions>();
//            mockOrder.Setup(x => x.CancelOrder(0)).Returns(0);
//            var orderFoodControllerObject = new OrderFoodController(mockOrder.Object);
//            var data = orderFoodControllerObject.Delete(0);

//            var okObjectResult = data as BadRequestObjectResult;
//            Assert.AreEqual(400, okObjectResult.StatusCode);
//        }
//    }
//}
