#region References
using MT.OnlineRestaurant.BusinessEntities;
#endregion

#region Namespace Definition
namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    #region Interface Definition
    public interface IBookYourTableBusiness
    {
        /// <summary>
        /// Defines for booking the table
        /// </summary>
        /// <param name="bookingTable"></param>
        /// <returns></returns>
        bool BookYourTable(BookTable bookingTable);

        /// <summary>
        /// verifies whether the table is available for booking or not
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        bool CheckTableAvailability(BookTable bookingTable);

        /// <summary>
        /// Updates the existing booking info
        /// </summary>
        /// <param name="bookTable"></param>
        /// <returns></returns>
        bool UpdateBooking(BookTable bookTable);
    }

    #endregion
}
#endregion

