using FluentValidation;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(v => v.CustomerId).GreaterThan(0);
            RuleFor(v => v.BookIds).NotEmpty();
        }
    }
}
