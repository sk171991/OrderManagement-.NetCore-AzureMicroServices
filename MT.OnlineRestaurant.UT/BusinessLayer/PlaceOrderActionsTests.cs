using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.Enums;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MT.OnlineRestaurant.UT.BusinessLayer
{
    [TestFixture]
    public class PlaceOrderActionsTests
    {
        [Test]
        public void Test_Food_Order()
        {
            OrderEntity orderEntity = new OrderEntity()
            {
                RestaurantId = 1,
                CustomerId = 1,
                OrderMenuDetails = new List<OrderMenus>()
                {
                    new OrderMenus()
                    {
                        MenuId = 1,
                        Price = 100
                    },
                    new OrderMenus()
                    {
                        MenuId = 2,
                        Price = 200
                    }
                },
                DeliveryAddress = "test address"
            };

            var mockOrder = new Mock<IPlaceOrderDbAccess>();

            //Configure mapping just for this test
            var config = new MapperConfiguration(cfg =>
            {
                // Add as many of these lines as you need to map your objects
                cfg.CreateMap<OrderEntity, TblFoodOrder>()
                .ForMember(dest => dest.TblRestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.TblCustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TblOrderStatusId, opt => opt.MapFrom(src => (int)Status.Initiated))
                .ForMember(dest => dest.TblPaymentTypeId, opt => opt.MapFrom(src => (int)PaymentType.NoPayment))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderMenuDetails.Sum(m => m.Price)))
                .ForMember(dest => dest.RecordTimeStamp, opt => opt.MapFrom(src => DateTime.Now));
            });

            var mockIMapper = config.CreateMapper();
            mockOrder.Setup(x => x.PlaceOrder(It.IsAny<TblFoodOrder>())).Returns(1);

            var someOptions = Options.Create(new ConnectionStrings());

            var orderFoodActionObject = new PlaceOrderActions(mockOrder.Object, mockIMapper, someOptions);
            var data = orderFoodActionObject.PlaceOrder(orderEntity);

            Assert.AreEqual(1, data);
        }

        [Test]
        public void Test_Cancel_Food_Order()
        {
            var mockOrder = new Mock<IPlaceOrderDbAccess>();
            var orderFoodActionObject = new PlaceOrderActions(mockOrder.Object);
            var data = orderFoodActionObject.CancelOrder(0);

            Assert.AreEqual(0, data);
        }

        [Test]
        public void Test_Cancel_InValid_Food_Order()
        {
            var mockOrder = new Mock<IPlaceOrderDbAccess>();
            mockOrder.Setup(x => x.CancelOrder(It.IsAny<int>())).Returns(1);
            var orderFoodActionObject = new PlaceOrderActions(mockOrder.Object);
            var data = orderFoodActionObject.CancelOrder(1);

            Assert.AreEqual(1, data);
        }
    }
}
