using AutoMapper;
using BookStorePatika.Application.GenreOperations.Commands.CreateGenre;
using BookStorePatika.Application.GenreOperations.Commands.DeleteGenre;
using BookStorePatika.Application.GenreOperations.Commands.UpdateGenre;
using BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail;
using BookStorePatika.Application.GenreOperations.Queries.GetGenres;
using BookStorePatika.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStorePatika.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStorePatika.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace BookStorePatika.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery getGenresQuery = new GetGenresQuery(_context, _mapper);
            var result = getGenresQuery.Handle();

            return Ok(result);
        }


        [HttpGet("id")]
        public IActionResult GetGenreDetail(int id)
        {
            GenreDetailViewModel result;


            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);

            getGenreDetailQuery.GenreId = id;
            GetGenreDetailQueryValidator genreDetailValidator = new GetGenreDetailQueryValidator();

            genreDetailValidator.ValidateAndThrow(getGenreDetailQuery);

            result = getGenreDetailQuery.Handle();

            return Ok(result);
        }


        [HttpPost]
        public IActionResult AddGenre(CreateGenreModel newGenre)
        {
            CreateGenreCommand createGenreCommand = new CreateGenreCommand(_context, _mapper);

            createGenreCommand.Model = newGenre;

            CreateGenreCommandValidator genreValidator = new CreateGenreCommandValidator();

            genreValidator.ValidateAndThrow(createGenreCommand);
            createGenreCommand.Handle();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, UpdateGenreViewModel updatedGenre)
        {

            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(_context);
            updateGenreCommand.GenreId = id;
            updateGenreCommand.Model = updatedGenre;
            UpdateGenreCommandValidator updateValidator = new UpdateGenreCommandValidator();

            updateValidator.ValidateAndThrow(updateGenreCommand);

            updateGenreCommand.Handle();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
            deleteGenreCommand.GenreId = id;
            DeleteGenreCommandValidator deleteValidator = new DeleteGenreCommandValidator();
            deleteValidator.ValidateAndThrow(deleteGenreCommand);


            deleteGenreCommand.Handle();


            return Ok();
        }
    }
}
