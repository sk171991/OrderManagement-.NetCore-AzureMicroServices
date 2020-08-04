using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using NUnit.Framework;

namespace MT.OnlineRestaurant.UT.BusinessLayer
{
    [TestFixture]
    public class PaymentActionsTests
    {
        [Test]
        public void Test_Payment()
        {
            PaymentEntity orderPaymentDetails = new PaymentEntity()
            {
                OrderId = 1,
                PaymentTypeId = 1,
                Remarks = "test",
                CustomerId = 1
            };

            var mockOrder = new Mock<IPaymentDbAccess>();            
            mockOrder.Setup(x => x.MakePaymentForOrder(It.IsAny<TblOrderPayment>())).Returns(1);
            var orderFoodActionObject = new PaymentActions(mockOrder.Object);
            var data = orderFoodActionObject.MakePaymentForOrder(orderPaymentDetails);

            Assert.AreEqual(1, data);
        }

        [Test]
        public void Test_Update_Payment_And_Order_Status()
        {
            UpdatePaymentEntity orderPaymentDetails = new UpdatePaymentEntity()
            {
                PaymentId = 1,
                TransactionReferenceNo = "xxxswrw1314",
                PaymentStatusId = 1
            };

            var mockOrder = new Mock<IPaymentDbAccess>();
            mockOrder.Setup(x => x.UpdatePaymentAndOrderStatus(It.IsAny<TblOrderPayment>())).Returns(1);
            var orderFoodActionObject = new PaymentActions(mockOrder.Object);
            var data = orderFoodActionObject.UpdatePaymentAndOrderStatus(orderPaymentDetails);

            Assert.AreEqual(1, data);
        }
    }
}
