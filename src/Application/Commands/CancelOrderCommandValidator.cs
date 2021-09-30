using FluentValidation;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(v => v.OrderId).GreaterThan(0);
        }
    }
}
