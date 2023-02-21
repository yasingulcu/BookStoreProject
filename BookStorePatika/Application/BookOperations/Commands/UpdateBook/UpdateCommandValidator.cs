using FluentValidation;

namespace BookStorePatika.Application.Commands.BookOperations.UpdateBook
{
    public class UpdateCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
