using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStorePatika.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            List<Author> authorList = _context.Authors.ToList();

            Author author = authorList.FirstOrDefault(x => x.FullName == Model.FullName);

            if (author != null)
            {
                throw new InvalidOperationException("Yazar Zaten Mevcut");
            }

            author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string FullName => $"{Name} {Surname}";
        }
    }
}
