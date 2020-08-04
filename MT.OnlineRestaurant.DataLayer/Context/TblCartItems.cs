using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public class TblCartItems
    {
        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public int? TblMenuId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
        [Column(TypeName = "bit")]
        public bool IsItemAvailable { get; set; }
        public int Quantity { get; set; }
    }
}
