using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblTableOrder
    {
        public TblTableOrder()
        {
            TblTableOrderMapping = new HashSet<TblTableOrderMapping>();
        }

        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? TblOrderStatusId { get; set; }
        public int? TblPaymentTypeId { get; set; }
        public decimal? Price { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual TblOrderStatus TblOrderStatus { get; set; }
        public virtual TblPaymentType TblPaymentType { get; set; }
        public virtual ICollection<TblTableOrderMapping> TblTableOrderMapping { get; set; }
    }
}
