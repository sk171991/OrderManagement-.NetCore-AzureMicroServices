#region References
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

#region Namespace
namespace MT.OnlineRestaurant.DataLayer
{
    #region Class Definition
    /// <summary>
    /// Implements the interface methods for booking a table for a repository into the database
    /// </summary>
    public class BookYourTableRepository : IBookYourTableRepository
    {
        #region Private Variables
        private readonly OrderManagementContext _orderManagementContext;
        private readonly IOptions<ConnectionStrings> _connectionStrings;
        #endregion

        #region Constructor
        public BookYourTableRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings;
            _orderManagementContext = new OrderManagementContext(_connectionStrings.Value.DatabaseConnectionString);
        }
        #endregion

        #region Interface Methods
        public bool BookYourTable(TblTableOrder tblTableOrder)
        {
            int? paymentId = null;
            if (tblTableOrder != null)
            {
                tblTableOrder.TblOrderStatusId = 1;
                tblTableOrder.TblPaymentTypeId = 4;
                tblTableOrder.UserCreated = tblTableOrder.TblCustomerId.HasValue ? tblTableOrder.TblCustomerId.Value : 1;
                tblTableOrder.UserModified = tblTableOrder.TblCustomerId.HasValue ? tblTableOrder.TblCustomerId.Value : 1;
                tblTableOrder.ToDate = tblTableOrder.FromDate;

                _orderManagementContext.Set<TblTableOrder>().Add(tblTableOrder);
                _orderManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// verifies whether the table is available for booking or not
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        public bool CheckTableAvailability(TblTableOrder tblTableOrder)
        {
            if (_orderManagementContext.Set<TblTableOrder>().Any(tto => tto.TblRestaurantId == tblTableOrder.TblRestaurantId
                                                                 && tto.TblCustomerId == tblTableOrder.TblCustomerId
                                                                 && tto.FromDate == tblTableOrder.FromDate))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the booking information
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        public bool UpdateBooking(TblTableOrder tblTableOrder)
        {
            TblTableOrder tblTableOrderEntity = _orderManagementContext.Set<TblTableOrder>().Where(tto => tto.Id == tblTableOrder.Id).FirstOrDefault();

            if(tblTableOrderEntity != null)
            {
                tblTableOrderEntity = tblTableOrder;
                tblTableOrder.UserCreated = tblTableOrder.TblCustomerId.HasValue ? tblTableOrder.TblCustomerId.Value : 1;
                tblTableOrder.UserModified = tblTableOrder.TblCustomerId.HasValue ? tblTableOrder.TblCustomerId.Value : 1;                
                tblTableOrderEntity.RecordTimeStamp = DateTime.Now;

                _orderManagementContext.SaveChanges();

                return true;
            }

            return false;

        }
        #endregion
    }
    #endregion
}
#endregion
