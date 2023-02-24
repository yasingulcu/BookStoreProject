using AutoMapper;
using BookStorePatika.Application.AuthorOperations.Commands.CreateAuthor;
using BookStorePatika.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStorePatika.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStorePatika.Application.AuthorOperations.Queries.GetAuthors;
using BookStorePatika.Application.AuthorOperations.Queries.GetAuthorsDetail;
using BookStorePatika.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStorePatika.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStorePatika.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStorePatika.Application.AuthorOperations.Queries.GetAuthorsDetail.GetAuthorDetailQuery;

namespace BookStorePatika.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery getAuthorsQuery = new GetAuthorsQuery(_context, _mapper);
            var result = getAuthorsQuery.Handle();

            return Ok(result);
        }


        [HttpGet("id")]
        public IActionResult GetAuthorDetail(int id)
        {
            AuthorDetailViewModel result;


            GetAuthorDetailQuery getauthorDetailQuery = new GetAuthorDetailQuery(_context, _mapper);

            getauthorDetailQuery.AuthorId = id;
            GetAuthorDetailQueryValidator genreDetailValidator = new GetAuthorDetailQueryValidator();

            genreDetailValidator.ValidateAndThrow(getauthorDetailQuery);

            result = getauthorDetailQuery.Handle();

            return Ok(result);
        }


        [HttpPost]
        public IActionResult AddAuthor(CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);

            createAuthorCommand.Model = newAuthor;

            CreateAuthorCommandValidator authorValidator = new CreateAuthorCommandValidator();

            authorValidator.ValidateAndThrow(createAuthorCommand);
            createAuthorCommand.Handle();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, UpdateAuthorViewModel updatedAuthor)
        {

            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(_context);
            updateAuthorCommand.AuthorId = id;
            updateAuthorCommand.Model = updatedAuthor;
            UpdateAuthorCommandValidator updateValidator = new UpdateAuthorCommandValidator();

            updateValidator.ValidateAndThrow(updateAuthorCommand);

            updateAuthorCommand.Handle();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {

            DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(_context);
            deleteAuthorCommand.AuthorId = id;
            DeleteAuthorCommandValidator deleteValidator = new DeleteAuthorCommandValidator();
            deleteValidator.ValidateAndThrow(deleteAuthorCommand);


            deleteAuthorCommand.Handle();


            return Ok();
        }
    }
}
