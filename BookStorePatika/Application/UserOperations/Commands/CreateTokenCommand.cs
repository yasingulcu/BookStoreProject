using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using BookStorePatika.TokenOperations;
using BookStorePatika.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BookStorePatika.Application.UserOperations.Commands
{
    public class CreateTokenCommand
    {

        public CreateTokenModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            User user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user != null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);

                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }

            else
            {
                throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı");
            }
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
