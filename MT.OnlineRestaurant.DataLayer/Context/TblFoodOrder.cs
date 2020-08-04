using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblFoodOrder
    {
        public TblFoodOrder()
        {
            TblFoodOrderMapping = new HashSet<TblFoodOrderMapping>();
            TblOrderPayment = new HashSet<TblOrderPayment>();
        }

        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public int? TblOrderStatusId { get; set; }
        public int? TblPaymentTypeId { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual TblOrderStatus TblOrderStatus { get; set; }
        public virtual TblPaymentType TblPaymentType { get; set; }
        public virtual ICollection<TblFoodOrderMapping> TblFoodOrderMapping { get; set; }
        public virtual ICollection<TblOrderPayment> TblOrderPayment { get; set; }
    }
}
