using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.OrderAPI.ModelValidators
{
    /// <summary>
    /// Payment entity validator
    /// </summary>
    public class PaymentEntityValidator : AbstractValidator<PaymentEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentEntityValidator()
        {
            RuleFor(m => m.OrderId)
                .NotEmpty()
                .NotNull();
            //    .Must(BeAValidOrder).When(p => p.OrderId != 0).WithMessage("Invalid order");

            RuleFor(m => m.PaymentTypeId)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.Remarks)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .NotNull();
        }

        /// <summary>
        /// Make a service call to fetch all orders and validate between them
        /// </summary>
        /// <param name="OrderId">Order Id</param>
        /// <returns>Boolean whether specified order is valid or invalid</returns>
        private bool BeAValidOrder(int OrderId)
        {
            bool IsValidOrder = false;

            return IsValidOrder;
        }
    }
}
