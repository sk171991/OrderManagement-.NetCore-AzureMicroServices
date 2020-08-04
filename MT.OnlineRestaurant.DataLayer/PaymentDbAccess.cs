using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System.Data.SqlClient;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class PaymentDbAccess : IPaymentDbAccess
    {
        private readonly OrderManagementContext _context;

        public PaymentDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int MakePaymentForOrder(TblOrderPayment orderPaymentDetails)
        {
            _context.TblOrderPayment.Add(orderPaymentDetails);
            _context.SaveChanges();
            return orderPaymentDetails.Id;
        }

        public int UpdatePaymentAndOrderStatus(TblOrderPayment orderPaymentDetails)
        {
            var ID = new SqlParameter
            {
                ParameterName = "@ID",
                DbType = System.Data.DbType.Int32,
                Value = orderPaymentDetails.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            var TransactionID = new SqlParameter
            {
                ParameterName = "@TransactionID",
                DbType = System.Data.DbType.String,
                Size = 20,
                Value = orderPaymentDetails.TransactionId,
                Direction = System.Data.ParameterDirection.Input
            };
            var tblPaymentStatusID = new SqlParameter
            {
                ParameterName = "@tblPaymentStatusID",
                DbType = System.Data.DbType.Int32,
                Value = orderPaymentDetails.TblPaymentStatusId,
                Direction = System.Data.ParameterDirection.Input
            };
            var ReturnValue = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("exec UpdatePaymentStatus @ID, @TransactionID, ");
            stringBuilder.Append("@tblPaymentStatusID, @ReturnValue OUT");

            _context.Database.ExecuteSqlCommand(stringBuilder.ToString(), 
                ID,
                TransactionID,
                tblPaymentStatusID,
                ReturnValue);

            _context.SaveChanges();
            return (int)ReturnValue.Value;
        }
    }
}
