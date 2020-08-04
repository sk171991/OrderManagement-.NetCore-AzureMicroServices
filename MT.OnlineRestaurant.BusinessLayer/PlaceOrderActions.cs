using AutoMapper;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.ServiceModels;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.interfaces;
using MT.OnlineRestaurant.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class PlaceOrderActions : IPlaceOrderActions
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly IPlaceOrderDbAccess _placeOrderDbAccess;
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public PlaceOrderActions()
        {
            
        }

        public PlaceOrderActions(IPlaceOrderDbAccess placeOrderDbAccess)
        {
            _placeOrderDbAccess = placeOrderDbAccess;
        }

        public PlaceOrderActions(IPlaceOrderDbAccess placeOrderDbAccess, IMapper mapper, IOptions<ConnectionStrings> connectionStrings)
        {
            _placeOrderDbAccess = placeOrderDbAccess;
            _mapper = mapper;
            _connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Place order
        /// </summary>
        /// <param name="orderEntity">Order details</param>
        /// <returns>order id</returns>
        public int PlaceOrder(OrderEntity orderEntity)
        {
            DataLayer.Context.TblFoodOrder tblFoodOrder = _mapper.Map<DataLayer.Context.TblFoodOrder>(orderEntity);

            IList<DataLayer.Context.TblFoodOrderMapping> tblFoodOrderMappings = new List<DataLayer.Context.TblFoodOrderMapping>();
            try
            {
                
                int tblFoodOrderId = _placeOrderDbAccess.PlaceOrder(tblFoodOrder);
                foreach (OrderMenus orderMenu in orderEntity.OrderMenuDetails)
                {
                    tblFoodOrderMappings.Add(new DataLayer.Context.TblFoodOrderMapping()
                    {
                        TblFoodOrderId = tblFoodOrderId,
                        TblMenuId = orderMenu.MenuId,
                        Price = orderMenu.Price,
                        UserCreated = 0,
                        RecordTimeStampCreated = DateTime.Now,
                        RecordTimeStamp = DateTime.Now
                });
                }

                _placeOrderDbAccess.FoodOrderMapping(tblFoodOrderMappings);
                return tblFoodOrderId;
            }
            catch(Exception ex)
            {
                throw ex;
            }
                     
        }

        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns></returns>
        public int CancelOrder(int orderId)
        {
            return (orderId > 0 ? _placeOrderDbAccess.CancelOrder(orderId) : 0);
        }

        public string CartPriceChange(int restaurantID, int? MenuID , decimal? price)
        {
            try
            {
                string priceChange = _placeOrderDbAccess.CartPriceChange(restaurantID, MenuID, price);
                if (priceChange != null)
                {
                    return priceChange;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return "failed";
        }

        public string CartUpdateOnItemAvailable(int restaurantID, int? MenuID)
        {
            try
            {
                string itemStatus = _placeOrderDbAccess.CartUpdateOnItemAvailable(restaurantID, MenuID);
                if (itemStatus != null)
                {
                    return itemStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "failed";
        }
        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<CustomerOrderReport> GetReports(int customerId)
        {
            var foodOrders = _placeOrderDbAccess.GetReports(customerId);
            if (foodOrders.Any())
            {
                return foodOrders.Select(x => new CustomerOrderReport
                {
                    OrderedDate = x.RecordTimeStampCreated,
                    OrderStatus = x.TblOrderStatus.Status,
                    OrderId = x.Id,
                    PaymentStatus = x.TblOrderPayment.Any() ? x.TblOrderPayment.FirstOrDefault().TblPaymentStatus.Status : string.Empty,
                    price = x.TotalPrice
                }).AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Add to Cart item
        /// </summary>
        /// <param name="orderEntity">Order details</param>
        /// <returns>Cart Id</returns>
        public int AddtoCartItems(OrderEntity orderEntity)
        {
            //DataLayer.Context.TblCartItems tblCartItems = _mapper.Map<DataLayer.Context.TblCartItems>(orderEntity);

            DataLayer.Context.TblCartItems tblCartItems = new DataLayer.Context.TblCartItems
            {
                TblCustomerId = orderEntity.CustomerId,
                TblRestaurantId = orderEntity.RestaurantId,
                DeliveryAddress = orderEntity.DeliveryAddress
            };
           try
            {
                foreach (OrderMenus orderMenu in orderEntity.OrderMenuDetails)
                {

                    tblCartItems.TblMenuId = orderMenu.MenuId;
                    tblCartItems.TotalPrice = orderMenu.Price;
                    tblCartItems.Quantity = orderMenu.Quantity;
                    tblCartItems.UserCreated = 0;
                    tblCartItems.UserModified = 0;
                    tblCartItems.RecordTimeStamp = DateTime.Now;
                    tblCartItems.RecordTimeStampCreated = DateTime.Now;
                    tblCartItems.IsItemAvailable = true;
                }
                return _placeOrderDbAccess.AddtoCart(tblCartItems);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<CartItemsEntity> GetCartItems(int CustomerID)
        {
            IQueryable<DataLayer.Context.TblCartItems> tblCartItems;
            List<CartItemsEntity> cartItems = new List<CartItemsEntity>();
            try
            {
                tblCartItems =  _placeOrderDbAccess.GetCartItems(CustomerID);
                foreach (var items in tblCartItems)
                {
                    CartItemsEntity cartItemsEntity = new CartItemsEntity
                    {
                        TblCustomerId = items.TblCustomerId,
                        TblMenuId = items.TblMenuId,
                        TblRestaurantId = items.TblRestaurantId,
                        TotalPrice = items.TotalPrice,
                        DeliveryAddress = items.DeliveryAddress,
                        IsItemAvailable = items.IsItemAvailable,
                        CartId = items.Id
                    };
                    cartItems.Add(cartItemsEntity);
                }
                return cartItems.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> IsValidRestaurantAsync(OrderEntity orderEntity, int UserId, string UserToken)
        {
            _connectionStrings.Value.RestaurantApiUrl = "http://localhost:10601/";
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ResturantDetail?RestaurantID=" + orderEntity.RestaurantId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
                    if(restaurantInformation != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsOrderItemInStock(OrderEntity orderEntity)
        {
            int? MenuID = null;
           
            //using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            foreach (var items in orderEntity.OrderMenuDetails)
            {
                MenuID = items.MenuId;
            }

            return _placeOrderDbAccess.IsOrderItemInStock(orderEntity.RestaurantId, MenuID);
            
        }

        public async Task<bool> IsValidCustomer(int UserId, string UserToken , int CustomerID)
        {
            _connectionStrings.Value.RestaurantApiUrl = "http://localhost:12697/";
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/Security/ValidateCustomerID?CustomerID=" + CustomerID);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    int Customer = JsonConvert.DeserializeObject<int>(json);
                    if (Customer != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
