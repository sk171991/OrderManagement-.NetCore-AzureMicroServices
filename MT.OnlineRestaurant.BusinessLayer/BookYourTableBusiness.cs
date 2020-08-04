#region References
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
#endregion

#region namespace
namespace MT.OnlineRestaurant.BusinessLayer
{
    #region Class Definition
    /// <summary>
    /// Implements the interface methods for booking the table for a restaurant
    /// </summary>
    public class BookYourTableBusiness : IBookYourTableBusiness
    {
        #region privateVariables
        private readonly IBookYourTableRepository _bookYourTableRepository;
        #endregion

        public BookYourTableBusiness(IBookYourTableRepository bookYourTableRepository)
        {
            _bookYourTableRepository = bookYourTableRepository;
        }

        #region Interface Methods
        public bool BookYourTable(BookTable bookingTable)
        {
            TblTableOrder tblTableOrder;

            if (bookingTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookingTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookingTable.CustomerId;
                tblTableOrder.FromDate = bookingTable.BookingDate;
                tblTableOrder.RecordTimeStampCreated = DateTime.Now;
                return _bookYourTableRepository.BookYourTable(tblTableOrder);
            }

            return false;
        }

        /// <summary>
        /// verifies whether the table is available for booking or not
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        public bool CheckTableAvailability(BookTable bookingTable)
        {
            TblTableOrder tblTableOrder;

            if (bookingTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookingTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookingTable.CustomerId;
                tblTableOrder.FromDate = bookingTable.BookingDate;

                return _bookYourTableRepository.CheckTableAvailability(tblTableOrder);

            }

            return false;

        }

        /// <summary>
        /// Updates the existing booking info
        /// </summary>
        /// <param name="bookTable"></param>
        /// <returns></returns>
        public bool UpdateBooking(BookTable bookTable)
        {
            TblTableOrder tblTableOrder;

            if (bookTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookTable.CustomerId;
                tblTableOrder.FromDate = bookTable.BookingDate;
                tblTableOrder.RecordTimeStamp = DateTime.Now;
                return _bookYourTableRepository.BookYourTable(tblTableOrder);
            }

            return false;
        }
        #endregion
    }
    #endregion
}
#endregion
