using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblPaymentType
    {
        public TblPaymentType()
        {
            TblFoodOrder = new HashSet<TblFoodOrder>();
            TblOrderPayment = new HashSet<TblOrderPayment>();
            TblTableOrder = new HashSet<TblTableOrder>();
        }

        public string Type { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual ICollection<TblFoodOrder> TblFoodOrder { get; set; }
        public virtual ICollection<TblOrderPayment> TblOrderPayment { get; set; }
        public virtual ICollection<TblTableOrder> TblTableOrder { get; set; }
    }
}
