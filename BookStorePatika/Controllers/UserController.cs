using AutoMapper;
using BookStorePatika.Application.Commands.UserOperations.CreateUser;
using BookStorePatika.Application.UserOperations.Commands;
using BookStorePatika.DBOperations;
using BookStorePatika.TokenOperations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static BookStorePatika.Application.Commands.UserOperations.CreateUser.CreateUserCommand;
using static BookStorePatika.Application.UserOperations.Commands.CreateTokenCommand;

namespace BookStorePatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create(CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken(CreateTokenModel loginModel)
        {
            CreateTokenCommand createTokenCommand = new CreateTokenCommand(_context, _mapper, _configuration);
            createTokenCommand.Model = loginModel;

            var token = createTokenCommand.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand(_context, _configuration);
            refreshTokenCommand.RefreshToken = token;

            var resultToken = refreshTokenCommand.Handle();
            return resultToken;
        }
    }

}
