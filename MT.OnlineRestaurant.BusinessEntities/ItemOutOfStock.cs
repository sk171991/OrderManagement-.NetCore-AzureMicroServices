using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class ItemOutOfStock
    {
        public int RestaurantId { get; set; }
        public int? MenuId { get; set; }
        public int UserID { get; set; }
        public string UserToken { get; set; }
    }
}
