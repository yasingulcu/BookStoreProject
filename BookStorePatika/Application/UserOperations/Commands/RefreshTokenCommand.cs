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
    public class RefreshTokenCommand
    {

        public string RefreshToken { get; set; }

        private readonly IBookStoreDbContext _context;
        readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            User user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

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
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı");
            }
        }
    }
}
