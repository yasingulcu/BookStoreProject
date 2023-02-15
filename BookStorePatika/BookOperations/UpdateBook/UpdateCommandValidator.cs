using BookStorePatika.BookOperations.CreateBook;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.BookOperations.UpdateBook
{
    public class UpdateCommandValidator : AbstractValidator<CreateBookCommand> 
    {
    }
}
