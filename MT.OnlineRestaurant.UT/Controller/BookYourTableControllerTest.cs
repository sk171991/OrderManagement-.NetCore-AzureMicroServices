using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.UT.Controller
{
    [TestFixture]
    public class BookYourTableControllerTest
    {
        [Test]
        public async Task Book_Your_Table_POST_true()
        {
            //Arrange
            BookTable bookYourTable = new BookTable() {
                BookingDate = DateTime.Now,
                CustomerId=1,
                RestaurantId=1,
                capacityCount=2,
                Id=1

             } ;
            //Act
            var mockOrder = new Mock<IBookYourTableBusiness>();
            mockOrder.Setup(x => x.BookYourTable(bookYourTable)).Returns(true);
            var tableBookingController = new TableBookingController(mockOrder.Object);
            tableBookingController.ControllerContext = new ControllerContext();
            tableBookingController.ControllerContext.HttpContext = new DefaultHttpContext();
            tableBookingController.ControllerContext.HttpContext.Request.Headers["CustomerId"] = "1";
            var data = await tableBookingController.Post(bookYourTable);

            //Assert
            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public async Task Book_Your_Table_POST_false()
        {
            //Arrange
            BookTable bookYourTable = new BookTable()
            {
                BookingDate = DateTime.Now,
                CustomerId = 1,
                RestaurantId = 1,
                capacityCount = 2,
                Id = 1

            };

            //Act
            var mockOrder = new Mock<IBookYourTableBusiness>();
            mockOrder.Setup(x => x.BookYourTable(bookYourTable)).Returns(false);
            var tableBookingController = new TableBookingController(mockOrder.Object);
            tableBookingController.ControllerContext = new ControllerContext();
            tableBookingController.ControllerContext.HttpContext = new DefaultHttpContext();
            tableBookingController.ControllerContext.HttpContext.Request.Headers["CustomerId"] = "1";
            var data = await tableBookingController.Put(bookYourTable);

            //Assert
            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }
    }
}
