using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.OrderAPI.ModelValidators
{
    /// <summary>
    /// Update payment entity validator
    /// </summary>
    public class UpdatePaymentEntityValidator : AbstractValidator<UpdatePaymentEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UpdatePaymentEntityValidator()
        {
            RuleFor(m => m.PaymentId)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.TransactionReferenceNo)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.PaymentStatusId)
                .NotEmpty()
                .NotNull();
        }
    }
}
