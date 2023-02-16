using AutoMapper;
using BookStorePatika.BookOperations.CreateBook;
using BookStorePatika.BookOperations.DeleteBook;
using BookStorePatika.BookOperations.GetBookDetail;
using BookStorePatika.BookOperations.GetBooks;
using BookStorePatika.BookOperations.UpdateBook;
using BookStorePatika.Entity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStorePatika.BookOperations.CreateBook.CreateBookCommand;
using static BookStorePatika.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStorePatika.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
            var result = getBooksQuery.Handle();

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

            //return BadRequest(ex.Message);

        }

        [HttpGet]
        public Book GetQueryStringById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            if (book == null)
            {
                return new Book();
            }
            return book;
        }

        [HttpPost]
        public IActionResult AddBook(CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);

            createBookCommand.Model = newBook;
            CreateCommandValidator validationRules = new CreateCommandValidator();

            validationRules.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();

            //return BadRequest(ex.Message);



            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, UpdateBookViewModel updatedBook)
        {

            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            updateBookCommand.BookId = id;
            updateBookCommand.Model = updatedBook;
            UpdateCommandValidator updateValidator = new UpdateCommandValidator();

            updateValidator.ValidateAndThrow(updateBookCommand);

            updateBookCommand.Handle();


            //return BadRequest(ex.Message);


            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.BookId = id;
            DeleteCommandValidator validationRules = new DeleteCommandValidator();
            validationRules.ValidateAndThrow(deleteBookCommand);


            deleteBookCommand.Handle();


            //return BadRequest(ex.Message);


            return Ok();
        }



    }
}
