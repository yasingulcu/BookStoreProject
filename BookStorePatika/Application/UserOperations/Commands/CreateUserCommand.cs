using System;
using System.Linq;
using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;


namespace BookStorePatika.Application.Commands.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            User user = _context.Users.FirstOrDefault(x => x.Email == Model.Email);

            if (user != null)
            {
                throw new InvalidOperationException("Kullanıcı zaten mevcut.");
            }

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
