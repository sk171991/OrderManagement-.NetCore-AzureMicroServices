using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
   public class ServiceBusMessage
    {
        public int RestaurantId { get; set; }
        public int? MenuId { get; set; }
        public string Message { get; set; }
        public int? ItemQuantity { get; set; }
        public int UserID { get; set; }
        public string UserToken { get; set; }
    }
}
