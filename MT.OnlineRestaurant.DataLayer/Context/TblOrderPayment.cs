using System;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblOrderPayment
    {
        public int TblFoodOrderId { get; set; }
        public int TblPaymentTypeId { get; set; }
        public string Remarks { get; set; }
        public string TransactionId { get; set; }
        public int TblCustomerId { get; set; }
        public int TblPaymentStatusId { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual TblFoodOrder TblFoodOrder { get; set; }
        public virtual TblPaymentStatus TblPaymentStatus { get; set; }
        public virtual TblPaymentType TblPaymentType { get; set; }
    }
}
