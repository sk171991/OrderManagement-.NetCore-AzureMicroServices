using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IPlaceOrderDbAccess
    {
        int PlaceOrder(TblFoodOrder foodOrderDetails);

        int CancelOrder(int orderId);

        int AddtoCart(TblCartItems cartItems);

        IQueryable<TblCartItems> GetCartItems(int CustomerID);

        int FoodOrderMapping(IList<TblFoodOrderMapping> tblFoodOrderMappings);

        string CartPriceChange(int restaurantID, int? MenuID, decimal? price);

        string CartUpdateOnItemAvailable(int restaurantID, int? MenuID);

        bool IsOrderItemInStock(int restaurantID, int? MenuID);
        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IQueryable<TblFoodOrder> GetReports(int customerId);
    }
}
