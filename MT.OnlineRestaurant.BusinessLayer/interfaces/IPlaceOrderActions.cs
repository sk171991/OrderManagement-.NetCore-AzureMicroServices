using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    public interface IPlaceOrderActions
    {
        int PlaceOrder(OrderEntity orderEntity);
        int CancelOrder(int orderId);
        int AddtoCartItems(OrderEntity orderEntity);
        string CartPriceChange(int restaurantID, int? MenuID, decimal? price);
        string CartUpdateOnItemAvailable(int restaurantID, int? MenuID);
        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IQueryable<CustomerOrderReport> GetReports(int customerId);
        IQueryable<CartItemsEntity> GetCartItems(int CustomerID);

        Task<bool> IsValidRestaurantAsync(OrderEntity orderEntity, int UserId, string UserToken);
        bool IsOrderItemInStock(OrderEntity orderEntity);
        Task<bool> IsValidCustomer(int UserId, string UserToken, int CustomerID);
    }
}
