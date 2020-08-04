#region References
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
#endregion

namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    [Produces("application/json")]
    public class TableBookingController : Controller
    {
        #region private Variables
        private readonly IBookYourTableBusiness _bookYourTableBusiness;
        #endregion

        #region Constructor

        public TableBookingController(IBookYourTableBusiness bookYourTableBusiness)
        {
            _bookYourTableBusiness = bookYourTableBusiness;
        }
        #endregion        

        // POST api/values
        [HttpPost]
        [Route("api/BookYourTable")]
        public async Task<IActionResult> Post([FromBody]BookTable bookTable)
        {
            if (Request.Headers.ContainsKey("CustomerId"))
            {
                var userId = HttpContext.Request.Headers["CustomerId"].ToString();
                bookTable.CustomerId = int.Parse(userId);
            }

            if (await Task<bool>.Run(() => _bookYourTableBusiness.CheckTableAvailability(bookTable)))
            {
                if (await Task<bool>.Run(() => _bookYourTableBusiness.BookYourTable(bookTable)))
                {
                    return  this.Ok("Table is booked");
                }
                else
                {
                    return this.BadRequest("Failed to book the table, please try again after sometime");
                }
            }

            return this.Ok(false);
        }

        [HttpPut]
        [Route("api/BookYourTable")]
        public async Task<IActionResult> Put([FromBody]BookTable bookTable)
        {
            if (Request.Headers.ContainsKey(""))
            {
                var userId = HttpContext.Request.Headers["CustomerId"].ToString();
                bookTable.CustomerId = int.Parse(userId);
            }

            if (await Task<bool>.Run(() => _bookYourTableBusiness.CheckTableAvailability(bookTable)))
            {
                if (await Task<bool>.Run(() => _bookYourTableBusiness.UpdateBooking(bookTable)))
                {
                    return this.Ok("Your booking is updated");
                }

                return this.BadRequest("Failed to update the booking, please try again later");
            }

            

            return this.Ok(false);
        }
    }
}
