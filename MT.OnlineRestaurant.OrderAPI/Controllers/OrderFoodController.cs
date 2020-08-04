#region References
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.ModelValidators;
using System;
using System.Linq;
using System.Threading.Tasks;
using LoggingManagement;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using Microsoft.Azure.ServiceBus.Core;
using System.IO;
#endregion

#region Namespace
namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    /// <summary>
    /// Food ordering controller
    /// </summary>
    [Produces("application/json")]
    public class OrderFoodController : Controller
    {
        private readonly IPlaceOrderActions _placeOrderActions;
        private static IPlaceOrderActions placeOrderBAL;
        private readonly ILogService _logService;
        private static ISubscriptionClient subscriptionClient;
        private static ITopicClient topicClient;
        /// <summary>
        /// Inject buisiness layer dependency
        /// </summary>
        /// <param name="placeOrderActions">Instance of this interface is injected in startup</param>
        public OrderFoodController(IPlaceOrderActions placeOrderActions, ILogService logService)
        {
            _placeOrderActions = placeOrderActions;
            placeOrderBAL = placeOrderActions;
            _logService = logService;
        }       
        /// <summary>
        /// POST api/OrderFood
        /// To order food
        /// </summary>
        /// <param name="orderEntity">Order entity</param>
        /// <returns>Status of order</returns>
        [HttpPost]
        [Route("api/OrderFood")]
        public async Task<IActionResult> Post([FromBody]OrderEntity orderEntity)
        {
            _logService.LogMessage("Order Entity received at endpoint : api/OrderFood, User ID : "+orderEntity.CustomerId);
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            string UserToken = (Request.Headers.ContainsKey("AuthToken") ? Convert.ToString(HttpContext.Request.Headers["AuthToken"]) : "");

            OrderEntityValidator orderEntityValidator = new OrderEntityValidator(UserId, UserToken, _placeOrderActions);
            ValidationResult validationResult = orderEntityValidator.Validate(orderEntity);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = await Task<int>.Run(() => _placeOrderActions.PlaceOrder(orderEntity));
                if (result == 0)
                {
                    return BadRequest("Failed to place order, Please try again later");
                }
            }
            await CallServiceBusForPlaceOrder(orderEntity, UserId,UserToken);
            return Ok("Order placed successfully");

            
        }

        public static async Task CallServiceBusForPlaceOrder(OrderEntity orderEntity, int UserID , string UserToken)
        {
            string ServiceBusConnectionString = "Endpoint=sb://placeorder.servicebus.windows.net/;SharedAccessKeyName=placefoodorder;SharedAccessKey=MQhPvPnWDJk8QXm4S9CjI532WDox1gSjYsEezV/2q8U=";
            string TopicName = "placeorder-topic";

            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            await PlaceOrderMessage(orderEntity,UserID,UserToken);

            await topicClient.CloseAsync();
        }

        public static async Task PlaceOrderMessage(OrderEntity orderEntity, int UserID , string UserToken)
        {
            int? menuId = null;
            int? Quantity = null;
            foreach (var items in orderEntity.OrderMenuDetails)
            {
                menuId = items.MenuId;
                Quantity = items.Quantity;
            }
            var messageBody = new ServiceBusMessage
            {
                Message = "OrderPlaced",
                RestaurantId = orderEntity.RestaurantId,
                MenuId = menuId,
                ItemQuantity = Quantity,
                UserID = UserID,
                UserToken = UserToken
            };
            var serializeMessage = JsonConvert.SerializeObject(messageBody);

            var busMessage = new Message(Encoding.UTF8.GetBytes(serializeMessage));

            await topicClient.SendAsync(busMessage);
        }

        [HttpGet]
        [Route("api/CartItemPriceChange")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult CartItemPriceChange(int RestaurantID, int? MenuID, decimal? Price)
        {
                string query_result = placeOrderBAL.CartPriceChange(RestaurantID, MenuID, Price);
                if (query_result == "success")
                {
                    return Ok(RestaurantID);
                }
                else if (query_result == "failed")
                {
                    return Ok(0);
                }
            
             return this.StatusCode((int)HttpStatusCode.InternalServerError, "error");
            
        }

       
        [HttpGet]
        [Route("api/UpdateCartOnItemStock")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult UpdateCartOnItemStock([FromQuery]int RestaurantID, int MenuID)
        {
            string query_result = placeOrderBAL.CartUpdateOnItemAvailable(RestaurantID, MenuID);
            if (query_result == "success")
            {
                return Ok(RestaurantID);
            }
            else if (query_result == "failed")
            {
                return Ok(0);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, "error");
        }
   
        /// <summary>
        /// DELETE api/CancelOrder
        /// Cancel the order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status of order</returns>
        [HttpDelete]
        [Route("api/CancelOrder")]
        public IActionResult Delete(int id)
        {
            var result = _placeOrderActions.CancelOrder(id);
            if (result > 0)
            {
                return Ok("Order cancelled successfully");
            }

            return BadRequest("Failed to cancel order, Please try again later");
        }
        //[HttpGet]
        //[Route("api/Reports")]
        //public IActionResult Reports(int customerId)
        //{
        //    IQueryable<CustomerOrderReport> result = _placeOrderActions.GetReports(customerId);
        //    if(result.Any())
        //    {
        //        return Ok(result.ToList());
        //    }

        //    return BadRequest("Failed to get the reports");
        //}     

        /// <summary>
        /// POST api/AddToCart
        /// To order food
        /// </summary>
        /// <param name="orderEntity">Order entity</param>
        /// <returns>Cart Status</returns>
        [HttpPost]
        [Route("api/AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody]OrderEntity orderEntity)
        {
            _logService.LogMessage("Add to Cart Entity received at endpoint : api/AddToCart, User ID : " + orderEntity.CustomerId);
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            string UserToken = (Request.Headers.ContainsKey("AuthToken") ? Convert.ToString(HttpContext.Request.Headers["AuthToken"]) : "");

            OrderEntityValidator orderEntityValidator = new OrderEntityValidator(UserId, UserToken, _placeOrderActions);
            ValidationResult validationResult = orderEntityValidator.Validate(orderEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = await Task<int>.Run(() => _placeOrderActions.AddtoCartItems(orderEntity));
                if (result == 0)
                {
                    return BadRequest("Failed to add the items in Cart as Items already exist");
                }
            }
            return Ok("Item added to cart successfully");
        }

        [HttpGet]
        [Route("api/GetCartItems")]
        public IActionResult GetCartItems(int CustomerID)
        {
            IQueryable<CartItemsEntity> cartItems;
            try
            {
                cartItems = _placeOrderActions.GetCartItems(CustomerID);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
#endregion