using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class PlaceOrderDbAccess : IPlaceOrderDbAccess
    {
        private readonly OrderManagementContext _context;

        public PlaceOrderDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int PlaceOrder(TblFoodOrder OrderedFoodDetails)
        {
            _context.TblFoodOrder.Add(OrderedFoodDetails);
            _context.SaveChanges();
            return OrderedFoodDetails.Id;
        }

        public int FoodOrderMapping(IList<TblFoodOrderMapping> tblFoodOrderMappings)
        {
            foreach (var items in tblFoodOrderMappings)
            {
                _context.TblFoodOrderMapping.Add(new TblFoodOrderMapping()
                { 
                       TblFoodOrderId = items.TblFoodOrderId,
                        TblMenuId = items.TblMenuId,
                        Price = items.Price,
                        UserCreated = 0,
                        RecordTimeStampCreated = DateTime.Now
                });
            }
            int retVal = _context.SaveChanges();
            return retVal;
        }

        public int CancelOrder(int orderId)
        {
            var orderedFood = _context.TblFoodOrder.Include(p => p.TblFoodOrderMapping)
                .SingleOrDefault(p => p.Id == orderId);

            orderedFood.TblFoodOrderMapping.ToList().ForEach(p => _context.TblFoodOrderMapping.Remove(p));
            _context.TblFoodOrder.Remove(orderedFood);
            _context.SaveChanges();
            
            return (orderedFood != null ? orderedFood.Id : 0);
        }

        public int AddtoCart(TblCartItems cartItems)
        {
            int validateCart = ValidateCartItems(cartItems.TblRestaurantId, cartItems.TblMenuId, cartItems.TblCustomerId);
            if (validateCart == 0)
            {
                _context.TblCartItems.Add(cartItems);
                _context.SaveChanges();
                return cartItems.Id;
            }
            return 0;
        }

        private int ValidateCartItems(int? RestaurantID , int? MenuID , int? CustomerID)
        {
            var cartItems = from cartdetails in _context.TblCartItems
                            where cartdetails.TblCustomerId == CustomerID &&
                            cartdetails.TblRestaurantId == RestaurantID &&
                            cartdetails.TblMenuId == MenuID
                            select cartdetails;
            int cartId = 0;
            if(cartItems != null)
            {
              foreach(var items in cartItems)
                {
                    cartId = items.Id;
                }
                return cartId;
            }
            return cartId;
        }
        public IQueryable<TblCartItems> GetCartItems(int CustomerID)
        {
            List<TblCartItems> tblCartItems = new List<TblCartItems>();
            var cartItems = from cartdetails in _context.TblCartItems
                            where cartdetails.TblCustomerId == CustomerID
                            select new TblCartItems
                            {
                                TblCustomerId = cartdetails.TblCustomerId,
                                TblRestaurantId = cartdetails.TblRestaurantId,
                                TblMenuId = cartdetails.TblMenuId,
                                TotalPrice = cartdetails.TotalPrice,
                                IsItemAvailable = cartdetails.IsItemAvailable,
                                DeliveryAddress = cartdetails.DeliveryAddress,
                                Id = cartdetails.Id
                            };

            tblCartItems = cartItems.ToList();
            return tblCartItems.AsQueryable();
        }

        public string CartPriceChange(int restaurantID, int? MenuID, decimal? Price)
        {
            try
            {
                var result = from cart in _context.TblCartItems
                             where cart.TblRestaurantId == restaurantID && cart.TblMenuId == MenuID
                             && cart.IsItemAvailable == true
                             select cart;

                foreach (var items in result)
                {
                    items.TotalPrice = Price;
                    items.RecordTimeStamp = DateTime.Now;
                }
                _context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CartUpdateOnItemAvailable(int restaurantID, int? MenuID)
        {
            try
            {
                var result = from cart in _context.TblCartItems
                             where cart.TblRestaurantId == restaurantID && cart.TblMenuId == MenuID
                             select cart;

                foreach (var items in result)
                {
                    items.IsItemAvailable = false;
                }

                TblItemOutOfStock tblItemOutOfStock = new TblItemOutOfStock
                {
                    TblMenuId = MenuID,
                    TblRestaurantId = restaurantID,
                    UserCreated = 1,
                    UserModified = 1,
                    RecordTimeStamp = DateTime.Now,
                    RecordTimeStampCreated = DateTime.Now,
                    IsItemAvailable = true
                
                };
                _context.TblItemOutOfStocks.Add(tblItemOutOfStock);
                _context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<TblFoodOrder> GetReports(int customerId)
        {
            return _context.TblFoodOrder.Where(fo => fo.TblCustomerId == customerId);
        }

        public bool IsOrderItemInStock(int restaurantID, int? MenuID)
        {
            try
            {
                var result = from items in _context.TblItemOutOfStocks
                             where items.TblRestaurantId == restaurantID && items.TblMenuId == MenuID
                             select items;

                if(result.Count() != 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
