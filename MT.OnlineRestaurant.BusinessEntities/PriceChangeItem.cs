using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class PriceChangeItem
    {
        public int RestaurantId { get; set; }
        public int? MenuId { get; set; }
        public decimal? Price { get; set; }
        public int UserID { get; set; }
        public string UserToken { get; set; }

    }
}
