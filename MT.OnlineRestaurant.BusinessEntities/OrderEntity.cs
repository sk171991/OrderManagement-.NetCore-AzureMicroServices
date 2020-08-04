using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class OrderEntity
    {
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderMenus> OrderMenuDetails { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
