using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class PaymentEntity
    {
        public int OrderId { get; set; }
        public int PaymentTypeId { get; set; }
        public string Remarks { get; set; }
        public int CustomerId { get; set; }
    }
}
