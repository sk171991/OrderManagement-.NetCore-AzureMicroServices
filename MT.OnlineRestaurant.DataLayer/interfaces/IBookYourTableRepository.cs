#region References
using System;
using System.Collections.Generic;
using System.Text;
using MT.OnlineRestaurant.DataLayer.Context;
#endregion

#region namespaces
namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    #region Interface Definition
    /// <summary>
    /// Defines data actions for booking the table
    /// </summary>
    public interface IBookYourTableRepository
    {
        bool BookYourTable(TblTableOrder tblTableOrder);

        /// <summary>
        /// verifies whether the table is available for booking or not
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        bool CheckTableAvailability(TblTableOrder tblTableOrder);

        /// <summary>
        /// Updates the booking information
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        bool UpdateBooking(TblTableOrder tblTableOrder);
    }
    #endregion
}
#endregion
