using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.OrderAPI.ModelValidators
{
    /// <summary>
    /// Food menu validator
    /// </summary>
    public class OrderMenuValidator : AbstractValidator<OrderMenus>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderMenuValidator()
        {
            // All your other validation rules for order. eg.
            RuleFor(x => x.MenuId)
                .NotEmpty()
                .NotNull();
                //.Must(BeAValidMenu).When(p => p.MenuId != 0).WithMessage("Invalid Menu");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull();
            //    .Must(BeAValidMenuPrice).When(p => p.Price != 0).WithMessage("Invalid Price");
        }

        /// <summary>
        /// Make a service call to fetch all menus and validate
        /// </summary>
        /// <param name="MenuId">Menu Id</param>
        /// <returns>Boolean whether specified menu is valid or invalid</returns>
        private bool BeAValidMenu(int MenuId)
        {
            bool IsValidMenu = false;

            return IsValidMenu;
        }

        /// <summary>
        /// Make a service call to fetch all menus prices and validate
        /// </summary>
        /// <param name="Price">Price</param>
        /// <returns>Boolean whether specified price for the menu is valid or invalid</returns>
        private bool BeAValidMenuPrice(decimal Price)
        {
            bool IsValidMenuPrice = false;

            return IsValidMenuPrice;
        }
    }
}
