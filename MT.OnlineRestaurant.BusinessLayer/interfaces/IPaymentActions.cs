using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    public interface IPaymentActions
    {
        int MakePaymentForOrder(PaymentEntity orderPaymentDetails);
        int UpdatePaymentAndOrderStatus(UpdatePaymentEntity orderPaymentDetails);
    }
}
