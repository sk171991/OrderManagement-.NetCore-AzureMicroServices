using Microsoft.AspNetCore.Mvc;
using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.UT.Controller
{
    [TestFixture]
    public class PaymentTests
    {
        [Test]
        public void Test_Valid_Make_Payment()
        {
            PaymentEntity paymentEntity = new PaymentEntity()
            {
                OrderId = 1,
                PaymentTypeId = 1,
                Remarks = "test",
                CustomerId = 1
            };

            var mockOrder = new Mock<IPaymentActions>();
            mockOrder.Setup(x => x.MakePaymentForOrder(It.IsAny<PaymentEntity>())).Returns(1);
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.MakePayment(paymentEntity);

            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public void Test_Valid_Make_Payment_With_Exception_Handling()
        {
            PaymentEntity paymentEntity = new PaymentEntity()
            {
                OrderId = 1,
                PaymentTypeId = 1,
                Remarks = "test",
                CustomerId = 1
            };

            var mockOrder = new Mock<IPaymentActions>();
            mockOrder.Setup(x => x.MakePaymentForOrder(It.IsAny<PaymentEntity>())).Returns(0);
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.MakePayment(paymentEntity);

            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }

        [Test]
        public void Test_InValid_Make_Payment()
        {
            PaymentEntity paymentEntity = new PaymentEntity()
            {
                OrderId = 0,
                PaymentTypeId = 1,
                Remarks = "test",
                CustomerId = 1
            };

            var mockOrder = new Mock<IPaymentActions>();
            mockOrder.Setup(x => x.MakePaymentForOrder(It.IsAny<PaymentEntity>())).Returns(1);
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.MakePayment(paymentEntity);

            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }

        [Test]
        public void Test_Valid_Update_Payment_And_Order_Status()
        {
            UpdatePaymentEntity paymentEntity = new UpdatePaymentEntity()
            {
                PaymentId = 1,
                TransactionReferenceNo = "14325sf",
                PaymentStatusId = 1
            };

            var mockOrder = new Mock<IPaymentActions>();
            mockOrder.Setup(x => x.UpdatePaymentAndOrderStatus(It.IsAny<UpdatePaymentEntity>())).Returns(1);
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.UpdatePaymentAndOrderStatus(paymentEntity);

            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public void Test_InValid_Update_Payment_And_Order_Status()
        {
            UpdatePaymentEntity paymentEntity = new UpdatePaymentEntity()
            {
                PaymentId = 0,
                TransactionReferenceNo = "",
                PaymentStatusId = 0
            };

            var mockOrder = new Mock<IPaymentActions>();
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.UpdatePaymentAndOrderStatus(paymentEntity);

            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }

        [Test]
        public void Test_Valid_Update_Payment_And_Order_Status_With_Exception_Handling()
        {
            UpdatePaymentEntity paymentEntity = new UpdatePaymentEntity()
            {
                PaymentId = 1,
                TransactionReferenceNo = "qwerty12345",
                PaymentStatusId = 1
            };

            var mockOrder = new Mock<IPaymentActions>();
            mockOrder.Setup(x => x.UpdatePaymentAndOrderStatus(It.IsAny<UpdatePaymentEntity>())).Returns(0);
            var orderFoodActionObject = new PaymentController(mockOrder.Object);
            var data = orderFoodActionObject.UpdatePaymentAndOrderStatus(paymentEntity);            
            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }
    }
}
