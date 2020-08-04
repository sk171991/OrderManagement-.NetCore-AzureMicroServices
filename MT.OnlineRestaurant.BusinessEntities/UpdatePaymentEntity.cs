using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class UpdatePaymentEntity
    {
        public int PaymentId { get; set; }
        public string TransactionReferenceNo { get; set; }
        public int PaymentStatusId { get; set; }
    }
}
