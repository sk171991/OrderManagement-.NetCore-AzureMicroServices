using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
   public class TblItemOutOfStock
    {
        public int TblRestaurantId { get; set; }
        public int? TblMenuId { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
        [Column(TypeName = "bit")]
        public bool IsItemAvailable { get; set; }
    }
}
