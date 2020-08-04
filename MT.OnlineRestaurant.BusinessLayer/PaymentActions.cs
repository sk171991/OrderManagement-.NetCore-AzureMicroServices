using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.Enums;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class PaymentActions : IPaymentActions
    {
        private readonly IPaymentDbAccess _paymentDbAccess;

        public PaymentActions(IPaymentDbAccess paymentDbAccess)
        {
            _paymentDbAccess = paymentDbAccess;
        }

        public int MakePaymentForOrder(PaymentEntity orderPaymentDetails)
        {
            return _paymentDbAccess.MakePaymentForOrder(new DataLayer.Context.TblOrderPayment()
            {
                TblFoodOrderId = orderPaymentDetails.OrderId,
                TblPaymentTypeId = orderPaymentDetails.PaymentTypeId,
                Remarks = orderPaymentDetails.Remarks,
                TblCustomerId = orderPaymentDetails.CustomerId,
                TblPaymentStatusId = (int)Status.Initiated,
                RecordTimeStampCreated = DateTime.Now
            });
        }

        public int UpdatePaymentAndOrderStatus(UpdatePaymentEntity orderPaymentDetails)
        {
            return _paymentDbAccess.UpdatePaymentAndOrderStatus(new DataLayer.Context.TblOrderPayment()
            {
                Id = orderPaymentDetails.PaymentId,
                TransactionId = orderPaymentDetails.TransactionReferenceNo,
                TblPaymentStatusId = orderPaymentDetails.PaymentStatusId
            });
        }
    }
}
