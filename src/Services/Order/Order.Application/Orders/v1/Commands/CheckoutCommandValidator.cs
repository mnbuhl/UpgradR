using FluentValidation;

namespace Order.Application.Orders.v1.Commands
{
    public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
    {
        public CheckoutCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Username is required");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("Email Address is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("Total price is required.")
                .GreaterThan(0).WithMessage("Total price should be greater than zero.");
        }
    }
}