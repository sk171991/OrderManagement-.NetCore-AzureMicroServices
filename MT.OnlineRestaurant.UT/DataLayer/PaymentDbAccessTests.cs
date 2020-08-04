using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using NUnit.Framework;

namespace MT.OnlineRestaurant.UT.DataLayer
{
    [TestFixture]
    public class PaymentDbAccessTests
    {
        [Test]
        public void Test_Make_Payment_For_Order()
        {
            TblOrderPayment OrderedFoodDetails = new TblOrderPayment()
            {
                TblPaymentTypeId = 1,
                Remarks = "test",
                TransactionId = "qwerty12345",
                TblPaymentStatusId = 3
            };

            var options = new DbContextOptionsBuilder<OrderManagementContext>()
            .UseInMemoryDatabase(databaseName: "OrderManagement")
            .Options;

            PaymentDbAccess placeOrderDbAccess = new PaymentDbAccess(new OrderManagementContext(options));
            int OrderId = placeOrderDbAccess.MakePaymentForOrder(OrderedFoodDetails);

            Assert.Greater(OrderId, 0);
        }

        //[Test]
        public void Test_Update_Payment_And_Order_Status()
        {
            TblOrderPayment OrderedFoodDetails = new TblOrderPayment()
            {
                Id = 1,
                TransactionId = "qwerty12345",
                TblPaymentStatusId = 3
            };

            var options = new DbContextOptionsBuilder<OrderManagementContext>()
            .UseInMemoryDatabase(databaseName: "OrderManagement")
            .Options;

            PaymentDbAccess placeOrderDbAccess = new PaymentDbAccess(new OrderManagementContext(options));
            int OrderId = placeOrderDbAccess.UpdatePaymentAndOrderStatus(OrderedFoodDetails);

            Assert.Greater(OrderId, 0);
        }
    }
}
