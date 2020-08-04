using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class CustomerOrderReport
    {
        public int OrderId;
        public string OrderStatus { get; set; }
        public DateTime OrderedDate { get; set; }
        public string PaymentStatus { get; set; }
        public decimal price { get; set; }
    }
}
