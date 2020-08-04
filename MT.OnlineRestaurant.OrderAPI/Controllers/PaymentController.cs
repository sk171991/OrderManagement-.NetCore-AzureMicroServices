#region References
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.ModelValidators;
#endregion

namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    /// <summary>
    /// Payment controller
    /// </summary>
    [Produces("application/json")]
    public class PaymentController : Controller
    {
        private readonly IPaymentActions _paymentActions;
        /// <summary>
        /// Inject buisiness layer dependency
        /// </summary>
        /// <param name="paymentActions"></param>
        public PaymentController(IPaymentActions paymentActions)
        {
            _paymentActions = paymentActions;
        }

        /// <summary>
        /// Make payments for orders
        /// </summary>
        /// <param name="paymentEntity">Payment details</param>
        /// <returns>Payment status</returns>
        [HttpPost]
        [Route("api/MakePayment")]
        public IActionResult MakePayment(PaymentEntity paymentEntity)
        {
            PaymentEntityValidator paymentEntityValidator = new PaymentEntityValidator();
            ValidationResult validationResult = paymentEntityValidator.Validate(paymentEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = _paymentActions.MakePaymentForOrder(paymentEntity);
                if (result == 0)
                {
                    return BadRequest("Payment failed, Please try again later");
                }
            }
            return Ok("Payment is successful");
        }

        /// <summary>
        /// Update payment status after retrieving status from payment gateway plugin
        /// </summary>
        /// <param name="paymentEntity">Payment details</param>
        /// <returns>Updated payment status and order status</returns>
        [HttpPut]
        [Route("api/UpdatePaymentAndOrderStatus")]
        public IActionResult UpdatePaymentAndOrderStatus(UpdatePaymentEntity paymentEntity)
        {
            UpdatePaymentEntityValidator paymentEntityValidator = new UpdatePaymentEntityValidator();
            ValidationResult validationResult = paymentEntityValidator.Validate(paymentEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = _paymentActions.UpdatePaymentAndOrderStatus(paymentEntity);
                if (result == 0)
                {
                    return BadRequest("Failed to update payment status, Please try again later");
                }
            }
            return Ok("Payment status updated successfully");
        }
    }
}