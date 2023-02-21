using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            List<Author> authorList = _context.Authors.OrderBy(x => x.Id).ToList();

            List<AuthorsViewModel> viewModels = _mapper.Map<List<AuthorsViewModel>>(authorList);

            return viewModels;
        }
    }


    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
