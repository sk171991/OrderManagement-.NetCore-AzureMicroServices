using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class OrderMenus
    {
        public int? MenuId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
