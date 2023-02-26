using AutoMapper;
using BookStorePatika.Application.BookOperations.GetBooks;
using BookStorePatika.Application.Commands.BookOperations.CreateBook;
using BookStorePatika.Application.Commands.BookOperations.DeleteBook;
using BookStorePatika.Application.Commands.BookOperations.UpdateBook;
using BookStorePatika.Application.Queries.BookOperations.GetBookDetail;
using BookStorePatika.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static BookStorePatika.Application.Commands.BookOperations.CreateBook.CreateBookCommand;
using static BookStorePatika.Application.Commands.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStorePatika.Controllers
{
    [Authorize]
    [Route("api/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
            List<BooksViewModel> result = getBooksQuery.Handle();

            return Ok(result);
        }


        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;


            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);

            getBookDetailQuery.BookId = id;
            GetBookDetailQueryValidator bookDetailValidator = new GetBookDetailQueryValidator();

            bookDetailValidator.ValidateAndThrow(getBookDetailQuery);

            result = getBookDetailQuery.Handle();

            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddBook(CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);

            createBookCommand.Model = newBook;
            CreateBookCommandValidator createValidator = new CreateBookCommandValidator();

            createValidator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, UpdateBookViewModel updatedBook)
        {

            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            updateBookCommand.BookId = id;
            updateBookCommand.Model = updatedBook;
            UpdateBookCommandValidator updateValidator = new UpdateBookCommandValidator();

            updateValidator.ValidateAndThrow(updateBookCommand);

            updateBookCommand.Handle();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validationRules = new DeleteBookCommandValidator();
            validationRules.ValidateAndThrow(deleteBookCommand);


            deleteBookCommand.Handle();

            return Ok();
        }
    }
}
