using BookStorePatika.BookOperations.CreateBook;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.BookOperations.DeleteBook
{
    public class DeleteCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
