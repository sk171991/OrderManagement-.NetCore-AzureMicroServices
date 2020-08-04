using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class CartItemsEntity
    {
      
        // Item availability
        public bool IsItemAvailable { get; set; }
        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public int? TblMenuId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public int CartId { get; set; }
    }
}
