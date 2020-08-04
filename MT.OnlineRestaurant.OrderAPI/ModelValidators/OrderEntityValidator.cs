using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.Utilities;

namespace MT.OnlineRestaurant.OrderAPI.ModelValidators
{
    /// <summary>
    /// Food order model validator
    /// </summary>
    public class OrderEntityValidator : AbstractValidator<OrderEntity>
    {
        private readonly IPlaceOrderActions _placeOrderActions;
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderEntityValidator(int UserId, string UserToken, IPlaceOrderActions placeOrderActions)
        {
            _placeOrderActions = placeOrderActions;

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .Must(r => BeAValidRestaurant(r, UserId, UserToken)).When(p => p.RestaurantId != 0).WithMessage("Invalid Restaurant");

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .Must(r => IsOrderItemInStock(r)).When(p => p.RestaurantId != 0).WithMessage("Item cannot be added due to OUT OF STOCK");

            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .NotNull()
                .Must(r => BeAValidCustomer(r, UserId, UserToken)).When(p => p.CustomerId != 0).WithMessage("Invalid CustomerID");

            RuleFor(m => m.DeliveryAddress)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100).WithMessage("DeliveryAddress must be less than 100 characters");

            RuleFor(m => m.OrderMenuDetails)
                .NotNull()
                .Must(m => m.Count > 0)
                .WithMessage("Menu items cannot be empty");

            RuleForEach(m => m.OrderMenuDetails).SetValidator(new OrderMenuValidator());
        }

        /// <summary>
        /// Make a service call to fetch all restaurants and validate between them
        /// </summary>
        /// <param name="orderEntity">OrderEntity</param>
        /// <param name="UserId">UserId</param>
        /// <param name="UserToken">UserToken</param>
        /// <returns>Boolean whether specified restaurant is valid or invalid</returns>
        public bool BeAValidRestaurant(OrderEntity orderEntity, int UserId, string UserToken)
        {
            bool IsValidRestaurant = _placeOrderActions.IsValidRestaurantAsync(orderEntity, UserId, UserToken).GetAwaiter().GetResult();
            return IsValidRestaurant;
        }
        /// <summary>
        /// Make a service call to check for Item availability
        /// </summary>
        /// <param name="orderEntity">OrderEntity</param>
        /// <param name="UserId">UserId</param>
        /// <param name="UserToken">UserToken</param>
        /// <returns>Boolean whether specified Order is valid or invalid</returns>
        //public bool BeAValidItemOrder(OrderEntity orderEntity, int UserId, string UserToken)
        //{
        //    bool IsValidRestaurant = _placeOrderActions.IsOrderItemInStock(orderEntity, UserId, UserToken).GetAwaiter().GetResult();
        //    return IsValidRestaurant;
        //}
        /// <summary>
        /// Make a service call to fetch all valid customers and validate
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <param name="UserId"></param>
        /// <param name="UserToken"></param>
        /// <returns>Boolean whether specified customer is valid or invalid</returns>
        private bool BeAValidCustomer(int CustomerId, int UserId, string UserToken)
        {
            bool IsValidCustomer = _placeOrderActions.IsValidCustomer(UserId,UserToken,CustomerId).GetAwaiter().GetResult(); 

            return IsValidCustomer;
        }

        /// <summary>
        /// Make a service call to check ItemOutOfStock
        /// </summary>
        /// <param name="orderEntity">OrderEntity</param>
        /// <returns>Boolean whether specified customer is valid or invalid</returns>
        private bool IsOrderItemInStock(OrderEntity orderEntity)
        {
            bool IsValidCustomer = _placeOrderActions.IsOrderItemInStock(orderEntity);

            return IsValidCustomer;
        }
    }
}
