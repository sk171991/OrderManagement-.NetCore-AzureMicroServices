using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblOrderStatus
    {
        public TblOrderStatus()
        {
            TblFoodOrder = new HashSet<TblFoodOrder>();
            TblTableOrder = new HashSet<TblTableOrder>();
        }

        public string Status { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual ICollection<TblFoodOrder> TblFoodOrder { get; set; }
        public virtual ICollection<TblTableOrder> TblTableOrder { get; set; }
    }
}
